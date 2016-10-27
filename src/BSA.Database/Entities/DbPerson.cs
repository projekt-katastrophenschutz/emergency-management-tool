// <copyright file="Person.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DbPerson : DbEntityBase
    {
        public string Prename { get; set; } // Vorname

        public string Surname { get; set; } // Nachname

        public DateTime BirthDate { get; set; } // Geburtsdatum

        public string Street { get; set; } // Straße

        public int ZipCode { get; set; } // Postleitzahl

        public string Place { get; set; } // Ort

        public string Additional { get; set; } // Zusatzinformationen-Sonstiges

        public string RadioCallName { get; set; } // Funkrufname

        public string Employer { get; set; } // Arbeitgeber

        public string PhoneNumber { get; set; } // Telefonnummer

        public string RelativesDetails { get; set; } // Infos zu Verwandten / Ansprechpartnern

        public Guid ForceId { get; set; }

        [ForeignKey(nameof(ForceId))]
        public DbForce Force { get; set; }
    }
}