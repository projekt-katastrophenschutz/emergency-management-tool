using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSA.Core.Models;

namespace BSA.Core.Utils
{
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
    public static class ConvertingUtils
    {
        public static string ConvertJobTypeToLocalizedString(object value, CultureInfo culture)
        {
            if (!(value is JobType))
                return null;

            var jobType = (JobType)value;
            switch (jobType)
            {
                case JobType.Flooding:
                    return "Hochwasser";
                case JobType.Hail:
                    return "Hagel";
                case JobType.Fire:
                    return "Feuer";
                case JobType.LightningStrike:
                    return "Blitzeinschlag";
                case JobType.Recovery:
                    return "Bergung";
                case JobType.Rescue:
                    return "Rettung";
            }

            return null;
        }

        public static string ConvertJobStateToLocalizedString(object value, CultureInfo culture)
        {
            if (!(value is JobState))
                return null;

            var jobType = (JobState)value;
            switch (jobType)
            {
                case JobState.NewCreated:
                    return "Neu erstellt";
                case JobState.InProgress:
                    return "In Bearbeitung";
                case JobState.Ended:
                    return "Beendet";
                default:
                    return null;
            }
        }

        public static string ConvertJobPriorityToLocalizedString(object value, CultureInfo culture)
        {
            if (!(value is JobPriority))
                return null;

            var jobType = (JobPriority)value;
            switch (jobType)
            {
                case JobPriority.Low:
                    return "Niedrig";
                case JobPriority.Medium:
                    return "Mittel";
                case JobPriority.High:
                    return "Hoch";
                default:
                    return null;
            }
        }
    }
}
