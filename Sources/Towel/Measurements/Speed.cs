using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Speed struct.</summary>
    public static class Speed
    {
        /// <summary>Units for Speed measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            [ComplexUnitNumerators(Length.Units.Meters)]
            [ComplexUnitDenominators(Time.Units.Seconds)]
            /// <summary>Units of an angle measurement.</summary>
            MertersPerSecond = 0,
        }
    }
}
