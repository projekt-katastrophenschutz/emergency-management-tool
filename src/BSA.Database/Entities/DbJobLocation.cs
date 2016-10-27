// <copyright file="DbJobLocation.cs" company="FT Software">
// Copyright (c) 2016 Florian Thurnwald. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;

    public class DbJobLocation : DbEntityBase
    {
        public DbJob Job { get; set; }

        public Guid JobId { get; set; }

        public string Street { get; set; } // Straße

        public string HouseNumber { get; set; } // Hausnummer

        public string ZipCode { get; set; } // Postleitzahl

        public string FloorLevel { get; set; } // Stockwerk

        public string City { get; set; } // Stadt 

        /// <summary>
        ///     Location related additional / alternative description ("Third tree of the street" or something)
        /// </summary>
        public string AdditionalDescription { get; set; }
    }
}