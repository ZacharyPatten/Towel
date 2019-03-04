using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Torque struct.</summary>
    public static class Torque
    {
        #region Units

        /// <summary>Units for Torque measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Torque measurement.</summary>
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
                throw new Exception("There is a bug in " + nameof(Towel) + ". (" + nameof(Torque) + "." + nameof(ConversionFactor) + " attempted on like units)");
            }
            throw new NotImplementedException(nameof(Towel) + " is missing a conversion factor in " + nameof(Torque) + " for " + a + " -> " + b + ".");
        }

        #endregion
    }

    /// <summary>An Torque measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Torque measurement.</typeparam>
    public struct Torque<T>
    {
        internal T _measurement;
        internal Torque.Units _units;

        #region Constructors

        /// <summary>Constructs an Torque with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Torque.</param>
        /// <param name="units">The units of the Torque.</param>
        public Torque(T measurement, Torque.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Torque.</summary>
        public Torque.Units Units
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
        internal T this[Torque.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Torque.ConversionFactor<T>(this._units, units);
                    return Compute.Multiply(this._measurement, factor);
                }
            }
        }

        ///// <summary>Gets the measurement in XXXXX.</summary>
        //public T XXXXX
        //{
        //    get
        //    {
        //        return this[Torque.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Torque<T> Add(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Torque<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Torque<T> operator +(Torque<T> a, Torque<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Torque<T> Subtract(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Torque<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Torque<T> operator -(Torque<T> a, Torque<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Torque<T> Multiply(Torque<T> a, T b)
        {
            return new Torque<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Torque<T> Multiply(T b, Torque<T> a)
        {
            return new Torque<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Torque<T> operator *(Torque<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Torque<T> operator *(T b, Torque<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Torque<T> Divide(Torque<T> a, T b)
        {
            return new Torque<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Torque<T> operator /(Torque<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Torque<T> a, Torque<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Torque<T> a, Torque<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Torque<T> a, Torque<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Torque<T> left, Torque<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Torque<T> a, Torque<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Torque<T> a, Torque<T> b)
        {
            Torque.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Torque<T> a, Torque<T> b)
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
                //case Torque.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Torque<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Torque<T>)
            {
                return this == ((Torque<T>)obj);
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
