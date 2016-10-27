// <copyright file="ViewModelLocator.cs" company="HS Augsburg">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using log4net;
    using Microsoft.Practices.ServiceLocation;
    using ProjektKatastrophenschutz.Services;

    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => LogManager.GetLogger(typeof(App)));

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                SimpleIoc.Default.Register<IJobService, SampleDataJobService>();
                SimpleIoc.Default.Register<IForcesService, SampleDataForcesService>();
            }
            else
            {
                // Create run time view services and models

                SimpleIoc.Default.Register<IJobService>(() => new ConnectionJobService(new DatabaseJobService()));
                SimpleIoc.Default.Register<IForcesService>(() => new ConnectionForcesService(new DatabaseForcesService()));

                //var jobService = new MemoryJobService();
                //SimpleIoc.Default.Register<IJobService>(() => new ConnectionJobService(jobService));
                //SimpleIoc.Default.Register<IForcesService>(() => new ConnectionForcesService(new MemoryForcesService(jobService)));
            }

            SimpleIoc.Default.Register<IShellService, ShellService>();

            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<JobsViewModel>();
            SimpleIoc.Default.Register<JobEditorViewModel>();
            SimpleIoc.Default.Register<ForceEditorViewModel>();
            SimpleIoc.Default.Register<ForcesViewModel>();
            SimpleIoc.Default.Register<StartupViewModel>();
            SimpleIoc.Default.Register<ForceToJobViewModel>();
            SimpleIoc.Default.Register<JobToForceViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public ShellViewModel Shell => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public JobsViewModel Jobs => ServiceLocator.Current.GetInstance<JobsViewModel>();

        public JobEditorViewModel JobEditor => ServiceLocator.Current.GetInstance<JobEditorViewModel>(Guid.NewGuid().ToString());

        public ForceEditorViewModel ForceEditor => ServiceLocator.Current.GetInstance<ForceEditorViewModel>(Guid.NewGuid().ToString());

        public ForcesViewModel Forces => ServiceLocator.Current.GetInstance<ForcesViewModel>();

        public ForceToJobViewModel ForceTJob => ServiceLocator.Current.GetInstance<ForceToJobViewModel>();

        public JobToForceViewModel JobTForce => ServiceLocator.Current.GetInstance<JobToForceViewModel>();

        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public StartupViewModel Startup => ServiceLocator.Current.GetInstance<StartupViewModel>();
        
        public static void Cleanup()
        {
        }
    }
}