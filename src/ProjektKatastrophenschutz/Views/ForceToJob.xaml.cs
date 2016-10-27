// <copyright file="ForceToJob.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BSA.Core.Models;

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaktionslogik für ForceToJob.xaml
    /// </summary>
    public partial class ForceToJob : UserControl
    {
        private bool isLoaded = false;

        public ForceToJob()
        {
            this.InitializeComponent();
            this.TryRegisterViewInViewModel();
            this.DataContextChanged += (sender, args) =>
            {
                this.TryRegisterViewInViewModel();
            };
        }

        /// <summary>
        /// Join Database
        /// </summary>
        public ForceToJobViewModel ViewModel => this.DataContext as ForceToJobViewModel;

        private void TryRegisterViewInViewModel()
        {
            var forceToJobViewModel = this.ViewModel;
            if (forceToJobViewModel != null)
            {
                forceToJobViewModel.View = this;
            }
        }

        /// <summary>
        /// Loading Forces and their jobs
        /// </summary>
        private void ForceToJob_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded == true)
                return;
            this.ViewModel.LoadForcesJob();
            this.ViewModel.LoadAllForces();
            isLoaded = true;           
        }

        /// <summary>
        /// Adding selected Forces
        /// </summary>
        private void AddToTemp(object sender, RoutedEventArgs eventArgs)
        {
            var selectedForces = this.AvailableForcesDataGrid.SelectedItems;
            if (selectedForces == null || selectedForces.Count < 1)
                return;

            var forces = new List<Force>(selectedForces.Cast<Force>());
            foreach (var force in forces)
                this.ViewModel.AssignForce(force);
        }

        /// <summary>
        /// Deleting selected Forces
        /// </summary>
        private void DeleteFromTemp(object sender, RoutedEventArgs eventArgs)
        {
            var selectedForces = this.AssignedForcesDataGrid.SelectedItems;
            if (selectedForces == null || selectedForces.Count < 1)
                return;

            var forces = new List<Force>(selectedForces.Cast<Force>());
            foreach (var force in forces)
                this.ViewModel.UnassignForce(force);
        }

        private void AvailableForcesDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(@"Selection changed");
        }
    }
}