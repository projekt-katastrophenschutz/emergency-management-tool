// <copyright file="StartupViewModel.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Forms;
    using BSA.Core;
    using BSA.Core.Network;
    using BSA.Core.Utils;
    using BSA.Database.Utils;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using log4net;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.Views;
    using MessageBox = System.Windows.MessageBox;
    using Settings = ProjektKatastrophenschutz.Properties.Settings;
    using BSA.Core.Messages;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight.Messaging;
    public class StartupViewModel : ViewModelBase
    {
        private readonly ILog log;
        private string ip;
        private bool loading;
        private RelayCommand startAsClientCommand;
        private RelayCommand startAsServerCommand;
        private RelayCommand startLocalCommand;
        private string userName;
        private readonly IJobService jobService;
        private readonly IForcesService forceService;
                              

        public StartupViewModel(IShellService shellService, ILog log, IJobService jobService, IForcesService forceService)
        {
            this.jobService = jobService;
            this.forceService = forceService;
            this.log = log;
            this.UserName = Settings.Default.LastUserName;
            this.IpAddress = Settings.Default.LastServerAddress;
            if (string.IsNullOrEmpty(this.IpAddress))
            {
                var firstAddress = this.Addresses.FirstOrDefault();
                this.IpAddress = firstAddress?.ToString() ?? string.Empty;
            }

            shellService.ShellActivated += (s, e) =>
            {
                if (Environment.GetCommandLineArgs().Contains("serverRole"))
                {
                    this.Loading = true;
                    this.log.Info("Start as server role");
                    this.StartAsServerCommand.Execute(null);
                }
            };
        }

        public StartupView View { get; set; }

        public IEnumerable<IPAddress> Addresses => AddressResolving.GetIp4Addresses();

        public string UserName
        {
            get { return this.userName; }
            set { this.Set(ref this.userName, value); }
        }

        public string IpAddress
        {
            get { return this.ip; }
            set { this.Set(ref this.ip, value); }
        }

        public bool Loading
        {
            get { return this.loading; }
            set { this.Set(ref this.loading, value); }
        }

        public RelayCommand StartAsServerCommand
        {
            get
            {
                return this.startAsServerCommand
                       ?? (this.startAsServerCommand = new RelayCommand(
                           () =>
                           {
                               this.Loading = true;
                               Settings.Default.LastUserName = this.UserName;
                               Settings.Default.LastServerAddress = this.IpAddress;
                               Settings.Default.Save();

                               if (UacHelper.IsElevated() == false)
                               {
                                   if (UacHelper.RestartWithAdminRights("serverRole") == false)
                                   {

                                       this.Loading = false;
                                       return;
                                   }

                                   Process.GetCurrentProcess().Kill();
                               }

                               var url = "";
                               try
                               {
                                   url = Server.Start(this.IpAddress);

                                   BsaContext.Initialize(this.IpAddress, this.UserName, ApplicationRole.Server);

                                   var result = MessageBox.Show("Aktuelle Datenbank löschen?", "DB Löschen",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

                                   if (result == MessageBoxResult.Yes)
                                   {
                                       this.jobService.ClearData();
                                       this.forceService.ClearData();
                 //                      DatabaseHelper.DeleteDatabaseFile();
                                       this.View.Visibility = Visibility.Collapsed;
                                   }
                                   else if (result == MessageBoxResult.No)
                                   {
                                       this.View.Visibility = Visibility.Collapsed;
                                   }
                               }
                               catch (Exception ex)
                               {
                                   this.log.Error("Der Server konnte nicht gestartet werden.", ex);
                                   MessageBox.Show(
                                       "Der Server konnte nicht gestartet werden. Stellen Sie sicher, dass die Anwendung nicht bereits gestartet wurde.",
                                       "Projekt Katastrophenschutz", MessageBoxButton.OK, MessageBoxImage.Error);
                                   this.Loading = false;
                               }
                           }));
            }
        }

        public RelayCommand StartAsClientCommand
        {
            get
            {
                return this.startAsClientCommand
                       ?? (this.startAsClientCommand = new RelayCommand(
                           async () =>
                           {
                               this.Loading = true;
                               if (await CommunicationProxy.ConnectToServer(this.IpAddress, 8081) == false)
                               {
                                   MessageBox.Show(
                                       $"Die Anwendung konnte nicht mit dem angegebenen Server ({this.IpAddress}) verbunden werden! Bitte überprüfen Sie ihre Angaben!",
                                       "Verbindung fehlgeschlagen");
                                   this.Loading = false;
                                   return;
                               }

                               var dialogResult =
                                   System.Windows.Forms.MessageBox.Show(
                                       "Die bestehende Datenbank wird gelöscht und synchronisiert, sind Sie damit einverstanden?",
                                       "Sicher?", MessageBoxButtons.YesNo);
                               if (dialogResult == DialogResult.Yes)
                               {
              //                     DatabaseHelper.DeleteDatabaseFile();
                                   this.jobService.ClearData();
                                   this.forceService.ClearData();
                                   await CommunicationProxy.SynchronizeAsync();
                               }
                               else if (dialogResult == DialogResult.No)
                               {
                                   var result = MessageBox.Show("Aktuelle Datenbank löschen?", "DB Löschen",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);
                                   if (result == MessageBoxResult.Yes)
                                   {
                                       this.jobService.ClearData();
                                       this.forceService.ClearData();
                       //                DatabaseHelper.DeleteDatabaseFile();
                                   }
                               }

                               BsaContext.Initialize(this.IpAddress, this.UserName, ApplicationRole.Client);

                               this.View.Visibility = Visibility.Collapsed;
                               Settings.Default.LastUserName = this.UserName;
                               Settings.Default.LastServerAddress = this.IpAddress;
                               Settings.Default.Save();
                               this.Loading = false;
                           }));
            }
        }

        public RelayCommand StartLocalCommand
        {
            get
            {
                return this.startLocalCommand
                       ?? (this.startLocalCommand = new RelayCommand(
                           () =>
                           {
                               var result = MessageBox.Show("Aktuelle Datenbank löschen?", "DB Löschen",
                                   MessageBoxButton.YesNo, MessageBoxImage.Question);
                               if (result == MessageBoxResult.Yes)
                               {
                                   this.jobService.ClearData();
                                   this.forceService.ClearData();
                                   //DatabaseHelper.DeleteDatabaseFile();
                               }

                               BsaContext.Initialize($"", this.UserName, ApplicationRole.Standalone);

                               this.View.Visibility = Visibility.Collapsed;
                           }));
            }
        }
    }
}