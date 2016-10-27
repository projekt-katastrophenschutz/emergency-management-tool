// <copyright file="Job.cs" company="FT Software">
// Copyright (c) 2016. All rights reserved.
// </copyright>
// <author>Florian Thurnwald</author>

namespace BSA.Database.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DbJob : DbEntityBase
    {
        public string Name { get; set; } // Auftragsname

        public DateTime Date { get; set; } // Eingansdatum und Zeit

        public string Messenger { get; set; } // Melder

        public string Organization { get; set; } // zugehörige Organisation
        
        public DbJobLocation Location { get; set; } // Ortsangaben des Auftrags

        public string JobType { get; set; } // Auftragstype, Schadensereignis

        public bool InjuredPerson { get; set; } // gibt es verletzte Personen

        public int NumberInjuredPerson { get; set; } // Anzahl verletzte Personen

        public string Communicationtool { get; set; } // Kommunikationsmittel

        public string Description { get; set; } // Beschreibung, Meldung

        public int JobPriority { get; set; } // Priorität des Auftrags

        public int JobStatus { get; set; } // offen, in Bearbeitung, beendet

        public List<DbForceJobRelation> Forces { get; set; } // Zugeteilte Einsatzkräfte (bei offenen Aufträgen leer)
    }
}