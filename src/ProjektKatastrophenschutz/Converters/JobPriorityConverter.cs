

using System;
using System.Globalization;
using System.Windows.Data;
using BSA.Core.Utils;

namespace ProjektKatastrophenschutz.Converters
{
    public class JobPriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertingUtils.ConvertJobPriorityToLocalizedString(value, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}