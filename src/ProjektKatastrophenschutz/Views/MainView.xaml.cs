// <copyright file="MainView.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows.Controls;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
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

        public MainViewModel ViewModel => this.DataContext as MainViewModel;
    }
}