using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjektKatastrophenschutz.ViewModels;
using BSA.Core.Network;
using ProjektKatastrophenschutz.ColorThemes;

namespace ProjektKatastrophenschutz.Views
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        public SettingsViewModel ViewModel => this.DataContext as SettingsViewModel;

        private void Settings_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LoadSettings();
            if (this.ViewModel.Role.Equals(ApplicationRole.Server)) {

                this.ViewModel.AcquireServerButton.Visibility = Visibility.Collapsed;
            }
        }


        ////private void ServerChange_Click(object sender, RoutedEventArgs e)
        ////{
        ////    if (ViewModel.Role.Equals(ApplicationRole.Client)){

        ////        ViewModel.ChangeToServer();

        ////    } else
        ////    {
        ////        return;

        ////    }


        ////}
        private void SetStandardColorsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.SelectedColorTheme = ColorTheme.DeepSea();
        }

        private void ColorThemeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ColorThemeComboBox.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.SelectedColorTheme = ColorTheme.DeepSea();
                    break;
                case 1:
                    Properties.Settings.Default.SelectedColorTheme = ColorTheme.SummerBreeze();
                    break;
                case 2:
                    Properties.Settings.Default.SelectedColorTheme = ColorTheme.PurplePassion();
                    break;
            }
        }

        private void LoadDeepSeaColorTheme()
        {
            // Standard values are set in Properties.Settings.Default
            // They are being used if the respective setting value is null
            // "Deep Sea" is the default color scheme
            //Properties.Settings.Default.TitleBarColorScheme = null;

            //Properties.Settings.Default.TabBarColorScheme = null;
            //Properties.Settings.Default.TabsForegroundColorScheme = null;
            //Properties.Settings.Default.TabsBackgroundColorScheme = null;
            //Properties.Settings.Default.TabsForegroundTextColorScheme = null;
            //Properties.Settings.Default.TabsNotForegroundTextColorScheme = null;
            //Properties.Settings.Default.TabsMouseoverColorScheme = null;

            //Properties.Settings.Default.StartupViewColorScheme = null;
            //Properties.Settings.Default.StartupViewFormBackgroundScheme = null;

            //Properties.Settings.Default.CommandBarColorScheme = null;
            //Properties.Settings.Default.CommandBarButtonMouseoverScheme = null;
            //Properties.Settings.Default.CommandBarButtonClickScheme = null;

            //Properties.Settings.Default.LeftBarBackgroundScheme = null;
            //Properties.Settings.Default.LeftBarTabItemSelectedScheme = null;
            //Properties.Settings.Default.LeftBarTabItemMouseoverScheme = null;

            //Properties.Settings.Default.RightBarBackgroundScheme = null;

            //Properties.Settings.Default.DataGridItemSelectedScheme = null;
            //Properties.Settings.Default.DataGridItemMouseoverScheme = null;
            //Properties.Settings.Default.DataGridItemBorderBrushScheme = null;

            //Properties.Settings.Default.AssignignArrowsColorScheme = null;
        }

        private void LoadSummerBreezeColorTheme()
        {
            //Properties.Settings.Default.TitleBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00));

            //Properties.Settings.Default.TabBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00));
            //Properties.Settings.Default.TabsForegroundColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3));
            //Properties.Settings.Default.TabsBackgroundColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            //Properties.Settings.Default.TabsForegroundTextColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
            //Properties.Settings.Default.TabsNotForegroundTextColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            //Properties.Settings.Default.TabsMouseoverColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF4, 0xA4, 0x60));

            //Properties.Settings.Default.StartupViewColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00));
            //Properties.Settings.Default.StartupViewFormBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

            //Properties.Settings.Default.CommandBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFA, 0xFA, 0xFA));
            //Properties.Settings.Default.CommandBarButtonMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xAD, 0xFF, 0x2F));
            //Properties.Settings.Default.CommandBarButtonClickScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xD2, 0xD2, 0xD2));

            //Properties.Settings.Default.LeftBarBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1));
            //Properties.Settings.Default.LeftBarTabItemSelectedScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00));
            //Properties.Settings.Default.LeftBarTabItemMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xAD, 0xFF, 0x2F));

            //Properties.Settings.Default.RightBarBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1));

            //Properties.Settings.Default.DataGridItemSelectedScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xB9, 0xFF, 0x8C, 0x00));
            //Properties.Settings.Default.DataGridItemMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x7C, 0xF0, 0x8E, 0x17));
            //Properties.Settings.Default.DataGridItemBorderBrushScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00));

            //Properties.Settings.Default.AssignignArrowsColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00, 0x00));

        }

        private void LoadDeepPurpleColorTheme()
        {

            //Properties.Settings.Default.TitleBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2));

            //Properties.Settings.Default.TabBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2));
            //Properties.Settings.Default.TabsForegroundColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3));
            //Properties.Settings.Default.TabsBackgroundColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            //Properties.Settings.Default.TabsForegroundTextColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
            //Properties.Settings.Default.TabsNotForegroundTextColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            //Properties.Settings.Default.TabsMouseoverColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x93, 0x70, 0xDB));

            //Properties.Settings.Default.StartupViewColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2));
            //Properties.Settings.Default.StartupViewFormBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

            //Properties.Settings.Default.CommandBarColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xEC, 0xEA, 0xED));
            //Properties.Settings.Default.CommandBarButtonMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x87, 0x7B, 0x68, 0xEE));
            //Properties.Settings.Default.CommandBarButtonClickScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xD2, 0xD2, 0xD2));

            //Properties.Settings.Default.LeftBarBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xF2, 0xF1, 0xF6, 0xFA));
            //Properties.Settings.Default.LeftBarTabItemSelectedScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xB9, 0x7B, 0x68, 0xEE));
            //Properties.Settings.Default.LeftBarTabItemMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x58, 0x7B, 0x68, 0xEE));

            //Properties.Settings.Default.RightBarBackgroundScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0xF1, 0xF1, 0xF1));

            //Properties.Settings.Default.DataGridItemSelectedScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xC1, 0x8A, 0x2B, 0xE2));
            //Properties.Settings.Default.DataGridItemMouseoverScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0x2A, 0x8A, 0x2B, 0xE2));
            //Properties.Settings.Default.DataGridItemBorderBrushScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2));

            //Properties.Settings.Default.AssignignArrowsColorScheme = ConvertColorToSolidColorBrush(System.Drawing.Color.FromArgb(0xFF, 0x8A, 0x2B, 0xE2));
        }


        private SolidColorBrush ConvertColorToSolidColorBrush(System.Drawing.Color color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
        }

        private void LoadThemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    
    }
}
