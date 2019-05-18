using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Angle struct.</summary>
    public static class Angle
    {
        /// <summary>Units for angle measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage of location conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            [ConversionFactor(Degrees, "9 / 10")]
            [ConversionFactor(Radians, "π / 200")]
            [ConversionFactor(Turns, "1 / 400")]
            /// <summary>Units of an angle measurement.</summary>
            Gradians = 0,
            [ConversionFactor(Gradians, "10 / 9")]
            [ConversionFactor(Radians, "π / 180")]
            [ConversionFactor(Turns, "1 / 360")]
            /// <summary>Units of an angle measurement.</summary>
            Degrees = 1,
            [ConversionFactor(Gradians, "200 / π")]
            [ConversionFactor(Degrees, "180 / π")]
            [ConversionFactor(Turns, "π / 2")]
            /// <summary>Units of an angle measurement.</summary>
			Radians = 2,
            [ConversionFactor(Gradians, "400")]
            [ConversionFactor(Degrees, "360")]
            [ConversionFactor(Radians, "2 / π")]
            /// <summary>Units of an angle measurement.</summary>
			Turns = 3,
        }
    }
}
