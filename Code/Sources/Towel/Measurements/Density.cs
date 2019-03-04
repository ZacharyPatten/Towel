using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Desnity struct.</summary>
    public static class Density
    {
        #region Units

        /// <summary>Units for Desnity measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.

            //[ConversionFactor(XXXXX, XXXXX, "XXX")]
            /// <summary>Units of an Desnity measurement.</summary>
			//UNITS = X,
        }

        #endregion
    }

    /// <summary>An Desnity measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Desnity measurement.</typeparam>
    public struct Desnity<T>
    {
        internal static T[][] Table = ConversionTable.Build<Density.Units, T>();
        internal T _measurement;
        internal Density.Units _units;

        #region Constructors

        /// <summary>Constructs an Desnity with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Desnity.</param>
        /// <param name="units">The units of the Desnity.</param>
        public Desnity(T measurement, Density.Units units)
        {
            this._measurement = measurement;
            this._units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Desnity.</summary>
        public Density.Units Units
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
        internal T this[Density.Units units]
        {
            get
            {
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Table[(int)this._units][(int)units];
                    return Compute.Multiply(this._measurement, factor);
                }
            }
        }

        ///// <summary>Gets the measurement in XXXXX.</summary>
        //public T XXXXX
        //{
        //    get
        //    {
        //        return this[Desnity.Units.XXXXX];
        //    }
        //}

        #endregion

        #region Mathematics

        #region Add

        public static Desnity<T> Add(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Desnity<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Desnity<T> operator +(Desnity<T> a, Desnity<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Desnity<T> Subtract(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Desnity<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Desnity<T> operator -(Desnity<T> a, Desnity<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Desnity<T> Multiply(Desnity<T> a, T b)
        {
            return new Desnity<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Desnity<T> Multiply(T b, Desnity<T> a)
        {
            return new Desnity<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Desnity<T> operator *(Desnity<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Desnity<T> operator *(T b, Desnity<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Desnity<T> Divide(Desnity<T> a, T b)
        {
            return new Desnity<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Desnity<T> operator /(Desnity<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Desnity<T> a, Desnity<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Desnity<T> a, Desnity<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Desnity<T> a, Desnity<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Desnity<T> left, Desnity<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Desnity<T> a, Desnity<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Desnity<T> a, Desnity<T> b)
        {
            Density.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Desnity<T> a, Desnity<T> b)
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
                //case Desnity.Units.Degrees: return this._measurement.ToString() + "°";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Desnity<T>) + ".");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Desnity<T>)
            {
                return this == ((Desnity<T>)obj);
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
