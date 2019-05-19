using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Time struct.</summary>
    public static class Time
    {
        /// <summary>Units for time measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage of location conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            [ConversionFactor(Seconds, "1/1000")]
            [ConversionFactor(Minutes, "1/60000")]
            [ConversionFactor(Hours, "1/3600000")]
            [ConversionFactor(Days, "1/86400000")]
            /// <summary>Units of an time measurement.</summary>
            Milliseconds = 0,
            [ConversionFactor(Milliseconds, "1000")]
            [ConversionFactor(Minutes, "1/60")]
            [ConversionFactor(Hours, "1/3600")]
            [ConversionFactor(Days, "1/86400")]
            /// <summary>Units of an time measurement.</summary>
            Seconds = 1,
            [ConversionFactor(Milliseconds, "60000")]
            [ConversionFactor(Seconds, "60")]
            [ConversionFactor(Hours, "1/60")]
            [ConversionFactor(Days, "1/1440")]
            /// <summary>Units of an time measurement.</summary>
            Minutes = 2,
            [ConversionFactor(Milliseconds, "3600000")]
            [ConversionFactor(Seconds, "3600")]
            [ConversionFactor(Minutes, "60")]
            [ConversionFactor(Days, "1/24")]
            /// <summary>Units of an time measurement.</summary>
            Hours = 3,
            [ConversionFactor(Milliseconds, "86400000")]
            [ConversionFactor(Seconds, "86400")]
            [ConversionFactor(Minutes, "1440")]
            [ConversionFactor(Hours, "24")]
            /// <summary>Units of an time measurement.</summary>
            Days = 4,
        }
    }
}
