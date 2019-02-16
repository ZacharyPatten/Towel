using System;
using System.Linq.Expressions;
using System.Reflection;

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
        public static T ReturnAssign<T>(ref T a, T b)
		{
			a = b;
			return b;
		}

        /// <summary>Performs an "is" type comparison followed by as "as" conversion and performs an operation on the converted value.</summary>
        /// <typeparam name="T">The generic type to convert the object to.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <param name="function">The function to perform if the "is" check returns true.</param>
        /// <returns>The result of the "is" check.</returns>
        public static bool IfAs<T>(object obj, Step<T> function)
            where T : class
        {
            if (obj is T)
            {
                function(obj as T);
                return true;
            }
            else
            {
                return false;
            }
        }
	}
}
