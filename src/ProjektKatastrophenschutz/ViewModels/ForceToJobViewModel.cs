using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using BSA.Core.Models;
using BSA.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using ProjektKatastrophenschutz.Utils;
using BSA.Core;
using BSA.Core.Network;
using GalaSoft.MvvmLight.Ioc;
using ProjektKatastrophenschutz.Elements;
using ProjektKatastrophenschutz.Services;
using ProjektKatastrophenschutz.Views;

namespace ProjektKatastrophenschutz.ViewModels
{
    public class ForceToJobViewModel : ViewModelBase, IClosable, IShellCommandBar, IShellViewStateAware
    {
        private readonly IForcesService forceService;
        private readonly IJobService jobService;
        private readonly IShellService shellService;

        public event EventHandler ClosingRequest;
        private Window window;

        private Job selectedJob;
        private IEnumerable<Force> selectedAvailableForces;
        private IEnumerable<Force> selectedAssignedForces;

        private readonly ObservableCollection<Force> availableForcesList;
        private readonly ObservableCollection<Force> assignedForcesList;

        private RelayCommand saveForceAssignmentCommand;
        private RelayCommand cancelForceAssignmentCommand;
        private RelayCommand<IList> availableForcesSelectionChangedCommand;
        private RelayCommand<IList> assignedForcesSelectionChangedCommand;

        private readonly ShellCommandBarButton showInTabCommandBarButton;
        private readonly ShellCommandBarButton showInWindowCommandBarButton;
        private readonly ShellCommandBarButton undockingCommandBarButton;
        private readonly ShellCommandBarButton saveAndCloseCommandBarButton;
        private readonly ShellCommandBarButton cancelAndCloseCommandBarButton;

        private List<Force> allForces;

        [PreferredConstructor]
        public ForceToJobViewModel(IShellService shellService, IForcesService forceService, IJobService jobService)
            : this(shellService, forceService, jobService, new Job())
        {
        }

