using Towel.Mathematics;

namespace Towel.Measurements
{
    public static class Metric<T>
    {
        #region Shared Constants

        private static Predicate<T> EqualsZero = (T value) => { return Compute<T>.Equals(value, Compute<T>.FromInt32(0)); };
        private static Predicate<T> EqualsOne = (T value) => { return Compute<T>.Equals(value, Compute<T>.FromInt32(0)); };

        private static T TenPowered(int exponent, params Predicate<T>[] predicates)
        {
            T computed_value = Compute<T>.Power(Compute<T>.FromInt32(10), Compute<T>.FromInt32(exponent));
            foreach (Predicate<T> predicate in predicates)
            {
                if (predicate(computed_value))
                    throw new System.InvalidOperationException("Unable to compute 10E" + exponent + " constant for type \"" + Meta.ConvertTypeToCsharpSource(typeof(T)) + "\".");
            }
            return computed_value;
        }

        private static bool _10En24Computed = false;
        private static T _10En24Value;
        /// <summary>10E-24</summary>
        internal static T _10En24
        {
            get
            {
                if (!_10En24Computed)
                {
                    _10En24Value = TenPowered(-24, EqualsZero, EqualsOne);
                    _10En24Computed = true;
                }
                return _10En24Value;
            }
        }

        private static bool _10En23Computed = false;
        private static T _10En23Value;
        /// <summary>10E-23</summary>
        internal static T _10En23
        {
            get
            {
                if (!_10En23Computed)
                {
                    _10En23Value = TenPowered(-23, EqualsZero, EqualsOne);
                    _10En23Computed = true;
                }
                return _10En23Value;
            }
        }

        private static bool _10En22Computed = false;
        private static T _10En22Value;
        /// <summary>10E-22</summary>
        internal static T _10En22
        {
            get
            {
                if (!_10En22Computed)
                {
                    _10En22Value = TenPowered(-22, EqualsZero, EqualsOne);
                    _10En22Computed = true;
                }
                return _10En22Value;
            }
        }

        private static bool _10En21Computed = false;
        private static T _10En21Value;
        /// <summary>10E-21</summary>
        public static T _10En21
        {
            get
            {
                if (!_10En21Computed)
                {
                    _10En21Value = TenPowered(-21, EqualsZero, EqualsOne);
                }
                return _10En21Value;
            }
        }

        private static bool _10En20Computed = false;
        private static T _10En20Value;
        /// <summary>10E-20</summary>
        public static T _10En20
        {
            get
            {
                if (!_10En20Computed)
                {
                    _10En20Value = TenPowered(-20, EqualsZero, EqualsOne);
                    _10En20Computed = true;
                }
                return _10En20Value;
            }
        }

        private static bool _10En19Computed = false;
        private static T _10En19Value;
        /// <summary>10E-19</summary>
        public static T _10En19
        {
            get
            {
                if (!_10En19Computed)
                {
                    _10En19Value = TenPowered(-19, EqualsZero, EqualsOne);
                    _10En19Computed = true;
                }
                return _10En19Value;
            }
        }

        private static bool _10En18Computed = false;
        private static T _10En18Value;
        /// <summary>10E-18</summary>
        public static T _10En18
        {
            get
            {
                if (!_10En18Computed)
                {
                    _10En18Value = TenPowered(-18, EqualsZero, EqualsOne);
                    _10En18Computed = true;
                }
                return _10En18Value;
            }
        }

        private static bool _10En17Computed = false;
        private static T _10En17Value;
        /// <summary>10E-17</summary>
        public static T _10En17
        {
            get
            {
                if (!_10En17Computed)
                {
                    _10En17Value = TenPowered(-17, EqualsZero, EqualsOne);
                    _10En17Computed = true;
                }
                return _10En17Value;
            }
        }

        private static bool _10En16Computed = false;
        private static T _10En16Value;
        /// <summary>10E-16</summary>
        public static T _10En16
        {
            get
            {
                if (!_10En16Computed)
                {
                    _10En16Value = TenPowered(-16, EqualsZero, EqualsOne);
                    _10En16Computed = true;
                }
                return _10En16Value;
            }
        }

