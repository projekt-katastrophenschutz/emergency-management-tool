// <copyright file="ShellViewModel.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using BSA.Core.Messages;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Views;

    public class ShellViewModel : ViewModelBase
    {
        private string currentTime;
        private string currentDate;
        private int lastDay = -1;
        private ShellTabViewModel selectedTab;
        private RelayCommand<ShellTabViewModel> closeTabCommand;

        public ShellViewModel(IShellService shellService)
        {
            this.ShellService = shellService;
            this.ShellTabs = new ObservableCollection<ShellTabViewModel>();
            this.HistoryItems = new ObservableCollection<HistoryItemMessage>();
            Messenger.Default.Register<HistoryItemMessage>(this, this.OnHistoryItem);
        }

        public IShellService ShellService { get; }

        public ObservableCollection<ShellTabViewModel> ShellTabs { get; }

        public ObservableCollection<HistoryItemMessage> HistoryItems { get; }

        public ShellTabViewModel SelectedTab
        {
            get { return this.selectedTab; }
            set { this.Set(ref this.selectedTab, value); }
        }

        public ShellTabViewModel MainShellTab { get; private set; }

        public string CurrentTime
        {
            get { return this.currentTime; }
            set { this.Set(ref this.currentTime, value); }
        }

        public string CurrentDate
        {
            get { return this.currentDate; }
            set { this.Set(ref this.currentDate, value); }
        }

        public RelayCommand<ShellTabViewModel> CloseTabCommand => this.closeTabCommand ??
            (this.closeTabCommand = new RelayCommand<ShellTabViewModel>(this.CloseTab));

        public void Load(Shell shell)
        {
            // Initialize and start clock timer (200ms interval for compensation of dispatcher delays)
            var clockTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            clockTimer.Tick += (s, e) =>
            {
                this.CurrentTime = DateTime.Now.ToLongTimeString();
                if (DateTime.Now.Day != this.lastDay)
                {
                    this.CurrentDate = DateTime.Now.ToLongDateString();
                    this.lastDay = DateTime.Now.Day;
                }
            };
            clockTimer.Start();
            
            this.ShellService.RegisterShell(shell);

            this.MainShellTab = new ShellTabViewModel()
            {
                Icon = new BitmapImage(new Uri("pack://application:,,/Resources/Images/TabIcons/Home.png")),
                Pinned = true,
                Title = "Emergency Management Tool",
                View = new MainView()
            };

            this.SelectedTab = this.MainShellTab;
            this.ShellTabs.Add(this.MainShellTab);
        }

        private void CloseTab(ShellTabViewModel shellTabViewModel)
        {
            // ToDo: Invoke BeforeClose() for a view model based check if the tab can be closed or not
            this.ShellTabs.Remove(shellTabViewModel);
        }

        private void OnHistoryItem(HistoryItemMessage historyItemMessage)
        {
            DispatcherHelper.RunAsync(() =>
            {
                this.HistoryItems.Insert(0, historyItemMessage);
            });
        }
    }
}