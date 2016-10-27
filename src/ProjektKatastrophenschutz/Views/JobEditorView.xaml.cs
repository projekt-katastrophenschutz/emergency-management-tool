// <copyright file="JobEditorView.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows.Controls;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaction logic for JobEditorView.xaml
    /// </summary>
    public partial class JobEditorView : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JobEditorView()
        {
            this.InitializeComponent();
            this.DataContextChanged += (sender, args) => this.RegisterInViewModel();
            this.Loaded += (sender, args) => this.RegisterInViewModel();
        }

        /// <summary>
        /// Join Database
        /// </summary>
        public JobEditorViewModel ViewModel => this.DataContext as JobEditorViewModel;

        /// <summary>
        /// Edit Register in ViewModel
        /// </summary>
        private void RegisterInViewModel()
        {
            var jobEditorViewModel = this.ViewModel;
            if (jobEditorViewModel != null)
            {
                jobEditorViewModel.View = this;
            }

            this.ViewModel.LoadForcesJob();
        }
    }
}