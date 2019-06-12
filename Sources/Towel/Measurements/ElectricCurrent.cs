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

            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Yoctocoulombs, Time.Units.Seconds)]
            Yoctoampheres = 0,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Zeptocoulombs, Time.Units.Seconds)]
            Zeptoampheres = 1,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Attocoulombs, Time.Units.Seconds)]
            Attoampheres = 2,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Femtocoulombs, Time.Units.Seconds)]
            Femtoampheres = 3,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Picocoulombs, Time.Units.Seconds)]
            Picoampheres = 4,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Nanocoulombs, Time.Units.Seconds)]
            Nanoampheres = 5,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Microcoulombs, Time.Units.Seconds)]
            Microampheres = 6,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Millicoulombs, Time.Units.Seconds)]
            Milliampheres = 7,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Centicoulombs, Time.Units.Seconds)]
            Centiampheres = 8,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Decicoulombs, Time.Units.Seconds)]
            Deciampheres = 9,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Coulombs, Time.Units.Seconds)]
            Amperes = 10,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Dekacoulombs, Time.Units.Seconds)]
            Dekaampheres = 11,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Hectocoulombs, Time.Units.Seconds)]
            Hectoampheres = 12,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Kilocoulombs, Time.Units.Seconds)]
            Kiloampheres = 13,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Megacoulombs, Time.Units.Seconds)]
            Megaampheres = 14,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Gigacoulombs, Time.Units.Seconds)]
            Gigaampheres = 15,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Teracoulombs, Time.Units.Seconds)]
            Teraampheres = 16,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Petacoulombs, Time.Units.Seconds)]
            Petaampheres = 17,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Exacoulombs, Time.Units.Seconds)]
            Exaampheres = 18,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Zettacoulombs, Time.Units.Seconds)]
            Zettaampheres = 19,
            /// <summary>Units of an electric charge measurement.</summary>
            [Units(ElectricCharge.Units.Yottacoulombs, Time.Units.Seconds)]
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
    public partial struct ElectricCurrent<T>
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

        public T this[MeasurementUnitsSyntaxTypes.ElectricCurrentUnits units]
        {
            get
            {
                return this[units.ElectricChargeUnits, units.TimeUnits];
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

        #region Custom Mathematics

        #region Bases

        internal static ElectricCurrent<T> MathBase(ElectricCurrent<T> a, T b, Func<T, T, T> func)
        {
            return new ElectricCurrent<T>(func(a._measurement, b), a._electricChargeUnits, a._timeUnits);
        }

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

        #region Divide

        /// <summary>Divides an ElectricCurrent measurement by another ElectricCurrent measurement resulting in a scalar numeric value.</summary>
        /// <param name="a">The first operand of the division operation.</param>
        /// <param name="b">The second operand of the division operation.</param>
        /// <returns>The scalar numeric value result from the division.</returns>
        public static T Divide(ElectricCurrent<T> a, ElectricCurrent<T> b)
        {
            ElectricCharge.Units electricChargeUnits = a._electricChargeUnits <= b._electricChargeUnits ? a._electricChargeUnits : b._electricChargeUnits;
            Time.Units timeUnits = a._timeUnits <= b._timeUnits ? a._timeUnits : b._timeUnits;

            T A = a[electricChargeUnits, timeUnits];
            T B = b[electricChargeUnits, timeUnits];

            return Compute.Divide(A, B);
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
