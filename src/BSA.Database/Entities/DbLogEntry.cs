// <copyright file="LogEntry.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;

    /// <summary>
    ///     A log entry representing a database transformation.
    /// </summary>
    public class DbLogEntry
    {
        /// <summary>
        ///     Gets or sets the id of the log entry.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        ///     Gets or sets the json content containing the database transformation data.
        /// </summary>
        public string JsonContent { get; set; }
    }
}