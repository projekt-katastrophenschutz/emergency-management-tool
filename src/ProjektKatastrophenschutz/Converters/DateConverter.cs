using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjektKatastrophenschutz.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var date = (DateTime) value;

            if (date == DateTime.MinValue)
                return string.Empty;

            return ((DateTime)value);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value?.ToString() ?? string.Empty;
            DateTime resultDateTime;

            if (string.IsNullOrEmpty(strValue))
                return DateTime.MinValue;

            var result = DateTime.TryParse(strValue, out resultDateTime) ? resultDateTime : value;
            return result;
        }
    }
}
