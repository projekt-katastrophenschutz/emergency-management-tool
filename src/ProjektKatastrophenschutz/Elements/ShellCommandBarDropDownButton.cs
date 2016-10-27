// <copyright file="ShellCommandBarDropDownButton.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System;
    using System.Collections.Generic;

    public class ShellCommandBarDropDownButton : ShellCommandBarItem
    {
        public Uri ImageUri { get; set; }

        public string Label { get; set; }

        public List<ShellCommandBarItem> Items { get; set; }

        public double DropDownHorizontalOffset { get; set; }
    }
}