namespace BSA.Core.Printing.Csv.Entities
{
    public class CsvJobLocation
    {
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
