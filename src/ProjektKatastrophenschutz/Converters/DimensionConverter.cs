using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Convert;

namespace ProjektKatastrophenschutz.Converters
{
    public class DimensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
              object parameter, CultureInfo culture)
        {
            var result = ToDouble(value) * double.Parse(parameter.ToString(), CultureInfo.InvariantCulture);
            return result;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
