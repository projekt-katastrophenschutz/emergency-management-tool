// <copyright file="JobToForceViewModel.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;
    using BSA.Core;
    using BSA.Core.Models;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Views;
    using Enumerable = System.Linq.Enumerable;
    using Settings = ProjektKatastrophenschutz.Properties.Settings;
    using System.Linq;
    public class JobToForceViewModel : ViewModelBase, IClosable, IShellCommandBar, IShellViewStateAware
    {
        private readonly ObservableCollection<Job> assignedJobsList;

        private readonly ObservableCollection<Job> availableJobsList;
        private readonly ShellCommandBarButton cancelAndCloseCommandBarButton;
        private readonly IForcesService forceService;
        private readonly IJobService jobService;
        private readonly ShellCommandBarButton saveAndCloseCommandBarButton;
        private readonly IShellService shellService;


        private readonly ShellCommandBarButton showInTabCommandBarButton;
        private readonly ShellCommandBarButton showInWindowCommandBarButton;
        private readonly ShellCommandBarButton undockingCommandBarButton;
        private RelayCommand<IList> assignedJobsSelectionChangedCommand;
        private RelayCommand<IList> availableJobsSelectionChangedCommand;
        private RelayCommand cancelJobAssignmentCommand;
        private readonly ObservableCollection<Job> changedJobs;
        public bool isLoaded;

        private RelayCommand saveJobAssignmentCommand;
        private IEnumerable<Job> selectedAssignedJobs;

        private IEnumerable<Job> selectedAvailableJobs;

        private Force selectedForce;
        private Job selectedJob;
        private Window window;

        [PreferredConstructor]
        public JobToForceViewModel(IShellService shellService, IForcesService forceService, IJobService jobService)
            : this(shellService, forceService, jobService, new Force())
        {
        }

        public JobToForceViewModel(IShellService shellService, IForcesService forceService, IJobService jobService,
            Force force)
        {
            this.shellService = shellService;
            this.forceService = forceService;
            this.jobService = jobService;

            this.SelectedForce = force;
            //      this.selectedJob = new Job();

            this.availableJobsList = new ObservableCollection<Job>();
            this.AvailableJobsView = new ListCollectionView(this.availableJobsList);

            this.assignedJobsList = new ObservableCollection<Job>();
            this.AssignedJobsView = new ListCollectionView(this.assignedJobsList);

            this.changedJobs = new ObservableCollection<Job>();

            // Configure shell
            this.ShellViewState = ShellViewState.Embedded;

            this.undockingCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_shell.scale-150.png"),
                Label = "Abdocken",
                Command = new RelayCommand(() =>
                {
                    this.View.DataContext = new ForceToJobViewModel(this.shellService, this.forceService,
                        this.jobService, this.SelectedJob);
                    var newView = new ForceToJob {DataContext = this};
                    this.shellService.ShowInNewTab("Neue Zuweisung", newView, this, true);
                }),
                Width = 284
            };

            this.showInTabCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_shell.scale-150.png"),
                Label = "Im Hauptfenster andocken",
                Command = new RelayCommand(() =>
                {
                    this.shellService.ShowWindowInNewTab(this.window);
                    this.window = null;
                }),
                Visibility = Visibility.Collapsed
            };

            this.showInWindowCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_window.scale-150.png"),
                Label = "In Fenster öffnen",
                Command = new RelayCommand(() =>
                {
                    var shellViewModel = SimpleIoc.Default.GetInstance<ShellViewModel>();
                    var shellTab = Enumerable.Single(shellViewModel.ShellTabs, tab => Equals(tab.View, this.View));
                    this.window = this.shellService.ShowTabInNewWindow(shellTab);
                })
            };

            this.saveAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und schließen",
                Command = this.SaveJobAssignmentCommand
            };

            this.cancelAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri =
                    new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                Label = "Abbrechen und schließen",
                Command = this.CancelJobAssignmentCommand
            };

            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                this.undockingCommandBarButton,
                this.showInTabCommandBarButton,
                this.showInWindowCommandBarButton,
                new ShellCommandBarSeparator(),
                this.saveAndCloseCommandBarButton,
                this.cancelAndCloseCommandBarButton
            };
        }


        public RelayCommand SaveJobAssignmentCommand => this.saveJobAssignmentCommand ??
                                                        (this.saveJobAssignmentCommand =
                                                            new RelayCommand(this.SaveJobsExecute));

        public RelayCommand CancelJobAssignmentCommand => this.cancelJobAssignmentCommand ??
                                                          (this.cancelJobAssignmentCommand =
                                                              new RelayCommand(this.CancelJobSelectionExecute));

        public RelayCommand<IList> AvailableForcesSelectionChangedCommand
            => this.availableJobsSelectionChangedCommand ??
               (this.availableJobsSelectionChangedCommand = new RelayCommand<IList>
                   (
                   items => { this.SelectedAvailableJobs = items?.Cast<Job>() ?? new List<Job>(); }
                   )
                   );

        public RelayCommand<IList> AssignedForcesSelectionChangedCommand
            => this.assignedJobsSelectionChangedCommand ??
                (this.assignedJobsSelectionChangedCommand =
                    new RelayCommand<IList>
                        (
                        items =>
                        {
                            this.SelectedAssignedJobs =
                                items?.Cast<Job>() ??
                                new List<Job>();
                        }
                        )
                    );

        public ListCollectionView AvailableJobsView { get; set; }
        public ListCollectionView AssignedJobsView { get; set; }

        public IEnumerable<Job> SelectedAvailableJobs
        {
            get { return this.selectedAvailableJobs; }
            set { this.Set(ref this.selectedAvailableJobs, value); }
        }

        public IEnumerable<Job> SelectedAssignedJobs
        {
            get { return this.selectedAssignedJobs; }
            set { this.Set(ref this.selectedAssignedJobs, value); }
        }

        public JobToForce View { get; set; }

        public Force SelectedForce
        {
            get { return this.selectedForce; }
            set { this.Set(ref this.selectedForce, value); }
        }

        public Job SelectedJob
        {
            get { return this.selectedJob; }
            set { this.Set(ref this.selectedJob, value); }
        }

        public event EventHandler ClosingRequest;

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; } = null;

        public ShellViewState ShellViewState { get; private set; }

        public void ShellViewStateChanged(ShellViewState shellViewState)
        {
            this.ShellViewState = shellViewState;
            this.undockingCommandBarButton.Visibility = shellViewState == ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.showInTabCommandBarButton.Visibility = shellViewState == ShellViewState.Window
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.showInWindowCommandBarButton.Visibility = shellViewState == ShellViewState.Tab
                ? Visibility.Visible
                : Visibility.Collapsed;

            this.saveAndCloseCommandBarButton.Visibility = shellViewState != ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.cancelAndCloseCommandBarButton.Visibility = shellViewState != ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public void LoadAllJobs()
        {
            this.availableJobsList.Clear();

            this.jobService.GetAllJobs().ForEach(job =>
            {
                if (!this.assignedJobsList.Contains(job))
                {
                    this.availableJobsList.Add(job);
                }
            });
        }

        public void LoadJobForces()
        {
            this.assignedJobsList.Clear();

            this.forceService.GetJobsForForces(this.SelectedForce).ForEach(
                job => this.assignedJobsList.Add(job));
            this.isLoaded = true;
        }

        public void AddTempJobs(Job job, Force force)
        {
            //         this.selectedJob = new Job();
            this.selectedJob = job;

            if (this.assignedJobsList.Contains(job) && this.SelectedJob.Forces.Contains(this.SelectedForce.Id))
            {
                return;
            }

            this.assignedJobsList.Add(job);
//            this.selectedJob.Forces.Add(force);
            this.availableJobsList.Remove(job);
            this.changedJobs.Add(this.selectedJob);
        }


        public void DeleteTempJobs(Job job)
        {
            if (!this.assignedJobsList.Contains(job) && !this.SelectedJob.Forces.Contains(this.SelectedForce.Id))
            {
                return;
            }

            this.selectedJob = job;

            this.assignedJobsList.Remove(job);
            //           this.SelectedJob.Forces.Remove(this.SelectedForce);
            this.availableJobsList.Add(job);
        }

        private void SaveJobsExecute()
        {
            this.SaveToDatabase();
            this.OnClosingRequest();
        }

        private void SaveToDatabase()
        {
            this.forceService.SaveForces(this.SelectedForce);

            foreach (var Job in this.assignedJobsList)
            {
                if (!Job.Forces.Contains(this.selectedForce.Id))
                {
                    Job.Forces.Add(this.selectedForce.Id);
                    Job.History.Add(new HistoryItem(BsaContext.GetUserName(),
                        $"Auftrag: {Job.Description} {Environment.NewLine} zu Einheit: {this.selectedForce.Name} hinzugefügt"));

                    if (Settings.Default.AutomaticallySetJobInProgressAfterForceAssignment)
                    {
                        if (Job.Forces.Count >= 1 && Job.JobStatus == JobState.NewCreated)
                        {
                            Job.JobStatus = JobState.InProgress;
                        }
                    }
                    this.jobService.SaveJob(Job);
                }
            }

            foreach (var Job in this.availableJobsList)
            {
                if (Job.Forces.Contains(this.selectedForce.Id))
                {
                    if (Job.Forces.Count >= 1)
                    {
                        Job.Forces.Remove(this.selectedForce.Id);
                        Job.History.Add(new HistoryItem(BsaContext.GetUserName(),
                            $"Auftrag: {Job.Description} {Environment.NewLine} von Einheit: {this.selectedForce.Name} entfernt"));
                        this.jobService.SaveJob(Job);
                    }
                }
            }
        }

        private void CancelJobSelectionExecute()
        {
            this.OnClosingRequest();
        }

        protected void OnClosingRequest()
        {
            // Close window. WindowClosingExecute will be called, if some window that uses this view subscribed to this event.
            this.ClosingRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}