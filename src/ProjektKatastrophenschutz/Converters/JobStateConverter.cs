using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Utils;

namespace ProjektKatastrophenschutz.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using BSA.Core.Models;

    public class JobStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertingUtils.ConvertJobStateToLocalizedString(value, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}