        private static bool _10En15Computed = false;
        private static T _10En15Value;
        /// <summary>10E-15</summary>
        public static T _10En15
        {
            get
            {
                if (!_10En15Computed)
                {
                    _10En15Value = TenPowered(-15, EqualsZero, EqualsOne);
                    _10En15Computed = true;
                }
                return _10En15Value;
            }
        }

        private static bool _10En14Computed = false;
        private static T _10En14Value;
        /// <summary>10E-14</summary>
        public static T _10En14
        {
            get
            {
                if (!_10En14Computed)
                {
                    _10En14Value = TenPowered(-14, EqualsZero, EqualsOne);
                    _10En14Computed = true;
                }
                return _10En14Value;
            }
        }

        private static bool _10En13Computed = false;
        private static T _10En13Value;
        /// <summary>10E-13</summary>
        public static T _10En13
        {
            get
            {
                if (!_10En13Computed)
                {
                    _10En13Value = TenPowered(-13, EqualsZero, EqualsOne);
                    _10En13Computed = true;
                }
                return _10En13Value;
            }
        }

        private static bool _10En12Computed = false;
        private static T _10En12Value;
        /// <summary>10E-12</summary>
        public static T _10En12
        {
            get
            {
                if (!_10En12Computed)
                {
                    _10En12Value = TenPowered(-12, EqualsZero, EqualsOne);
                    _10En12Computed = true;
                }
                return _10En12Value;
            }
        }

        private static bool _10En11Computed = false;
        private static T _10En11Value;
        /// <summary>10E-11</summary>
        public static T _10En11
        {
            get
            {
                if (!_10En11Computed)
                {
                    _10En11Value = TenPowered(-11, EqualsZero, EqualsOne);
                    _10En11Computed = true;
                }
                return _10En11Value;
            }
        }

        private static bool _10En10Computed = false;
        private static T _10En10Value;
        /// <summary>10E-10</summary>
        public static T _10En10
        {
            get
            {
                if (!_10En10Computed)
                {
                    _10En10Value = TenPowered(-10, EqualsZero, EqualsOne);
                    _10En10Computed = true;
                }
                return _10En10Value;
            }
        }

        private static bool _10En9Computed = false;
        private static T _10En9Value;
        /// <summary>10E-9</summary>
        public static T _10En9
        {
            get
            {
                if (!_10En9Computed)
                {
                    _10En9Value = TenPowered(-9, EqualsZero, EqualsOne);
                    _10En9Computed = true;
                }
                return _10En9Value;
            }
        }

        private static bool _10En8Computed = false;
        private static T _10En8Value;
        /// <summary>10E-8</summary>
        public static T _10En8
        {
            get
            {
                if (!_10En8Computed)
                {
                    _10En8Value = TenPowered(-8, EqualsZero, EqualsOne);
                    _10En8Computed = true;
                }
                return _10En8Value;
            }
        }

        private static bool _10En7Computed = false;
        private static T _10En7Value;
        /// <summary>10E-7</summary>
        public static T _10En7
        {
            get
            {
                if (!_10En7Computed)
                {
                    _10En7Value = TenPowered(-7, EqualsZero, EqualsOne);
                    _10En7Computed = true;
                }
                return _10En7Value;
            }
        }

        private static bool _10En6Computed = false;
        private static T _10En6Value;
        /// <summary>10E-6</summary>
        public static T _10En6
        {
            get
            {
                if (!_10En6Computed)
                {
                    _10En6Value = TenPowered(-6, EqualsZero, EqualsOne);
                    _10En6Computed = true;
                }
                return _10En6Value;
            }
        }

        private static bool _10En5Computed = false;
        private static T _10En5Value;
        /// <summary>10E-5</summary>
        public static T _10En5
        {
            get
            {
                if (!_10En5Computed)
                {
                    _10En5Value = TenPowered(-5, EqualsZero, EqualsOne);
                    _10En5Computed = true;
                }
                return _10En5Value;
            }
        }

        private static bool _10En4Computed = false;
        private static T _10En4Value;
        /// <summary>10E-4</summary>
        public static T _10En4
        {
            get
            {
                if (!_10En4Computed)
                {
                    _10En4Value = TenPowered(-4, EqualsZero, EqualsOne);
                    _10En4Computed = true;
                }
                return _10En4Value;
            }
        }

