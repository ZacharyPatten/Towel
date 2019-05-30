using System;
using Towel.Measurements;

namespace Measurements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are runnning the Measurements example.");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            #region Angle

            Console.WriteLine("  Angle--------------------------------------");
            Console.WriteLine();
            Angle<double> angle1 = new Angle<double>(90, Angle.Units.Degrees);
            Console.WriteLine("    angle1 = " + angle1);
            Angle<double> angle2 = new Angle<double>(0.5, Angle.Units.Turns);
            Console.WriteLine("    angle2 = " + angle2);
            Console.WriteLine("    angle1 + angle2 = " + (angle1 + angle2));
            Console.WriteLine("    angle2 - angle1 = " + (angle1 - angle2));
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
            Length<double> length1 = new Length<double>(1d, Length.Units.Meters);
            Console.WriteLine("    length1 = " + length1);
            Length<double> length2 = new Length<double>(2d, Length.Units.Yards);
            Console.WriteLine("    length2 = " + length2);
            Console.WriteLine("    length1 + length2 = " + (length1 + length2));
            Console.WriteLine("    length2 - length1 = " + (length1 - length2));
            Console.WriteLine("    length1 * 2 = " + (length1 * 2));
            Console.WriteLine("    length1 / 2 = " + (length1 / 2));
            Console.WriteLine("    length1 > length2 = " + (length1 > length2));
            Console.WriteLine("    length1 == length2 = " + (length1 == length2));
            Console.WriteLine("    length1 * 2 == length2 = " + (length1 * 2 == length2));
            Console.WriteLine("    length1 != length2 = " + (length1 != length2));
            Console.WriteLine();

            //object result = angle1 + length1; WILL NOT COMPILE (this is a good thing) :)

            #endregion

            #region Mass

            Console.WriteLine("  Mass--------------------------------------");
            Console.WriteLine();
            Mass<double> mass1 = new Mass<double>(1d, Mass.Units.Grams);
            Console.WriteLine("    mass1 = " + mass1);
            Mass<double> mass2 = new Mass<double>(2d, Mass.Units.Kilograms);
            Console.WriteLine("    mass2 = " + mass2);
            Console.WriteLine("    mass1 + mass2 = " + (mass1 + mass2));
            Console.WriteLine("    mass2 - mass1 = " + (mass1 - mass2));
            Console.WriteLine("    mass1 * 2 = " + (mass1 * 2));
            Console.WriteLine("    mass1 / 2 = " + (mass1 / 2));
            Console.WriteLine("    mass1 > mass2 = " + (mass1 > mass2));
            Console.WriteLine("    mass1 == mass2 = " + (mass1 == mass2));
            Console.WriteLine("    mass1 * 2 == mass2 = " + (mass1 * 2 == mass2));
            Console.WriteLine("    mass1 != mass2 = " + (mass1 != mass2));
            Console.WriteLine();

            //object result = angle1 + mass1; WILL NOT COMPILE (this is a good thing) :)

            #endregion

            #region Time

            Console.WriteLine("  Time--------------------------------------");
            Console.WriteLine();
            Time<double> time1 = new Time<double>(1d, Time.Units.Seconds);
            Console.WriteLine("    time1 = " + time1);
            Time<double> time2 = new Time<double>(2d, Time.Units.Minutes);
            Console.WriteLine("    time2 = " + time2);
            Console.WriteLine("    time1 + time2 = " + (time1 + time2));
            Console.WriteLine("    time2 - time1 = " + (time1 - time2));
            Console.WriteLine("    time1 * 2 = " + (time1 * 2));
            Console.WriteLine("    time1 / 2 = " + (time1 / 2));
            Console.WriteLine("    time1 > time2 = " + (time1 > time2));
            Console.WriteLine("    time1 == time2 = " + (time1 == time2));
            Console.WriteLine("    time1 * 2 == time2 = " + (time1 * 2 == time2));
            Console.WriteLine("    time1 != time2 = " + (time1 != time2));
            Console.WriteLine();

            #endregion

            #region Speed

            Console.WriteLine("  Speed--------------------------------------");
            Console.WriteLine();
            Speed<double> speed1 = new Speed<double>(1d, Length.Units.Meters, Time.Units.Seconds);
            Console.WriteLine("    speed1 = " + speed1);
            Speed<double> speed2 = new Speed<double>(2d, Length.Units.Inches, Time.Units.Milliseconds);
            Console.WriteLine("    speed2 = " + speed2);
            Console.WriteLine("    speed1 + speed2 = " + (speed1 + speed2));
            Console.WriteLine("    speed2 - speed1 = " + (speed1 - speed2));
            Console.WriteLine("    speed1 * 2 = " + (speed1 * 2));
            Console.WriteLine("    speed1 / 2 = " + (speed1 / 2));
            Console.WriteLine("    speed1 > speed2 = " + (speed1 > speed2));
            Console.WriteLine("    speed1 == speed2 = " + (speed1 == speed2));
            Console.WriteLine("    speed1 * 2 == speed2 = " + (speed1 * 2 == speed2));
            Console.WriteLine("    speed1 != speed2 = " + (speed1 != speed2));
            Console.WriteLine("    speed1 * time2 = " + (speed1 * time2));
            Speed<double> speed3 = new Speed<double>(6d, Speed.Units.Knots);
            Console.WriteLine("    speed3 = " + speed3);
            Console.WriteLine("    speed1 + speed3 = " + (speed1 + speed3));
            Console.WriteLine();

            #endregion

            #region Acceleration

            Console.WriteLine("  Acceleration--------------------------------------");
            Console.WriteLine();
            Acceleration<double> acceleration1 = new Acceleration<double>(5d, Length.Units.Meters, Time.Units.Seconds, Time.Units.Seconds);
            Console.WriteLine("    acceleration1 = " + acceleration1);
            Acceleration<double> acceleration2 = new Acceleration<double>(4d, Length.Units.Inches, Time.Units.Milliseconds,  Time.Units.Milliseconds);
            Console.WriteLine("    acceleration2 = " + acceleration2);
            Console.WriteLine("    acceleration1 + acceleration2 = " + (acceleration1 + acceleration2));
            Console.WriteLine("    acceleration2 - acceleration1 = " + (acceleration1 - acceleration2));
            Console.WriteLine("    acceleration1 * 2 = " + (acceleration1 * 2));
            Console.WriteLine("    acceleration1 / 2 = " + (acceleration1 / 2));
            Console.WriteLine("    acceleration1 > acceleration2 = " + (acceleration1 > acceleration2));
            Console.WriteLine("    acceleration1 == acceleration2 = " + (acceleration1 == acceleration2));
            Console.WriteLine("    acceleration1 * 2 == acceleration2 = " + (acceleration1 * 2 == acceleration2));
            Console.WriteLine("    acceleration1 != acceleration2 = " + (acceleration1 != acceleration2));
            Console.WriteLine("    acceleration1 * time2 = " + (acceleration1 * time2));
            Console.WriteLine();

            #endregion

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Example Complete...");
            Console.ReadLine();
        }
    }
}
