using System;

namespace Towel
{
    public struct ChanceSyntax
    {
        public static Random Algorithm = new Random();
        //public static ChanceSyntax Chance => default;
        public static bool operator %(double value, ChanceSyntax chance) =>
            value < 0 ? throw new ArgumentOutOfRangeException(nameof(chance)) :
            value > 100 ? throw new ArgumentOutOfRangeException(nameof(chance)) :
            value is 100 ? true :
            value is 0 ? false :
            Algorithm.NextDouble() < value / 100;
    }
}
