// <copyright file="ShellTabViewModel.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using GalaSoft.MvvmLight;

    /// <summary>
    ///     The view model for shell tabs.
    /// </summary>
    public class ShellTabViewModel : ViewModelBase
    {
        /// <summary>
        ///     The title displayed in the tab.
        /// </summary>
        private string title;

        /// <summary>
        ///     The view displayed if the tab is active.
        /// </summary>
        private UserControl view;
        
        private bool pinned;

        private ImageSource icon;

        /// <summary>
        ///     Gets or sets the title displayed in the tab.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.Set(ref this.title, value); }
        }

        /// <summary>
        ///     Gets or sets the view displayed if the tab is active.
        /// </summary>
        public UserControl View
        {
            get { return this.view; }
            set { this.Set(ref this.view, value); }
        }
        
        public bool Pinned
        {
            get { return this.pinned; }
            set { this.Set(ref this.pinned, value); }
        }

        public ImageSource Icon
        {
            get { return this.icon; }
            set { this.Set(ref this.icon, value); }
        }
    }
}