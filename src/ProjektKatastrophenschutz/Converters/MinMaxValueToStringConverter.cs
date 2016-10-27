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
    public class MinMaxValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint result;
            var success = uint.TryParse(value.ToString(), out result);

            // Can't parse value (= no number, just random stuff entered), or the result is the max value? 
            // return default value (null)
            if (!success)
                return null;

            // Result is min or max number? Just return
            var converterType = parameter?.ToString();
            if (converterType == "Min" && result == uint.MinValue)
                return null;
            if (converterType == "Max" && result == uint.MaxValue)
                return null;

            // Otherwise, Return parsed number
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();

            uint result;
            var success = uint.TryParse(stringValue, out result);

            // Can't parse value (= no number, just random stuff entered), or the result is the min/max value? 
            // return default value
            if (!success || string.IsNullOrEmpty(stringValue))
            {
                var converterType = parameter?.ToString();
                if (converterType == "Min")
                    return uint.MinValue;
                if (converterType == "Max")
                    return uint.MaxValue;
            }
                
            // Otherwise, return the number
            return result;
        }
    }
}
