using System.Collections.Generic;
using System.Linq;
using BSA.Core.Printing.Csv.Entities;
using CsvHelper.TypeConversion;

namespace BSA.Core.Printing.Csv.Converters
{
    public class PersonConverter : DefaultTypeConverter
    {
        public override string ConvertToString(TypeConverterOptions options, object value)
        {
            var personsCollection = value as IEnumerable<CsvPerson>;
            var singlePerson = value as CsvPerson;

            var result = string.Empty;

            // If the object is a list of persons, save full person list of persons
            if (personsCollection != null)
            {
                result = personsCollection.Aggregate(string.Empty, (current, person) => current +
                    $"({person.Prename} {person.Surname}) ");
            }

            // if it's just a single person, save this person
            if (singlePerson != null)
            {
                result = $"{singlePerson.Prename} {singlePerson.Surname}";
            }

            return result;
        }
    }
}
