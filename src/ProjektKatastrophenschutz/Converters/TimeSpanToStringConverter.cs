// <copyright file="TimeSpanToStringConverter.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is TimeSpan == false)
            {
                return string.Empty;
            }

            var timeSpanValue = (TimeSpan) value;
            return new TimeSpan(timeSpanValue.Hours, timeSpanValue.Minutes, timeSpanValue.Seconds).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}