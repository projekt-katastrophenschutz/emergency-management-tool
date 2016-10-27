

using System.Collections.Generic;
using System.Linq;

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BSA.Core.Models;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    /// Interaktionslogik für JobToForce.xaml
    /// </summary>
    public partial class JobToForce : UserControl
    {

        private bool isLoaded = false;

        public JobToForce()
        {
            this.InitializeComponent();
            this.TryRegisterViewInViewModel();
            this.DataContextChanged += (sender, args) =>
            {
                this.TryRegisterViewInViewModel();
            };
        }

        public JobToForceViewModel ViewModel => this.DataContext as JobToForceViewModel;

        private void TryRegisterViewInViewModel()
        {
            var jobToForceViewModel = this.ViewModel;
            if (jobToForceViewModel != null)
            {
                jobToForceViewModel.View = this;
            }
        }

        private void JobToForces_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded == true)
                return;

            this.ViewModel.LoadJobForces();
            this.ViewModel.LoadAllJobs();
            isLoaded = true;
        }

        private void AddToTemp(object sender, RoutedEventArgs eventArgs)
        {
            var selectedJobs = AvailableJobsDataGrid.SelectedItems;
            if (selectedJobs == null || selectedJobs.Count < 1)
                return;

            var jobs = new List<Job>(selectedJobs.Cast<Job>());
            foreach (var job in jobs)
                this.ViewModel.AddTempJobs(job, this.ViewModel.SelectedForce);
        }

        private void DeleteFromTemp(object sender, RoutedEventArgs eventArgs)
        {
            var selectedJobs = AssignedJobsDataGrid.SelectedItems;
            if (selectedJobs == null || selectedJobs.Count < 1)
                return;

            var jobs = new List<Job>(selectedJobs.Cast<Job>());
            foreach (var job in jobs)
                this.ViewModel.DeleteTempJobs(job);
        }  
    }
}
