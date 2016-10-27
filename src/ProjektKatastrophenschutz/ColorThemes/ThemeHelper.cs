using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ProjektKatastrophenschutz.ColorThemes
{
    public class ThemeHelper
    {
        public static SolidColorBrush ConvertColorToSolidColorBrush(System.Drawing.Color color)
        {
            var result = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            return result;
        }

        public static ColorTheme LoadFromFile(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var theme = JsonConvert.DeserializeObject<ColorTheme>(json);

                return theme;
            }
            catch (Exception exc)
            {
                MessageBox.Show(@"Kann Theme nicht laden. Grund: " + exc.Message);
            }

            return null;
        }

        public static void SaveToFile(ColorTheme theme, string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(theme, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception exc)
            {
                MessageBox.Show(@"Kann Theme nicht abspeichern. Grund: " + exc.Message);
            }
        }
    }
}
