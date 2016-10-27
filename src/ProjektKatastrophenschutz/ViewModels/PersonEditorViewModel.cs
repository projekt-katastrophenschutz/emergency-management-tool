// <copyright file="PersonEditorViewModel.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using BSA.Core.Models;
    using BSA.Core.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Ioc;
    using ProjektKatastrophenschutz.Elements;
    using ProjektKatastrophenschutz.Properties;

    public class PersonEditorViewModel : ViewModelBase, IClosable, IShellCommandBar
    {
        private readonly IForcesService forceService;
        private RelayCommand cancelPersonEditCommand;
        private bool isSaveRequested;

        private Person person;
        private RelayCommand savePersonCommand;
        private RelayCommand windowClosingCommand;

        [PreferredConstructor]
        public PersonEditorViewModel(IForcesService forceService = null, Person person = null)
        {
            this.forceService = forceService;
            this.Person = person ?? new Person();
            this.BeginPersonEdit(this.Person);
            this.isSaveRequested = false;
            this.IntializeCommandBar();
        }

        public PersonEditorViewModel(IForcesService forceService)
        {
            this.Person = new Person();
            this.IntializeCommandBar();
        }

        public RelayCommand CloseWindowCommand => this.windowClosingCommand ??
                                                  (this.windowClosingCommand = new RelayCommand(this.OnClosingRequest));

        public RelayCommand SavePersonCommand => this.savePersonCommand ??
                                                 (this.savePersonCommand = new RelayCommand(this.SavePersonExecute));

        public RelayCommand CancelPersonEditCommand => this.cancelPersonEditCommand ??
                                                       (this.cancelPersonEditCommand =
                                                           new RelayCommand(this.CancelPersonEditExecute));

        public Person Person
        {
            get { return this.person; }
            set { this.Set(ref this.person, value); }
        }

        public event EventHandler ClosingRequest;

        public ObservableCollection<ShellCommandBarItem> CommandBarItems { get; private set; }

        public ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; } = null;

        // ReSharper disable once ParameterHidesMember
        public void BeginPersonEdit(Person person)
        {
            this.Person = person;
            this.Person.BeginEdit();
        }

        private void WindowClosingExecute()
        {
            if (!this.isSaveRequested)
                this.Person.CancelEdit();
        }

        protected void OnClosingRequest()
        {
            // Cancel personb edit, if no save was requested
            this.WindowClosingExecute();
            // Close window. 
            // The call has no effect if this ViewModel is not being used by a window that subscribed to the ClosingRequest event.
            this.ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        private void SaveToDatabase()
        {
            //// Hier muss das Objekt abgespeichert werden.
        }

        private void CancelPersonEditExecute()
        {
            this.Person.CancelEdit();
            this.OnClosingRequest();
        }

        private void SavePersonExecute()
        {
            this.isSaveRequested = true;

            // End edit and save to Database
            this.Person.EndEdit();
            this.SaveToDatabase();

            // Begin new PersonEdit, for "Create Person" - View.
            // That means: All information (TextBoxes etc.) of the form will be cleared, if user clicks the "Save" button
            this.BeginPersonEdit(new Person());

            // Invoke close request, so the window closes if the view is being used in a extra window and the window
            // subscribed to the ClosingRequest event
            this.OnClosingRequest();

            if (Settings.Default.ShowSaveSuccessMessageBoxes)
                MessageBox.Show(@"Person wurde erfolgreich gespeichert!");
        }

        private void IntializeCommandBar()
        {
            this.CommandBarItems = new ObservableCollection<ShellCommandBarItem>
            {
                new ShellCommandBarButton
                {
                    ImageUri = new Uri("pack://application:,,/Resources/Images/ShellIcons/save_green.scale-150.png"),
                    Label = "Speichern und schließen",
                    Command = this.SavePersonCommand
                },
                new ShellCommandBarButton
                {
                    ImageUri =
                        new Uri("pack://application:,,/Resources/Images/ShellIcons/delete_sign_red.scale-150.png"),
                    Label = "Abbrechen und schließen",
                    Command = this.CancelPersonEditCommand
                }
            };
        }
    }
}