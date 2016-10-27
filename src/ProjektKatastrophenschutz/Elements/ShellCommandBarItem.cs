// <copyright file="ShellCommandBarItem.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System.Windows;

    public abstract class ShellCommandBarItem
    {
        public double Width { get; set; } = double.NaN;

        public Visibility Visibility { get; set; }
    }
}