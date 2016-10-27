// <copyright file="ForceEditorView.xaml.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BSA.Core.Models;
    using ProjektKatastrophenschutz.Utils;
    using ProjektKatastrophenschutz.ViewModels;

    /// <summary>
    ///     Interaction logic for ForceEditorView.xaml
    /// </summary>
    public partial class ForceEditorView : UserControl
    {
        public ForceEditorView()
        {
            this.InitializeComponent();
            this.DataContextChanged += (sender, args) => this.RegisterInViewModel();
            this.Loaded += (sender, args) => this.RegisterInViewModel();
        }

        /// <summary> 
        /// Join Database
        /// </summary>
        public ForceEditorViewModel ViewModel => this.DataContext as ForceEditorViewModel;
        
        /// <summary>
        /// Viewmodelcheck
        /// </summary>
        private void RegisterInViewModel()
        {
            var forceEditorViewModel = this.ViewModel;
            if (forceEditorViewModel != null)
            {
                forceEditorViewModel.View = this;
            }

            //ViewModel.LoadForcesJob();
        }

        /// <summary>
        /// Person Datacontent on double Click
        /// </summary>
        private void PersonsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            // If the DataGrid is editable, comment out all lines of this method

            var person = Gui.GetObjectOfClickedDataGridRow<Person>(sender, eventArgs);
            if (person != null)
            {
                this.ViewModel.EditPerson(person);
            }
        }

        /// <summary>
        /// Person Datacontent on Right Click
        /// </summary>
        private void PersonsDataGrid_OnMouseRightButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var person = Gui.GetObjectOfClickedDataGridRow<Person>(sender, eventArgs);
            if (person == null)
            {
                return;
            }

            var contextMenu = new ContextMenu();

            var editItem = new MenuItem {Header = "Editor für diese Person öffnen"};
            editItem.Click += this.EditPersonContextMenuItem_OnClick;
            var deleteItem = new MenuItem {Header = "Person aus Liste löschen"};
            deleteItem.Click += this.DeletePersonContextMenuItem_OnClick;
            var setAsLeaderItem = new MenuItem {Header = "Als Gruppenführer festlegen"};
            setAsLeaderItem.Click += this.SetAsLeaderContextMenuItem_OnClick;

            contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(setAsLeaderItem);

            contextMenu.IsOpen = true;
        }

        /// <summary>
        /// PersonEdit Menueitem  on  Click
        /// </summary>
        private void EditPersonContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var person = this.ViewModel.SelectedPerson;
            if (person != null)
            {
                this.ViewModel.EditPerson(person);
            }
        }

        /// <summary>
        /// Delete Person  on  Click
        /// </summary>
        private void DeletePersonContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var person = this.ViewModel.SelectedPerson;
            if (person != null)
            {
                this.ViewModel.DeletePerson(person);
            }
        }

        /// <summary>
        /// Set Person as Leader on Click
        /// </summary>
        private void SetAsLeaderContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var person = this.ViewModel.SelectedPerson;
            if (person != null)
            {
                this.ViewModel.SetPersonAsLeader(person);
            }
        }

        /// <summary>
        /// Leaderbutton Datacontent on Click
        /// </summary>
        private void ToggleLeaderButton_OnClick(object sender, RoutedEventArgs eventArgs)
        {
            var person = Gui.GetObjectOfClickedControl<Button, Person>(sender);
            if (person != null)
            {
                this.ViewModel.ToggleLeader(person);
            }
        }

        //}
        //    CsvExport.csvPersonWriter(list, force );
        //    Force force = this.ViewModel.Force;
        //    var list = persons.Cast<Person>();
        //    var persons = this.ViewModel.PersonsView;
        //{

        //private void CreatePrinterForceTextBlock_MouseLeftButtonUp(object sender, RoutedEventArgs e)
    }
}