        private static bool _10En3Computed = false;
        private static T _10En3Value;
        /// <summary>10E-3</summary>
        public static T _10En3
        {
            get
            {
                if (!_10En3Computed)
                {
                    _10En3Value = TenPowered(-3, EqualsZero, EqualsOne);
                    _10En3Computed = true;
                }
                return _10En3Value;
            }
        }

        private static bool _10En2Computed = false;
        private static T _10En2Value;
        /// <summary>10E-2</summary>
        public static T _10En2
        {
            get
            {
                if (!_10En2Computed)
                {
                    _10En2Value = TenPowered(-2, EqualsZero, EqualsOne);
                    _10En2Computed = true;
                }
                return _10En2Value;
            }
        }

        private static bool _10En1Computed = false;
        private static T _10En1Value;
        /// <summary>10E-1</summary>
        public static T _10En1
        {
            get
            {
                if (!_10En1Computed)
                {
                    _10En1Value = TenPowered(-1, EqualsZero, EqualsOne);
                    _10En1Computed = true;
                }
                return _10En1Value;
            }
        }

        private static bool _10E1Computed = false;
        private static T _10E1Value;
        /// <summary>10E1</summary>
        public static T _10E1
        {
            get
            {
                if (!_10E1Computed)
                {
                    _10E1Value = TenPowered(1);
                    _10E1Computed = true;
                }
                return _10E1Value;
            }
        }

        private static bool _10E2Computed = false;
        private static T _10E2Value;
        /// <summary>10E2</summary>
        public static T _10E2
        {
            get
            {
                if (!_10E2Computed)
                {
                    _10E2Value = TenPowered(2);
                    _10E2Computed = true;
                }
                return _10E2Value;
            }
        }

        private static bool _10E3Computed = false;
        private static T _10E3Value;
        /// <summary>10E3</summary>
        public static T _10E3
        {
            get
            {
                if (!_10E3Computed)
                {
                    _10E3Value = TenPowered(3);
                    _10E3Computed = true;
                }
                return _10E3Value;
            }
        }

        private static bool _10E6Computed = false;
        private static T _10E6Value;
        /// <summary>10E6</summary>
        public static T _10E6
        {
            get
            {
                if (!_10E6Computed)
                {
                    _10E6Value = TenPowered(6);
                    _10E6Computed = true;
                }
                return _10E6Value;
            }
        }

        private static bool _10E9Computed = false;
        private static T _10E9Value;
        /// <summary>10E9</summary>
        public static T _10E9
        {
            get
            {
                if (!_10E9Computed)
                {
                    _10E9Value = TenPowered(9);
                    _10E9Computed = true;
                }
                return _10E9Value;
            }
        }

        private static bool _10E12Computed = false;
        private static T _10E12Value;
        /// <summary>10E12</summary>
        public static T _10E12
        {
            get
            {
                if (!_10E12Computed)
                {
                    _10E12Value = TenPowered(12);
                    _10E12Computed = true;
                }
                return _10E12Value;
            }
        }

        private static bool _10E15Computed = false;
        private static T _10E15Value;
        /// <summary>10E15</summary>
        public static T _10E15
        {
            get
            {
                if (!_10E15Computed)
                {
                    _10E15Value = TenPowered(15);
                    _10E15Computed = true;
                }
                return _10E15Value;
            }
        }

        private static bool _10E18Computed = false;
        private static T _10E18Value;
        /// <summary>10E18</summary>
        public static T _10E18
        {
            get
            {
                if (!_10E18Computed)
                {
                    _10E18Value = TenPowered(18);
                    _10E18Computed = true;
                }
                return _10E18Value;
            }
        }

        private static bool _10E21Computed = false;
        private static T _10E21Value;
        /// <summary>10E21</summary>
        public static T _10E21
        {
            get
            {
                if (!_10E21Computed)
                {
                    _10E21Value = TenPowered(21);
                    _10E21Computed = true;
                }
                return _10E21Value;
            }
        }

        private static bool _10E24Computed = false;
        private static T _10E24Value;
        /// <summary>10E24</summary>
        public static T _10E24
        {
            get
            {
                if (!_10E24Computed)
                {
                    _10E24Value = TenPowered(24);
                    _10E24Computed = true;
                }
                return _10E24Value;
            }
        }


        #endregion

        #region Base

