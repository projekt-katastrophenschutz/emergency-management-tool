// <copyright file="SettingsViewModel.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Linq;
using ProjektKatastrophenschutz.ColorThemes;
using ProjektKatastrophenschutz.Utils;

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using BSA.Core;
    using BSA.Core.Network;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using ProjektKatastrophenschutz.Elements;
    using Settings = ProjektKatastrophenschutz.Properties.Settings;
    using System.Net;
    public class SettingsViewModel : ViewModelBase, IShellCommandBar
    {
        private string ip;
        private ApplicationRole role;
        private string url;
        private string userName;
        public string IpAddress
        {
            get { return this.ip; }
            set { this.Set(ref this.ip, value); }
        }

       
          
  
        private RelayCommand loadThemeCommand;
        private RelayCommand saveThemeCommand;
       

        // Problem: Can't use a loop to create the dropdown menu buttons for the AquireServerButton, because 
        //      Command = new RelayCommand(() => this.ChangeToServer(addresses[i].ToString()) 
        // can't be executed, due to access to modified closure (the variable i) within lambda expression.
        // Normally you can create a new local variable (var i1 = i) and use this local variable within the lambda expression 
        // -> no access to modified closure anymore.
        // But this doesn't work here, too.
        // You can't specify a index of the addresses list within the "Command" - lambda expression here.
        // It has to be a constant value.
        // 
        // This method is a pseudo-workaround (very, very ugly).
        // Wasn't able to find another solution at this point.
        // It stops working, once AddressResolving.GetIpv4Addresses() returns more than 5 elements.
        // ToDo find another solution to add the ip addresses to the AquireServerButton, with some working loop
        private void AddAquireServerButtonDropdownElements()
        {
            var addresses = AddressResolving.GetIp4Addresses().ToList();
            var dropdownButtons = new List<ShellCommandBarButton>();

            // This does NOT work!
            //foreach (var ipAddress in addresses)
            //{
            //    this.AcquireServerButton.Items.Add(
            //        new ShellCommandBarButton
            //        {
            //            ImageUri =
            //                new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
            //            Label = ipAddress.ToString(),
            //            Command = new RelayCommand(() => this.ChangeToServer(ipAddress.ToString()))
            //        });
            //}

            // The following code works, but it's ugly
            // ToDo Fix that, create a better code

            try
            {
                if (addresses.Count >= 1)
                {
                    dropdownButtons.Add(new ShellCommandBarButton
                    {
                        ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                        Label = addresses[0].ToString(),
                        Command = new RelayCommand(() => this.ChangeToServer(AddressResolving.GetIp4Addresses().ToList()[0].ToString()))
                    });
                }

                if (addresses.Count >= 2)
                {
                    dropdownButtons.Add(new ShellCommandBarButton
                    {
                        ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                        Label = addresses[1].ToString(),
                        Command = new RelayCommand(() => this.ChangeToServer(AddressResolving.GetIp4Addresses().ToList()[1].ToString()))
                    });
                }

                if (addresses.Count >= 3)
                {
                    dropdownButtons.Add(new ShellCommandBarButton
                    {
                        ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                        Label = addresses[2].ToString(),
                        Command = new RelayCommand(() => this.ChangeToServer(AddressResolving.GetIp4Addresses().ToList()[2].ToString()))
                    });
                }

                if (addresses.Count >= 4)
                {
                    dropdownButtons.Add(new ShellCommandBarButton
                    {
                        ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                        Label = addresses[3].ToString(),
                        Command = new RelayCommand(() => this.ChangeToServer(AddressResolving.GetIp4Addresses().ToList()[3].ToString()))
                    });
                }

                if (addresses.Count >= 5)
                {
                    dropdownButtons.Add(new ShellCommandBarButton
                    {
                        ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                        Label = addresses[4].ToString(),
                        Command = new RelayCommand(() => this.ChangeToServer(AddressResolving.GetIp4Addresses().ToList()[4].ToString()))
                    });
                }

                // Add buttons to dropdown menu
                this.AcquireServerButton.Items.AddRange(dropdownButtons);
            }
            catch (Exception)
            {
                // Do nothing.
                // Just wanted to create some exception handling when dealing with about 3 billion indices.
            }          
        }

        public SettingsViewModel()
        {
            this.AcquireServerButton = new ShellCommandBarDropDownButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/filter.scale-150.png"),
                Label = "Server werden",
                Items = new List<ShellCommandBarItem>(),
                DropDownHorizontalOffset = -55.0
            };
            this.AcquireServerButton.Visibility = Visibility.Hidden;

            //  this.AddAquireServerButtonDropdownElements();

            //foreach (var ipAddress in AddressResolving.GetIp4Addresses())
            //{
            //    this.AcquireServerButton.Items.Add(
            //        new ShellCommandBarButton
            //        {
            //            ImageUri =
            //                new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
            //            Label = ipAddress.ToString(),
            //            Command = new RelayCommand(() => this.ChangeToServer(ipAddress.ToString()))
            //        });
            //}

            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                // Create job button
                new ShellCommandBarButton
                {
                    //ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/plus_math.scale-150.png"),
                    //Label = "Ich bin nutzlos",
                    Width = 284,
                    Command = new RelayCommand(() =>
                    {
                    })
                },

                new ShellCommandBarSeparator(),
                this.AcquireServerButton
            };
        }

        public IEnumerable<IPAddress> Addresses => AddressResolving.GetIp4Addresses();

        public RelayCommand LoadThemeCommand => this.loadThemeCommand ??
                                                  (this.loadThemeCommand = new RelayCommand(this.LoadColorThemeFromFile));

        public RelayCommand SaveThemeCommand => this.saveThemeCommand ??
                                                  (this.saveThemeCommand = new RelayCommand(this.SaveCurrentColorThemeToFile));
        public RelayCommand StartAsServerCommand => this.saveThemeCommand ??
                                                  (this.saveThemeCommand = new RelayCommand(() => ChangeToServer(this.IpAddress)));

        private void LoadColorThemeFromFile()
        {
            var path = FileUtils.GetFileFromOpenFileDialog(KnownFolders.GetPath(KnownFolder.Documents));
            if (path == null)
                return;

            var theme = ThemeHelper.LoadFromFile(path);
            if (theme == null)
                return;

            Properties.Settings.Default.SelectedColorTheme = theme;
        }

        private void SaveCurrentColorThemeToFile()
        {
            var path = FileUtils.GetFileFromSaveFileDialog(KnownFolders.GetPath(KnownFolder.Documents));
            if (path == null)
                return;

            var theme = Properties.Settings.Default.SelectedColorTheme;
            ThemeHelper.SaveToFile(theme, path);
        }

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; } = null;

        public ShellCommandBarDropDownButton AcquireServerButton { get; }

        public string UserName
        {
            get { return this.userName; }
            set { this.Set(ref this.userName, value); }
        }

        public string Url
        {
            get { return this.url; }
            set { this.Set(ref this.url, value); }
        }

        public string IP
        {
            get { return this.ip; }
            set { this.Set(ref this.ip, value); }
        }

        public ApplicationRole Role
        {
            get { return this.role; }
            set { this.Set(ref this.role, value); }
        }


        public void LoadSettings()
        {
            this.UserName = BsaContext.GetUserName();
            this.Url = $"http://{BsaContext.GetURL()}:8081";
            this.Role = BsaContext.GetUserRole();
            this.IP = AddressResolving.GetIp4AddressesString();
        }

        
        public void ChangeToServer(string ipAddress)
        {
            if (ipAddress.Length > 15)
            {
                MessageBox.Show("Bitte wählen Sie eine IP Adresse aus");
                return;
            }

            if (Role == ApplicationRole.Client)
            {
                if (CommunicationProxy.PermitServerChange())
                {
                    Settings.Default.LastUserName = this.UserName;
                    Settings.Default.LastServerAddress = this.IpAddress;
                    Settings.Default.Save();
                    switch (CommunicationProxy.AcquireServer(ipAddress))
                    {
                        case 0:
                           
                            break;
                        case 1:
                            MessageBox.Show("Beim Serverwechsel ist ein Fehler aufgetreten!");
                            break;
                        case 2:
                            MessageBox.Show("Es ist ein Fehler aufgetreten, bitte nochmal versuchen!");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Der jetzige Server möchte nicht Client werden");

                }
            }else
            {
                MessageBox.Show("Sie sind bereits Server!");
            }
            
        }
    }
}