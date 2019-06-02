using System;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    /// <summary>Contains unit types and conversion factors for the generic ElectricCurrent struct.</summary>
	public static class ElectricCurrent
    {
        /// <summary>Units for electric current measurements.</summary>
        [Serializable]
        public enum Units
        {
            // Enum values must be 0, 1, 2, 3... as they are used for array look ups.
            // They need not be in any specific order as they are converted into the
            // relative base units.

            [Units(ElectricCharge.Units.Yoctocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Yoctoampheres = 0,
            [Units(ElectricCharge.Units.Zeptocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Zeptoampheres = 1,
            [Units(ElectricCharge.Units.Attocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Attoampheres = 2,
            [Units(ElectricCharge.Units.Femtocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Femtoampheres = 3,
            [Units(ElectricCharge.Units.Picocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Picoampheres = 4,
            [Units(ElectricCharge.Units.Nanocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Nanoampheres = 5,
            [Units(ElectricCharge.Units.Microcoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Microampheres = 6,
            [Units(ElectricCharge.Units.Millicoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Milliampheres = 7,
            [Units(ElectricCharge.Units.Centicoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Centiampheres = 8,
            [Units(ElectricCharge.Units.Decicoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Deciampheres = 9,
            [Units(ElectricCharge.Units.Coulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Amperes = 10,
            [Units(ElectricCharge.Units.Dekacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Dekaampheres = 11,
            [Units(ElectricCharge.Units.Hectocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Hectoampheres = 12,
            [Units(ElectricCharge.Units.Kilocoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Kiloampheres = 13,
            [Units(ElectricCharge.Units.Megacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Megaampheres = 14,
            [Units(ElectricCharge.Units.Gigacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Gigaampheres = 15,
            [Units(ElectricCharge.Units.Teracoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Teraampheres = 16,
            [Units(ElectricCharge.Units.Petacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Petaampheres = 17,
            [Units(ElectricCharge.Units.Exacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Exaampheres = 18,
            [Units(ElectricCharge.Units.Zettacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Zettaampheres = 19,
            [Units(ElectricCharge.Units.Yottacoulombs, Time.Units.Seconds)]
            /// <summary>Units of an electric charge measurement.</summary>
            Yottaampheres = 20,
        }

        #region Units Mapping

        internal static (ElectricCharge.Units, Time.Units)[] UnitMapings;

        [AttributeUsage(AttributeTargets.Field)]
        internal class UnitsAttribute : Attribute
        {
            internal ElectricCharge.Units ElectricChargeUnits;
            internal Time.Units TimeUnits;

            internal UnitsAttribute(ElectricCharge.Units electricChargeUnits, Time.Units timeUnits)
            {
                ElectricChargeUnits = electricChargeUnits;
                TimeUnits = timeUnits;
            }
        }

        /// <summary>Maps a units to relative base units.</summary>
        public static void Map(ElectricCurrent.Units electricCurrentUnits, out ElectricCharge.Units electricChargeUnits, out Time.Units timeUnits)
        {
            if (UnitMapings is null)
            {
                UnitMapings = Enum.GetValues(typeof(Units)).Cast<Units>().Select(x =>
                {
                    UnitsAttribute unitsAttribute = x.GetEnumAttribute<UnitsAttribute>();
                    return (unitsAttribute.ElectricChargeUnits, unitsAttribute.TimeUnits);
                }).ToArray();
            }

            (ElectricCharge.Units, Time.Units) mapping = UnitMapings[(int)electricCurrentUnits];
            electricChargeUnits = mapping.Item1;
            timeUnits = mapping.Item2;
        }

        #endregion
    }

    /// <summary>An ElectricCurrent measurement.</summary>
    /// <typeparam name="T">The generic numeric type used to store the ElectricCurrent measurement.</typeparam>
    [Serializable]
    public struct ElectricCurrent<T>
    {
        internal T _measurement;
        internal ElectricCharge.Units _electricChargeUnits;
        internal Time.Units _timeUnits;

        #region Constructors

        public ElectricCurrent(T measurement, MeasurementUnitsSyntaxTypes.ElectricCurrentUnits units) : this(measurement, units.ElectricChargeUnits, units.TimeUnits) { }

        /// <summary>Constructs an ElectricCurrent with the specified measurement and units.</summary>
        /// <param name="measurement">The measurement of the ElectricCurrent.</param>
        /// <param name="units">The units of the ElectricCurrent.</param>
        public ElectricCurrent(T measurement, ElectricCharge.Units electricChargeUnits, Time.Units timeUnits)
        {
            _measurement = measurement;
            _electricChargeUnits = electricChargeUnits;
            _timeUnits = timeUnits;
        }

        #endregion

        #region Properties

        public ElectricCharge.Units ElectricChargeUnits
        {
            get { return _electricChargeUnits; }
            set
            {
                if (value != _electricChargeUnits)
                {
                    _measurement = this[value, _timeUnits];
                    _electricChargeUnits = value;
                }
            }
        }

        public Time.Units TimeUnits
        {
            get { return _timeUnits; }
            set
            {
                if (value != _timeUnits)
                {
                    _measurement = this[_electricChargeUnits, value];
                    _timeUnits = value;
                }
            }
        }

        /// <summary>Gets the measurement in the desired units.</summary>
        /// <param name="units">The units you want the measurement to be in.</param>
        /// <returns>The measurement in the specified units.</returns>
        public T this[ElectricCurrent.Units units]
        {
            get
            {
                ElectricCurrent.Map(units, out ElectricCharge.Units electricChargeUnits, out Time.Units timeUnits);
                return this[electricChargeUnits, timeUnits];
            }
        }

        public T this[ElectricCharge.Units electricChargeUnits, Time.Units timeUnits]
        {
            get
            {
                T measurement = _measurement;
                if (electricChargeUnits != _electricChargeUnits)
                {
                    if (electricChargeUnits < _electricChargeUnits)
                    {
                        measurement = ElectricCharge<T>.Table[(int)_electricChargeUnits][(int)electricChargeUnits](measurement);
                    }
                    else
                    {
                        measurement = ElectricCharge<T>.Table[(int)electricChargeUnits][(int)_electricChargeUnits](measurement);
                    }
                }
                if (timeUnits != _timeUnits)
                {
                    if (timeUnits > _timeUnits)
                    {
                        measurement = Time<T>.Table[(int)_timeUnits][(int)timeUnits](measurement);
                    }
                    else
                    {
                        measurement = Time<T>.Table[(int)timeUnits][(int)_timeUnits](measurement);
                    }
                }
                return measurement;
            }
        }

        #endregion

        #region Mathematics

        #region Bases

        internal static ElectricCurrent<T> MathBase(ElectricCurrent<T> a, ElectricCurrent<T> b, Func<T, T, T> func)
        {
            ElectricCharge.Units electricChargeUnits = a._electricChargeUnits <= b._electricChargeUnits ? a._electricChargeUnits : b._electricChargeUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[electricChargeUnits, timeUnits];
            T B = b[electricChargeUnits, timeUnits];
            T C = func(A, B);

            return new ElectricCurrent<T>(C, electricChargeUnits, timeUnits);
        }

        internal static bool LogicBase(ElectricCurrent<T> a, ElectricCurrent<T> b, Func<T, T, bool> func)
        {
            ElectricCharge.Units electricChargeUnits = a._electricChargeUnits <= b._electricChargeUnits ? a._electricChargeUnits : b._electricChargeUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[electricChargeUnits, timeUnits];
            T B = b[electricChargeUnits, timeUnits];

            return func(A, B);
        }

        #endregion

        #region Add

        public static ElectricCurrent<T> Add(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return MathBase(a, b, Compute.AddImplementation<T>.Function);
        }

        public static ElectricCurrent<T> operator +(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return Add(a, b);
        }

        #endregion

        #region Subtract

        public static ElectricCurrent<T> Subtract(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return MathBase(a, b, Compute.SubtractImplementation<T>.Function);
        }

        public static ElectricCurrent<T> operator -(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return Subtract(a, b);
        }

        #endregion

        #region Multiply

        public static ElectricCurrent<T> Multiply(ElectricCurrent<T> a, T b)
        {
            return new ElectricCurrent<T>(Compute.Multiply(a._measurement, b), a._electricChargeUnits, a._timeUnits);
        }

        public static ElectricCurrent<T> Multiply(T b, ElectricCurrent<T> a)
        {
            return Multiply(a, b);
        }

        public static ElectricCurrent<T> operator *(ElectricCurrent<T> a, T b)
        {
            return Multiply(a, b);
        }

        public static ElectricCurrent<T> operator *(T b, ElectricCurrent<T> a)
        {
            return Multiply(b, a);
        }

        #endregion

        #region Divide

        public static ElectricCurrent<T> Divide(ElectricCurrent<T> a, T b)
        {
            return new ElectricCurrent<T>(Compute.Divide(a._measurement, b), a._electricChargeUnits, a._timeUnits);
        }

        public static ElectricCurrent<T> operator /(ElectricCurrent<T> a, T b)
        {
            return Divide(a, b);
        }

        #endregion

        #region LessThan

        public static bool LessThan(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.LessThanImplementation<T>.Function);
        }

        public static bool operator <(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LessThan(a, b);
        }

        #endregion

        #region GreaterThan

        public static bool GreaterThan(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanImplementation<T>.Function);
        }

        public static bool operator >(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return GreaterThan(a, b);
        }

        #endregion

        #region LessThanOrEqual

        public static bool LessThanOrEqual(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.LessThanOrEqualImplementation<T>.Function);
        }

        public static bool operator <=(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LessThanOrEqual(a, b);
        }

        #endregion

        #region GreaterThanOrEqual

        public static bool GreaterThanOrEqual(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.GreaterThanOrEqualImplementation<T>.Function);
        }

        public static bool operator >=(ElectricCurrent<T> left, ElectricCurrent<T> right)
        {
            return GreaterThanOrEqual(left, right);
        }

        #endregion

        #region Equal

        public static bool Equal(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.EqualImplementation<T>.Function);
        }

        public static bool operator ==(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return Equal(a, b);
        }

        public static bool NotEqual(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return LogicBase(a, b, Compute.NotEqualImplementation<T>.Function);
        }

        public static bool operator !=(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            return NotEqual(a, b);
        }

        #endregion

        #endregion

        #region Overrides

        public override string ToString()
        {
            return _measurement + " " + _electricChargeUnits + "/" + _timeUnits;
        }

        public override bool Equals(object obj)
        {
            if (obj is ElectricCurrent<T>)
            {
                return this == ((ElectricCurrent<T>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return
                _measurement.GetHashCode() ^
                _electricChargeUnits.GetHashCode() ^
                _timeUnits.GetHashCode();
        }

        #endregion
    }
}
