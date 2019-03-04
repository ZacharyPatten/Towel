using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Luminosity struct.</summary>
    public static class Luminosity
    {
        #region Units

        /// <summary>Units for Luminosity measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Luminosity measurement.</summary>
			//UNITS = X,
        }

        internal struct Conversion
        {
            internal Units A;
            internal Units B;

            internal Conversion(Units a, Units b)
            {
                this.A = a;
                this.B = b;
            }
        }

        internal static class ConversionConstant<T>
        {
            // Note: we unfortunately need to store these constants in hard coded
            // static fields for performance purposes. If there is any way to avoid this
            // but keep the performmance PLEASE let me know!

            // internal static T XXXXXtoXXXXX = ConversionFactorAttribute.Get(Units.XXXXX, Units.XXXXX).Value<T>();
        }

        internal static T ConversionFactor<T>(Units a, Units b)
        {
            Conversion conversion = new Conversion(a, b);
            switch (conversion)
            {
                //case var C when C.A == Units.XXXXX && C.B == Units.XXXXX: return ConversionConstant<T>.XXXXXToXXXXX;
            }
            if (a == b)
            {
                throw new Exception("There is a bug in " + nameof(Towel) + ". (" + nameof(Luminosity) + "." + nameof(ConversionFactor) + " attempted on like units)");
            }
            throw new NotImplementedException(nameof(Towel) + " is missing a conversion factor in " + nameof(Luminosity) + " for " + a + " -> " + b + ".");
        }

        #endregion
    }

    /// <summary>An Luminosity measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Luminosity measurement.</typeparam>
    public struct Luminosity<T>
    {
        internal T _measurement;
        internal Luminosity.Units _units;

        #region Constructors

        /// <summary>Constructs an Luminosity with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Luminosity.</param>
        /// <param name="units">The units of the Luminosity.</param>
        public Luminosity(T measurement, Luminosity.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Luminosity.</summary>
        public Luminosity.Units Units
        {
            get { return this._units; }
            set
            {
                if (value != this._units)
                {
                    this._measurement = this[value];
                    this._units = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        internal T this[Luminosity.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Luminosity.ConversionFactor<T>(this._units, units);
                    return Compute.Multiply(this._measurement, factor);
                }
            }
        }

        ///// <summary>Gets the measurement in XXXXX.</summary>
        //public T XXXXX
        //{
        //    get
        //    {
        //        return this[Luminosity.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Luminosity<T> Add(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Luminosity<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Luminosity<T> operator +(Luminosity<T> a, Luminosity<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Luminosity<T> Subtract(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Luminosity<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Luminosity<T> operator -(Luminosity<T> a, Luminosity<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Luminosity<T> Multiply(Luminosity<T> a, T b)
        {
            return new Luminosity<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Luminosity<T> Multiply(T b, Luminosity<T> a)
        {
            return new Luminosity<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Luminosity<T> operator *(Luminosity<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Luminosity<T> operator *(T b, Luminosity<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Luminosity<T> Divide(Luminosity<T> a, T b)
        {
            return new Luminosity<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Luminosity<T> operator /(Luminosity<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Luminosity<T> a, Luminosity<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Luminosity<T> a, Luminosity<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Luminosity<T> a, Luminosity<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Luminosity<T> left, Luminosity<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Luminosity<T> a, Luminosity<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Luminosity<T> a, Luminosity<T> b)
        {
            Luminosity.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Luminosity<T> a, Luminosity<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            switch (this._units)
            {
                //case Luminosity.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Luminosity<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Luminosity<T>)
            {
                return this == ((Luminosity<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this._measurement.GetHashCode() ^ this._units.GetHashCode();
        }

        #endregion
    }
}
