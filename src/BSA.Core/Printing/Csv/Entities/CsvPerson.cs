using System;

namespace BSA.Core.Printing.Csv.Entities
{
    // This class is needed, because the csv mapper needs a class with a parameterless constructor for serialization.
    // That's why we can't use the model objects.
    public class CsvPerson
    {
        public string Prename { get; set; } // Vorname

        public string Surname { get; set; } // Nachname

        public DateTime BirthDate { get; set; } // Geburtsdatum

        public string Street { get; set; } // Straße

        public string ZipCode { get; set; } // Postleitzahl

        public string Place { get; set; } // Ort

        public string Additional { get; set; } // Zusatzinformationen-Sonstiges

        public string RadioCallName { get; set; } // Funkrufname

        public string Employer { get; set; } // Arbeitgeber

        public string PhoneNumber { get; set; } // Telefonnummer

        public string RelativesDetails { get; set; } // Infos zu Verwandten / Ansprechpartnern
    }
}
