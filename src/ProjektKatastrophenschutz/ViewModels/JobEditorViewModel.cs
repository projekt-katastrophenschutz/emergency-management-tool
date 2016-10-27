// <copyright file="JobEditorViewModel.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using BSA.Core.Models;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Views;
    using System.Windows.Data;
    using BSA.Core;
    using ProjektKatastrophenschutz.Converters;

    public class JobEditorViewModel : ViewModelBase, IClosable, IShellCommandBar, IShellViewStateAware
    {
        private readonly IJobService jobService;
        private readonly IForcesService forcesService;
        private readonly IShellService shellService;
        private RelayCommand cancelJobEditCommand;
        private bool isSaveRequested;
        private Job job;
        private RelayCommand saveJobCommand;
        private readonly ShellCommandBarButton showInTabCommandBarButton;
        private readonly ShellCommandBarButton showInWindowCommandBarButton;
        private readonly ShellCommandBarButton undockingCommandBarButton;
        private readonly ShellCommandBarButton saveAndNextCommandBarButton;
        private readonly ShellCommandBarButton saveAndCloseCommandBarButton;
        private readonly ShellCommandBarButton cancelCommandBarButton;
        private readonly ShellCommandBarButton cancelAndCloseCommandBarButton;
        private Window window;
        //private RelayCommand windowClosingCommand;
        private readonly ObservableCollection<Force> forcesList;

        private JobTypeToLocalizedStringConverter jobTypeToLocalizedStringConverter = new JobTypeToLocalizedStringConverter();
        
        [PreferredConstructor]
        public JobEditorViewModel(IShellService shellService, IJobService jobService, IForcesService forcesService)
            : this(shellService, jobService, forcesService, new Job())
        {
        }

        public JobEditorViewModel(IShellService shellService, IJobService jobService, IForcesService forcesService, Job job)
        {
            this.shellService = shellService;
            this.jobService = jobService;
            this.forcesService = forcesService;
            this.Job = job ?? new Job();
            this.BeginJobEdit(this.Job);
            this.isSaveRequested = false;

            this.forcesList = new ObservableCollection<Force>();
            this.ForcesView = new ListCollectionView(this.forcesList);

            // Set up messenger completion values
            this.jobService.GetAllJobs().Select(j => j.Messenger).Distinct().ToList()
                .ForEach(messenger => this.MessengerValues.Add(messenger));

            this.AddMessengerValueIfNotExists("Leitstelle");

            // Configure shell
            this.ShellViewState = ShellViewState.Embedded;
            this.undockingCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_shell.scale-150.png"),
                Label = "Abdocken",
                Command = new RelayCommand(() =>
                {
                    this.View.DataContext = new JobEditorViewModel(this.shellService, this.jobService, this.forcesService);
                    var newView = new JobEditorView {DataContext = this};
                    this.shellService.ShowInNewTab("Neuer Auftrag", newView, this, true);
                }),
                Width = 284,
                Visibility = this.Job.IsNew ? Visibility.Visible : Visibility.Collapsed
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
                Visibility = this.Job.IsNew ? Visibility.Collapsed : Visibility.Visible
            };

            this.saveAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und schließen",
                Command = this.SaveJobCommand,
                Visibility = this.Job.IsNew ? Visibility.Collapsed : Visibility.Visible
            };

            this.saveAndNextCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und neuen Auftrag anlegen",
                Command = this.SaveJobCommand,
                Visibility = this.Job.IsNew ? Visibility.Visible : Visibility.Collapsed
            };

            this.cancelCommandBarButton = new ShellCommandBarButton
            {
                ImageUri =
                    new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                Label = "Abbrechen",
                Command = this.CancelJobEditCommand,
                Visibility = this.Job.IsNew ? Visibility.Visible : Visibility.Collapsed
            };

            this.cancelAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri =
                    new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                Label = "Abbrechen und schließen",
                Command = this.CancelJobEditCommand,
                Visibility = this.Job.IsNew ? Visibility.Collapsed : Visibility.Visible
            };

            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                this.undockingCommandBarButton,
                this.showInTabCommandBarButton,
                this.showInWindowCommandBarButton,
                new ShellCommandBarSeparator(),
                this.saveAndCloseCommandBarButton,
                this.saveAndNextCommandBarButton,
                this.cancelAndCloseCommandBarButton,
                this.cancelCommandBarButton
            };
        }

        public JobEditorView View { get; set; }

        ////public RelayCommand CloseWindowCommand => this.windowClosingCommand ??
        ////                                          (this.windowClosingCommand = new RelayCommand(this.OnClosingRequest));

        public RelayCommand SaveJobCommand => this.saveJobCommand ??
                                              (this.saveJobCommand = new RelayCommand(this.SaveJobExecute));

        public RelayCommand CancelJobEditCommand => this.cancelJobEditCommand ??
                                                    (this.cancelJobEditCommand =
                                                        new RelayCommand(this.CancelJobEditExecute));

        public ObservableCollection<string> MessengerValues { get; } = new ObservableCollection<string>();

        public Job Job
        {
            get { return this.job; }
            set { this.Set(ref this.job, value); }
        }

        public ListCollectionView ForcesView { get; set; }

        public IEnumerable<JobPriority> JobPriorities
            => Enum.GetValues(typeof (JobPriority)).Cast<JobPriority>();

        public IEnumerable<SelectionMenu> menu
            => Enum.GetValues(typeof (SelectionMenu)).Cast<SelectionMenu>();

        public IEnumerable<string> JobTypes
            => Enum.GetValues(typeof (JobType))
            .Cast<JobType>()
            .Select(jobType => (string)this.jobTypeToLocalizedStringConverter.Convert(jobType, typeof(string), null, null));

        public IEnumerable<JobState> JobStates
            => Enum.GetValues(typeof (JobState)).Cast<JobState>();

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

            this.saveAndNextCommandBarButton.Visibility = shellViewState == ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.saveAndCloseCommandBarButton.Visibility = shellViewState != ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.cancelCommandBarButton.Visibility = shellViewState == ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.cancelAndCloseCommandBarButton.Visibility = shellViewState != ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // ReSharper disable once ParameterHidesMember
        public void BeginJobEdit(Job job)
        {
            this.Job = job;
            this.Job.BeginEdit();
        }

        public event EventHandler ClosingRequest;

        private void WindowClosingExecute()
        {
            if (!this.isSaveRequested)
            {
                this.Job.CancelEdit();
            }
        }

        public void LoadForcesJob()
        {
            var allForces = this.forcesService.GetAllForces();
            this.forcesList.Clear();
            this.Job.Forces.ForEach(
                force => this.forcesList.Add(allForces.Single(f => f.Id == force)));
        }


        protected void OnClosingRequest()
        {
            // Cancel job edit, if no save was requested
            this.WindowClosingExecute();
            // Close window. 
            // The call has no effect if this ViewModel is not being used by a window that subscribed to the ClosingRequest event.
            this.ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        private void SaveToDatabase()
        {
            this.Job.History.Add(this.Job.IsNew
                ? new HistoryItem(BsaContext.GetUserName(), $"Neuer Auftrag angelegt: {this.Job.Description}")
                : new HistoryItem(BsaContext.GetUserName(), $"Auftrag {this.Job.Description} modifiziert"));
            this.jobService.SaveJob(this.Job);
            
            // ToDo: This does not fall within the remit of the presentation layer. Implementation should lie in an IJobService instance instead! 
            ////CommunicationProxy.SendJobChange(BsaContext.GetUserName(), this.job);
        }

        private void CancelJobEditExecute()
        {
            this.Job.CancelEdit();
            this.OnClosingRequest();
        }

        private void SaveJobExecute()
        {
            this.isSaveRequested = true;

            if (this.Job.IsNew)
            {
                // ToDo: Where is the new number-based id handling?
                // ToDo: Who is responsible for id generation? Business or database layer?
                ////Job.Identifier = GetNewJobIdentifier();
                this.Job.Date = DateTime.Now;
                this.Job.JobStatus = JobState.NewCreated;
            }
            else
            {
                ////var changes = this.Job.GetChanges();
            }

            // End edit and save to Database
            this.Job.EndEdit();
            this.SaveToDatabase();

            // Add current messenger value to the completion list
            this.AddMessengerValueIfNotExists(this.Job.Messenger);

            // Begin new JobEdit, for "Create Job" - View.
            // That means: All information (TextBoxes etc.) of the form will be cleared, if user clicks the "Save" button
            this.BeginJobEdit(new Job());

            // Invoke close request, so the window closes if the view is being used in a extra window and the window
            // subscribed to the ClosingRequest event
            this.OnClosingRequest();

            if (Properties.Settings.Default.ShowSaveSuccessMessageBoxes)
                MessageBox.Show(@"Auftrag wurde erfolgreich gespeichert!");
        }

        private void AddMessengerValueIfNotExists(string value)
        {
            if (this.MessengerValues.Contains(value) == false)
                this.MessengerValues.Add(value);
        }
    }
}