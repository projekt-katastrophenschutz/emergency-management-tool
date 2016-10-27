using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektKatastrophenschutz.Utils
{
    public class NumberRangeFilter
    {
        public uint Min { get; set; } = uint.MinValue;

        public uint Max { get; set; } = uint.MaxValue;
    }
}
