using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjektKatastrophenschutz.Converters
{
    public class DateToDefaultDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            // Convert the given parameter (the default value) to a DateTime object
            DateTime defaultDateTime;
            DateTime.TryParse(parameter.ToString(), out defaultDateTime);

            // If the date has no value (-> MinValue), take defaultDateTime instead
            if (date == DateTime.MinValue)
                return defaultDateTime;

            return date;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValueResult = value?.ToString() ?? string.Empty;
            
            // If the date field is blank, just return.
            if (string.IsNullOrEmpty(strValueResult))
                return null;

            DateTime resultDateTime, defaultDateTime;

            // convert parameter ("for example, "25.01.2020") to a string and try to parse it (this is the default value)
            DateTime.TryParse(parameter.ToString(), out defaultDateTime);

            // try to parse the selected date
            var result = DateTime.TryParse(strValueResult, out resultDateTime) ? resultDateTime : value;

            // if the selected date is equal to the given default value, return null value (-> no date was selected)
            // ToDo: Problem: If a person's BirthDate is REALLY the default date, it can't be saved... (a null value will be saved)
            if (resultDateTime == defaultDateTime)
                return null;

            return result;
        }
    }
}
