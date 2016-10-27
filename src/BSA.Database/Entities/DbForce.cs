// <copyright file="Forces.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DbForce : DbEntityBase
    {
        public string Name { get; set; } // Einsatzkraftname

        public string RadioName { get; set; } // Funkname

        //public DbPerson Leader { get; set; }

        public Guid LeaderId { get; set; } // Einsatzkraftführer

        public string Vehicle { get; set; } // Einsatzfahrzeug

        public string Notes { get; set; } // Anmerkungen

        ////[InverseProperty("Force")]
        public List<DbPerson> Persons { get; set; } // zugeteilte Personen

        public List<DbForceJobRelation> Jobs { get; set; }
    }
}