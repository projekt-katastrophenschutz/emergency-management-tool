using ProjektKatastrophenschutz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BSA.Core.Models;
using ProjektKatastrophenschutz.Utils;

namespace ProjektKatastrophenschutz.Views
{
    /// <summary>
    /// Interaktionslogik für AvailableForcesView.xaml
    /// </summary>
    public partial class ForcesView : UserControl 
    {
        private bool filterVisible = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public ForcesView()
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
        public ForcesViewModel ViewModel => this.DataContext as ForcesViewModel;

        /// <summary>
        /// Filter Datacontent 
        /// </summary>
        public void ToggleFilterVisibility()
        {
            var visibility = Visibility.Visible;
            if (this.filterVisible)
            {
                visibility = Visibility.Collapsed;
            }

            this.FilterBar.Visibility = visibility;
            //this.CreateDummyDataTextBlock.Visibility = visibility;

            this.filterVisible = !this.filterVisible;
        }

        /// <summary>
        /// Switch Register in Viewmodel 
        /// </summary>
        private void TryRegisterViewInViewModel()
        {
            var forcesViewModel = this.ViewModel;
            if (forcesViewModel != null)
            {
                forcesViewModel.View = this;
            }
        }

        /// <summary>
        /// Load Force  Datacontent 
        /// </summary>
        private void ForcesView_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LoadData();

        }

        /// <summary>
        /// Create Dummy Datacontent on Left Click
        /// </summary>
        private void CreateDummyDataTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ViewModel.CreateDummyData();
        }

        /// <summary>
        /// Create Force Datacontent on Double Click
        /// </summary>
        private void ForcesDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            var force = Gui.GetObjectOfClickedDataGridRow<Force>(sender, eventArgs);
            if (force != null)
            {
                ViewModel.EditForce(force);
            }
        }

        /// <summary>
        /// Bind Items with Datacontent 
        /// </summary>
        private void AddItems(ItemsControl contextMenu, IEnumerable<Force> forces)
        {
            var selectedForcesCount = forces.ToList().Count();

            var editItem = new MenuItem { Header = "Editieren" };
            editItem.Click += this.EditForceContextMenuItem_OnClick;
            var deleteItem = new MenuItem { Header = "Löschen" };
            deleteItem.Click += this.DeleteForceContextMenuItem_OnClick;
            var exportItem = new MenuItem { Header = "Exportieren" };
            exportItem.Click += this.ExportContextMenuItem_OnClick;
            var editJobsItem = new MenuItem { Header = "Einsatzaufträge zuweisen" };
            editJobsItem.Click += this.EditJobsContextMenuItem_OnClick;

            if (!(selectedForcesCount > 1))
                contextMenu.Items.Add(editItem);

            //contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);
            contextMenu.Items.Add(exportItem);
            //contextMenu.Items.Add(editJobsItem);

            if (!(selectedForcesCount > 1))
                contextMenu.Items.Add(editJobsItem);
        }

        private void ForcesDataGrid_OnMouseRightButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            // If the user clicked somewhere at the DataGrid but on no Job element, just return
            // Otherwise, show context menu.
            var force = Gui.GetObjectOfClickedDataGridRow<Force>(sender, eventArgs);
            if (force == null)
            {
                return;
            }

            var contextMenu = new ContextMenu();
            AddItems(contextMenu, this.ViewModel.SelectedForces);
            contextMenu.IsOpen = true;
        }

        /// <summary>
        /// EditJob Datacontent on Click
        /// </summary>
        private void EditJobsContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var force = this.ViewModel.SelectedForce;
            
            if (force != null)
                ViewModel.EditJobs(force);
        }

        /// <summary>
        /// EditForce Datacontent on Click
        /// </summary>
        private void EditForceContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var force = this.ViewModel.SelectedForce;
            if (force != null)
                ViewModel.EditForce(force);
        }

        /// <summary>
        /// Delete Force Datacontent on Click
        /// </summary>
        private void DeleteForceContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var forces = new List<Force>(this.ViewModel.SelectedForces);

            foreach (var force in forces)
                this.ViewModel.DeleteForce(force);
        }

        /// <summary>
        /// Export Context Content on Click
        /// </summary>
        private void ExportContextMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var forces = new List<Force>(this.ViewModel.SelectedForces);
            this.ViewModel.ExportForces(forces);
        }
    }
}

