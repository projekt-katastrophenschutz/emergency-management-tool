// <copyright file="IClosable.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Elements
{
    using System;

    public interface IClosable
    {
        event EventHandler ClosingRequest;
    }
}