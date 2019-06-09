using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic Acceleration struct.</summary>
    public static class Acceleration
    {
        /// <summary>Units for acceleration measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.


            /// <summary>Units of an Acceleration measurement.</summary>
            //[Units(Length.Units.NauticalMiles, Time.Units.Seconds, Time.Units.Seconds)]
            //Gravities,
        }

        #region Units Mapping

        internal static (Length.Units, Time.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal Length.Units LengthUnits;
            internal Time.Units TimeUnits1;
            internal Time.Units TimeUnits2;

            internal UnitsAttribute(Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2)
            {
                LengthUnits = lengthUnits;
                TimeUnits1 = timeUnits1;
                TimeUnits2 = timeUnits2;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(Acceleration.Units accelerationUnits, out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.LengthUnits, unitsAttribute.TimeUnits1, unitsAttribute.TimeUnits2);
                }).ToArray();
            }

            (Length.Units, Time.Units, Time.Units) mapping = UnitMapings[(int)accelerationUnits];
            lengthUnits = mapping.Item1;
            timeUnits1 = mapping.Item2;
            timeUnits2 = mapping.Item3;
        }

        #endregion
    }

    /// <summary>An Acceleration measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the Acceleration measurement.</typeparam>
    [Serializable]
    public struct Acceleration<T>
    {
        internal T _measurement;
        internal Length.Units _lengthUnits;
        internal Time.Units _timeUnits1;
        internal Time.Units _timeUnits2;

        #region Constructors

        /// <summary>Constructs an Acceleration with the specified measurement and units.</summary>
        /// <param name="measurement">The value of the measurement.</param>
        /// <param name="units">The units of the measurement.</param>
        public Acceleration(T measurement, MeasurementUnitsSyntaxTypes.AccelerationUnits units) :
            this(measurement, units.LengthUnits, units.TimeUnits1, units.TimeUnits2)
        { }

        /// <summary>Constructs an Acceleration with the specified measurement and units.</summary>
        /// <param name="measurement">The value of the measurement.</param>
        /// <param name="units">The units of the measurement.</param>
        public Acceleration(T measurement, Acceleration.Units units)
        {
            _measurement = measurement;
            Acceleration.Map(units, out _lengthUnits, out _timeUnits1, out _timeUnits2);
        }

        /// <summary>Constructs an Acceleration with the specified measurement and units.</summary>
        /// <param name="measurement">The value of the measurement.</param>
        /// <param name="lengthUnits">The length [A] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        /// <param name="timeUnits1">The first time [B] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        /// <param name="timeUnits2">The second time [C] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        public Acceleration(T measurement, Length.Units lengthUnits, Time.Units timeUnits1, Time.Units timeUnits2)
        {
            _measurement = measurement;
            _lengthUnits = lengthUnits;
            _timeUnits1 = timeUnits1;
            _timeUnits2 = timeUnits2;
        }

        #endregion

        #region Properties

        /// <summary>The length [A] component of the units: Length[A] / Time1[B] / Time2[C].</summary>
        public Length.Units LengthUnits
        {
            get { return _lengthUnits; }
            set
            {
                if (value != _lengthUnits)
                {
                    _measurement = this[value, _timeUnits1, _timeUnits2];
                    _lengthUnits = value;
                }
            }
        }

        /// <summary>The first time [B] component of the units: Length[A] / Time1[B] / Time2[C].</summary>
        public Time.Units TimeUnits1
        {
            get { return _timeUnits1; }
            set
            {
                if (value != _timeUnits1)
                {
                    _measurement = this[_lengthUnits, value, _timeUnits2];
                    _timeUnits1 = value;
                }
            }
        }

        /// <summary>The second time [C] component of the units: Length[A] / Time1[B] / Time2[C].</summary>
        public Time.Units TimeUnits2
        {
            get { return _timeUnits2; }
            set
            {
                if (value != _timeUnits2)
                {
                    _measurement = this[_lengthUnits, _timeUnits1, value];
                    _timeUnits2 = value;
                }
            }
        }

        /// <summary>Gets the measurement value in the specified units.</summary>
        /// <param name="units">The units to get the measurement value in.</param>
        /// <returns>The measurment value in the specified units.</returns>
        public T this[MeasurementUnitsSyntaxTypes.AccelerationUnits units]
        {
            get
            {
                return this[units.LengthUnits, units.TimeUnits1, units.TimeUnits2];
            }
        }

        /// <summary>Gets the measurement value in the specified units.</summary>
        /// <param name="units">The units to get the measurement value in.</param>
        /// <returns>The measurment value in the specified units.</returns>
        public T this[Acceleration.Units units]
        {
            get
            {
                Acceleration.Map(units, out Length.Units lengthUnits, out Time.Units timeUnits1, out Time.Units timeUnits2);
                return this[lengthUnits, timeUnits1, timeUnits2];
            }
        }

        /// <summary>Gets the measurement value in the specified units.</summary>
        /// <param name="lengthUnits">The length [A] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        /// <param name="timeUnits1">The first time [B] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        /// <param name="timeUnits2">The second time [C] component of the units: Length[A] / Time1[B] / Time2[C].</param>
        /// <returns>The measurment value in the specified units.</returns>
        public T this[
            Length.Units lengthUnits,
            Time.Units timeUnits1,
            Time.Units timeUnits2]
        {
            get
            {
                T measurement = _measurement;
                if (lengthUnits != _lengthUnits)
                {
                    if (lengthUnits < _lengthUnits)
                    {
                        measurement = Length<T>.Table[(int)_lengthUnits][(int)lengthUnits](measurement);
                    }
                    else
                    {
                        measurement = Length<T>.Table[(int)lengthUnits][(int)_lengthUnits](measurement);
                    }
                }
                if (timeUnits1 != _timeUnits1)
                {
                    if (timeUnits1 > _timeUnits1)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits1][(int)timeUnits1](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits1][(int)_timeUnits1](measurement);
                    }
                }
                if (timeUnits2 != _timeUnits2)
                {
                    if (timeUnits2 > _timeUnits2)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits2][(int)timeUnits2](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits2][(int)_timeUnits2](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Mathematics

        #region Bases

        internal static Acceleration<T> MathBase(Acceleration<T> a, Acceleration<T> b, Func<T, T, T> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[lengthUnits, timeUnits1, timeUnits2];
            T B = b[lengthUnits, timeUnits1, timeUnits2];
            T C = func(A, B);

            return new Acceleration<T>(C, lengthUnits, timeUnits1, timeUnits2);
        }

        internal static bool LogicBase(Acceleration<T> a, Acceleration<T> b, Func<T, T, bool> func)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[lengthUnits, timeUnits1, timeUnits2];
            T B = b[lengthUnits, timeUnits1, timeUnits2];

            return func(A, B);
        }

        #endregion

        #region Add

        /// <summary>Adds two acceleration measurements.</summary>
        /// <param name="a">The first operand of the addition.</param>
        /// <param name="b">The second operand of the addition.</param>
        /// <returns>The result of the addition operation.</returns>
        public static Acceleration<T> Add(Acceleration<T> a, Acceleration<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        /// <summary>Adds two acceleration measurements.</summary>
        /// <param name="a">The first operand of the addition.</param>
        /// <param name="b">The second operand of the addition.</param>
        /// <returns>The result of the addition operation.</returns>
        public static Acceleration<T> operator +(Acceleration<T> a, Acceleration<T> b)
        {
            return Add(a, b);
        }

        /// <summary>Adds two acceleration measurements.</summary>
        /// <param name="b">The second operand of the addition.</param>
        /// <returns>The result of the addition operation.</returns>
        public Acceleration<T> Add(Acceleration<T> b)
        {
            return this + b;
        }

        #endregion

        #region Subtract

        /// <summary>Subtracts two acceleration measurements.</summary>
        /// <param name="a">The first operand of the subtraction.</param>
        /// <param name="b">The second operand of the subtraction.</param>
        /// <returns>The result of the addition subtraction.</returns>
        public static Acceleration<T> Subtract(Acceleration<T> a, Acceleration<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        /// <summary>Subtracts two acceleration measurements.</summary>
        /// <param name="a">The first operand of the subtraction.</param>
        /// <param name="b">The second operand of the subtraction.</param>
        /// <returns>The result of the addition subtraction.</returns>
        public static Acceleration<T> operator -(Acceleration<T> a, Acceleration<T> b)
        {
            return Subtract(a, b);
        }

        /// <summary>Subtracts two acceleration measurements.</summary>
        /// <param name="b">The second operand of the subtraction.</param>
        /// <returns>The result of the addition subtraction.</returns>
        public Acceleration<T> Subtract(Acceleration<T> b)
        {
            return this - b;
        }

        #endregion

        #region Multiply

        /// <summary>Multiplies an acceleration by a scalar numeric value.</summary>
        /// <param name="a">The acceleration measurement to multiply.</param>
        /// <param name="b">The scalar numeric value to multiply the measurement by.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Acceleration<T> Multiply(Acceleration<T> a, T b)
        {
            return new Acceleration<T>(Compute.Multiply(a._measurement, b), a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        /// <summary>Multiplies an acceleration by a scalar numeric value.</summary>
        /// <param name="a">The acceleration measurement to multiply.</param>
        /// <param name="b">The scalar numeric value to multiply the measurement by.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Acceleration<T> Multiply(T b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies an acceleration by a scalar numeric value.</summary>
        /// <param name="a">The acceleration measurement to multiply.</param>
        /// <param name="b">The scalar numeric value to multiply the measurement by.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Acceleration<T> operator *(Acceleration<T> a, T b)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies an acceleration by a scalar numeric value.</summary>
        /// <param name="a">The acceleration measurement to multiply.</param>
        /// <param name="b">The scalar numeric value to multiply the measurement by.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Acceleration<T> operator *(T b, Acceleration<T> a)
        {
            return Multiply(b, a);
        }

        /// <summary>Multiplies an acceleration by a scalar numeric value.</summary>
        /// <param name="b">The scalar numeric value to multiply the measurement by.</param>
        /// <returns>The result of the multiplication.</returns>
        public Acceleration<T> Add(T b)
        {
            return this * b;
        }

        /// <summary>Multiplies an Accleration measurement by a Time measurement resulting in a Speed measurement.</summary>
        /// <param name="a">The Acceleration measurement to multiply by a Time measurement.</param>
        /// <param name="b">The Time measurement to multiply by an Acceleration measurement.</param>
        /// <returns>The Speed measurement result from the multiplication.</returns>
        public static Speed<T> Multiply(Acceleration<T> a, Time<T> b)
        {
            Time.Units TIME1 = a._timeUnits1 <= b._units ? a._timeUnits1 : b._units;

            T A = a[a._lengthUnits, TIME1, a._timeUnits2];
            T B = b[TIME1];
            T C = Compute.Multiply(A, B);

            return new Speed<T>(C, a._lengthUnits, a._timeUnits2);
        }

        /// <summary>Multiplies an Accleration measurement by a Time measurement resulting in a Speed measurement.</summary>
        /// <param name="a">The Acceleration measurement to multiply by a Time measurement.</param>
        /// <param name="b">The Time measurement to multiply by an Acceleration measurement.</param>
        /// <returns>The Speed measurement result from the multiplication.</returns>
        public static Speed<T> Multiply(Time<T> b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies an Accleration measurement by a Time measurement resulting in a Speed measurement.</summary>
        /// <param name="a">The Acceleration measurement to multiply by a Time measurement.</param>
        /// <param name="b">The Time measurement to multiply by an Acceleration measurement.</param>
        /// <returns>The Speed measurement result from the multiplication.</returns>
        public static Speed<T> operator *(Acceleration<T> a, Time<T> b)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies an Accleration measurement by a Time measurement resulting in a Speed measurement.</summary>
        /// <param name="a">The Acceleration measurement to multiply by a Time measurement.</param>
        /// <param name="b">The Time measurement to multiply by an Acceleration measurement.</param>
        /// <returns>The Speed measurement result from the multiplication.</returns>
        public static Speed<T> operator *(Time<T> b, Acceleration<T> a)
        {
            return Multiply(a, b);
        }

        /// <summary>Multiplies an Accleration measurement by a Time measurement resulting in a Speed measurement.</summary>
        /// <param name="b">The Time measurement to multiply by an Acceleration measurement.</param>
        /// <returns>The Speed measurement result from the multiplication.</returns>
        public Speed<T> Multiply(Time<T> b)
        {
            return this * b;
        }

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="mass">The Mass measurement to multiply the Acceleration measurement by.</param>
        /// <param name="acceleration">The Acceleration measurement to multiply the Mass measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public static Force<T> Multiply(Mass<T> mass, Acceleration<T> acceleration)
        {
            T measurement = Compute.Multiply(mass._measurement, acceleration._measurement);
            return new Force<T>(measurement, mass._units, acceleration._lengthUnits, acceleration._timeUnits1, acceleration._timeUnits2);
        }

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="mass">The Mass measurement to multiply the Acceleration measurement by.</param>
        /// <param name="acceleration">The Acceleration measurement to multiply the Mass measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public static Force<T> Multiply(Acceleration<T> acceleration, Mass<T> mass)
        {
            return Multiply(mass, acceleration);
        }

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="mass">The Mass measurement to multiply the Acceleration measurement by.</param>
        /// <param name="acceleration">The Acceleration measurement to multiply the Mass measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public static Force<T> operator *(Mass<T> mass, Acceleration<T> acceleration)
        {
            return Multiply(mass, acceleration);
        }

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="mass">The Mass measurement to multiply the Acceleration measurement by.</param>
        /// <param name="acceleration">The Acceleration measurement to multiply the Mass measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public static Force<T> operator *(Acceleration<T> acceleration, Mass<T> mass)
        {
            return Multiply(mass, acceleration);
        }

        /// <summary>Multiplies an Accleration measurement by a Mass measurement resulting in a Force measurement.</summary>
        /// <param name="mass">The Mass measurement to multiply the Acceleration measurement by.</param>
        /// <returns>The Force measurement result from the multiplication.</returns>
        public Force<T> Multiply(Mass<T> mass)
        {
            return mass * this;
        }

        #endregion

        #region Divide

        /// <summary>Divides this acceleration measurement by a numaric scalar value.</summary>
        /// <param name="a">The acceleration measurement to divide.</param>
        /// <param name="b">The numeric scalar to divide by.</param>
        /// <returns>The result of the division.</returns>
        public static Acceleration<T> Divide(Acceleration<T> a, T b)
        {
            return new Acceleration<T>(Compute.Divide(a._measurement, b), a._lengthUnits, a._timeUnits1, a._timeUnits2);
        }

        /// <summary>Divides this acceleration measurement by a numaric scalar value.</summary>
        /// <param name="a">The acceleration measurement to divide.</param>
        /// <param name="b">The numeric scalar to divide by.</param>
        /// <returns>The result of the division.</returns>
        public static Acceleration<T> operator /(Acceleration<T> a, T b)
        {
            return Divide(a, b);
        }

        /// <summary>Divides this acceleration measurement by a numaric scalar value.</summary>
        /// <param name="b">The numeric scalar to divide by.</param>
        /// <returns>The result of the division.</returns>
        public Acceleration<T> Divide(T b)
        {
            return this / b;
        }

        /// <summary>Divides an Acceleration measurement by another Acceleration measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(Acceleration<T> a, Acceleration<T> b)
        {
            Length.Units lengthUnits = a._lengthUnits <= b._lengthUnits ? a._lengthUnits : b._lengthUnits;
            Time.Units timeUnits1 = a._timeUnits1 <= b._timeUnits1 ? a._timeUnits1 : b._timeUnits1;
            Time.Units timeUnits2 = a._timeUnits2 <= b._timeUnits2 ? a._timeUnits2 : b._timeUnits2;

            T A = a[lengthUnits, timeUnits1, timeUnits2];
            T B = b[lengthUnits, timeUnits1, timeUnits2];

            return Compute.Divide(A, B);
        }

        /// <summary>Divides an Acceleration measurement by another Acceleration measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T operator /(Acceleration<T> a, Acceleration<T> b)
        {
            return Divide(a, b);
        }

        /// <summary>Divides an Acceleration measurement by another Acceleration measurement resulting in a scalar numeric value.</summary>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public T Divide(Acceleration<T> b)
        {
            return this / b;
        }

        #endregion

        #region LessThan

        /// <summary>Determines if an Acceleration measurement is less than another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the less than operation.</param>
        /// <param name="b">The second operand of the less than operation.</param>
        /// <returns>True if the first operand is less than the second operand. False if not.</returns>
        public static bool LessThan(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is less than another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the less than operation.</param>
        /// <param name="b">The second operand of the less than operation.</param>
        /// <returns>True if the first operand is less than the second operand. False if not.</returns>
        public static bool operator <(Acceleration<T> a, Acceleration<T> b)
        {
            return LessThan(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is less than another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the less than operation.</param>
        /// <returns>True if the first operand is less than the second operand. False if not.</returns>
        public bool LessThan(Acceleration<T> b)
        {
            return this < b;
        }

        #endregion

        #region GreaterThan

        /// <summary>Determines if an Acceleration measurement is greater than another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the greater than operation.</param>
        /// <param name="b">The second operand of the greater than operation.</param>
        /// <returns>True if the first operand is greater than the second operand. False if not.</returns>
        public static bool GreaterThan(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is greater than another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the greater than operation.</param>
        /// <param name="b">The second operand of the greater than operation.</param>
        /// <returns>True if the first operand is greater than the second operand. False if not.</returns>
        public static bool operator >(Acceleration<T> a, Acceleration<T> b)
        {
            return GreaterThan(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is greater than another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the greater than operation.</param>
        /// <returns>True if the first operand is greater than the second operand. False if not.</returns>
        public bool GreaterThan(Acceleration<T> b)
        {
            return this > b;
        }

        #endregion

        #region LessThanOrEqual

        /// <summary>Determines if an Acceleration measurement is less than or equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the less than or equal to operation.</param>
        /// <param name="b">The second operand of the less than or equal to operation.</param>
        /// <returns>True if the first operand is less than or equal to the second operand. False if not.</returns>
        public static bool LessThanOrEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is less than or equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the less than or equal to operation.</param>
        /// <param name="b">The second operand of the less than or equal to operation.</param>
        /// <returns>True if the first operand is less than or equal to the second operand. False if not.</returns>
        public static bool operator <=(Acceleration<T> a, Acceleration<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is less than or equal to another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the less than or equal to operation.</param>
        /// <returns>True if the first operand is less than or equal to the second operand. False if not.</returns>
        public bool LessThanOrEqual(Acceleration<T> b)
        {
            return this <= b;
        }

        #endregion

        #region GreaterThanOrEqual

        /// <summary>Determines if an Acceleration measurement is greater than or equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the greater than or equal to operation.</param>
        /// <param name="b">The second operand of the greater than or equal to operation.</param>
        /// <returns>True if the first operand is greater than or equal to the second operand. False if not.</returns>
        public static bool GreaterThanOrEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is greater than or equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the greater than or equal to operation.</param>
        /// <param name="b">The second operand of the greater than or equal to operation.</param>
        /// <returns>True if the first operand is greater than or equal to the second operand. False if not.</returns>
        public static bool operator >=(Acceleration<T> a, Acceleration<T> b)
        {
            return GreaterThanOrEqual(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is greater than or equal to another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the greater than or equal to operation.</param>
        /// <returns>True if the first operand is greater than or equal to the second operand. False if not.</returns>
        public bool GreaterThanOrEqual(Acceleration<T> b)
        {
            return this >= b;
        }

        #endregion

        #region Equal

        /// <summary>Determines if an Acceleration measurement is equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the equal to operation.</param>
        /// <param name="b">The second operand of the equal to operation.</param>
        /// <returns>True if the first operand is equal to the second operand. False if not.</returns>
        public static bool Equal(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the equal to operation.</param>
        /// <param name="b">The second operand of the equal to operation.</param>
        /// <returns>True if the first operand is equal to the second operand. False if not.</returns>
        public static bool operator ==(Acceleration<T> a, Acceleration<T> b)
        {
            return Equal(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is equal to another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the equal to operation.</param>
        /// <returns>True if the first operand is equal to the second operand. False if not.</returns>
        public bool Equal(Acceleration<T> b)
        {
            return this == b;
        }

        #endregion

        #region NotEqual

        /// <summary>Determines if an Acceleration measurement is not equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the not equal to operation.</param>
        /// <param name="b">The second operand of the not equal to operation.</param>
        /// <returns>True if the first operand is not equal to the second operand. False if not.</returns>
        public static bool NotEqual(Acceleration<T> a, Acceleration<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        /// <summary>Determines if an Acceleration measurement is not equal to another Acceleration measurement.</summary>
        /// <param name="a">The first operand of the not equal to operation.</param>
        /// <param name="b">The second operand of the not equal to operation.</param>
        /// <returns>True if the first operand is not equal to the second operand. False if not.</returns>
        public static bool operator !=(Acceleration<T> a, Acceleration<T> b)
        {
            return NotEqual(a, b);
        }

        /// <summary>Determines if an Acceleration measurement is not equal to another Acceleration measurement.</summary>
        /// <param name="b">The second operand of the not equal to operation.</param>
        /// <returns>True if the first operand is not equal to the second operand. False if not.</returns>
        public bool NotEqual(Acceleration<T> b)
        {
            return this != b;
        }

        #endregion

        #endregion

        #region Overrides

        /// <summary>Converts the Acceleration measurement to a string represenation.</summary>
        /// <returns>The string representation of the measurement.</returns>
        public override string ToString()
        {
            return _measurement + " " + _lengthUnits + "/(" + _timeUnits1 + "*" + _timeUnits2 + ")";
        }

        /// <summary>Base Equals override that performs a type and value equality check.</summary>
        /// <param name="obj">The object to check for equality with.</param>
        /// <returns>True if the types and values equal. False if not.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Acceleration<T>)
            {
                return this == ((Acceleration<T>)obj);
            }
            return false;
        }

        /// <summary>Base hashing function for Acceleration measurements.</summary>
        /// <returns>Computed hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _lengthUnits.GetHashCode() ^
                _timeUnits1.GetHashCode() ^
                _timeUnits2.GetHashCode();
        }

        #endregion
    }
}