        /// <summary>The factor for converting base to yotta (10E-24).</summary>
        public static T BaseToYottaFactor { get { return _10En24; } }
        /// <summary>Converts base to yotta.</summary>
        /// <param name="measurement">The measurement in base to convert to yotta.</param>
        /// <returns>The measurement in yotta.</returns>
        public static T BaseToYotta(T measurement) { return Compute<T>.Multiply(measurement, BaseToYottaFactor); }

        /// <summary>The factor for converting base to zetta (10E-21).</summary>
        public static T BaseToZettaFactor { get { return _10En21; } }
        /// <summary>Converts base to zetta.</summary>
        /// <param name="measurement">The measurement in base to convert to zetta.</param>
        /// <returns>The measurement in zetta.</returns>
        public static T BaseToZetta(T measurement) { return Compute<T>.Multiply(measurement, BaseToZettaFactor); }

        /// <summary>The factor for converting base to exa (10E-18).</summary>
        public static T BaseToExaFactor { get { return _10En18; } }
        /// <summary>Converts base to exa.</summary>
        /// <param name="measurement">The measurement in base to convert to exa.</param>
        /// <returns>The measurement in exa.</returns>
        public static T BaseToExa(T measurement) { return Compute<T>.Multiply(measurement, BaseToExaFactor); }

        /// <summary>The factor for converting base to peta (10E-15).</summary>
        public static T BaseToPetaFactor { get { return _10En15; } }
        /// <summary>Converts base to peta.</summary>
        /// <param name="measurement">The measurement in base to convert to peta.</param>
        /// <returns>The measurement in peta.</returns>
        public static T BaseToPeta(T measurement) { return Compute<T>.Multiply(measurement, BaseToPetaFactor); }

        /// <summary>The factor for converting base to tera (10E-12).</summary>
        public static T BaseToTeraFactor { get { return _10En12; } }
        /// <summary>Converts base to tera.</summary>
        /// <param name="measurement">The measurement in base to convert to tera.</param>
        /// <returns>The measurement in tera.</returns>
        public static T BaseToTera(T measurement) { return Compute<T>.Multiply(measurement, BaseToTeraFactor); }

        /// <summary>The factor for converting base to giga (10E-9).</summary>
        public static T BaseToGigaFactor { get { return _10En9; } }
        /// <summary>Converts base to giga.</summary>
        /// <param name="measurement">The measurement in base to convert to giga.</param>
        /// <returns>The measurement in giga.</returns>
        public static T BaseToGiga(T measurement) { return Compute<T>.Multiply(measurement, BaseToGigaFactor); }

        /// <summary>The factor for converting base to giga (10E-6).</summary>
        public static T BaseToMegaFactor { get { return _10En6; } }
        /// <summary>Converts base to mega.</summary>
        /// <param name="measurement">The measurement in base to convert to mega.</param>
        /// <returns>The measurement in mega.</returns>
        public static T BaseToMega(T measurement) { return Compute<T>.Multiply(measurement, BaseToMegaFactor); }

        /// <summary>The factor for converting base to kilo (10E-3).</summary>
        public static T BaseToKiloFactor { get { return _10En3; } }
        /// <summary>Converts base to kilo.</summary>
        /// <param name="measurement">The measurement in base to convert to kilo.</param>
        /// <returns>The measurement in kilo.</returns>
        public static T BaseToKilo(T measurement) { return Compute<T>.Multiply(measurement, BaseToKiloFactor); }

        /// <summary>The factor for converting base to hecto (10E-2).</summary>
        public static T BaseToHectoFactor { get { return _10En2; } }
        /// <summary>Converts base to hecto.</summary>
        /// <param name="measurement">The measurement in base to convert to hecto.</param>
        /// <returns>The measurement in hecto.</returns>
        public static T BaseToHecto(T measurement) { return Compute<T>.Multiply(measurement, BaseToHectoFactor); }

        /// <summary>The factor for converting base to deca (10E-1).</summary>
        public static T BaseToDecaFactor { get { return _10En1; } }
        /// <summary>Converts base to deca.</summary>
        /// <param name="measurement">The measurement in base to convert to deca.</param>
        /// <returns>The measurement in deca.</returns>
        public static T BaseToDeca(T measurement) { return Compute<T>.Multiply(measurement, BaseToDecaFactor); }

