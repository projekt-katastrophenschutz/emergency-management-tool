// <copyright file="ForceEditorViewModel.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using System.Collections.Generic;
using ProjektKatastrophenschutz.Extensions;

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
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
    using Settings = ProjektKatastrophenschutz.Properties.Settings;

    public class ForceEditorViewModel : ViewModelBase, IClosable, IShellCommandBar, IShellViewStateAware
    {
        private readonly ShellCommandBarButton saveAndCloseCommandBarButton;
        private readonly ShellCommandBarButton saveAndNextCommandBarButton;

        private readonly ShellCommandBarButton showInTabCommandBarButton;
        private readonly ShellCommandBarButton showInWindowCommandBarButton;
        private readonly ShellCommandBarButton undockingCommandBarButton;
        private RelayCommand addPersonCommand;
        private RelayCommand cancelForceEditCommand;

        private Force force;
        private readonly IForcesService forceService;
        private bool isSaveRequested;
        private ObservableCollection<Person> persons;
        private ListCollectionView personsView;
        private RelayCommand saveForceCommand;
        private Person selectedPerson;
        private readonly IShellService shellService;
        private Window window;
        private RelayCommand windowClosingCommand;

        private List<Person> personsUnmodified;

        [PreferredConstructor]
        public ForceEditorViewModel(IShellService shellService, IForcesService forceService)
            : this(shellService, forceService, new Force())
        {
        }

        public ForceEditorViewModel(IShellService shellService, IForcesService forceService, Force force)
        {
            this.shellService = shellService;
            this.forceService = forceService;
            this.Force = force ?? new Force();

            this.BeginForceEdit(this.Force);
            //this.personsUnmodified = this.Force.Persons.Clone().ToList();
            //foreach (var p in this.Force.Persons)
            //    personsUnmodified.Add(p);

            //this.Persons = new ObservableCollection<Person>(this.Force.Persons);
            //this.PersonsView = new ListCollectionView(this.Force.Persons);


            this.ShellViewState = ShellViewState.Embedded;

            this.undockingCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/open_in_shell.scale-150.png"),
                Label = "Abdocken",
                Command = new RelayCommand(() =>
                {
                    this.View.DataContext = new ForceEditorViewModel(this.shellService, this.forceService);
                    var newView = new ForceEditorView {DataContext = this};
                    this.shellService.ShowInNewTab("Neue Einheit", newView, this, true);
                }),
                Width = 284,
                Visibility = this.Force.IsNew ? Visibility.Visible : Visibility.Collapsed
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
                Visibility = this.Force.IsNew ? Visibility.Collapsed : Visibility.Visible
            };

            this.saveAndCloseCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und schließen",
                Command = this.SaveForceCommand,
                Visibility = this.Force.IsNew ? Visibility.Collapsed : Visibility.Visible
            };

            this.saveAndNextCommandBarButton = new ShellCommandBarButton
            {
                ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                Label = "Speichern und neue Einheit anlegen",
                Command = this.SaveForceCommand,
                Visibility = this.Force.IsNew ? Visibility.Visible : Visibility.Collapsed
            };

            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                this.undockingCommandBarButton,
                this.showInTabCommandBarButton,
                this.showInWindowCommandBarButton,
                new ShellCommandBarSeparator(),
                this.saveAndCloseCommandBarButton,
                this.saveAndNextCommandBarButton,
                new ShellCommandBarButton
                {
                    ImageUri =
                        new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                    Label = "Abbrechen",
                    Command = this.CancelForceEditCommand
                }
            };
        }

        public ListCollectionView PersonsView
        {
            get { return this.personsView; }
            set { this.Set(ref this.personsView, value); }
        }

        public ForceEditorView View { get; set; }

        public RelayCommand CloseWindowCommand => this.windowClosingCommand ??
                                                  (this.windowClosingCommand = new RelayCommand(this.OnClosingRequest));

        public RelayCommand SaveForceCommand => this.saveForceCommand ??
                                                (this.saveForceCommand = new RelayCommand(this.SaveForceExecute));

        public RelayCommand CancelForceEditCommand => this.cancelForceEditCommand ??
                                                      (this.cancelForceEditCommand =
                                                          new RelayCommand(this.CancelPersonEditExecute));

        public RelayCommand AddPersonCommand => this.addPersonCommand ??
                                                (this.addPersonCommand = new RelayCommand(this.AddPerson));


        public Force Force
        {
            get { return this.force; }
            set { this.Set(ref this.force, value); }
        }

        //public ObservableCollection<Person> Persons
        //{
        //    get { return this.persons; }
        //    set { this.Set(ref this.persons, value); }
        //}

        public Person SelectedPerson
        {
            get { return this.selectedPerson; }
            set { this.Set(ref this.selectedPerson, value); }
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

            this.saveAndNextCommandBarButton.Visibility = shellViewState == ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
            this.saveAndCloseCommandBarButton.Visibility = shellViewState != ShellViewState.Embedded
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // ReSharper disable once ParameterHidesMember
        public void BeginForceEdit(Force force)
        {
            this.Force = force;

            if (this.Force.Leader == null)
            {
                var dummyLeader = new Person
                {
                    Prename = "Max",
                    Surname = "Mustermann",
                    BirthDate = new DateTime(1980, 12, 14),
                    Additional = "Jede Einheit hat standardmäßig einen Gruppenführer."
                };
                this.Force.Leader = dummyLeader;
                this.Force.Persons.Add(dummyLeader);
            }

            this.PersonsView = new ListCollectionView(this.Force.Persons);
            this.Force.BeginEdit();
        }

        public void AddPerson()
        {
            var person = new Person();
            this.Force.Persons.Add(person);
            this.EditPerson(person);
        }

        public void EditPerson(Person person)
        {
            var userControl = new PersonEditorView();
            var viewModel = new PersonEditorViewModel(this.forceService, person);
            this.shellService.ShowInNewWindow(@"Einsatzkraft-Editor", userControl, viewModel, 500, 500, true);
            //viewModel.ClosingRequest += (sender, e) => dialog.Close();
        }

        public void DeletePerson(Person person)
        {
            this.Force.Persons.Remove(person);
            if (this.IsPersonLeader(person))
            {
                this.Force.Leader = null;
            }
        }

        private void WindowClosingExecute()
        {
            if (!this.isSaveRequested)
            {
                this.Force.CancelEdit();
            }
        }

        protected void OnClosingRequest()
        {
            this.WindowClosingExecute();
            this.ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        private void SaveToDatabase()
        {
            this.Force.History.Add(this.force.IsNew
                ? new HistoryItem(BsaContext.GetUserName(), $"Neue Einheit angelegt: {this.Force.Name}")
                : new HistoryItem(BsaContext.GetUserName(), $"Einheit {this.Force.Name} modifiziert"));

            //this.AddPersonsHistoryItems();
            this.forceService.SaveForces(this.Force);    
        }

        private void AddPersonsHistoryItems()
        {
            foreach (var person in this.Force.Persons)
            {
                var unmodifiedPendant = this.personsUnmodified.FirstOrDefault(x => x.Id == person.Id);
                if (unmodifiedPendant == null)
                {
                    // the person doesn't seem to exist on the unmodified list. That means, this is a new person!
                    person.History.Add(new HistoryItem(BsaContext.GetUserName(),
                        $"Person {person.Prename} {person.Surname} in Einheit {this.Force.Name} erstellt"));
                    return;
                }

                var changes = Comparison.GetChanges(person, unmodifiedPendant);
                if (changes.Count > 0)
                {
                    person.History.Add(new HistoryItem(BsaContext.GetUserName(),
                        $"Person {unmodifiedPendant.Prename} {unmodifiedPendant.Surname} in Einheit {this.Force.Name} modifiziert"));
                }
            }
        }

        private void CancelPersonEditExecute()
        {
            this.Force.CancelEdit();
            this.OnClosingRequest();
        }

        private void SaveForceExecute()
        {
            this.isSaveRequested = true;

            // End edit and save to Database
            this.Force.EndEdit();
            this.SaveToDatabase();

            // Begin new ForceEdit, for "Create Force" - View.
            // That means: All information (TextBoxes etc.) of the form will be cleared, if user clicks the "Save" button
            this.BeginForceEdit(new Force());

            // Invoke close request, so the window closes if the view is being used in a extra window and the window
            // subscribed to the ClosingRequest event
            this.OnClosingRequest();

            if (Settings.Default.ShowSaveSuccessMessageBoxes)
                MessageBox.Show(@"Einheit wurde erfolgreich gespeichert!");
        }

        public void SetPersonAsLeader(Person person)
        {
            this.Force.Leader = person;
        }

        public bool IsPersonLeader(Person person)
        {
            if (this.Force.Leader == null)
            {
                return false;
            }

            return this.Force.Leader == person;
        }

        public void ToggleLeader(Person person)
        {
            if (this.Force.Leader == person)
            {
                this.Force.Leader = null;
                return;
            }

            this.Force.Leader = person;
        }
    }
}