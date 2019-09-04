using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace Towel
{
	/// <summary>Breaks type safeness by making assumptions about types.</summary>
	public static class Assume
	{
		public static bool TryParse<T>(string @string, out T value)
		{
			return TryParseImplementation<T>.Function(@string, out value);
		}

		private static class TryParseImplementation<T>
		{
			internal delegate bool TryParseDelegate(string @string, out T value);

			internal static TryParseDelegate Function = (string @string, out T value) =>
			{
				Type type = typeof(T);
				Type[] parameterTypes = new Type[] { typeof(string), type.MakeByRefType() };
				MethodInfo methodInfo =
					type.GetMethod("TryParse",
						BindingFlags.Static |
						BindingFlags.Public |
						BindingFlags.NonPublic,
						null,
						parameterTypes,
						null);
				Function =
					methodInfo is null
					?
					(string _string, out T _value) =>
					{
						_value = default;
						return false;
					}
				:
					(TryParseDelegate)methodInfo.CreateDelegate(typeof(TryParseDelegate));
				return Function(@string, out value);
			};
		}
	}
}
