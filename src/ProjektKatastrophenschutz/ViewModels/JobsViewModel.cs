// <copyright file="JobsViewModel.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Collections;
using BSA.Core;

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using AutoMapper;
    using BSA.Core.Messages;
    using BSA.Core.Models;
    using BSA.Core.Printing.Csv.Entities;
    using BSA.Core.Printing.Csv.Maps;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.Views;

    public class JobsViewModel : ViewModelBase, IShellCommandBar
    {
        private readonly IForcesService forceService;

        private readonly GroupFilter groupFilter;
        private readonly ObservableCollection<Job> jobs;
        private readonly IJobService jobService;
        private readonly IShellService shellService;
        private RelayCommand addressFilterCommand;
        private string addressFilterText;
        private RelayCommand dateFilterCommand;
        private DateTime? dateFilterValue;
        private RelayCommand descriptionFilterCommand;
        private string descriptionFilterText;

        private RelayCommand filterByEndedStateCommand;
        private RelayCommand filterByOpenOrInProgressCommand;
        private RelayCommand filterByInProgressStateCommand;
        private RelayCommand filterByOpenStateCommand;
        private RelayCommand exportAllCommand;
        private JobPriority? priorityFilterValue;
        private RelayCommand prioritytFilterCommand;
        private RelayCommand removeAllFiltersCommand;
        private RelayCommand resetDateFilterCommand;
        private RelayCommand selectAllJobsCommand;
        private RelayCommand exportSelectedJobsCommand;
        private RelayCommand toggleFilterBarVisibilityCommand;
        private RelayCommand<IList> selectionChangedCommand;
        private Job selectedJob;
        private IEnumerable<Job> selectedJobs;

        private JobState? stateFilterValue;
        private RelayCommand typeFilterCommand;
        private string typeFilterValue;

        private RelayCommand resetFilterCommand;
        private string currentFilterText;

        public JobsViewModel(IJobService jobService, IForcesService forceService, IShellService shellService)
        {
            this.jobService = jobService;
            this.forceService = forceService;
            this.shellService = shellService;
            this.jobs = new ObservableCollection<Job>();
            this.SelectedJobs = new List<Job>();
            this.JobsView = new ListCollectionView(this.jobs);
            this.groupFilter = new GroupFilter();

            this.JobsView.Filter = this.groupFilter.Filter;

            // Apply standard filter
            ApplyStandardFilter();


            // Register messages
            Messenger.Default.Register<JobAddedMessage>(this, this.OnJobAdded);
            Messenger.Default.Register<JobUpdatedMessage>(this, this.OnJobUpdated);
            Messenger.Default.Register<JobDeletedMessage>(this, this.OnJobDeleted);

            // Exposing command bar items
            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                // Create job button
                new ShellCommandBarButton
                {
                    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/plus_math.scale-150.png"),
                    Label = "Auftrag anlegen",
                    Width = 284,
                    Command = new RelayCommand(() =>
                    {
                        var mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
                        mainViewModel.SelectedTabIndex = 2;
                    })
                },

                new ShellCommandBarSeparator(),

                // Filter types
                new ShellCommandBarDropDownButton
                {
                    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/filter.scale-150.png"),
                    Label = "Filtern nach Auftragsstatus",
                    Items = new List<ShellCommandBarItem>()
                    {
                        // All jobs
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                            Label = "Alle Aufträge",
                            Command = this.ResetFilterCommand
                        },

                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/versions.scale-150.png"),
                            Label = "Neu oder in Bearbeitung",
                            Command = this.FilterByOpenOrInProgressCommand
                        },

                        // New jobs
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/add_list.scale-150.png"),
                            Label = "Neue Aufträge",
                            Command = this.FilterByOpenCommand
                        },

                        // Jobs in progress
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/edit_file.scale-150.png"),
                            Label = "In Bearbeitung",
                            Command = this.FilterByInProgressCommand
                        },

                        // Ended jobs
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/check_file.scale-150.png"),
                            Label = "Beendete Aufträge",
                            Command = this.FilterByEndedCommand
                        },

                        //new ShellCommandBarButton
                        //{
                        //    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/check_file.scale-150.png"),
                        //    Label = "Erweiterte Filter ein-/ausblenden",
                        //    Command = this.ToggleFilterBarVisibilityCommand
                        //},
                    },
                    DropDownHorizontalOffset = -55.0
                },

                // Hide / show filters
                ////new ShellCommandBarButton
                ////{
                ////    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/invisible.scale-150.png"),
                ////    Label = "Filter ein/ausblenden",
                ////    Command = new RelayCommand(() => this.View.ToggleFilterVisibility())
                ////},

                // Export dropdown menu
                new ShellCommandBarDropDownButton
                {
                    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save.scale-150.png"),
                    Label = "Export",
                    Items = new List<ShellCommandBarItem>()
                    {
                        // Export all
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/export.scale-150.png"),
                            Label = "Alle Aufträge exportieren",
                            Command = this.ExportAllCommand
                        },

                         // Select all
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/select_all.scale-150.png"),
                            Label = "Alle sichtbaren Aufträge markieren",
                            Command = this.SelectAllJobsCommand
                        },

                        // Export selected
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/export_blue.scale-150.png"),
                            Label = "Alle markierten Aufträge exportieren",
                            Command = this.ExportSelectedJobsCommand
                        },
                    },
                    DropDownHorizontalOffset = -55.0
                },
            };
        }

        public JobsView View { get; set; }

        public RelayCommand ResetFilterCommand => this.resetFilterCommand ??
                                                   (this.resetFilterCommand =
                                                       new RelayCommand(
                                                           () =>
                                                           {
                                                               this.CurrentFilterText = string.Empty;
                                                               this.StateFilterValue = null;
                                                               this.JobsView.Refresh();
                                                           }));

        public RelayCommand FilterByOpenOrInProgressCommand => this.filterByOpenOrInProgressCommand ??
                                                         (this.filterByOpenOrInProgressCommand =
                                                             new RelayCommand(
                                                                 ApplyStandardFilter));

        public RelayCommand FilterByOpenCommand => this.filterByOpenStateCommand ??
                                                   (this.filterByOpenStateCommand =
                                                       new RelayCommand(
                                                           () =>
                                                           {
                                                               this.CurrentFilterText =
                                                                   "Aktuell werden nur neue Aufträge angezeigt.";
                                                               this.ApplyStateFilter(JobState.NewCreated);
                                                           }));

        public RelayCommand FilterByInProgressCommand => this.filterByInProgressStateCommand ??
                                                         (this.filterByInProgressStateCommand =
                                                             new RelayCommand(
                                                                 () =>
                                                                 {
                                                                     this.CurrentFilterText =
                                                                         "Aktuell werden nur in Bearbeitung befindliche Aufträge angezeigt.";
                                                                     this.ApplyStateFilter(JobState.InProgress);
                                                                 }));

        public RelayCommand FilterByEndedCommand => this.filterByEndedStateCommand ??
                                                    (this.filterByEndedStateCommand =
                                                        new RelayCommand(
                                                            () =>
                                                            {
                                                                this.CurrentFilterText =
                                                                    "Aktuell werden nur beendete Aufträge angezeigt.";
                                                                this.ApplyStateFilter(JobState.Ended);
                                                            }));

        public RelayCommand DescriptionFilterChangedCommand => this.descriptionFilterCommand ??
                                                               (this.descriptionFilterCommand =
                                                                   new RelayCommand(
                                                                       () =>
                                                                       {
                                                                           this.ApplyFilter(this.DescriptionFilter);
                                                                       }));

        public RelayCommand AddressFilterChangedCommand => this.addressFilterCommand ??
                                                           (this.addressFilterCommand =
                                                               new RelayCommand(
                                                                   () => { this.ApplyFilter(this.AddressFilter); }));

        public RelayCommand TypeFilterChangedCommand => this.typeFilterCommand ??
                                                        (this.typeFilterCommand =
                                                            new RelayCommand(
                                                                () => { this.ApplyFilter(this.TypeFilter); }));

        public RelayCommand PriorityFilterCommand => this.prioritytFilterCommand ??
                                                     (this.prioritytFilterCommand =
                                                         new RelayCommand(
                                                             () => { this.ApplyFilter(this.PriorityFilter); }));

        public RelayCommand DateFilterCommand => this.dateFilterCommand ??
                                                 (this.dateFilterCommand =
                                                     new RelayCommand(() => { this.ApplyFilter(this.DateFilter); }));

        public RelayCommand ResetDateFilterCommand => this.resetDateFilterCommand ??
                                                      (this.resetDateFilterCommand =
                                                          new RelayCommand(() => { this.DateFilterValue = null; }));

        public RelayCommand RemoveAllFiltersCommand => this.removeAllFiltersCommand ??
                                                       (this.removeAllFiltersCommand =
                                                           new RelayCommand(this.RemoveAllFilters));

        public RelayCommand ExportAllCommand => this.exportAllCommand ??
                                                  (this.exportAllCommand =
                                                      new RelayCommand(
                                                          () => { this.ExportJobs(this.jobs); }));

        public RelayCommand SelectAllJobsCommand => this.selectAllJobsCommand ??
                                                  (this.selectAllJobsCommand =
                                                      new RelayCommand(
                                                          () =>
                                                          {
                                                              View.JobDataGrid.SelectedItems.Clear();
                                                              foreach (var job in this.JobsView.Cast<Job>())
                                                                  View.JobDataGrid.SelectedItems.Add(job);
                                                          }));

        public RelayCommand ExportSelectedJobsCommand => this.exportSelectedJobsCommand ??
                                                  (this.exportSelectedJobsCommand =
                                                      new RelayCommand(
                                                          () => { this.ExportJobs(this.SelectedJobs); }));

        public RelayCommand ToggleFilterBarVisibilityCommand => this.toggleFilterBarVisibilityCommand ??
                                                  (this.toggleFilterBarVisibilityCommand =
                                                      new RelayCommand(
                                                          () => { this.View.ToggleFilterVisibility(); }));

        public RelayCommand<IList> SelectionChangedCommand => this.selectionChangedCommand ??
            (this.selectionChangedCommand = new RelayCommand<IList>
                (
                    items =>
                    {
                        SelectedJobs = items?.Cast<Job>() ?? new List<Job>();
                    }
                 )
            );

        public ListCollectionView JobsView { get; set; }

        public Job SelectedJob
        {
            get { return this.selectedJob; }
            set { this.Set(ref this.selectedJob, value); }
        }

        public IEnumerable<Job> SelectedJobs
        {
            get { return this.selectedJobs; }
            set { this.Set(ref this.selectedJobs, value); }
        }

        public IEnumerable<JobType?> JobTypes
            => Enum.GetValues(typeof (JobType)).Cast<JobType?>();

        public IEnumerable<JobPriority?> JobPriorities
            => Enum.GetValues(typeof (JobPriority)).Cast<JobPriority?>();

        public string DescriptionFilterText
        {
            get { return this.descriptionFilterText; }
            set
            {
                if (value == this.descriptionFilterText)
                {
                    return;
                }

                this.descriptionFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string AddressFilterText
        {
            get { return this.addressFilterText; }
            set
            {
                if (value == this.addressFilterText)
                {
                    return;
                }

                this.addressFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string TypeFilterValue
        {
            get { return this.typeFilterValue; }
            set
            {
                if (value == this.typeFilterValue)
                {
                    return;
                }

                this.typeFilterValue = value;
                this.RaisePropertyChanged();
            }
        }

        public JobState? StateFilterValue
        {
            get { return this.stateFilterValue; }
            set
            {
                if (value == this.stateFilterValue)
                {
                    return;
                }

                this.stateFilterValue = value;
                this.RaisePropertyChanged();
            }
        }

        public JobPriority? PriorityFilterValue
        {
            get { return this.priorityFilterValue; }
            set
            {
                if (value == this.priorityFilterValue)
                {
                    return;
                }

                this.priorityFilterValue = value;
                this.RaisePropertyChanged();
            }
        }

        public DateTime? DateFilterValue
        {
            get { return this.dateFilterValue; }
            set
            {
                if (value == this.dateFilterValue)
                {
                    return;
                }

                this.dateFilterValue = value;
                this.RaisePropertyChanged();
            }
        }

        public string CurrentFilterText
        {
            get { return this.currentFilterText; }
            set { this.Set(ref this.currentFilterText, value); }
        }

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; } = null;

        private void ApplyFilter(Predicate<object> filter)
        {
            this.groupFilter.AddFilter(filter);
            this.JobsView.Refresh();
        }

        private void RemoveAllFilters()
        {
            this.DescriptionFilterText = string.Empty;
            this.AddressFilterText = string.Empty;
            this.TypeFilterValue = null;
            this.DateFilterValue = null;
            this.PriorityFilterValue = null;
        }

        private bool StateFilter(object obj)
        {
            if (this.StateFilterValue == null)
            {
                return true;
            }

            var job = obj as Job;
            if (job?.JobStatus == null)
            {
                return false;
            }
            
            var result = (job.JobStatus & this.StateFilterValue) != 0;
            return result;
        }

        private bool DescriptionFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.DescriptionFilterText))
            {
                return true;
            }

            var job = obj as Job;
            if (string.IsNullOrEmpty(job?.Description))
            {
                return false;
            }

            return job.Description.ToLower().Contains(this.DescriptionFilterText.ToLower());
        }

        private bool AddressFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.AddressFilterText))
            {
                return true;
            }

            var job = obj as Job;
            if (job?.Location == null)
            {
                return false;
            }

            return job.Location.ToString().ToLower().Contains(this.AddressFilterText.ToLower());
        }

        private bool TypeFilter(object obj)
        {
            if (string.IsNullOrEmpty(TypeFilterValue))
            {
                return true;
            }

            var job = obj as Job;
            if (job?.JobType == null)
            {
                return false;
            }

            return job.JobType.ToLower().Contains(this.TypeFilterValue.ToLower());
        }

        private bool PriorityFilter(object obj)
        {
            if (this.PriorityFilterValue == null)
            {
                return true;
            }

            var job = obj as Job;
            if (job?.JobPriority == null)
            {
                return false;
            }

            return job.JobPriority == this.PriorityFilterValue;
        }


        private bool DateFilter(object obj)
        {
            if (this.DateFilterValue == null)
            {
                return true;
            }

            var job = obj as Job;
            // ReSharper disable once UseNullPropagation
            if (job?.Date == null)
            {
                return false;
            }

            return job.Date.Date == this.DateFilterValue?.Date;
        }

        private void ApplyStateFilter(JobState state)
        {
            this.StateFilterValue = state;
            this.ApplyFilter(this.StateFilter);
            this.JobsView.Refresh();
        }

        private void ApplyStandardFilter()
        {
            this.ApplyStateFilter(JobState.NewCreated | JobState.InProgress);
            this.CurrentFilterText = "Aktuell werden nur offene oder in Bearbeitung befindliche Aufträge angezeigt.";
        }

        public void LoadData()
        {
            this.jobs.Clear();
            this.jobService.GetAllJobs().ForEach(
                job => this.jobs.Add(job));
        }

        public void EditJob(Job job)
        {
            var jobEditorViewModel = new JobEditorViewModel(this.shellService, this.jobService, this.forceService, job);
            this.shellService.ShowInNewTab("Job Editor", new JobEditorView(), jobEditorViewModel, true);
        }

        public void DeleteJob(Job job)
        {
            
            this.jobService.DeleteJob(job);
            this.jobs.Remove(job);
        }

        public void SetJobStatus(Job job, JobState state)
        {
            job.JobStatus = state;
            this.JobsView.Refresh();
        }

        public void EditForces(Job job)
        {
            var forceToJobViewModel = new ForceToJobViewModel(this.shellService, this.forceService, this.jobService, job);
            this.shellService.ShowInNewTab("Einheiten zuweisen", new ForceToJob(), forceToJobViewModel, true);
        }

        public async void CreateDummyData()
        {
            //this.jobService.SaveJob(new Job(BSA.Database.SampleData.SampleJobs.SampleJob1));
            //this.jobService.SaveJob(new Job(BSA.Database.SampleData.SampleJobs.SampleJob2));
            //this.jobService.SaveJob(new Job(BSA.Database.SampleData.SampleJobs.SampleJob3));
            //this.LoadData();

            var randomWords = await DummyDataHelper.GetRandomWords(2000);
            var dummyDataCount = 100;
            var random = new Random();

            for (var i = 0; i < dummyDataCount; i++)
            {
                var day = await DummyDataHelper.GetRandomNumber(1, 28, random);
                var month = await DummyDataHelper.GetRandomNumber(1, 12, random);
                var year = await DummyDataHelper.GetRandomNumber(1950, 2050, random);

                var hour = await DummyDataHelper.GetRandomNumber(0, 23, random);
                var minute = await DummyDataHelper.GetRandomNumber(0, 59, random);
                var second = await DummyDataHelper.GetRandomNumber(0, 59, random);

                var date = new DateTime(year, month, day, hour, minute, second);

                var job = new Job
                {
                    Name = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 5, random),
                    Date = date,
                    Messenger = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 2, random),
                    Organization = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 5, random),
                    Communicationtool = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 1, random),
                    Location = new JobLocation
                    {
                        Street = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 2, random),
                        HouseNumber = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 1, random),
                        ZipCode = (await DummyDataHelper.GetRandomNumber(111111, 999999, random)).ToString(),
                        City = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 1, random),
                        FloorLevel = (await DummyDataHelper.GetRandomNumber(0, 50, random)).ToString(),
                        AdditionalDescription = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 20, random)
                    },
                    JobType = DummyDataHelper.GetRandomEnumValue<JobType>(random).ToString(),
                    NumberInjuredPerson = await DummyDataHelper.GetRandomNumber(0, 250, random),
                    Description = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 20, random),
                    JobPriority = DummyDataHelper.GetRandomEnumValue<JobPriority>(random),
                    JobStatus = DummyDataHelper.GetRandomEnumValue<JobState>(random)
                };

                job.History.Add(job.IsNew
                    ? new HistoryItem(BsaContext.GetUserName(), $"Neuer Auftrag angelegt: {job.Description}")
                    : new HistoryItem(BsaContext.GetUserName(), $"Auftrag #1  ({job.Description}) modifiziert"));
                this.jobService.SaveJob(job);
            }


            //var job1 = new Job
            //{
            //    Name = "This is the job name",
            //    Date = DateTime.Now,
            //    Messenger = "Leitstelle",
            //    Organization = "Microsoft",
            //    Communicationtool = "Internet",
            //    Location = new JobLocation
            //    {
            //        Street = "Schaezlerstraße",
            //        HouseNumber = "30",
            //        ZipCode = "86152",
            //        City = "Augsburg",
            //        FloorLevel = "3",
            //        AdditionalDescription = "Kein Aufzug vorhanden!"
            //    },
            //    JobType = JobType.Fire.ToString(),
            //    NumberInjuredPerson = 3,
            //    Description = "Backofen wurde nicht ausgeschaltet",
            //    JobPriority = JobPriority.High,
            //    JobStatus = JobState.InProgress
            //};

            //var job2 = new Job
            //{
            //    Name = "This is the job name",
            //    Date = DateTime.Now,
            //    Messenger = "Random person",
            //    Organization = "HS AUgsburg",
            //    Communicationtool = "Brieftaube",
            //    Location = new JobLocation
            //    {
            //        Street = "Friedberger Straße",
            //        HouseNumber = "12",
            //        ZipCode = "86150",
            //        City = "Augsburg",
            //        FloorLevel = "3",
            //        AdditionalDescription = "Raum W3.18"
            //    },
            //    JobType = JobType.Flooding.ToString(),
            //    NumberInjuredPerson = 7,
            //    Description = "Wasserflasche ist umgefallen",
            //    JobPriority = JobPriority.Medium,
            //    JobStatus = JobState.NewCreated
            //};

            //this.jobs.Add(job1);
            //this.jobs.Add(job2);
        }

        public void ExportJobs(IEnumerable<Job> jobsCollection)
        {
            var path = FileUtils.GetFileFromSaveFileDialog(KnownFolders.GetPrintFolder(), @"Aufträge.csv");
            if (path == null)
            {
                return;
            }

            // Save the jobs to a specified filepath as a csv file
            var csvJobsList = Mapper.Map<IEnumerable<Job>, IEnumerable<CsvJob>>(jobsCollection);
            PrintHelper.SaveToCsv<JobCsvMap>(csvJobsList, path, true, ";");
        }

        private void OnJobAdded(JobAddedMessage jobAddedMessage)
        {
            DispatcherHelper.RunAsync(() => this.jobs.Add(jobAddedMessage.Job));
        }

        private void OnJobUpdated(JobUpdatedMessage jobUpdatedMessage)
        {
            DispatcherHelper.RunAsync(() =>
            {
                this.RemoveJobIfExists(jobUpdatedMessage.Job.Id);
                this.jobs.Add(jobUpdatedMessage.Job);
            });
        }

        private void OnJobDeleted(JobDeletedMessage jobDeletedMessage)
        {
            DispatcherHelper.RunAsync(() => this.RemoveJobIfExists(jobDeletedMessage.Job.Id));
        }

        private void RemoveJobIfExists(Guid jobId)
        {
            var existingJob = this.jobs.SingleOrDefault(job => job.Id == jobId);
            if (existingJob != null)
            {
                this.jobs.Remove(existingJob);
            }
        }
    }
}