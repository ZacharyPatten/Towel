namespace Towel
{
    /// <summary>Just syntax sugar. Not recommended for use outside the Towel Framework.</summary>
    internal class Code
    {
        /// <summary>Performs an assignment and returns the value of the assignment.</summary>
        /// <typeparam name="T">The type of the assignment and return.</typeparam>
        /// <param name="a">The variable to assign a value to.</param>
        /// <param name="b">The value of the assignment and return.</param>
        /// <returns>The value of th assignment.</returns>
        internal static T ReturnAssign<T>(ref T a, T b)
        {
            a = b;
            return b;
        }
    }
}
