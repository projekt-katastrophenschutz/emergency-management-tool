using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjektKatastrophenschutz.ColorThemes;
// ReSharper disable LoopCanBeConvertedToQuery

namespace ProjektKatastrophenschutz.Converters
{
    /// <summary>
    ///     Converter for the ColorTheme class
    ///     We need this converter to store the SolidColorBrush color values
    ///     values of the ColorTheme into user settings (as hex string values).
    /// 
    ///     <see cref="ColorTheme"/>
    ///     <see cref="SolidColorBrush"/>
    /// </summary>
    public class SolidBrushHexStringConverter : TypeConverter
    {
        // Seperator for the hex string values
        private static char Seperator { get; } = ',';

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var theme = new ColorTheme();

            try
            {
                // Check if we can parse the value
                if (!(value is string))
                    return base.ConvertFrom(context, culture, value);

                // If we got an empty string (empty input), just return some default theme
                if (value.ToString() == string.Empty)
                    return ColorTheme.DeepSea();

                // Split the brushes string by the given seperator.
                // A brushes string looks like "#FF00CED1,#FFFF7F50,#FFFFFFFF,#FFFF8C00," (if the seperator is set to ',')
                // The first value is the respective value of the first property of the ColorTheme class,
                // The second value is the respective value of the second property of the ColorTheme class,
                // and so on...
                var brushesStrings = ((string)value).Split(Seperator);

                // Get properties of the ColorTheme class
                var brushesProperties = theme.GetType().GetProperties();

                // Iterate properties
                for (var i = 0; i < brushesProperties.Count(); i++)
                {
                    // Convert the hex string part to SolidColorBrush value
                    // Using the example string a few lines above, brushesStrings[2] would be "#FFFFFFFF"
                    var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(brushesStrings[i]));

                    // Set the respective property of the colorTheme class
                    brushesProperties[i].SetValue(theme, Convert.ChangeType(brush, brushesProperties[i].PropertyType), null);
                }
            }
            catch (Exception)
            {
                // If something bad happens when trying to parse & set the values, just return some default theme
                return ColorTheme.DeepSea();
            }
            
            return theme;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            var result = string.Empty;

            try
            {
                // Check if we can parse the value
                if (destinationType != typeof(string))
                    return base.ConvertTo(context, culture, value, destinationType);

                // Try to convert the value parameter to ColorTheme
                var theme = value as ColorTheme;
                if (theme == null)
                    return base.ConvertTo(context, culture, value, destinationType);

                // Get properties of the ColorTheme class
                var brushesProperties = theme.GetType().GetProperties();

                // Create hex string values from brushesProperties
                // The full hex string string looks like "#FF00CED1,#FFFF7F50,#FFFFFFFF,#FFFF8C00," (if the seperator is set to ',')
                // The first hex string is the SolidColorBrush value of the first property of the ColorTheme class,
                // The second hex string is the SolidColorBrush value of the second property of the ColorTheme class,
                // and so on...
                foreach (var brush in brushesProperties)
                    result += (brush.GetValue(theme) + Seperator.ToString());
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
            return result;
        }
    }
}
