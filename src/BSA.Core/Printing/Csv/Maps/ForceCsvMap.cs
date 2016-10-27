using BSA.Core.Printing.Csv.Converters;
using BSA.Core.Printing.Csv.Entities;
using CsvHelper.Configuration;

namespace BSA.Core.Printing.Csv.Maps
{
    public sealed class ForceCsvMap : CsvClassMap<CsvForce>
    {
        public ForceCsvMap()
        {
            Map(f => f.Name).Name("Name");
            Map(f => f.RadioName).Name("Funkrufname");
            Map(f => f.Leader).Name("Gruppenführer").TypeConverter<PersonConverter>();
            Map(f => f.Persons).Name("Personen").TypeConverter<PersonConverter>();
        }
    }
}
