namespace Towel
{
    internal static class RegularExpressions
    {
        private static string spacePattern = @"[\s]*";
        private static string identPattern = @"[_a-zA-Z][_a-zA-Z0-9]*";
        private static string paramPattern = @"(ref|out)?" + spacePattern + identPattern + spacePattern + identPattern;
        private static string openParenPattern = @"\(";
        private static string closeParenPattern = @"\)";

        /// <summary>Pattern for matching C# method calls in a file.</summary>
        internal static string methodPattern =
                  identPattern + spacePattern
                + identPattern + spacePattern
                + openParenPattern + spacePattern
                + "(" + paramPattern + spacePattern + "," + spacePattern + ")*" + spacePattern
                + "(" + paramPattern + spacePattern + ")?" + spacePattern
                + closeParenPattern;
    }
}
