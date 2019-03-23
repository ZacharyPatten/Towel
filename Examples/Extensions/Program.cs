using System;
using Towel.DataStructures;
using Towel.Mathematics;

namespace Extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are runnning the Extensions example.");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            #region Decimal To Words

            Console.WriteLine("  Converting Decimal To Words---------------------------");
            Console.WriteLine();

            decimal value1 = 12345.6789m;
            Console.WriteLine("    Value1 = " + value1);
            Console.WriteLine("    Value1 To Words = " + value1.ToWords());
            Console.WriteLine();

            decimal value2 = 999.888m;
            Console.WriteLine("    Value2 = " + value2);
            Console.WriteLine("    Value2 To Words = " + value2.ToWords());
            Console.WriteLine();

            decimal value3 = 1111111.2m;
            Console.WriteLine("    Value3 = " + value3);
            Console.WriteLine("    Value3 To Words = " + value3.ToWords());
            Console.WriteLine();

            #endregion

            #region Type To C# Source Code

            Console.WriteLine("  Type To C# Source Code---------------------------");
            Console.WriteLine();
            Console.WriteLine("    Note: this can be useful for runtime compilation from strings");
            Console.WriteLine();

            Console.WriteLine("    " + typeof(OmnitreePoints<Vector<double>, double, double, double>).ConvertToCsharpSource());
            Console.WriteLine();
            Console.WriteLine("    " + typeof(Symbolics.Add).ConvertToCsharpSource());
            Console.WriteLine();

            #endregion

            #region Random Extensions

            Console.WriteLine("  Random Extensions---------------------------");
            Console.WriteLine();
            Console.WriteLine("    Note: there are overloads of these methods");
            Console.WriteLine();

            Random random = new Random();

            Console.WriteLine("    Random.NextLong(): " + random.NextLong());
            Console.WriteLine("    Random.NextDateTime(): " + random.NextDateTime());
            Console.WriteLine("    Random.NextAlphaNumericString(15): " + random.NextAlphaNumericString(15));
            Console.WriteLine("    Random.NextChar('a', 'z'): " + random.NextChar('a', 'z'));
            Console.WriteLine("    Random.NextDecimal(): " + random.NextDecimal());
            Console.WriteLine("    Random.NextTimeSpan(): " + random.NextTimeSpan());
            Console.WriteLine();

            #endregion

            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Example Complete...");
            Console.ReadLine();
        }
    }
}
