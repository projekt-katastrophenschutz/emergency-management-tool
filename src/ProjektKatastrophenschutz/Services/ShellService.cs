// <copyright file="ShellService.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Services
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using GalaSoft.MvvmLight;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Utils.Windows;
    using ProjektKatastrophenschutz.ViewModels;

    public class ShellService : IShellService
    {
        private Shell appShell;

        /// <summary>
        ///     Register the application shell.
        /// </summary>
        /// <param name="shell">The application shell.</param>
        public void RegisterShell(Shell shell)
        {
            this.appShell = shell;
        }

        public event EventHandler ShellActivated;

        /// <summary>
        ///     Opens a new tab with the specific view and view model.
        /// </summary>
        /// <param name="title">The tab title.</param>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="autoSelectNewTab">Defines if the created tab should be selected or not.</param>
        public ShellTabViewModel ShowInNewTab(string title, UserControl view, ViewModelBase viewModel,
            bool autoSelectNewTab = false)
        {
            view.DataContext = viewModel;
            var shellTabViewModel = new ShellTabViewModel
            {
                Pinned = false,
                Title = title,
                View = view
            };
            this.appShell.ViewModel.ShellTabs.Add(shellTabViewModel);

            var awareViewModel = viewModel as IShellViewStateAware;
            awareViewModel?.ShellViewStateChanged(ShellViewState.Tab);

            if (autoSelectNewTab)
            {
                this.appShell.ViewModel.SelectedTab = shellTabViewModel;
            }

            this.RegisterTabCloseable(shellTabViewModel);

            return shellTabViewModel;
        }

        public Window ShowInNewWindow(string title, UserControl view, ViewModelBase viewModel, int minWidth,
            int minHeight, bool isDialog = false)
        {
            view.DataContext = viewModel;

            var shellViewPresenter = new ShellViewPresenter()
            {
                View = view
            };

            var window = new Window
            {
                Content = shellViewPresenter,
                Title = title,
                MinWidth = minWidth,
                MinHeight = minHeight
            };

            var awareViewModel = viewModel as IShellViewStateAware;
            awareViewModel?.ShellViewStateChanged(ShellViewState.Window);

            this.RegisterWindowClosable(window, viewModel as IClosable);

            if (isDialog)
                window.ShowDialog();
            else
                window.Show();
            
            //var windowHandle = new WindowInteropHelper(window).Handle;
            //WinApi.SetForegroundWindow(windowHandle);       

            return window;
        }

        public Window ShowTabInNewWindow(ShellTabViewModel shellTab)
        {
            this.appShell.ViewModel.ShellTabs.Remove(shellTab);

            var shellViewPresenter = new ShellViewPresenter()
            {
                View = shellTab.View
            };

            var window = new Window
            {
                Content = shellViewPresenter,
                Title = shellTab.Title,
                MinWidth = 400,
                MinHeight = 300
            };

            var awareViewModel = shellTab.View.DataContext as IShellViewStateAware;
            awareViewModel?.ShellViewStateChanged(ShellViewState.Window);
            
            window.Show();
            var windowHandle = new WindowInteropHelper(window).Handle;
            WinApi.SetForegroundWindow(windowHandle);

            this.RegisterWindowClosable(window, shellTab.View.DataContext as IClosable);
            this.UnregisterTabCloseable(shellTab);

            return window;
        }

        public ShellTabViewModel ShowWindowInNewTab(Window window)
        {
            var view = (window.Content as ShellViewPresenter)?.View as UserControl;
            var shellTabViewModel = new ShellTabViewModel
            {
                Pinned = false,
                Title = window.Title,
                View = view
            };

            this.appShell.ViewModel.ShellTabs.Add(shellTabViewModel);

            var awareViewModel = view?.DataContext as IShellViewStateAware;
            awareViewModel?.ShellViewStateChanged(ShellViewState.Tab);

            this.appShell.ViewModel.SelectedTab = shellTabViewModel;

            window.Close();

            this.RegisterTabCloseable(shellTabViewModel);

            return shellTabViewModel;
        }

        public void ToggleSidebar()
        {
            this.appShell.ToggleSidebar();
        }

        public void CloseTab(ShellTabViewModel shellTab)
        {
            this.appShell.ViewModel.ShellTabs.Remove(shellTab);
        }

        public void InvokeShellActivated(object sender, EventArgs e)
        {
            this.ShellActivated?.Invoke(sender, e);
        }

        private void RegisterTabCloseable(ShellTabViewModel shellTab)
        {
            var closableView = shellTab.View?.DataContext as IClosable;
            if (closableView != null)
            {
                closableView.ClosingRequest += this.ClosableViewOnClosingRequest;
            }
        }

        private void UnregisterTabCloseable(ShellTabViewModel shellTab)
        {
            var closableView = shellTab.View?.DataContext as IClosable;
            if (closableView != null)
            {
                closableView.ClosingRequest -= this.ClosableViewOnClosingRequest;
            }
        }

        private void ClosableViewOnClosingRequest(object sender, EventArgs eventArgs)
        {
            var shellTabViewModel = this.appShell.ViewModel.ShellTabs.SingleOrDefault(shellTab => shellTab.View?.DataContext == sender);
            if (shellTabViewModel != null)
            {
                this.UnregisterTabCloseable(shellTabViewModel);
                this.appShell.ViewModel.ShellTabs.Remove(shellTabViewModel);
            }
        }

        private void RegisterWindowClosable(Window window, IClosable closable)
        {
            if (closable != null && window != null)
            {
                closable.ClosingRequest += (s, e) => window.Close();
            }
        }
    }
}