        /// <summary>The factor for converting base to deci (10E1).</summary>
        public static T BaseToDeciFactor { get { return _10E1; } }
        /// <summary>Converts base to deci.</summary>
        /// <param name="measurement">The measurement in base to convert to deci.</param>
        /// <returns>The measurement in deci.</returns>
        public static T BaseToDeci(T measurement) { return Compute<T>.Multiply(measurement, BaseToDeciFactor); }

        /// <summary>The factor for converting base to centi (10E2).</summary>
        public static T BaseToCentiFactor { get { return _10E2; } }
        /// <summary>Converts base to centi.</summary>
        /// <param name="measurement">The measurement in base to convert to centi.</param>
        /// <returns>The measurement in centi.</returns>
        public static T BaseToCenti(T measurement) { return Compute<T>.Multiply(measurement, BaseToCentiFactor); }

        /// <summary>The factor for converting base to milli (10E3).</summary>
        public static T BaseToMilliFactor { get { return _10E3; } }
        /// <summary>Converts base to milli.</summary>
        /// <param name="measurement">The measurement in base to convert to milli.</param>
        /// <returns>The measurement in milli.</returns>
        public static T BaseToMilli(T measurement) { return Compute<T>.Multiply(measurement, BaseToMilliFactor); }

        /// <summary>The factor for converting base to micro (10E6).</summary>
        public static T BaseToMicroFactor { get { return _10E6; } }
        /// <summary>Converts base to micro.</summary>
        /// <param name="measurement">The measurement in base to convert to micro.</param>
        /// <returns>The measurement in micro.</returns>
        public static T BaseToMicro(T measurement) { return Compute<T>.Multiply(measurement, BaseToMicroFactor); }

        /// <summary>The factor for converting base to nano (10E9).</summary>
        public static T BaseToNanoFactor { get { return _10E9; } }
        /// <summary>Converts base to nano.</summary>
        /// <param name="measurement">The measurement in base to convert to nano.</param>
        /// <returns>The measurement in nano.</returns>
        public static T BaseToNano(T measurement) { return Compute<T>.Multiply(measurement, BaseToNanoFactor); }

        /// <summary>The factor for converting base to pico (10E12).</summary>
        public static T BaseToPicoFactor { get { return _10E12; } }
        /// <summary>Converts base to pico.</summary>
        /// <param name="measurement">The measurement in base to convert to pico.</param>
        /// <returns>The measurement in pico.</returns>
        public static T BaseToPico(T measurement) { return Compute<T>.Multiply(measurement, BaseToPicoFactor); }

        /// <summary>The factor for converting base to femto (10E15).</summary>
        public static T BaseToFemtoFactor { get { return _10E15; } }
        /// <summary>Converts base to femto.</summary>
        /// <param name="measurement">The measurement in base to convert to femto.</param>
        /// <returns>The measurement in femto.</returns>
        public static T BaseToFemto(T measurement) { return Compute<T>.Multiply(measurement, BaseToFemtoFactor); }

        /// <summary>The factor for converting base to atto (10E18).</summary>
        public static T BaseToAttoFactor { get { return _10E18; } }
        /// <summary>Converts base to atto.</summary>
        /// <param name="measurement">The measurement in base to convert to atto.</param>
        /// <returns>The measurement in atto.</returns>
        public static T BaseToAtto(T measurement) { return Compute<T>.Multiply(measurement, BaseToAttoFactor); }

        /// <summary>The factor for converting base to zepto (10E21).</summary>
        public static T BaseToZeptoFactor { get { return _10E21; } }
        /// <summary>Converts base to zepto.</summary>
        /// <param name="measurement">The measurement in base to convert to zepto.</param>
        /// <returns>The measurement in zepto.</returns>
        public static T BaseToZepto(T measurement) { return Compute<T>.Multiply(measurement, BaseToZeptoFactor); }

        /// <summary>The factor for converting base to yocto (10E24).</summary>
        public static T BaseToYoctoFactor { get { return _10E24; } }
        /// <summary>Converts base to yocto.</summary>
        /// <param name="measurement">The measurement in base to convert to yocto.</param>
        /// <returns>The measurement in yocto.</returns>
        public static T BaseToYocto(T measurement) { return Compute<T>.Multiply(measurement, BaseToYoctoFactor); }

        #endregion

        #region Deci

