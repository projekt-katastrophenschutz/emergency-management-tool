using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Printing.Csv.Entities;
using CsvHelper.TypeConversion;

namespace BSA.Core.Printing.Csv.Converters
{
    public class LocationConverter : DefaultTypeConverter
    {
        public override string ConvertToString(TypeConverterOptions options, object value)
        {
            var location = value as CsvJobLocation;           
            if (location == null)
                return string.Empty;

            var result =
                // ReSharper disable once UseStringInterpolation
                string.Format("{0} {1} {2} {3} {4} {5}",
                    location.Street,
                    location.HouseNumber,
                    string.IsNullOrEmpty(location.FloorLevel) ? string.Empty : "(Stockwerk: " + location.FloorLevel + ")",
                    location.ZipCode,
                    location.City,
                    string.IsNullOrEmpty(location.AdditionalDescription) ? string.Empty : "Zusatzbeschreibung: " + location.AdditionalDescription);

            return result;
        }
    }
}
