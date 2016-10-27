using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BSA.Core.Models;
using ProjektKatastrophenschutz.ViewModels;
using ProjektKatastrophenschutz.Views;

namespace ProjektKatastrophenschutz.Converters
{
    public class PersonToForceLeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var person = values[0] as Person;
            var viewModel = (values[1] as ForceEditorView)?.ViewModel;

            if (viewModel == null)
                return false;
            
            var result = viewModel.Force.Leader == person;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // ...
            // (should never be called)
            throw new NotSupportedException("This method should never be called");
        }
    }
}
