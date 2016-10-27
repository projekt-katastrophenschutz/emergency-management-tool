using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Printing.Csv.Converters;
using BSA.Core.Printing.Csv.Entities;
using CsvHelper.Configuration;

namespace BSA.Core.Printing.Csv.Maps
{
    public sealed class JobCsvMap : CsvClassMap<CsvJob>
    {
        public JobCsvMap()
        {
            Map(j => j.Name).Name("Name");
            Map(j => j.JobStatus).Name("Status").TypeConverter<JobEnumConverter>();
            Map(j => j.JobPriority).Name("Priorität").TypeConverter<JobEnumConverter>(); ;
            Map(j => j.JobType).Name("Typ");
            Map(j => j.Location).Name("Ortsangaben").TypeConverter<LocationConverter>(); ;
            Map(j => j.Communicationtool).Name("Kommunikationsmittel");
            Map(j => j.NumberInjuredPerson).Name("Anzahl verletzter Personen");
            Map(j => j.Messenger).Name("Melder");
            Map(j => j.Description).Name("Beschreibung");                                      
        }
    }
}
