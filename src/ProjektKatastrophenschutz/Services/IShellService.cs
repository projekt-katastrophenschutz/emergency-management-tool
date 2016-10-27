// <copyright file="IShellService.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Services
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using GalaSoft.MvvmLight;
    using ProjektKatastrophenschutz.ViewModels;

    public interface IShellService
    {
        /// <summary>
        ///     Register the application shell.
        /// </summary>
        /// <param name="shell">The application shell.</param>
        void RegisterShell(Shell shell);

        /// <summary>
        ///     Opens a new tab with the specific view and view model.
        /// </summary>
        /// <param name="title">The tab title.</param>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="autoSelectNewTab">Defines if the created tab should be selected or not.</param>
        ShellTabViewModel ShowInNewTab(string title, UserControl view, ViewModelBase viewModel,
            bool autoSelectNewTab = false);

        Window ShowInNewWindow(string title, UserControl view, ViewModelBase viewModel, int minWidth, int minHeight, bool isDialog = false);

        Window ShowTabInNewWindow(ShellTabViewModel shellTab);

        ShellTabViewModel ShowWindowInNewTab(Window window);

        void ToggleSidebar();

        void CloseTab(ShellTabViewModel shellTab);

        event EventHandler ShellActivated;

        void InvokeShellActivated(object sender, EventArgs e);
    }
}