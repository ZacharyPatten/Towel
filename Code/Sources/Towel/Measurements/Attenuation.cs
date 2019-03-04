using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Attenuation struct.</summary>
    public static class Attenuation
    {
        #region Units

        /// <summary>Units for Attenuation measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Attenuation measurement.</summary>
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
                throw new Exception("There is a bug in " + nameof(Towel) + ". (" + nameof(Attenuation) + "." + nameof(ConversionFactor) + " attempted on like units)");
            }
            throw new NotImplementedException(nameof(Towel) + " is missing a conversion factor in " + nameof(Attenuation) + " for " + a + " -> " + b + ".");
        }

        #endregion
    }

    /// <summary>An Attenuation measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Attenuation measurement.</typeparam>
    public struct Attenuation<T>
    {
        internal T _measurement;
        internal Attenuation.Units _units;

        #region Constructors

        /// <summary>Constructs an Attenuation with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Attenuation.</param>
        /// <param name="units">The units of the Attenuation.</param>
        public Attenuation(T measurement, Attenuation.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Attenuation.</summary>
        public Attenuation.Units Units
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
        internal T this[Attenuation.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Attenuation.ConversionFactor<T>(this._units, units);
                    return Compute.Multiply(this._measurement, factor);
                }
            }
        }

        ///// <summary>Gets the measurement in XXXXX.</summary>
        //public T XXXXX
        //{
        //    get
        //    {
        //        return this[Attenuation.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Attenuation<T> Add(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Attenuation<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Attenuation<T> operator +(Attenuation<T> a, Attenuation<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Attenuation<T> Subtract(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Attenuation<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Attenuation<T> operator -(Attenuation<T> a, Attenuation<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Attenuation<T> Multiply(Attenuation<T> a, T b)
        {
            return new Attenuation<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Attenuation<T> Multiply(T b, Attenuation<T> a)
        {
            return new Attenuation<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Attenuation<T> operator *(Attenuation<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Attenuation<T> operator *(T b, Attenuation<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Attenuation<T> Divide(Attenuation<T> a, T b)
        {
            return new Attenuation<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Attenuation<T> operator /(Attenuation<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Attenuation<T> a, Attenuation<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Attenuation<T> a, Attenuation<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Attenuation<T> a, Attenuation<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Attenuation<T> left, Attenuation<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Attenuation<T> a, Attenuation<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Attenuation<T> a, Attenuation<T> b)
        {
            Attenuation.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Attenuation<T> a, Attenuation<T> b)
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
                //case Attenuation.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Attenuation<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Attenuation<T>)
            {
                return this == ((Attenuation<T>)obj);
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
