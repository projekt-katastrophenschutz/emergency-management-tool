using System;
using BSA.Core.Models;

namespace BSA.Core.Printing.Csv.Entities
{
    // This class is needed, because the csv mapper needs a class with a parameterless constructor for serialization.
    // That's why we can't use the model objects.
    public class CsvJob
    {
        public string Name { get; set; } // Auftragsname

        public DateTime Date { get; set; } // Eingansdatum und Zeit

        public string Messenger { get; set; } // Melder

        public string Organization { get; set; } // zugehörige Organisation

        public CsvJobLocation Location { get; set; } // Ortsangaben des Auftrags

        public string JobType { get; set; } // Auftragstype, Schadensereignis

        public bool InjuredPerson { get; set; } // gibt es verletzte Personen

        public int NumberInjuredPerson { get; set; } // Anzahl verletzte Personen

        public string Communicationtool { get; set; } // Kommunikationsmittel

        public string Description { get; set; } // Beschreibung, Meldung

        public JobPriority JobPriority { get; set; } // Priorität des Auftrags

        public JobState JobStatus { get; set; } // offen, in Bearbeitung, beendet
    }
}
