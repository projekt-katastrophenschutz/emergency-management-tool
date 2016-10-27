// <copyright file="IShellCommandBar.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Declares that an implementing class exposes several command bar items
    /// </summary>
    internal interface IShellCommandBar
    {
        ObservableCollection<ShellCommandBarItem> CommandBarItems { get; }

        ObservableCollection<ShellCommandBarItem> CommandBarSecondaryItems { get; }
    }
}