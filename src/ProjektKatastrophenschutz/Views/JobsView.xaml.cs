// <copyright file="JobsView.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BSA.Core.Models;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.ViewModels;

    public partial class JobsView : UserControl
    {
        private bool filterVisible = true;

        /// <summary>
        /// Edit Register View in ViewModel
        /// </summary>
        public JobsView()
        {
            this.InitializeComponent();
            this.TryRegisterViewInViewModel();
            this.DataContextChanged += (sender, args) =>
            {
                this.TryRegisterViewInViewModel();
            };

            //this.ToggleFilterVisibility();
        }

        /// <summary>
        /// Join Database
        /// </summary>
        public JobsViewModel ViewModel => this.DataContext as JobsViewModel;

        /// <summary>
        /// Select Filter content visibility 
        /// </summary>
        public void ToggleFilterVisibility()
        {
            var visibility = Visibility.Visible;
            if (this.filterVisible)
            {
                visibility = Visibility.Collapsed;
            }

            //this.ToggleButton1.Visibility = visibility;
            //this.ToggleButton2.Visibility = visibility;
            //this.ToggleButton3.Visibility = visibility;
            this.FilterBar.Visibility = visibility;
            //this.RemoveAllFiltersTextBlock.Visibility = visibility;
            //this.CreateDummyDataTextBlock.Visibility = visibility;

            this.filterVisible = !this.filterVisible;
        }

        /// <summary>
        /// Edit Register in Viewmodel
        /// </summary>
        private void TryRegisterViewInViewModel()
        {
            var jobsViewModel = this.ViewModel;
            if (jobsViewModel != null)
            {
                jobsViewModel.View = this;
            }
        }

        /// <summary>
        /// Loading Jobs
        /// </summary>
        private void JobsView_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LoadData();
        }

        /// <summary>
        /// Create Dummydata
        /// </summary>
        private void CreateDummyDataTextBlock_MouseLeftButtonUp(object sender,
            MouseButtonEventArgs e)
        {
            this.ViewModel.CreateDummyData();
        }

        //private void ExportJobTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{

        //    var jobs = this.ViewModel.JobsView;
        //    var list = jobs.Cast<Job>();
        //    CsvExport.csvJobWriter(list);
        //}

        /// <summary>
        /// Edit Dummyjob
        /// </summary>
        private void DummyJobDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            var job = Gui.GetObjectOfClickedDataGridRow<Job>(sender, eventArgs);
            if (job != null)
            {
                this.ViewModel.EditJob(job);
            }
        }

        /// <summary>
        /// Bind some XAML Items with Content
        /// </summary>
        private void AddItems(ItemsControl contextMenu, IEnumerable<Job> jobs)
        {
            var selectedJobsCount = jobs.ToList().Count();

            var editItem = new MenuItem { Header = "Editieren" };
            editItem.Click += this.EditJobContextMenuItem_OnClick;

            var deleteItem = new MenuItem { Header = "Löschen" };
            deleteItem.Click += this.DeleteJobContextMenuItem_OnClick;

            var exportItem = new MenuItem { Header = "Exportieren" };
            exportItem.Click += this.ExportContextMenuItem_OnClick;

            var editForcesItem = new MenuItem { Header = "Einheiten zuweisen" };
            editForcesItem.Click += this.EditForcesContextMenuItem_OnClick;

            var setStatusItem = new MenuItem() { Header = "Status setzen" };

            var setOpenItem = new MenuItem() { Header = "Offen" };
            setOpenItem.Click += this.SetOpenStateContextMenuItem_OnClick;

            var setInProgressItem = new MenuItem() { Header = "In Bearbeitung" };
            setInProgressItem.Click += this.SetInProgressStateContextMenuItem_OnClick;

            var setEndedItem = new MenuItem() { Header = "Beendet" };
            setEndedItem.Click += this.SetEndedStateContextMenuItem_OnClick;

            // Doesn't work anymore. These items are not visible. ToDo Fix that.
            setStatusItem.ItemsSource = new List<MenuItem> { setOpenItem, setInProgressItem, setEndedItem };

            if (!(selectedJobsCount > 1))
                contextMenu.Items.Add(editItem);

            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(exportItem);
            contextMenu.Items.Add(setStatusItem);          

            if (!(selectedJobsCount > 1))
                contextMenu.Items.Add(editForcesItem);
        }

        private void DummyJobDataGrid_OnMouseRightButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            // If the user clicked somewhere at the DataGrid but on no Job element, just return
            // Otherwise, show context menu.
            var job = Gui.GetObjectOfClickedDataGridRow<Job>(sender, eventArgs);
            if (job == null)
                return;

            var selectedJobs = this.ViewModel.SelectedJobs;
            var contextMenu = new ContextMenu();

            AddItems(contextMenu, selectedJobs.ToList());
            contextMenu.IsOpen = true;
        }

        /// <summary>
        /// Edit Force content on click
        /// </summary>
        private void EditForcesContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var job = this.ViewModel.SelectedJob;
            if (job != null)
            {
                this.ViewModel.EditForces(job);
            }
        }

        /// <summary>
        /// Edit Job Datacontent on Click
        /// </summary>
        private void EditJobContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var job = this.ViewModel.SelectedJob;
            if (job != null)
            {
                this.ViewModel.EditJob(job);
            }
        }

        /// <summary>
        /// Delete Job Datacontent on Click
        /// </summary>
        private void DeleteJobContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var jobs = new List<Job>(this.ViewModel.SelectedJobs);

            foreach (var job in jobs)
                this.ViewModel.DeleteJob(job);
        }

        /// <summary>
        /// Export Datacontent on Click
        /// </summary>
        private void ExportContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var jobs = new List<Job>(this.ViewModel.SelectedJobs);
            this.ViewModel.ExportJobs(jobs);
        }

        /// <summary>
        /// Set Job State Datacontent on Click
        /// </summary>
        private void SetOpenStateContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var jobs = new List<Job>(this.ViewModel.SelectedJobs);

            foreach (var job in jobs)
                this.ViewModel.SetJobStatus(job, JobState.NewCreated);
        }

        /// <summary>
        /// Set Job Progress Datacontent on Click
        /// </summary>
        private void SetInProgressStateContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var jobs = new List<Job>(this.ViewModel.SelectedJobs);

            foreach (var job in jobs)
                this.ViewModel.SetJobStatus(job, JobState.InProgress);
        }

        /// <summary>
        /// Set End State Datacontent on Click
        /// </summary>
        private void SetEndedStateContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var jobs = new List<Job>(this.ViewModel.SelectedJobs);

            foreach (var job in jobs)
                this.ViewModel.SetJobStatus(job, JobState.Ended);
        }

        /// <summary>
        /// Load Job Datacontent 
        /// </summary>
        private void JobDataGrid_OnLoaded(object sender, EventArgs e)
        {
            // should be "priority" column
            var firstColumn = JobDataGrid.Columns.First();
            // Absteigende Sortierung
            var sortDirection = ListSortDirection.Descending;

            firstColumn.SortDirection = sortDirection;
            JobDataGrid.Items.SortDescriptions.Add(new SortDescription(firstColumn.SortMemberPath, sortDirection));
        }
    }
}