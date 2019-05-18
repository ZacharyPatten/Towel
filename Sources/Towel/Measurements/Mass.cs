using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Mass struct.</summary>
    public static class Mass
    {
        /// <summary>Units for Mass measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            [MetricUnit(MetricUnits.Yocto)]
            /// <summary>Units of an mass measurement.</summary>
            Yoctograms = 0,
            [MetricUnit(MetricUnits.Zepto)]
            /// <summary>Units of an mass measurement.</summary>
            Zeptograms = 1,
            [MetricUnit(MetricUnits.Atto)]
            /// <summary>Units of an mass measurement.</summary>
            Attograms = 2,
            [MetricUnit(MetricUnits.Femto)]
            /// <summary>Units of an mass measurement.</summary>
            Femtograms = 3,
            [MetricUnit(MetricUnits.Pico)]
            /// <summary>Units of an mass measurement.</summary>
            Picograms = 4,
            [MetricUnit(MetricUnits.Nano)]
            /// <summary>Units of an mass measurement.</summary>
            Nanograms = 5,
            [MetricUnit(MetricUnits.Micro)]
            /// <summary>Units of an mass measurement.</summary>
            Micrograms = 6,
            [MetricUnit(MetricUnits.Milli)]
            /// <summary>Units of an mass measurement.</summary>
            Milligrams = 7,
            [MetricUnit(MetricUnits.Centi)]
            /// <summary>Units of an mass measurement.</summary>
            Centigrams = 8,
            [MetricUnit(MetricUnits.Deci)]
            /// <summary>Units of an mass measurement.</summary>
            Decigrams = 10,
            [MetricUnit(MetricUnits.BASE)]
            /// <summary>Units of an mass measurement.</summary>
            Grams = 13,
            [MetricUnit(MetricUnits.Deka)]
            /// <summary>Units of an mass measurement.</summary>
            Dekagrams = 14,
            [MetricUnit(MetricUnits.Hecto)]
            /// <summary>Units of an mass measurement.</summary>
            Hectograms = 15,
            [MetricUnit(MetricUnits.Kilo)]
            /// <summary>Units of an mass measurement.</summary>
            Kilograms = 16,
            [MetricUnit(MetricUnits.Mega)]
            /// <summary>Units of an mass measurement.</summary>
            Megagrams = 18,
            [MetricUnit(MetricUnits.Giga)]
            /// <summary>Units of an mass measurement.</summary>
            Gigagrams = 19,
            [MetricUnit(MetricUnits.Tera)]
            /// <summary>Units of an mass measurement.</summary>
            Teragrams = 20,
            [MetricUnit(MetricUnits.Peta)]
            /// <summary>Units of an mass measurement.</summary>
            Petagrams = 21,
            [MetricUnit(MetricUnits.Exa)]
            /// <summary>Units of an mass measurement.</summary>
            Exagrams = 22,
            [MetricUnit(MetricUnits.Zetta)]
            /// <summary>Units of an mass measurement.</summary>
            Zettagrams = 23,
            [MetricUnit(MetricUnits.Yotta)]
            /// <summary>Units of an mass measurement.</summary>
            Yottagrams = 24,
        }
    }
}
