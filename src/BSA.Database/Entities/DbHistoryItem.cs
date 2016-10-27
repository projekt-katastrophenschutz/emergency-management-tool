// <copyright file="HistoryItem.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;

    /// <summary>
    /// Represents a single entity modification.
    /// </summary>
    public class DbHistoryItem
    {
        /// <summary>
        /// Gets or sets an unique entity id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date/time of the change.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who made the change.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the description of the change.
        /// </summary>
        public string Changes { get; set; }

        public DbForce DbForce { get; set; }

        public DbJob DbJob { get; set; }
    }
}