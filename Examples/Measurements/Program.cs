using System;
using Towel.Mathematics;
using Towel.Measurements;

using static Towel.Measurements.MeasurementUnitsSyntax; // allows measurement syntax
using Speedf = Towel.Measurements.Speed<float>;

namespace Measurements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are runnning the Measurements example.");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            #region Specific Measurement Type Examples

            #region Angle

            Console.WriteLine("  Angle--------------------------------------");
            Console.WriteLine();
            Angle<double> angle1 = new Angle<double>(90, Degrees);
            Console.WriteLine("    angle1 = " + angle1);
            Angle<double> angle2 = new Angle<double>(0.5, Turns);
            Console.WriteLine("    angle2 = " + angle2);
            Console.WriteLine("    angle1 + angle2 = " + (angle1 + angle2));
            Console.WriteLine("    angle2 - angle1 = " + (angle2 - angle1));
            Console.WriteLine("    angle1 * 2 = " + (angle1 * 2));
            Console.WriteLine("    angle1 / 2 = " + (angle1 / 2));
            Console.WriteLine("    angle1 > angle2 = " + (angle1 > angle2));
            Console.WriteLine("    angle1 == angle2 = " + (angle1 == angle2));
            Console.WriteLine("    angle1 * 2 == angle2 = " + (angle1 * 2 == angle2));
            Console.WriteLine("    angle1 != angle2 = " + (angle1 != angle2));
            Console.WriteLine();

            #endregion

            #region Length

            Console.WriteLine("  Length--------------------------------------");
            Console.WriteLine();
            Length<double> length1 = new Length<double>(1d, Meters);
            Console.WriteLine("    length1 = " + length1);
            Length<double> length2 = new Length<double>(2d, Yards);
            Console.WriteLine("    length2 = " + length2);
            Console.WriteLine("    length1 + length2 = " + (length1 + length2));
            Console.WriteLine("    length2 - length1 = " + (length2 - length1));
            Console.WriteLine("    length1 * 2 = " + (length1 * 2));
            Console.WriteLine("    length1 / 2 = " + (length1 / 2));
            Console.WriteLine("    length1 > length2 = " + (length1 > length2));
            Console.WriteLine("    length1 == length2 = " + (length1 == length2));
            Console.WriteLine("    length1 * 2 == length2 = " + (length1 * 2 == length2));
            Console.WriteLine("    length1 != length2 = " + (length1 != length2));
            Console.WriteLine();

            //object result = angle1 + length1; // WILL NOT COMPILE (this is a good thing) :)

            #endregion

            #region Mass

            Console.WriteLine("  Mass--------------------------------------");
            Console.WriteLine();
            Mass<double> mass1 = new Mass<double>(1d, Grams);
            Console.WriteLine("    mass1 = " + mass1);
            Mass<double> mass2 = new Mass<double>(2d, Kilograms);
            Console.WriteLine("    mass2 = " + mass2);
            Console.WriteLine("    mass1 + mass2 = " + (mass1 + mass2));
            Console.WriteLine("    mass2 - mass1 = " + (mass2 - mass1));
            Console.WriteLine("    mass1 * 2 = " + (mass1 * 2));
            Console.WriteLine("    mass1 / 2 = " + (mass1 / 2));
            Console.WriteLine("    mass1 > mass2 = " + (mass1 > mass2));
            Console.WriteLine("    mass1 == mass2 = " + (mass1 == mass2));
            Console.WriteLine("    mass1 * 2 == mass2 = " + (mass1 * 2 == mass2));
            Console.WriteLine("    mass1 != mass2 = " + (mass1 != mass2));
            Console.WriteLine();

            #endregion

            #region Time

            Console.WriteLine("  Time--------------------------------------");
            Console.WriteLine();
            Time<double> time1 = new Time<double>(1d, Seconds);
            Console.WriteLine("    time1 = " + time1);
            Time<double> time2 = new Time<double>(2d, Minutes);
            Console.WriteLine("    time2 = " + time2);
            Console.WriteLine("    time1 + time2 = " + (time1 + time2));
            Console.WriteLine("    time2 - time1 = " + (time2 - time1));
            Console.WriteLine("    time1 * 2 = " + (time1 * 2));
            Console.WriteLine("    time1 / 2 = " + (time1 / 2));
            Console.WriteLine("    time1 > time2 = " + (time1 > time2));
            Console.WriteLine("    time1 == time2 = " + (time1 == time2));
            Console.WriteLine("    time1 * 2 == time2 = " + (time1 * 2 == time2));
            Console.WriteLine("    time1 != time2 = " + (time1 != time2));
            Console.WriteLine();

            #endregion

            #region Area

            Console.WriteLine("  Area--------------------------------------");
            Console.WriteLine();
            Area<double> area1 = new Area<double>(1d, Meters * Meters);
            Console.WriteLine("    area1 = " + area1);
            Area<double> area2 = new Area<double>(2d, Yards * Yards);
            Console.WriteLine("    area2 = " + area2);
            Console.WriteLine("    area1 + area2 = " + (area1 + area2));
            Console.WriteLine("    area2 - area1 = " + (area2 - area1));
            Console.WriteLine("    area1 * 2 = " + (area1 * 2));
            Console.WriteLine("    area1 / 2 = " + (area1 / 2));
            Console.WriteLine("    area1 > area2 = " + (area1 > area2));
            Console.WriteLine("    area1 == area2 = " + (area1 == area2));
            Console.WriteLine("    area1 * 2 == area2 = " + (area1 * 2 == area2));
            Console.WriteLine("    area1 != area2 = " + (area1 != area2));
            Console.WriteLine();

            #endregion

            #region Volume

            Console.WriteLine("  Volume--------------------------------------");
            Console.WriteLine();
            Volume<double> volume1 = new Volume<double>(1d, Meters * Meters * Meters);
            Console.WriteLine("    volume1 = " + volume1);
            Volume<double> volume2 = new Volume<double>(2d, Yards * Yards * Yards);
            Console.WriteLine("    volume2 = " + volume2);
            Console.WriteLine("    volume1 + volume2 = " + (volume1 + volume2));
            Console.WriteLine("    volume2 - volume1 = " + (volume2 - volume1));
            Console.WriteLine("    volume1 * 2 = " + (volume1 * 2));
            Console.WriteLine("    volume1 / 2 = " + (volume1 / 2));
            Console.WriteLine("    volume1 > volume2 = " + (volume1 > volume2));
            Console.WriteLine("    volume1 == volume2 = " + (volume1 == volume2));
            Console.WriteLine("    volume1 * 2 == volume2 = " + (volume1 * 2 == volume2));
            Console.WriteLine("    volume1 != volume2 = " + (volume1 != volume2));
            Area<double> area3 = volume1 / length1;
            Console.WriteLine("    volume1 / length1 = " + area3);
            Console.WriteLine();

            #endregion

            #region Speed

            Console.WriteLine("  Speed--------------------------------------");
            Console.WriteLine();
            Speed<double> speed1 = new Speed<double>(1d, Meters / Seconds);
            Console.WriteLine("    speed1 = " + speed1);
            Speed<double> speed2 = new Speed<double>(2d, Inches / Milliseconds);
            Console.WriteLine("    speed2 = " + speed2);
            Console.WriteLine("    speed1 + speed2 = " + (speed1 + speed2));
            Console.WriteLine("    speed2 - speed1 = " + (speed2 - speed1));
            Console.WriteLine("    speed1 * 2 = " + (speed1 * 2));
            Console.WriteLine("    speed1 / 2 = " + (speed1 / 2));
            Console.WriteLine("    speed1 > speed2 = " + (speed1 > speed2));
            Console.WriteLine("    speed1 == speed2 = " + (speed1 == speed2));
            Console.WriteLine("    speed1 * 2 == speed2 = " + (speed1 * 2 == speed2));
            Console.WriteLine("    speed1 != speed2 = " + (speed1 != speed2));
            Console.WriteLine("    speed1 * time2 = " + (speed1 * time2));
            Speed<double> speed3 = new Speed<double>(6d, Knots);
            Console.WriteLine("    speed3 = " + speed3);
            Console.WriteLine("    speed1 + speed3 = " + (speed1 + speed3));
            Console.WriteLine();

            #endregion

            #region Acceleration

            Console.WriteLine("  Acceleration--------------------------------------");
            Console.WriteLine();
            Acceleration<double> acceleration1 = new Acceleration<double>(5d, Meters / Seconds / Seconds);
            Console.WriteLine("    acceleration1 = " + acceleration1);
            Acceleration<double> acceleration2 = new Acceleration<double>(4d, Inches / Milliseconds / Milliseconds);
            Console.WriteLine("    acceleration2 = " + acceleration2);
            Console.WriteLine("    acceleration1 + acceleration2 = " + (acceleration1 + acceleration2));
            Console.WriteLine("    acceleration2 - acceleration1 = " + (acceleration2 - acceleration1));
            Console.WriteLine("    acceleration1 * 2 = " + (acceleration1 * 2));
            Console.WriteLine("    acceleration1 / 2 = " + (acceleration1 / 2));
            Console.WriteLine("    acceleration1 > acceleration2 = " + (acceleration1 > acceleration2));
            Console.WriteLine("    acceleration1 == acceleration2 = " + (acceleration1 == acceleration2));
            Console.WriteLine("    acceleration1 * 2 == acceleration2 = " + (acceleration1 * 2 == acceleration2));
            Console.WriteLine("    acceleration1 != acceleration2 = " + (acceleration1 != acceleration2));
            Console.WriteLine("    acceleration1 * time2 = " + (acceleration1 * time2));
            Console.WriteLine();

            #endregion

            #region Force

            Console.WriteLine("  Force--------------------------------------");
            Console.WriteLine();
            Force<double> force1 = new Force<double>(1d, Kilograms * Meters / Seconds / Seconds);
            Console.WriteLine("    force1 = " + force1);
            Force<double> force2 = new Force<double>(2d, Newtons);
            Console.WriteLine("    force2 = " + force2);
            Console.WriteLine("    force1 + force2 = " + (force1 + force2));
            Console.WriteLine("    force2 - force1 = " + (force2 - force1));
            Console.WriteLine("    force1 * 2 = " + (force1 * 2));
            Console.WriteLine("    force1 / 2 = " + (force1 / 2));
            Console.WriteLine("    force1 > force2 = " + (force1 > force2));
            Console.WriteLine("    force1 == force2 = " + (force1 == force2));
            Console.WriteLine("    force1 * 2 == force2 = " + (force1 * 2 == force2));
            Console.WriteLine("    force1 != force2 = " + (force1 != force2));
            Console.WriteLine();

            #endregion

            #region Electric Current

            Console.WriteLine("  ElectricCurrent--------------------------------------");
            Console.WriteLine();
            ElectricCurrent<double> electricCurrent1 = new ElectricCurrent<double>(5d, Coulombs / Seconds);
            Console.WriteLine("    electricCurrent1 = " + electricCurrent1);
            ElectricCurrent<double> electricCurrent2 = new ElectricCurrent<double>(4d, Amperes);
            Console.WriteLine("    electricCurrent2 = " + electricCurrent2);
            Console.WriteLine("    electricCurrent1 + electricCurrent2 = " + (electricCurrent1 + electricCurrent2));
            Console.WriteLine("    acceleration2 - electricCurrent1 = " + (electricCurrent2 - electricCurrent1));
            Console.WriteLine("    electricCurrent1 * 2 = " + (electricCurrent1 * 2));
            Console.WriteLine("    electricCurrent1 / 2 = " + (electricCurrent1 / 2));
            Console.WriteLine("    electricCurrent1 > electricCurrent2 = " + (electricCurrent1 > electricCurrent2));
            Console.WriteLine("    electricCurrent1 == electricCurrent2 = " + (electricCurrent1 == electricCurrent2));
            Console.WriteLine("    electricCurrent1 * 2 == electricCurrent2 = " + (electricCurrent1 * 2 == electricCurrent2));
            Console.WriteLine("    electricCurrent1 != electricCurrent2 = " + (electricCurrent1 != electricCurrent2));
            Console.WriteLine();

            #endregion

            #endregion

            #region Syntax Sugar Example (removing the generic type via alias)

            // If you hate seeing the "<float>" or "<double>" you can add syntax 
            // sugar to your files with an alias in C#:
            //
            // using Speedf = Towel.Measurements.Speed<float>;

            Speedf speedf = new Speedf(1f, Meters / Seconds);

            #endregion

            #region Vectors Examples

            // You can use measurements inside Vectors in Towel.

            Vector<Speed<float>> velocity1 = new Vector<Speed<float>>(
                new Speed<float>(1f, Meters / Seconds),
                new Speed<float>(2f, Meters / Seconds),
                new Speed<float>(3f, Meters / Seconds));

            Vector<Speedf> velocity2 = new Vector<Speedf>(
                new Speedf(.1f, Centimeters / Seconds),
                new Speedf(.2f, Centimeters / Seconds),
                new Speedf(.3f, Centimeters / Seconds));

            Vector<Speed<float>> velocity3 = velocity1 + velocity2;

            #endregion

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Example Complete...");
            Console.ReadLine();
        }
    }
}
