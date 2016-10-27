// <copyright file="ModelBase.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Core.Models
{
    using System;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight;

    /// <summary>
    ///     Base class for models
    /// </summary>
    public class ModelBase : ObservableObject
    {
        public ModelBase(bool isNew)
        {
            this.IsNew = isNew;
        }

        public Guid Id { get; set; }

        // ToDo: Check proper mapping of lists
        public List<HistoryItem> History { get; set; }

        public bool IsNew { get; set; }
    }
}