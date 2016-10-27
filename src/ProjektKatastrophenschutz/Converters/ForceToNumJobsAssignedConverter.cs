using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BSA.Core.Models;
using ProjektKatastrophenschutz.Views;

namespace ProjektKatastrophenschutz.Converters
{
    class ForceToNumJobsAssignedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var force = values[0] as Force;
            var viewModel = (values[1] as ForceToJob)?.ViewModel;

            if (viewModel == null)
                return false;

            var count = viewModel.GetNumJobsForForce(force);
            return count.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // ...
            // (should never be called)
            throw new NotSupportedException("This method should never be called");
        }
    }

}
