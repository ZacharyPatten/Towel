using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Measurements
{
    public static class MeasurementUnitsSyntaxTypes
    {
        public struct AngleUnits
        {
            public Angle.Units Units;

            public static implicit operator AngleUnits(Angle.Units units)
            {
                return new AngleUnits() { Units = units, };
            }
        }

        public struct LengthUnits
        {
            public Length.Units Units;

            public static implicit operator LengthUnits(Length.Units units)
            {
                return new LengthUnits() { Units = units, };
            }

            public static SpeedUnits operator /(LengthUnits lengthUnits, TimeUnits timeUnits)
            {
                return new SpeedUnits() { LengthUnits = lengthUnits.Units, TimeUnits = timeUnits.Units, };
            }
        }

        public struct MassUnits
        {
            public Mass.Units Units;

            public static implicit operator MassUnits(Mass.Units units)
            {
                return new MassUnits() { Units = units, };
            }
        }

        public struct TimeUnits
        {
            public Time.Units Units;

            public static implicit operator TimeUnits(Time.Units units)
            {
                return new TimeUnits() { Units = units, };
            }
        }

        public struct SpeedUnits
        {
            public Length.Units LengthUnits;
            public Time.Units TimeUnits;

            public static implicit operator SpeedUnits(Speed.Units units)
            {
                SpeedUnits speedUnits = new SpeedUnits();
                Speed.Map(units, out speedUnits.LengthUnits, out speedUnits.TimeUnits);
                return speedUnits;
            }

            public static AccelerationUnits operator /(SpeedUnits speedUnits, TimeUnits timeUnits)
            {
                return new AccelerationUnits()
                {
                    LengthUnits = speedUnits.LengthUnits,
                    TimeUnits1 = speedUnits.TimeUnits,
                    TimeUnits2 = timeUnits.Units,
                };
            }
        }

        public struct AccelerationUnits
        {
            public Length.Units LengthUnits;
            public Time.Units TimeUnits1;
            public Time.Units TimeUnits2;

            public static implicit operator AccelerationUnits(Acceleration.Units units)
            {
                AccelerationUnits accelerationUnits = new AccelerationUnits();
                Acceleration.Map(units,
                    out accelerationUnits.LengthUnits,
                    out accelerationUnits.TimeUnits1,
                    out accelerationUnits.TimeUnits2);
                return accelerationUnits;
            }
        }
    }

    /// <summary>Provides syntax for measurement unit definition. Intended to be referenced via "using static" keyword in files.</summary>
    public static class MeasurementUnitsSyntax
    {
        /// <summary>Units of an angle measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.AngleUnits Gradians = Angle.Units.Gradians;
        /// <summary>Units of an angle measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.AngleUnits Degrees = Angle.Units.Degrees;
        /// <summary>Units of an angle measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.AngleUnits Radians = Angle.Units.Radians;
        /// <summary>Units of an angle measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.AngleUnits Turns = Angle.Units.Turns;

        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Yoctometers = Length.Units.Yoctometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Zeptometers = Length.Units.Zeptometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Attometers = Length.Units.Attometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Femtometers = Length.Units.Femtometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Picometers = Length.Units.Picometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Nanometers = Length.Units.Nanometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Micrometers = Length.Units.Micrometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Millimeters = Length.Units.Millimeters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Centimeters = Length.Units.Centimeters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Inches = Length.Units.Inches;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Decimeters = Length.Units.Decimeters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Feet = Length.Units.Feet;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Yards = Length.Units.Yards;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Meters = Length.Units.Meters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Dekameters = Length.Units.Dekameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Hectometers = Length.Units.Hectometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Kilometers = Length.Units.Kilometers;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Miles = Length.Units.Miles;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits NauticalMiles = Length.Units.NauticalMiles;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Megameters = Length.Units.Megameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Gigameters = Length.Units.Gigameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Terameters = Length.Units.Terameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Petameters = Length.Units.Petameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Exameters = Length.Units.Exameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Zettameters = Length.Units.Zettameters;
        /// <summary>Units of an length measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.LengthUnits Yottameters = Length.Units.Yottameters;

        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Yoctograms = Mass.Units.Yoctograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Zeptograms = Mass.Units.Zeptograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Attograms = Mass.Units.Attograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Femtograms = Mass.Units.Femtograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Picograms = Mass.Units.Picograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Nanograms = Mass.Units.Nanograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Micrograms = Mass.Units.Micrograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Milligrams = Mass.Units.Milligrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Centigrams = Mass.Units.Centigrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Decigrams = Mass.Units.Decigrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Grams = Mass.Units.Grams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Dekagrams = Mass.Units.Dekagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Hectograms = Mass.Units.Hectograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Kilograms = Mass.Units.Kilograms;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Megagrams = Mass.Units.Megagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Gigagrams = Mass.Units.Gigagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Teragrams = Mass.Units.Teragrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Petagrams = Mass.Units.Petagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Exagrams = Mass.Units.Exagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Zettagrams = Mass.Units.Zettagrams;
        /// <summary>Units of an mass measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.MassUnits Yottagrams = Mass.Units.Yottagrams;

        /// <summary>Units of an speed measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.SpeedUnits Knots = Speed.Units.Knots;

        /// <summary>Units of an time measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.TimeUnits Milliseconds = Time.Units.Milliseconds;
        /// <summary>Units of an time measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.TimeUnits Seconds = Time.Units.Seconds;
        /// <summary>Units of an time measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.TimeUnits Minutes = Time.Units.Minutes;
        /// <summary>Units of an time measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.TimeUnits Hours = Time.Units.Hours;
        /// <summary>Units of an time measurement.</summary>
        public static MeasurementUnitsSyntaxTypes.TimeUnits Days = Time.Units.Days;
    }
}
