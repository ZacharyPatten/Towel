using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Acceleration struct.</summary>
    public static class Acceleration
    {
        /// <summary>Units for acceleration measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Note: These enum values are critical. They are used to determine
            // unit priorities and storage of location conversion factors. They 
            // need to be small and in non-increasing order of unit size.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Acceleration measurement.</summary>
            //UNITS = X,
        }
    }
}