        public ForceToJobViewModel(IShellService shellService, IForcesService forceService, IJobService jobService, Job job)
        {
            this.shellService = shellService;
            this.forceService = forceService;
            this.jobService = jobService;

            this.SelectedJob = job;

            this.availableForcesList = new ObservableCollection<Force>();
            this.AvailableForcesView = new ListCollectionView(this.availableForcesList);
          
            this.assignedForcesList = new ObservableCollection<Force>();
            this.AssignedForcesView = new ListCollectionView(this.assignedForcesList);

            this.allForces = this.forceService.GetAllForces();

            // Configure shell
            this.ShellViewState = ShellViewState.Embedded;

            this.undockingCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_shell.scale-150.png"),
                Label = "Abdocken",
                Command = new RelayCommand(() =>
                {
                    this.View.DataContext = new ForceToJobViewModel(this.shellService, this.forceService, this.jobService, this.SelectedJob);
                    var newView = new ForceToJob { DataContext = this };
                    this.shellService.ShowInNewTab("Neue Zuweisung", newView, this, true);
                }),
                Width = 284,
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
                    var shellTab = shellViewModel.ShellTabs.Single(tab => Equals(tab.View, this.View));
                    this.window = this.shellService.ShowTabInNewWindow(shellTab);
                }),
            };

            this.saveAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und schließen",
                Command = this.SaveForceAssignmentCommand,
            };

            this.cancelAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri =
                    new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                Label = "Abbrechen und schließen",
                Command = this.CancelForceAssignmentCommand,
            };

            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                this.undockingCommandBarButton,
                this.showInTabCommandBarButton,
                this.showInWindowCommandBarButton,
                new ShellCommandBarSeparator(),
                this.saveAndCloseCommandBarButton,
                this.cancelAndCloseCommandBarButton,
            };
        }

        public void LoadAllForces()
        {
            this.availableForcesList.Clear();
            this.allForces.ForEach(force =>
            {
                if (! assignedForcesList.Contains(force))
                    this.availableForcesList.Add(force);
            });
        }

        public void LoadForcesJob()
        {
            this.assignedForcesList.Clear();
            this.SelectedJob.Forces.ForEach(
                force => this.assignedForcesList.Add(this.allForces.Single(f => f.Id == force)));
        }

        public int GetNumJobsForForce(Force force)
        {
            var jobs = this.forceService.GetJobsForForces(force);
            var numJobs = jobs.Count;

            return numJobs;
        }

        public void AssignForce(Force force)
        {
            if (assignedForcesList.Contains(force))
                return;

            this.availableForcesList.Remove(force);
  //        this.SelectedJob.Forces.Add(force);
            this.assignedForcesList.Add(force);           
        }
 
        public void UnassignForce(Force force)
        {
            if (!assignedForcesList.Contains(force))
                return;

            this.assignedForcesList.Remove(force);
   //       this.SelectedJob.Forces.Remove(force);
            this.availableForcesList.Add(force);
        }

        public RelayCommand SaveForceAssignmentCommand => this.saveForceAssignmentCommand ??
             (this.saveForceAssignmentCommand = new RelayCommand(this.SaveForcesExecute));

        public RelayCommand CancelForceAssignmentCommand => this.cancelForceAssignmentCommand ??
             (this.cancelForceAssignmentCommand = new RelayCommand(this.CancelForceSelectionExecute));
                                                   
        public RelayCommand<IList> AvailableForcesSelectionChangedCommand => this.availableForcesSelectionChangedCommand ??
            (this.availableForcesSelectionChangedCommand = new RelayCommand<IList>
                (
                    items =>
                    {
                        SelectedAvailableForces = items?.Cast<Force>() ?? new List<Force>();
                    }
                 )
            );

        public RelayCommand<IList> AssignedForcesSelectionChangedCommand => this.assignedForcesSelectionChangedCommand ??
            (this.assignedForcesSelectionChangedCommand = new RelayCommand<IList>
                (
                    items =>
                    {
                        SelectedAssignedForces = items?.Cast<Force>() ?? new List<Force>();
                    }
                 )
            );

        public ListCollectionView AvailableForcesView { get; set; }
        public ListCollectionView AssignedForcesView { get; set; }

        public IEnumerable<Force> SelectedAvailableForces
        {
            get { return this.selectedAvailableForces; }
            set { this.Set(ref this.selectedAvailableForces, value); }
        }

        public IEnumerable<Force> SelectedAssignedForces
        {
            get { return this.selectedAssignedForces; }
            set { this.Set(ref this.selectedAssignedForces, value); }
        }

        public ForceToJob View { get; set; }

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

        public Job SelectedJob
        {
            get { return this.selectedJob; }
            set { this.Set(ref this.selectedJob, value); }
        }

        //public Force SelectedForce
        //{ 
        //    get { return this.selectedForce; }
        //    set { this.Set(ref this.selectedForce, value); }
        //}

        private void SaveForcesExecute()
        {
            this.SaveToDatabase();
            this.OnClosingRequest();
        }

        private void SaveToDatabase()
        {
            //var differenceQuery = assignedForcesList.Except(SelectedJob.Forces);
            //var differenceQueryList = differenceQuery as IList<Force> ?? differenceQuery.ToList();

            //if (SelectedJob.Forces.Count == 0 && differenceQueryList.Any()
            //    && this.SelectedJob.JobStatus == JobState.NewCreated)
            //{
            //    this.SelectedJob.JobStatus = JobState.InProgress;
            //}
                
            //if (! differenceQueryList.Any())
            //    return;

            foreach (var force in assignedForcesList)
            {
                if(!this.SelectedJob.Forces.Contains(force.Id))
                {
                    this.SelectedJob.Forces.Add(force.Id);
                    this.selectedJob.History.Add(new HistoryItem(BsaContext.GetUserName(), $"Einheit: {force.Name} {System.Environment.NewLine} zu Auftrag: {this.selectedJob.Description} hinzugefügt"));

                    if (Properties.Settings.Default.AutomaticallySetJobInProgressAfterForceAssignment)
                    {
                        if (this.SelectedJob.Forces.Count >= 1 && this.SelectedJob.JobStatus == JobState.NewCreated)
                            this.SelectedJob.JobStatus = JobState.InProgress;
                    }

                    this.jobService.SaveJob(this.SelectedJob);

                }
            }

            foreach (var force in availableForcesList)
            {
                if (this.SelectedJob.Forces.Contains(force.Id))
                {
                    if (this.SelectedJob.Forces.Count >= 1)
                    {
                        this.SelectedJob.Forces.Remove(force.Id);
                        this.selectedJob.History.Add(new HistoryItem(BsaContext.GetUserName(), $"Einheit: {force.Name} {System.Environment.NewLine} von Auftrag: {this.selectedJob.Description} entfernt"));
                        this.jobService.SaveJob(this.SelectedJob);
                    }
                }
            }
        }

        private void CancelForceSelectionExecute()
        {
            this.OnClosingRequest();
        }

        protected void OnClosingRequest()
        {
            this.ClosingRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
