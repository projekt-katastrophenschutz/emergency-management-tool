using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjektKatastrophenschutz.Converters
{
    public class ColorToSolidColorBrushValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            var result = brush?.Color;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)           
        {
            if (!(value is Color))
                return null;

            var color = (Color)value;
            var result = new SolidColorBrush(color);

            return result;       
        }
    }
}