        /// <summary>The factor for converting deci to yotta (10E-24).</summary>
        public static T DeciToYottaFactor { get { return _10En24; } }
        /// <summary>Converts deci to yotta.</summary>
        /// <param name="measurement">The measurement in deci to convert to yotta.</param>
        /// <returns>The measurement in yotta.</returns>
        public static T DeciToYotta(T measurement) { return Compute<T>.Multiply(measurement, DeciToYottaFactor); }

        /// <summary>The factor for converting deci to zetta (10E-21).</summary>
        public static T DeciToZettaFactor { get { return _10En21; } }
        /// <summary>Converts deci to zetta.</summary>
        /// <param name="measurement">The measurement in deci to convert to zetta.</param>
        /// <returns>The measurement in zetta.</returns>
        public static T DeciToZetta(T measurement) { return Compute<T>.Multiply(measurement, DeciToZettaFactor); }

        /// <summary>The factor for converting deci to exa (10E-18).</summary>
        public static T DeciToExaFactor { get { return _10En18; } }
        /// <summary>Converts deci to exa.</summary>
        /// <param name="measurement">The measurement in deci to convert to exa.</param>
        /// <returns>The measurement in exa.</returns>
        public static T DeciToExa(T measurement) { return Compute<T>.Multiply(measurement, DeciToExaFactor); }

        /// <summary>The factor for converting deci to peta (10E-15).</summary>
        public static T DeciToPetaFactor { get { return _10En15; } }
        /// <summary>Converts deci to peta.</summary>
        /// <param name="measurement">The measurement in deci to convert to peta.</param>
        /// <returns>The measurement in peta.</returns>
        public static T DeciToPeta(T measurement) { return Compute<T>.Multiply(measurement, DeciToPetaFactor); }

        /// <summary>The factor for converting deci to tera (10E-12).</summary>
        public static T DeciToTeraFactor { get { return _10En12; } }
        /// <summary>Converts deci to tera.</summary>
        /// <param name="measurement">The measurement in deci to convert to tera.</param>
        /// <returns>The measurement in tera.</returns>
        public static T DeciToTera(T measurement) { return Compute<T>.Multiply(measurement, DeciToTeraFactor); }

        /// <summary>The factor for converting deci to giga (10E-9).</summary>
        public static T DeciToGigaFactor { get { return _10En9; } }
        /// <summary>Converts deci to giga.</summary>
        /// <param name="measurement">The measurement in deci to convert to giga.</param>
        /// <returns>The measurement in giga.</returns>
        public static T DeciToGiga(T measurement) { return Compute<T>.Multiply(measurement, DeciToGigaFactor); }

        /// <summary>The factor for converting deci to giga (10E-6).</summary>
        public static T DeciToMegaFactor { get { return _10En6; } }
        /// <summary>Converts deci to mega.</summary>
        /// <param name="measurement">The measurement in deci to convert to mega.</param>
        /// <returns>The measurement in mega.</returns>
        public static T DeciToMega(T measurement) { return Compute<T>.Multiply(measurement, DeciToMegaFactor); }

        /// <summary>The factor for converting deci to kilo (10E-3).</summary>
        public static T DeciToKiloFactor { get { return _10En3; } }
        /// <summary>Converts deci to kilo.</summary>
        /// <param name="measurement">The measurement in deci to convert to kilo.</param>
        /// <returns>The measurement in kilo.</returns>
        public static T DeciToKilo(T measurement) { return Compute<T>.Multiply(measurement, DeciToKiloFactor); }

        /// <summary>The factor for converting deci to hecto (10E-2).</summary>
        public static T DeciToHectoFactor { get { return _10En2; } }
        /// <summary>Converts deci to hecto.</summary>
        /// <param name="measurement">The measurement in deci to convert to hecto.</param>
        /// <returns>The measurement in hecto.</returns>
        public static T DeciToHecto(T measurement) { return Compute<T>.Multiply(measurement, DeciToHectoFactor); }

        /// <summary>The factor for converting deci to deca (10E-1).</summary>
        public static T DeciToDecaFactor { get { return _10En1; } }
        /// <summary>Converts deci to deca.</summary>
        /// <param name="measurement">The measurement in deci to convert to deca.</param>
        /// <returns>The measurement in deca.</returns>
        public static T DeciToDeca(T measurement) { return Compute<T>.Multiply(measurement, DeciToDecaFactor); }

