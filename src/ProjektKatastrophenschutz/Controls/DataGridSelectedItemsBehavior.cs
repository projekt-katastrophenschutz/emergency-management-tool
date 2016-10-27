using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using BSA.Core.Models;

namespace ProjektKatastrophenschutz.Controls
{
    public class DataGridSelectedItemsBlendBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<object>),
            typeof(DataGridSelectedItemsBlendBehavior),
            new FrameworkPropertyMetadata(null)
            {
                BindsTwoWayByDefault = true
            });

        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return (ObservableCollection<object>)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += OnSelectionChanged;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItems != null)
            {
                var selectedItems = this.SelectedItems.ToList();
                foreach (var obj in selectedItems)
                {
                    var rowContainer = this.AssociatedObject.ItemContainerGenerator.ContainerFromItem(obj) as DataGridRow;
                    if (rowContainer != null)
                    {
                        rowContainer.IsSelected = true;
                    }
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectionChanged -= OnSelectionChanged;
                this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0 && this.SelectedItems != null)
            {
                foreach (var obj in e.AddedItems)
                    this.SelectedItems.Add(obj);
            }

            if (e.RemovedItems != null && e.RemovedItems.Count > 0 && this.SelectedItems != null)
            {
                foreach (var obj in e.RemovedItems)
                    this.SelectedItems.Remove(obj);
            }
        }
    }
}
