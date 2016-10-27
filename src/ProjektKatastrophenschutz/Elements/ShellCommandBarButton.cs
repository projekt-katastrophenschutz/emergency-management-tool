// <copyright file="ShellCommandBarButton.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System;
    using System.Windows.Input;

    public class ShellCommandBarButton : ShellCommandBarItem
    {
        public Uri ImageUri { get; set; }

        public string Label { get; set; }

        public ICommand Command { get; set; }
    }
}