        /// <summary>The factor for converting deci to deci (10E1).</summary>
        public static T DeciToDeciFactor { get { return _10E1; } }
        /// <summary>Converts deci to deci.</summary>
        /// <param name="measurement">The measurement in deci to convert to deci.</param>
        /// <returns>The measurement in deci.</returns>
        public static T DeciToDeci(T measurement) { return Compute<T>.Multiply(measurement, DeciToDeciFactor); }

        /// <summary>The factor for converting deci to centi (10E2).</summary>
        public static T DeciToCentiFactor { get { return _10E2; } }
        /// <summary>Converts deci to centi.</summary>
        /// <param name="measurement">The measurement in deci to convert to centi.</param>
        /// <returns>The measurement in centi.</returns>
        public static T DeciToCenti(T measurement) { return Compute<T>.Multiply(measurement, DeciToCentiFactor); }

        /// <summary>The factor for converting deci to milli (10E3).</summary>
        public static T DeciToMilliFactor { get { return _10E3; } }
        /// <summary>Converts deci to milli.</summary>
        /// <param name="measurement">The measurement in deci to convert to milli.</param>
        /// <returns>The measurement in milli.</returns>
        public static T DeciToMilli(T measurement) { return Compute<T>.Multiply(measurement, DeciToMilliFactor); }

        /// <summary>The factor for converting deci to micro (10E6).</summary>
        public static T DeciToMicroFactor { get { return _10E6; } }
        /// <summary>Converts deci to micro.</summary>
        /// <param name="measurement">The measurement in deci to convert to micro.</param>
        /// <returns>The measurement in micro.</returns>
        public static T DeciToMicro(T measurement) { return Compute<T>.Multiply(measurement, DeciToMicroFactor); }

        /// <summary>The factor for converting deci to nano (10E9).</summary>
        public static T DeciToNanoFactor { get { return _10E9; } }
        /// <summary>Converts deci to nano.</summary>
        /// <param name="measurement">The measurement in deci to convert to nano.</param>
        /// <returns>The measurement in nano.</returns>
        public static T DeciToNano(T measurement) { return Compute<T>.Multiply(measurement, DeciToNanoFactor); }

        /// <summary>The factor for converting deci to pico (10E12).</summary>
        public static T DeciToPicoFactor { get { return _10E12; } }
        /// <summary>Converts deci to pico.</summary>
        /// <param name="measurement">The measurement in deci to convert to pico.</param>
        /// <returns>The measurement in pico.</returns>
        public static T DeciToPico(T measurement) { return Compute<T>.Multiply(measurement, DeciToPicoFactor); }

        /// <summary>The factor for converting deci to femto (10E15).</summary>
        public static T DeciToFemtoFactor { get { return _10E15; } }
        /// <summary>Converts deci to femto.</summary>
        /// <param name="measurement">The measurement in deci to convert to femto.</param>
        /// <returns>The measurement in femto.</returns>
        public static T DeciToFemto(T measurement) { return Compute<T>.Multiply(measurement, DeciToFemtoFactor); }

        /// <summary>The factor for converting deci to atto (10E18).</summary>
        public static T DeciToAttoFactor { get { return _10E18; } }
        /// <summary>Converts deci to atto.</summary>
        /// <param name="measurement">The measurement in deci to convert to atto.</param>
        /// <returns>The measurement in atto.</returns>
        public static T DeciToAtto(T measurement) { return Compute<T>.Multiply(measurement, DeciToAttoFactor); }

        /// <summary>The factor for converting deci to zepto (10E21).</summary>
        public static T DeciToZeptoFactor { get { return _10E21; } }
        /// <summary>Converts deci to zepto.</summary>
        /// <param name="measurement">The measurement in deci to convert to zepto.</param>
        /// <returns>The measurement in zepto.</returns>
        public static T DeciToZepto(T measurement) { return Compute<T>.Multiply(measurement, DeciToZeptoFactor); }

        /// <summary>The factor for converting deci to yocto (10E24).</summary>
        public static T DeciToYoctoFactor { get { return _10E24; } }
        /// <summary>Converts deci to yocto.</summary>
        /// <param name="measurement">The measurement in deci to convert to yocto.</param>
        /// <returns>The measurement in yocto.</returns>
        public static T DeciToYocto(T measurement) { return Compute<T>.Multiply(measurement, DeciToYoctoFactor); }

        #endregion
    }
}
