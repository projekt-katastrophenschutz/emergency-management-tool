// <copyright file="ShellCommandBarControlSelector.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using ProjektKatastrophenschutz.Elements;

    public class ShellCommandBarControlSelector : DataTemplateSelector
    {
        public DataTemplate ButtonTemplate { get; set; }

        public DataTemplate DropDownButtonTemplate { get; set; }

        public DataTemplate SeparatorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ShellCommandBarSeparator)
            {
                return this.SeparatorTemplate;
            }

            if (item is ShellCommandBarButton)
            {
                return this.ButtonTemplate;
            }
            
            if (item is ShellCommandBarDropDownButton)
            {
                return this.DropDownButtonTemplate;
            }

            throw new NotSupportedException();
        }
    }
}