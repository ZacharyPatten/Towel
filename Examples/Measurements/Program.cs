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

            //object result = angle1 + length1; WILL NOT COMPILE (this is a good thing) :)

            #endregion

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Example Complete...");
            Console.ReadLine();
        }
    }
}
