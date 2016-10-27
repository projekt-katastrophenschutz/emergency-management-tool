// <copyright file="MainViewModel.cs" company="HS Augsburg">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Views;

    public class MainViewModel : ViewModelBase, IShellCommandBar
    {
        private readonly IShellService shellService;
        private RelayCommand debugCommand;
        private int selectedTabIndex;
        private TabItem selectedTabItem;
        private UserControl selectedTabItemView;

        public MainViewModel(IShellService shellService)
        {
            this.shellService = shellService;
            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>();
            this.CommandBarSecondaryItems = new ObservableCollection<ShellCommandBarItem>
            {
                new ShellCommandBarButton
                {
                    Label = "Seitenleiste ein/ausblenden",
                    Command = new RelayCommand(this.shellService.ToggleSidebar)
                }
            };
            this.SelectedTabIndex = 1;
        }

        public MainView View { get; set; }

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; }

        /// <summary>
        ///     Shows the debug tab.
        /// </summary>
        public RelayCommand DebugCommand
        {
            get
            {
                return this.debugCommand
                       ?? (this.debugCommand = new RelayCommand(
                           () => this.shellService.ShowInNewTab("Debug", new DebugView(), null, true)));
            }
        }

        public int SelectedTabIndex
        {
            get { return this.selectedTabIndex; }
            set
            {
                this.Set(ref this.selectedTabIndex, value);
            }
        }

        public TabItem SelectedTabItem
        {
            get { return this.selectedTabItem; }
            set
            {
                this.selectedTabItem = value;

                if (this.selectedTabItemView != null)
                {
                    this.selectedTabItemView.DataContextChanged -= this.SelectedTabItemView_OnDataContextChanged;
                }

                this.selectedTabItemView = value?.Content as UserControl;

                if (this.selectedTabItemView != null)
                {
                    this.selectedTabItemView.DataContextChanged += this.SelectedTabItemView_OnDataContextChanged;
                }

                this.RefreshShellCommandBar();
            }
        }

        private void SelectedTabItemView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            this.RefreshShellCommandBar();
        }

        private void RefreshShellCommandBar()
        {
            this.CommandBarItems.Clear();
            IShellCommandBar viewModel = this.selectedTabItemView?.DataContext as IShellCommandBar;
            if (viewModel != null)
            {
                foreach (var command in viewModel.CommandBarItems)
                {
                    this.CommandBarItems.Add(command);
                }
            }
        }
    }
}