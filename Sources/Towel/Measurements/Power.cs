using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Power struct.</summary>
    public static class Power
    {
        /// <summary>Units for Power measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Power measurement.</summary>
            //UNITS = X,
        }
    }
}
