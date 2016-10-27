// <copyright file="ShellViewPresenter.xaml.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    ///     Interaction logic for ShellViewPresenter.xaml
    /// </summary>
    public partial class ShellViewPresenter : UserControl
    {
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register(
            nameof(View),
            typeof (UserControl),
            typeof (ShellViewPresenter),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnViewChanged));

        public static readonly DependencyProperty CommandBarItemsOffsetProperty = DependencyProperty.Register(
            nameof(CommandBarItemsOffset),
            typeof(double),
            typeof(ShellViewPresenter),
            new FrameworkPropertyMetadata(
                0d,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnCommandBarItemsOffsetChanged));

        public ShellViewPresenter()
        {
            this.InitializeComponent();
        }

        public UserControl View
        {
            get { return (UserControl) this.GetValue(ViewProperty); }
            set
            {
                this.SetValue(ViewProperty, value);
                this.UpdateViewInternal();
            }
        }

        public double CommandBarItemsOffset
        {
            get { return (double)this.GetValue(CommandBarItemsOffsetProperty); }
            set
            {
                this.SetValue(CommandBarItemsOffsetProperty, value);
                this.UpdateCommandBarItemsOffsetInternal();
            }
        }

        private static void OnViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var shellViewPresenter = (ShellViewPresenter) d;
            shellViewPresenter.UpdateViewInternal();
        }

        private static void OnCommandBarItemsOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var shellViewPresenter = (ShellViewPresenter)d;
            shellViewPresenter.UpdateCommandBarItemsOffsetInternal();
        }

        private void UpdateViewInternal()
        {
            this.ContentControl.Content = this.View;
        }

        private void UpdateCommandBarItemsOffsetInternal()
        {
            this.CommandBarItemsControl.Margin = new Thickness(this.CommandBarItemsOffset, 0, 0, 0);
        }

        private void ShellCommandBarDropDownButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.Placement = PlacementMode.Bottom;
                button.ContextMenu.PlacementTarget = button;
                ////button.ContextMenu.HorizontalOffset = -1.0;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void ShellCommandBarDropDownButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}