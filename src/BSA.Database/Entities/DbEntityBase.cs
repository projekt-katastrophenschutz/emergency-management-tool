// <copyright file="EntityBase.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The base class for all database entities.
    /// </summary>
    public class DbEntityBase
    {
        /// <summary>
        /// Gets or sets an unique entity id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a list of entity modifications.
        /// </summary>
        public List<DbHistoryItem> History { get; set; } 
    }
}