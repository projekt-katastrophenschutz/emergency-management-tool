using System.ComponentModel;
using System.Configuration;
using System.Windows.Media;
using ProjektKatastrophenschutz.Converters;
using Color = System.Drawing.Color;

namespace ProjektKatastrophenschutz.ColorThemes
{

    /// <summary>
    ///     Color theme class
    ///     ------------------------------------------------------------------
    ///     WARNING: Do NOT add new properties at the top-most or in the middle of two other values!
    ///     Instead, add new properties after the bottom-most existing one.
    ///     If you add new properties anywhere else, loading themes that were created before adding the new properties will lead to a mess.
    ///     ------------------------------------------------------------------
    /// </summary>
    [TypeConverter(typeof(SolidBrushHexStringConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class ColorTheme
    {
        public SolidColorBrush TitleBarBackground { get; set; }

        public SolidColorBrush StartupViewBackground { get; set; }
        public SolidColorBrush StartupViewFormBackground { get; set; }

        public SolidColorBrush TabBarBackground { get; set; }
        public SolidColorBrush TabBarTabSelectedBackground { get; set; }
        public SolidColorBrush TabBarTabNotSelectedBackground { get; set; }
        public SolidColorBrush TabBarTabSelectedText { get; set; }
        public SolidColorBrush TabBarTabNotSelectedText { get; set; }
        public SolidColorBrush TabBarTabMouseoverBackground { get; set; }

        public SolidColorBrush CommandBarBackground { get; set; }
        public SolidColorBrush CommandBarButtonMouseoverBackground { get; set; }
        public SolidColorBrush CommandBarButtonClickedBackground { get; set; }

        public SolidColorBrush LeftBarBackground { get; set; }
        public SolidColorBrush LeftBarTabItemSelectedBackground { get; set; }
        public SolidColorBrush LeftBarTabItemMouseoverBackground { get; set; }

        public SolidColorBrush RightBarBackgroundColor { get; set; }

        public SolidColorBrush DataGridItemSelectedBackground { get; set; }
        public SolidColorBrush DataGridItemMouseoverBackground { get; set; }
        public SolidColorBrush DataGridBorderBrush { get; set; }

        public SolidColorBrush AssignmentArrowsBackground { get; set; }
        public SolidColorBrush SettingsTabExpanderBackground { get; set; }
        public SolidColorBrush SettingsTabExpanderBorderBrush { get; set; }

        public static ColorTheme DeepSea()
        {
            var theme = new ColorTheme
            {
                TitleBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x73, 0xC7)),

                StartupViewBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x73, 0xC7)),
                StartupViewFormBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),

                TabBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x73, 0xC7)),
                TabBarTabSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3)),
                TabBarTabNotSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00)), // transparent
                TabBarTabSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x5C, 0x9F)),
                TabBarTabNotSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),
                TabBarTabMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x2A, 0x8A, 0xD4)),

                CommandBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3)),
                CommandBarButtonClickedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xD2, 0xD2, 0xD2)),
                CommandBarButtonMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0xE6, 0xE6)),

                LeftBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA)),
                LeftBarTabItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xCD, 0xE6, 0xF7)),
                LeftBarTabItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0xF2, 0xFA)),

                RightBarBackgroundColor = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1)),

                DataGridItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x41, 0xB1, 0xE1)),
                DataGridItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xD8, 0xF0)),
                DataGridBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x73, 0xC7)),

                AssignmentArrowsBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x73, 0xC7)),

                SettingsTabExpanderBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
                SettingsTabExpanderBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
            };

            return theme;
        }

        public static ColorTheme PurplePassion()
        {
            var theme = new ColorTheme
            {
                TitleBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2)),

                StartupViewBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2)),
                StartupViewFormBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),

                TabBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2)),
                TabBarTabSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3)),
                TabBarTabNotSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00)), // transparent
                TabBarTabSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),
                TabBarTabNotSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),
                TabBarTabMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x93, 0x70, 0xDB)),

                CommandBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xEC, 0xEA, 0xED)),
                CommandBarButtonClickedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xD2, 0xD2, 0xD2)),
                CommandBarButtonMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x87, 0x7B, 0x68, 0xEE)),

                LeftBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xF2, 0xF1, 0xF6, 0xFA)),
                LeftBarTabItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xB9, 0x7B, 0x68, 0xEE)),
                LeftBarTabItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x58, 0x7B, 0x68, 0xEE)),

                RightBarBackgroundColor = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1)),

                DataGridItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xC1, 0x8A, 0x2B, 0xE2)),
                DataGridItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x2A, 0x8A, 0x2B, 0xE2)),
                DataGridBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2)),

                AssignmentArrowsBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2)),

                SettingsTabExpanderBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
                SettingsTabExpanderBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
            };

            return theme;
        }

        public static ColorTheme SummerBreeze()
        {
            var theme = new ColorTheme
            {
                TitleBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00)),

                StartupViewBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00)),
                StartupViewFormBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),

                TabBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00)),
                TabBarTabSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3)),
                TabBarTabNotSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00)), // transparent
                TabBarTabSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),
                TabBarTabNotSelectedText = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),
                TabBarTabMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0xA4, 0x60)),

                CommandBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA)),
                CommandBarButtonClickedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xD2, 0xD2, 0xD2)),
                CommandBarButtonMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xAA, 0xF4, 0xA4, 0x60)),

                LeftBarBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1)),
                LeftBarTabItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00)),
                LeftBarTabItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xAA, 0xF4, 0xA4, 0x60)),

                RightBarBackgroundColor = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1)),

                DataGridItemSelectedBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xB9, 0xFF, 0x8C, 0x00)),
                DataGridItemMouseoverBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0x7C, 0xF0, 0x8E, 0x17)),
                DataGridBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00)),

                AssignmentArrowsBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),

                SettingsTabExpanderBackground = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
                SettingsTabExpanderBorderBrush = ThemeHelper.ConvertColorToSolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF8, 0xFF)),
            };

            return theme;
        }
    }
}
