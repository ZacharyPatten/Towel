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
            
            #region Angles

            Console.WriteLine("  Angles--------------------------------------");
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

            Console.WriteLine("  Lengths--------------------------------------");
            Console.WriteLine();
            Length<double> length1 = new Length<double>(1d, Length.Units.Meters);
            Length<double> length2 = new Length<double>(2d, Length.Units.Yards);
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
