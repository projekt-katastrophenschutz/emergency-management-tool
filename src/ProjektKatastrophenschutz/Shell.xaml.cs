// <copyright file="Shell.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;
using ProjektKatastrophenschutz.ColorThemes;
using ProjektKatastrophenschutz.Properties;

namespace ProjektKatastrophenschutz
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using ChromeTabs;
    using log4net;
    using MahApps.Metro.Controls;
    using ProjektKatastrophenschutz.Utils.Windows;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        private bool shown;
        private double lastSidebarWidth;

        public Shell()
        {
            this.InitializeComponent();
        }

        public ShellViewModel ViewModel => this.DataContext as ShellViewModel;
        
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (this.shown)
            {
                return;
            }

            this.shown = true;

            var windowHandle = new WindowInteropHelper(this).Handle;
            WinApi.SetForegroundWindow(windowHandle);
            this.ViewModel.ShellService.InvokeShellActivated(this, e);
        }

        public void ToggleSidebar()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (this.RootGrid.ColumnDefinitions[1].MinWidth > 0)
            {
                this.lastSidebarWidth = this.RootGrid.ColumnDefinitions[1].ActualWidth;
                this.RootGrid.ColumnDefinitions[1].MinWidth = 0;
                this.RootGrid.ColumnDefinitions[1].Width = new GridLength(0);
            }
            else
            {
                this.RootGrid.ColumnDefinitions[1].MinWidth = 100;
                this.RootGrid.ColumnDefinitions[1].Width = new GridLength(this.lastSidebarWidth);
            }
        }

        private void Shell_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Load(this);

            LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
            ));
        }

        private void Shell_OnClosed(object sender, EventArgs e)
        {          
            //Settings.Default.SelectedColorTheme = null;
            Settings.Default.Save();
        }

        private void ChromeTabControl_ContainerItemPreparedForOverride(object sender, ContainerOverrideEventArgs e)
        {
            var viewModel = e.Model as ShellTabViewModel;
            if (e.TabItem != null && viewModel != null)
            {
                e.TabItem.IsPinned = viewModel.Pinned;
            }

            e.Handled = true;
        }

        private void ChromeTabControl_TabDraggedOutsideBonds(object sender, TabDragEventArgs e)
        {
            var shellTab = e.Tab as ShellTabViewModel;
            if (shellTab?.Pinned == false)
            {
                this.ViewModel.ShellService.ShowTabInNewWindow(shellTab);
            }
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            //Settings.Default.Save();
            //if (ColorPicker.SelectedColor != null)
            //{
            //    var color = ColorPicker.SelectedColor.Value;
            //    if (color == null)
            //        return;

            //    //this.ViewModel.BackgroundColor = new SolidColorBrush(color);
            //    Settings.Default.ColorScheme = new SolidColorBrush(color);
            //    Settings.Default.Save();
            //}
        } 
    }
}