// <copyright file="ChangeEntry.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Utils
{
    using System.Reflection;

    public class ChangeEntry
    {
        public PropertyInfo PropertyInfo { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }

        public string PropertyName => this.PropertyInfo.Name;
    }
}