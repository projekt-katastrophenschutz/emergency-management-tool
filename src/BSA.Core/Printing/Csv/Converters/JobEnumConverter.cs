using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Models;
using BSA.Core.Printing.Csv.Entities;
using BSA.Core.Utils;
using CsvHelper.TypeConversion;

namespace BSA.Core.Printing.Csv.Converters
{
    public class JobEnumConverter : DefaultTypeConverter
    {
        public override string ConvertToString(TypeConverterOptions options, object value)
        {
            if (value is JobType)
                return ConvertingUtils.ConvertJobTypeToLocalizedString(value, CultureInfo.CurrentCulture);
            if (value is JobState)
                return ConvertingUtils.ConvertJobStateToLocalizedString(value, CultureInfo.CurrentCulture);
            if (value is JobPriority)
                return ConvertingUtils.ConvertJobPriorityToLocalizedString(value, CultureInfo.CurrentCulture);

            return string.Empty;
        }
    }
}
