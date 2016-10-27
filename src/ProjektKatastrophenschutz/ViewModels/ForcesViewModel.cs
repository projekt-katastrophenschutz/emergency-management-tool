// <copyright file="ForcesViewModel.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Collections;
using BSA.Core;
using BSA.Database.SampleData;

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
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Services;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.Views;
    using GalaSoft.MvvmLight.Threading;
    public class ForcesViewModel : ViewModelBase, IShellCommandBar
    {
        private readonly ObservableCollection<Force> forces;
        private readonly IForcesService forceService;
        private readonly GroupFilter groupFilter;

        private readonly IJobService jobService;
        private readonly IShellService shellService;
        private RelayCommand leaderFilterCommand;
        private string leaderNameFilterText;
        private RelayCommand nameFilterCommand;
        private RelayCommand exportAllCommand;
        private RelayCommand selectAllVisibleForcesCommand;
        private RelayCommand exportSelectedForcesCommand;
        private RelayCommand<IList> selectionChangedCommand;
        private IEnumerable<Force> selectedForces;

        private string nameFilterText;
        private RelayCommand notesFilterCommand;
        private string notesFilterText;
        private NumberRangeFilter personsCountFilter;

        private RelayCommand personsCountFilterCommand;
        private RelayCommand printToFileCommand;
        private RelayCommand radioNameFilterCommand;
        private string radioNameFilterText;
        private Force selectedForce;
        private RelayCommand vehicleFilterCommand;
        private string vehicleFilterText;

        public ForcesViewModel(IForcesService forceService, IShellService shellService, IJobService jobService)
        {
            this.forceService = forceService;
            this.shellService = shellService;
            this.jobService = jobService;

            this.forces = new ObservableCollection<Force>();

            this.ForcesView = new ListCollectionView(this.forces);
            this.groupFilter = new GroupFilter();
            this.ForcesView.Filter = this.groupFilter.Filter;
            this.personsCountFilter = new NumberRangeFilter();

            // Register messages
            Messenger.Default.Register<ForceAddedMessage>(this, this.OnForceAdded);
            Messenger.Default.Register<ForceUpdatedMessage>(this, this.OnForceUpdated);
            Messenger.Default.Register<ForceDeletedMessage>(this, this.OnForceDeleted);

            // Exposing command bar items
            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                // Create force button
                new ShellCommandBarButton
                {
                    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/plus_math.scale-150.png"),
                    Label = "Einheit anlegen",
                    Width = 284,
                    Command = new RelayCommand(() =>
                    {
                        var mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
                        // Create force tab
                        mainViewModel.SelectedTabIndex = 5;
                    })
                },
                new ShellCommandBarSeparator(),

                // Hide / unhide filters
                //new ShellCommandBarButton
                //{
                //    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/invisible.scale-150.png"),
                //    Label = "Filter ein/ausblenden",
                //    Command = new RelayCommand(() => this.View.ToggleFilterVisibility())
                //},

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
                            Label = "Alle Einheiten exportieren",
                            Command = this.ExportAllCommand
                        },

                         // Select all
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/select_all.scale-150.png"),
                            Label = "Alle sichtbaren Einheiten markieren",
                            Command = this.SelectAllForcesCommand
                        },

                        // Export selected
                        new ShellCommandBarButton
                        {
                            ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/export_blue.scale-150.png"),
                            Label = "Alle markierten Einheiten exportieren",
                            Command = this.ExportSelectedForcesCommand
                        },
                    },
                    DropDownHorizontalOffset = -55.0
                },
            };
        }

        public ListCollectionView ForcesView { get; set; }

        public ForcesView View { get; set; }

        public Force SelectedForce
        {
            get { return this.selectedForce; }
            set { this.Set(ref this.selectedForce, value); }
        }

        public IEnumerable<Force> SelectedForces
        {
            get { return this.selectedForces; }
            set { this.Set(ref this.selectedForces, value); }
        }

        public RelayCommand PersonsCountFilterCommand => this.personsCountFilterCommand ??
                                                         (this.personsCountFilterCommand =
                                                             new RelayCommand(
                                                                 () => { this.ApplyFilter(this.PersonsCountFilter); }));

        public RelayCommand NameFilterCommand => this.nameFilterCommand ??
                                                 (this.nameFilterCommand =
                                                     new RelayCommand(() => { this.ApplyFilter(this.NameFilter); }));

        public RelayCommand RadioNameFilterCommand => this.radioNameFilterCommand ??
                                                      (this.radioNameFilterCommand =
                                                          new RelayCommand(
                                                              () => { this.ApplyFilter(this.RadionameFilter); }));

        public RelayCommand LeaderFilterCommand => this.leaderFilterCommand ??
                                                   (this.leaderFilterCommand =
                                                       new RelayCommand(
                                                           () => { this.ApplyFilter(this.LeaderNameFilter); }));

        public RelayCommand VehicleFilterCommand => this.vehicleFilterCommand ??
                                                    (this.vehicleFilterCommand =
                                                        new RelayCommand(() => { this.ApplyFilter(this.VehicleFilter); }))
            ;

        public RelayCommand NotesFilterCommand => this.notesFilterCommand ??
                                                  (this.notesFilterCommand =
                                                      new RelayCommand(() => { this.ApplyFilter(this.NotesFilter); }));

        public RelayCommand PrintToFileCommand => this.printToFileCommand ??
                                                  (this.printToFileCommand =
                                                      new RelayCommand(
                                                          () => { this.ExportForces(this.ForcesView.Cast<Force>()); }));

        public RelayCommand ExportAllCommand => this.exportAllCommand ??
                                                  (this.exportAllCommand =
                                                      new RelayCommand(
                                                          () => { this.ExportForces(this.forces); }));

        public RelayCommand SelectAllForcesCommand => this.selectAllVisibleForcesCommand ??
                                                  (this.selectAllVisibleForcesCommand =
                                                      new RelayCommand(
                                                          () =>
                                                          {
                                                              View.ForcesDataGrid.SelectedItems.Clear();
                                                              foreach (var force in this.ForcesView.Cast<Force>())
                                                                  View.ForcesDataGrid.SelectedItems.Add(force);
                                                          }));

        public RelayCommand ExportSelectedForcesCommand => this.exportSelectedForcesCommand ??
                                                  (this.exportSelectedForcesCommand =
                                                      new RelayCommand(
                                                          () => { this.ExportForces(this.SelectedForces); }));

        public RelayCommand<IList> SelectionChangedCommand => this.selectionChangedCommand ??
            (this.selectionChangedCommand = new RelayCommand<IList>
                (
                    items =>
                    {
                        SelectedForces = items?.Cast<Force>() ?? new List<Force>();
                    }
                 )
            );

        public NumberRangeFilter PersonsCountFilterValue
        {
            get { return this.personsCountFilter; }
            set
            {
                if (value == this.personsCountFilter)
                    return;

                this.personsCountFilter = value;
                this.RaisePropertyChanged();
            }
        }

        public string NameFilterText
        {
            get { return this.nameFilterText; }
            set
            {
                if (value == this.nameFilterText)
                    return;

                this.nameFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string RadionameFilterText
        {
            get { return this.radioNameFilterText; }
            set
            {
                if (value == this.radioNameFilterText)
                    return;

                this.radioNameFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string LeaderNameFilterText
        {
            get { return this.leaderNameFilterText; }
            set
            {
                if (value == this.leaderNameFilterText)
                    return;

                this.leaderNameFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string VehicleFilterText
        {
            get { return this.vehicleFilterText; }
            set
            {
                if (value == this.vehicleFilterText)
                    return;

                this.vehicleFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public string NotesFilterText
        {
            get { return this.notesFilterText; }
            set
            {
                if (value == this.notesFilterText)
                    return;

                this.notesFilterText = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; } = null;

        private void ApplyFilter(Predicate<object> filter)
        {
            this.groupFilter.AddFilter(filter);
            this.ForcesView.Refresh();
        }

        private void RemoveAllFilters()
        {
            this.NameFilterText = string.Empty;
            this.RadionameFilterText = string.Empty;
            this.LeaderNameFilterText = string.Empty;
            this.VehicleFilterText = string.Empty;
            this.NotesFilterText = string.Empty;
        }

        private bool PersonsCountFilter(object obj)
        {
            var force = obj as Force;
            if (force == null)
                return false;

            var personsCount = force.Persons.Count;

            if (personsCount >= this.PersonsCountFilterValue.Min
                && personsCount <= this.PersonsCountFilterValue.Max)
            {
                return true;
            }

            return false;
        }

        private bool NameFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.NameFilterText))
                return true;

            var force = obj as Force;
            if (string.IsNullOrEmpty(force?.Name))
                return false;

            return force.Name.ToLower().Contains(this.NameFilterText.ToLower());
        }

        private bool RadionameFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.RadionameFilterText))
                return true;

            var force = obj as Force;
            if (string.IsNullOrEmpty(force?.RadioName))
                return false;

            return force.RadioName.ToLower().Contains(this.RadionameFilterText.ToLower());
        }

        private bool LeaderNameFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.LeaderNameFilterText))
                return true;

            var force = obj as Force;
            if (force?.Leader == null)
                return false;

            return force.Leader.ToString().ToLower().Contains(this.LeaderNameFilterText.ToLower());
        }

        private bool VehicleFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.VehicleFilterText))
                return true;

            var force = obj as Force;
            if (string.IsNullOrEmpty(force?.Vehicle))
                return false;

            return force.Vehicle.ToLower().Contains(this.VehicleFilterText.ToLower());
        }

        private bool NotesFilter(object obj)
        {
            if (string.IsNullOrEmpty(this.NotesFilterText))
                return true;

            var force = obj as Force;
            if (string.IsNullOrEmpty(force?.Notes))
                return false;

            return force.Notes.ToLower().Contains(this.NotesFilterText.ToLower());
        }

        public void LoadData()
        {
            this.forces.Clear();
            this.forceService.GetAllForces().ForEach(
                force => this.forces.Add(force));
        }

        public void EditForce(Force force)
        {
            var forceEditorViewModel = new ForceEditorViewModel(this.shellService, this.forceService, force);
            this.shellService.ShowInNewTab("Force Editor", new ForceEditorView(), forceEditorViewModel, true);
        }

        public void DeleteForce(Force force)
        {
            this.forceService.DeleteForces(force);
            this.forces.Remove(force);
        }

        public void EditJobs(Force force)
        {
            var forceToJobViewModel = new JobToForceViewModel(this.shellService, this.forceService, this.jobService, force);
            this.shellService.ShowInNewTab("Einsatzaufträge zuweisen", new JobToForce(), forceToJobViewModel, true);
        }

        public void ExportForces(IEnumerable<Force> forcesCollection)
        {
            var path = FileUtils.GetFileFromSaveFileDialog(KnownFolders.GetPrintFolder(), @"Einheiten.csv");
            if (path == null)
                return;

            // Save the forces to a specified filepath as a csv file
            var csvForcesList = Mapper.Map<IEnumerable<Force>, IEnumerable<CsvForce>>(forcesCollection);
            PrintHelper.SaveToCsv<ForceCsvMap>(csvForcesList, path, true, ";");
        }
        
        public async void CreateDummyData()
        {
            var randomWords = await DummyDataHelper.GetRandomWords(2000);
            var dummyDataCount = 100;
            var random = new Random();

            for (var i = 0; i < dummyDataCount; i++)
            {
                var persons = new ObservableCollection<Person>();
                var personCount = await DummyDataHelper.GetRandomNumber(1, 50, random);

                for (var ii = 0; ii < personCount; ii++)
                {
                    var day = await DummyDataHelper.GetRandomNumber(1, 28, random);
                    var month = await DummyDataHelper.GetRandomNumber(1, 12, random);
                    var year = await DummyDataHelper.GetRandomNumber(1930, 2005, random);

                    var hour = await DummyDataHelper.GetRandomNumber(0, 23, random);
                    var minute = await DummyDataHelper.GetRandomNumber(0, 59, random);
                    var second = await DummyDataHelper.GetRandomNumber(0, 59, random);

                    var date = new DateTime(year, month, day, hour, minute, second);

                    var person = new Person
                    {
                        Prename = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 1, random),
                        Surname = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 1, random),
                        RadioCallName = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 3, random),
                        BirthDate = date,
                        Additional = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 10, random),
                        Street = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 5, random),
                        ZipCode = (await DummyDataHelper.GetRandomNumber(111111, 999999, random)).ToString(),
                        Employer = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 3, random),
                        PhoneNumber = (await DummyDataHelper.GetRandomNumber(10000, 1000000, random)).ToString(),
                        RelativesDetails = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 20, random),
                    };
                    persons.Add(person);
                }

                var force = new Force
                {
                    Name = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 3, random),
                    RadioName = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 3, random),
                    Leader = persons[await DummyDataHelper.GetRandomNumber(0, persons.Count, random)],
                    Persons = persons,
                    Vehicle = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 5, random),
                    Notes = await DummyDataHelper.GetRandomWordsFromList(randomWords, 1, 10, random)
                };

                force.History.Add(force.IsNew
                    ? new HistoryItem(BsaContext.GetUserName(), $"Neue Einheit angelegt: {force.Name}")
                    : new HistoryItem(BsaContext.GetUserName(), $"Einheit ({force.Name}) modifiziert"));
                this.forceService.SaveForces(force);
            }

            //var sniper = new Person {Prename = "Chris", Surname = "Kyle", RadioCallName = "Peacemaker"};
            //var diver = new Person {Prename = "Colonel", Surname = "Sanders", RadioCallName = "The chicken"};
            //var cook = new Person {Prename = "Old", Surname = "McDonald", RadioCallName = "Mealmaker"};

            //var force = new Force
            //{
            //    Name = "US Navy",
            //    RadioName = "The Sharks",
            //    Leader = sniper,
            //    Persons = new ObservableCollection<Person>{sniper, cook, diver},
            //    Vehicle = "Submarine",
            //    Notes = "Incompetent"
            //};

            //var force2Leader = new Person {Prename = "Marvin", Surname = "Reiter", RadioCallName = "Mj"};
            //var force2 = new Force
            //{
            //    Name = "Mj-petworld",
            //    RadioName = "The petguy",
            //    Leader = force2Leader,
            //    Persons = new ObservableCollection<Person> { force2Leader},
            //    Vehicle = "Chopper",
            //    Notes = "Competent"
            //};

            //this.forces.Add(force);
            //this.forces.Add(force2);

            //this.forceService.SaveForces(new Force(SampleForces.SampleForce1));
            //this.forceService.SaveForces(new Force(SampleForces.SampleForce2));
            //this.forceService.SaveForces(new Force(SampleForces.SampleForce3));
        }

        private void OnForceAdded(ForceAddedMessage forceAddedMessage)
        {
            DispatcherHelper.RunAsync(() => this.forces.Add(forceAddedMessage.Force));
        }

        private void OnForceUpdated(ForceUpdatedMessage forceUpdatedMessage)
        {
            DispatcherHelper.RunAsync(() =>
            {
                this.RemoveForceIfExists(forceUpdatedMessage.Force.Id);
                this.forces.Add(forceUpdatedMessage.Force);
            });
        }

        private void OnForceDeleted(ForceDeletedMessage forceDeletedMessage)
        {
            DispatcherHelper.RunAsync(() => this.RemoveForceIfExists(forceDeletedMessage.Force.Id));
        }

        private void RemoveForceIfExists(Guid forceId)
        {
            var existingForce = this.forces.SingleOrDefault(force => force.Id == forceId);
            if (existingForce != null)
            {
                this.forces.Remove(existingForce);
            }
        }
    }
}