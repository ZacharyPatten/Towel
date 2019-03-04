using System;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Angle struct.</summary>
    public static class Angle
    {
        #region Units

        /// <summary>Units for angle measurements.</summary>
        public enum Units
        {
            // Note: It is critical that these enum values are in increasing order of size.
            // Their value is used as a priority when doing operations on measurements in
            // different units.
            [ConversionFactor(Degrees, Gradians, "10 / 9")]
            [ConversionFactor(Degrees, Turns, "1 / 360")]
            /// <summary>Units of an angle measurement.</summary>
            Degrees = 2,
            /// <summary>Units of an angle measurement.</summary>
			Radians = 3,
            [ConversionFactor(Turns, Gradians, "360")]
            [ConversionFactor(Turns, Degrees, "400")]
            /// <summary>Units of an angle measurement.</summary>
			Turns = 4,
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

            internal static T GradiansToDegrees = ConversionFactorAttribute.Get(Units.Gradians, Units.Degrees).Value<T>();
            internal static T GradiansToRadians = ConversionFactorAttribute.Get(Units.Gradians, Units.Radians).Value<T>();
            internal static T GradiansToTurns = ConversionFactorAttribute.Get(Units.Gradians, Units.Turns).Value<T>();

            internal static T DegreesToGradians = ConversionFactorAttribute.Get(Units.Degrees, Units.Gradians).Value<T>();
            internal static T DegreesToRadians = ConversionFactorAttribute.Get(Units.Degrees, Units.Radians).Value<T>();
            internal static T DegreesToTurns = ConversionFactorAttribute.Get(Units.Degrees, Units.Turns).Value<T>();

            internal static T RadiansToGradians = ConversionFactorAttribute.Get(Units.Radians, Units.Gradians).Value<T>();
            internal static T RadiansToDegrees = ConversionFactorAttribute.Get(Units.Radians, Units.Degrees).Value<T>();
            internal static T RadiansToTurns = ConversionFactorAttribute.Get(Units.Radians, Units.Turns).Value<T>();

            internal static T TurnsToGradians = ConversionFactorAttribute.Get(Units.Turns, Units.Gradians).Value<T>();
            internal static T TurnsToDegrees = ConversionFactorAttribute.Get(Units.Turns, Units.Degrees).Value<T>();
            internal static T TurnsToRadians = ConversionFactorAttribute.Get(Units.Turns, Units.Radians).Value<T>();
        }

        internal static T ConversionFactor<T>(Units a, Units b)
        {
            Conversion conversion = new Conversion(a, b);
            switch (conversion)
            {
                case var C when C.A == Units.Gradians && C.B == Units.Degrees: return ConversionConstant<T>.GradiansToDegrees;
                case var C when C.A == Units.Gradians && C.B == Units.Radians: return ConversionConstant<T>.GradiansToRadians;
                case var C when C.A == Units.Gradians && C.B == Units.Turns: return ConversionConstant<T>.GradiansToTurns;

                case var C when C.A == Units.Degrees && C.B == Units.Gradians: return ConversionConstant<T>.DegreesToGradians;
                case var C when C.A == Units.Degrees && C.B == Units.Radians: return ConversionConstant<T>.DegreesToRadians;
                case var C when C.A == Units.Degrees && C.B == Units.Turns: return ConversionConstant<T>.DegreesToTurns;

                case var C when C.A == Units.Radians && C.B == Units.Degrees: return ConversionConstant<T>.RadiansToGradians;
                case var C when C.A == Units.Radians && C.B == Units.Gradians: return ConversionConstant<T>.RadiansToDegrees;
                case var C when C.A == Units.Radians && C.B == Units.Turns: return ConversionConstant<T>.RadiansToTurns;

                case var C when C.A == Units.Turns && C.B == Units.Degrees: return ConversionConstant<T>.TurnsToGradians;
                case var C when C.A == Units.Turns && C.B == Units.Gradians: return ConversionConstant<T>.TurnsToDegrees;
                case var C when C.A == Units.Turns && C.B == Units.Radians: return ConversionConstant<T>.TurnsToRadians;
            }
            if (a == b)
            {
                throw new Exception("There is a bug in " + nameof(Towel) + ". (" + nameof(Angle) + "." + nameof(ConversionFactor) + " attempted on like units)");
            }
            throw new NotImplementedException(nameof(Towel) + " is missing a conversion factor in " + nameof(Angle) + " for " + a + " -> " + b + ".");
        }
        
		#endregion
	}

	/// <summary>An angle measurement.</summary>
	/// <typeparam name="T">The generic numeric type used to store the angle measurement.</typeparam>
	public struct Angle<T>
	{
		internal T _measurement;
		internal Angle.Units _units;

		#region Constructors

        /// <summary>Constructs an angle with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the angle.</param>
        /// <param name="units">The units of the angle.</param>
		public Angle(T measurement, Angle.Units units)
		{
			this._measurement = measurement;
			this._units = units;
		}

        #endregion

		#region Properties

		/// <summary>The current units used to represent the angle.</summary>
		public Angle.Units Units
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
		internal T this[Angle.Units units]
		{
			get
			{
                if (this._units == units)
                {
                    return this._measurement;
                }
                else
                {
                    T factor = Angle.ConversionFactor<T>(this._units, units);
                    return Compute.Multiply(this._measurement, factor);
                }
			}
		}

        /// <summary>Gets the measurement in Gradians.</summary>
		public T Gradians
        {
            get
            {
                return this[Angle.Units.Gradians];
            }
        }

        /// <summary>Gets the measurement in Degrees.</summary>
		public T Degrees
        {
            get
            {
                return this[Angle.Units.Degrees];
            }
        }

        /// <summary>Gets the measurement in Radians.</summary>
		public T Radians
        {
            get
            {
                return this[Angle.Units.Radians];
            }
        }

        /// <summary>Gets the measurement in Turns.</summary>
		public T Turns
        {
            get
            {
                return this[Angle.Units.Turns];
            }
        }

        #endregion

        #region Mathematics

        #region Add

        public static Angle<T> Add(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Angle<T>(Compute.Add(a[units], b[units]), units);
        }

        public static Angle<T> operator +(Angle<T> a, Angle<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static Angle<T> Subtract(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return new Angle<T>(Compute.Subtract(a[units], b[units]), units);
        }

        public static Angle<T> operator -(Angle<T> a, Angle<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static Angle<T> Multiply(Angle<T> a, T b)
        {
            return new Angle<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Angle<T> Multiply(T b, Angle<T> a)
        {
            return new Angle<T>(Compute.Multiply(a._measurement, b), a._units);
        }

        public static Angle<T> operator *(Angle<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static Angle<T> operator *(T b, Angle<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static Angle<T> Divide(Angle<T> a, T b)
        {
            return new Angle<T>(Compute.Divide(a._measurement, b), a._units);
        }

        public static Angle<T> operator /(Angle<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThan(a[units], b[units]);
        }

        public static bool operator <(Angle<T> a, Angle<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThan(a[units], b[units]);
        }

        public static bool operator >(Angle<T> a, Angle<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.LessThanOrEqual(a[units], b[units]);
        }

        public static bool operator <=(Angle<T> a, Angle<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.GreaterThanOrEqual(a[units], b[units]);
        }

        public static bool operator >=(Angle<T> left, Angle<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.Equal(a[units], b[units]);
        }

        public static bool operator ==(Angle<T> a, Angle<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(Angle<T> a, Angle<T> b)
        {
            Angle.Units units = a.Units <= b.Units ? a.Units : b.Units;
            return Compute.NotEqual(a[units], b[units]);
        }

        public static bool operator !=(Angle<T> a, Angle<T> b)
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
                case Angle.Units.Degrees: return this._measurement.ToString() + "°";
                case Angle.Units.Gradians: return this._measurement.ToString() + "ᵍ";
                case Angle.Units.Radians: return this._measurement.ToString() + "rad";
                case Angle.Units.Turns: return this._measurement.ToString() + "turn";
                default: throw new NotImplementedException(nameof(Towel) + " is missing a to string conversion in " + nameof(Angle<T>) + ".");
            }
        }

		public override bool Equals(object obj)
		{
            if (obj is Angle<T>)
            {
                return this == ((Angle<T>)obj);
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
