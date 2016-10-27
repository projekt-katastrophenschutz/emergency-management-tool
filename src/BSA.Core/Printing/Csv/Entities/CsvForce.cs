using System.Collections.Generic;

namespace BSA.Core.Printing.Csv.Entities
{
    // This class is needed, because the csv mapper needs a class with a parameterless constructor for serialization.
    // That's why we can't use the model objects.
    public class CsvForce
    {
        public string Name { get; set; } // Einsatzkraftname

        public string RadioName { get; set; } // Funkname

        public CsvPerson Leader { get; set; } // Einsatzkraftführer

        public string Vehicle { get; set; } // Einsatzfahrzeug

        public string Notes { get; set; } // Anmerkungen

        public List<CsvPerson> Persons { get; set; } // zugeteilte Personen
    }
}
