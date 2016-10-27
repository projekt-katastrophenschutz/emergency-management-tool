// <copyright file="JobTypeToLocalizedStringConverter.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

using BSA.Core.Utils;

namespace ProjektKatastrophenschutz.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using BSA.Core.Models;

    public class JobTypeToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertingUtils.ConvertJobTypeToLocalizedString(value, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}