using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Attenuation struct.</summary>
    public static class Attenuation
    {
        /// <summary>Units for Attenuation measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Attenuation measurement.</summary>
			//UNITS = X,
        }
    }
}
