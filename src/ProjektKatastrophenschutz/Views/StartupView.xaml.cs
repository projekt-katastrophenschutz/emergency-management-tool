// <copyright file="StartupView.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows.Controls;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaction logic for StartupView.xaml
    /// </summary>
    public partial class StartupView : UserControl
    {
        public StartupView()
        {
            this.InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (this.ViewModel != null)
                {
                    this.ViewModel.View = this;
                }
            };
        }

        public StartupViewModel ViewModel => this.DataContext as StartupViewModel;
    }
}