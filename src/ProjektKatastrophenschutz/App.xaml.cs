// <copyright file="App.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz
{
    using System.Windows;
    using AutoMapper;
    using BSA.Core;
    using BSA.Database.Management;
    using GalaSoft.MvvmLight.Threading;
    using log4net;
    using log4net.Config;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Owin;
    using Utils;

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private HandleDisconnect disconnecter = new HandleDisconnect();

        public App()
        {
            // Configure logging
            XmlConfigurator.Configure();
            this.DispatcherUnhandledException += (s, e) =>
            {
                var log = LogManager.GetLogger(typeof(App));
                log.Fatal(e.Exception.Message, e.Exception);
            };

            DispatcherHelper.Initialize();
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // Intialize database
            using (var databaseContext = new DatabaseContext())
            {
                databaseContext.Database.Migrate();
            }

            // Configure auto mapper
            Mapper.Initialize(configuration => configuration.AddProfile<AutoMapperProfile>());

            // Configure SignalR / WebApp
            // ToDo: Check for running processes / other instances of this application
            var context = new OwinContext();
        }
    }
}