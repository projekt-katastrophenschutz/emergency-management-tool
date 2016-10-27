// <copyright file="IShellViewStateAware.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    public interface IShellViewStateAware
    {
        ShellViewState ShellViewState { get; }

        void ShellViewStateChanged(ShellViewState shellViewState);
    }
}