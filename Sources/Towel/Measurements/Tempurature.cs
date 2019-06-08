using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Tempurature struct.</summary>
    public static class Tempurature
    {
        /// <summary>Units for Tempurature measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They also need to be in order of preferred autoconversion.

            /// <summary>Units of an Tempurature measurement.</summary>
            Kelvin = 0,
            /// <summary>Units of an Tempurature measurement.</summary>
            Celsius = 1,
            /// <summary>Units of an Tempurature measurement.</summary>
            Fahrenheit = 2,
        }

        public static Func<T, T>[][] BuildConversionTable<T>()
        {
            T A = (Symbolics.Parse<T>("273.15").Simplify() as Symbolics.Constant<T>).Value;
            T B = (Symbolics.Parse<T>("9 / 5").Simplify() as Symbolics.Constant<T>).Value;
            T C = (Symbolics.Parse<T>("459.67").Simplify() as Symbolics.Constant<T>).Value;
            T D = (Symbolics.Parse<T>("32").Simplify() as Symbolics.Constant<T>).Value;

            Func<T, T>[][] table = Extensions.ConstructSquareJagged<Func<T, T>>(3);

            table[(int)Units.Kelvin][(int)Units.Kelvin] = x => x;
            table[(int)Units.Kelvin][(int)Units.Celsius] = x => Compute.Subtract(x, A);
            table[(int)Units.Kelvin][(int)Units.Fahrenheit] = x => Compute.Subtract(Compute.Multiply(x, B), C);

            table[(int)Units.Celsius][(int)Units.Celsius] = x => x;
            table[(int)Units.Celsius][(int)Units.Kelvin] = x => Compute.Add(x, A);
            table[(int)Units.Celsius][(int)Units.Fahrenheit] = x => Compute.Add(Compute.Multiply(x, B), D);

            table[(int)Units.Fahrenheit][(int)Units.Fahrenheit] = x => x;
            table[(int)Units.Fahrenheit][(int)Units.Celsius] = x => Compute.Divide(Compute.Subtract(x, D), B);
            table[(int)Units.Fahrenheit][(int)Units.Kelvin] = x => Compute.Divide(Compute.Add(x, C), B);

            return table;
        }
    }

    /// <summary>An Tempurature measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Tempurature measurement.</typeparam>
    [Serializable]
    public struct Tempurature<T>
    {
        internal static Func<T, T>[][] Table = Tempurature.BuildConversionTable<T>();
        internal T _measurement;
        internal Tempurature.Units _units;

        #region Constructors

        /// <summary>Constructs an Tempurature with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the Tempurature.</param>
        /// <param name="units">The units of the Tempurature.</param>
        public Tempurature(T measurement, Tempurature.Units units)
        {
            _measurement = measurement;
            _units = units;
        }

        #endregion

        #region Properties

        /// <summary>The current units used to represent the Tempurature.</summary>
        public Tempurature.Units Units
        {
            get { return _units; }
            set
            {
                if (value != _units)
                {
                    _measurement = this[value];
                    _units = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[Tempurature.Units units]
        {
            get
            {
                if (_units == units)
                {
                    return _measurement;
                }
                else
                {
                    return Table[(int)_units][(int)units](_measurement);
                }
            }
        }

        #endregion

        #region Mathematics

        #region Add

        public static Tempurature<T> Add(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Tempurature<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Tempurature<T> operator +(Tempurature<T> a, Tempurature<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Tempurature<T> Subtract(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Tempurature<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Tempurature<T> operator -(Tempurature<T> a, Tempurature<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Tempurature<T> Multiply(Tempurature<T> a, T b)
        {
            return new Tempurature<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Tempurature<T> Multiply(T b, Tempurature<T> a)
        {
            return new Tempurature<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Tempurature<T> operator *(Tempurature<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Tempurature<T> operator *(T b, Tempurature<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Tempurature<T> Divide(Tempurature<T> a, T b)
        {
            return new Tempurature<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Tempurature<T> operator /(Tempurature<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Tempurature<T> a, Tempurature<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Tempurature<T> a, Tempurature<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Tempurature<T> a, Tempurature<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Tempurature<T> left, Tempurature<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Tempurature<T> a, Tempurature<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Tempurature<T> a, Tempurature<T> b)
        {
            Tempurature.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Tempurature<T> a, Tempurature<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            switch (_units)
            {
                default: return _measurement + " " + _units;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Tempurature<T>)
            {
                return this == ((Tempurature<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _measurement.GetHashCode() ^ _units.GetHashCode();
        }

        #endregion
    }
}
