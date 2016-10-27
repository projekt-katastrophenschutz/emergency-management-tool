using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektKatastrophenschutz.Utils
{
    public static class Gui
    {
        /// <summary>
        /// Gets the object of the ListView item a user clicked a control of
        /// </summary>
        /// <typeparam name="T1">The type of the sender (for example, Button, TextBox, ...)</typeparam>
        /// <typeparam name="T2">The type of the object the ListView contains items with</typeparam>
        /// <param name="sender"></param>
        /// <returns>The object of the ListView item the control belongs to</returns>
        public static T2 GetObjectOfClickedControl<T1, T2>(object sender)
            where T1 : Control
        {
            var element = (T1)sender;
            var obj = (T2)element?.DataContext;
            return obj;
        }

        public static T GetObjectOfClickedDataGridRow<T>(object sender, MouseEventArgs eventArgs)
        {
            var inputElement = eventArgs.MouseDevice.DirectlyOver;
            var frameworkElement = inputElement as FrameworkElement;
            var parent = frameworkElement?.Parent;

            if (parent is DataGridCell || frameworkElement is TextBlock || frameworkElement is Border)
            {
                var grid = sender as DataGrid;
                if (grid?.SelectedItems == null || grid.SelectedItems.Count < 1)
                    return default(T);

                var obj = (T)grid.SelectedItem;
                return obj;
            }

            return default(T);
        }
    }
}
