// <copyright file="GroupFilter.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace ProjektKatastrophenschutz.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GroupFilter
    {
        private readonly List<Predicate<object>> mFilters;

        public GroupFilter()
        {
            this.mFilters = new List<Predicate<object>>();
            this.Filter = this.InternalFilter;
        }

        public Predicate<object> Filter { get; }

        private bool InternalFilter(object o)
        {
            return this.mFilters.All(filter => filter(o));
        }

        public void AddFilter(Predicate<object> filter)
        {
            if (this.mFilters.Contains(filter))
                return;

            this.mFilters.Add(filter);
        }

        public void RemoveFilter(Predicate<object> filter)
        {
            if (!this.mFilters.Contains(filter))
                return;

            this.mFilters.Remove(filter);
        }
    }
}