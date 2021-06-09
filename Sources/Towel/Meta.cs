using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Towel.DataStructures;
using System.Linq;
using static Towel.Statics;

namespace Towel
{
	/// <summary>Constains static analysis methods of the code (reflection).</summary>
	public static class Meta
	{
		#region Getting Methods Via Reflection

		#region System.Type.GetMethod

		#if false
		/// <summary>Gets a method on a type by signature.</summary>
		/// <typeparam name="Signature">
		/// The signature of the method to get as a delegate type. Must match the 
		/// method in return type, parameter types, generic types, parameter names,
		/// generic parameter names, and delegate/method name.
		/// </typeparam>
		/// <param name="declaringType">The declaring type of the method to get.</param>
		/// <returns>The method info if found. null if not.</returns>
		public static MethodInfo GetMethod<Signature>(this Type declaringType)
			where Signature : Delegate
		{
			Type signature = typeof(Signature);
			string name = Regex.Replace(signature.Name, @"`\d+", string.Empty);
			Type[] signatureGenerics = signature.GetGenericArguments();
			MethodInfo signatureMethodInfo = signature.GetMethod("Invoke");
			ParameterInfo[] signatureParameters = signatureMethodInfo.GetParameters();
			Type signatureGeneric = signature.GetGenericTypeDefinition();
			Type[] signatureGenericGenerics = signatureGeneric.GetGenericArguments();
			foreach (MethodInfo currentMethodInfo in declaringType.GetMethods(
				BindingFlags.Instance |
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic))
			{
				MethodInfo methodInfo = currentMethodInfo;
				if (methodInfo.Name != name ||
					signature.IsGenericType != methodInfo.IsGenericMethod)
				{
					continue;
				}
				if (signature.IsGenericType)
				{
					Type[] methodInfoGenerics = methodInfo.GetGenericArguments();
					if (methodInfoGenerics.Length != signatureGenerics.Length ||
						!Equate<Type>(signatureGenericGenerics, methodInfoGenerics, (a, b) => a.Name == b.Name))
					{
						continue;
					}
					try
					{
						methodInfo = methodInfo.MakeGenericMethod(signatureGenerics);
					}
					catch (ArgumentException)
					{
						// this is likely a contraint validation error
						continue;
					}
				}
				if (signatureMethodInfo.ReturnType != methodInfo.ReturnType ||
					!Equate<ParameterInfo>(signatureParameters, methodInfo.GetParameters(), (a, b) => a.ParameterType == b.ParameterType && a.Name == b.Name))
				{
					continue;
				}
				return methodInfo;
			}
			return null;
		}
		#endif

		#endregion

		#region GetTryParseMethod

		/// <summary>Gets the TryParse <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> TryParse(<see cref="string"/>, out <typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The TryParse <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetTryParseMethod<A>() => GetTryParseMethodCache<A>.Value;

		/// <summary>Gets the TryParse <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> TryParse(<see cref="string"/>, out <paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The TryParse <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetTryParseMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("TryParse",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(typeof(string), a.MakeByRefType()),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
				? methodInfo
				: null;
		}

		internal static class GetTryParseMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetTryParseMethod(typeof(A));
		}

		#endregion

		#region GetFactorialMethod

		/// <summary>Gets the Factorial <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> Factorial(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsPrime <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetFactorialMethod<A>() => GetFactorialMethodCache<A>.Value;

		/// <summary>Gets the Factorial <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> Factorial(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsNonNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetFactorialMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("Factorial",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetFactorialMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetFactorialMethod(typeof(A));
		}

		#endregion

		#region GetIsPrimeMethod

		/// <summary>Gets the IsPrime <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsPrime(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsPrime <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsPrimeMethod<A>() => GetIsPrimeMethodCache<A>.Value;

		/// <summary>Gets the IsPrime <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsPrime(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsNonNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsPrimeMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("IsPrime",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
				? methodInfo
				: null;
		}

		internal static class GetIsPrimeMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsPrimeMethod(typeof(A));
		}

		#endregion

		#region GetIsNonNegativeMethod

		/// <summary>Gets the IsNonNegative <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsNonNegative(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsNonNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsNonNegativeMethod<A>() => GetIsNonNegativeMethodCache<A>.Value;

		/// <summary>Gets the IsNonNegative <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsNonNegative(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsNonNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsNonNegativeMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("IsNonNegative",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
				? methodInfo
				: null;
		}

		internal static class GetIsNonNegativeMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsNonNegativeMethod(typeof(A));
		}

		#endregion

		#region GetIsNegativeMethod

		/// <summary>Gets the IsNegative <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsNegative(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsNegativeMethod<A>() => GetIsNegativeMethodCache<A>.Value;

		/// <summary>Gets the IsNegative <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsNegative(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsNegative <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsNegativeMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("IsNegative",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetIsNegativeMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsNegativeMethod(typeof(A));
		}

		#endregion

		#region GetIsPositiveMethod

		/// <summary>Gets the IsPositive <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsPositive(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsPositive <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsPositiveMethod<A>() => GetIsPositiveMethodCache<A>.Value;

		/// <summary>Gets the IsPositive <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsPositive(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsPositive <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsPositiveMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod("IsPositive",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetIsPositiveMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsPositiveMethod(typeof(A));
		}

		#endregion

		#region GetIsEvenMethod

		/// <summary>Gets the IsEven <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsEven(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsEven <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsEvenMethod<A>() => GetIsEvenMethodCache<A>.Value;

		/// <summary>Gets the IsEven <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsEven(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsEven <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsEvenMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod(
				"IsOdd",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetIsEvenMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsEvenMethod(typeof(A));
		}

		#endregion

		#region GetIsOddMethod

		/// <summary>Gets the IsOdd <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsOdd(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The IsOdd <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsOddMethod<A>() => GetIsOddMethodCache<A>.Value;

		/// <summary>Gets the IsOdd <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsOdd(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The IsOdd <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsOddMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod(
				"IsOdd",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetIsOddMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsOddMethod(typeof(A));
		}

		#endregion

		#region GetIsIntegerMethod

		/// <summary>Gets the IsInteger <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsInteger(<typeparamref name="A"/>)].</summary>
		/// <typeparam name="A">The type of the out parameter.</typeparam>
		/// <returns>The TryParse <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsIntegerMethod<A>() => GetIsIntegerMethodCache<A>.Value;

		/// <summary>Gets the IsInteger <see cref="MethodInfo"/> on a type if it exists [<see cref="bool"/> IsInteger(<paramref name="a"/>)].</summary>
		/// <param name="a">The type of the out parameter.</param>
		/// <returns>The TryParse <see cref="MethodInfo"/> if found or null if not.</returns>
		public static MethodInfo? GetIsIntegerMethod(Type a)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			MethodInfo? methodInfo = a.GetMethod(
				"IsInteger",
				BindingFlags.Static |
				BindingFlags.Public |
				BindingFlags.NonPublic,
				null,
				Ɐ(a),
				null);
			return methodInfo is not null && methodInfo.ReturnType == typeof(bool)
					? methodInfo
					: null;
		}

		internal static class GetIsIntegerMethodCache<A>
		{
			internal static readonly MethodInfo? Value = GetIsIntegerMethod(typeof(A));
		}

		#endregion

		#region GetLessThanMethod

		/// <summary>Determines if an op_LessThan member exists.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <returns>True if the op_LessThan member exists or false if not.</returns>
		public static MethodInfo? GetLessThanMethod<A, B, C>() => GetLessThanMethodCache<A, B, C>.Value;

		/// <summary>Determines if an op_LessThan member exists.</summary>
		/// <param name="a">The type of the left operand.</param>
		/// <param name="b">The type of the right operand.</param>
		/// <param name="c">The type of the return.</param>
		/// <returns>True if the op_LessThan member exists or false if not.</returns>
		internal static MethodInfo? GetLessThanMethod(Type a, Type b, Type c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			MethodInfo? CheckType(Type type)
			{
				MethodInfo? methodInfo = type.GetMethod(
					"op_LessThan",
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic,
					null,
					Ɐ(a, b),
					null);
				return methodInfo is not null
					&& methodInfo.ReturnType == c
					&& methodInfo.IsSpecialName
						? methodInfo
						: null;
			}
			return CheckType(a) ?? CheckType(b);
		}

		internal static class GetLessThanMethodCache<A, B, C>
		{
			internal static readonly MethodInfo? Value = GetLessThanMethod(typeof(A), typeof(B), typeof(C));
		}

		#endregion

		#region GetGreaterThanMethod

		/// <summary>Determines if an op_GreaterThan member exists.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <returns>True if the op_GreaterThan member exists or false if not.</returns>
		public static MethodInfo? GetGreaterThanMethod<A, B, C>() => GetGreaterThanMethodCache<A, B, C>.Value;

		/// <summary>Determines if an op_GreaterThan member exists.</summary>
		/// <param name="a">The type of the left operand.</param>
		/// <param name="b">The type of the right operand.</param>
		/// <param name="c">The type of the return.</param>
		/// <returns>True if the op_GreaterThan member exists or false if not.</returns>
		internal static MethodInfo? GetGreaterThanMethod(Type a, Type b, Type c)
		{
			_ = a ?? throw new ArgumentNullException(nameof(a));
			_ = b ?? throw new ArgumentNullException(nameof(b));
			MethodInfo? CheckType(Type type)
			{
				MethodInfo? methodInfo = type.GetMethod(
					"op_GreaterThan",
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic,
					null,
					Ɐ(a, b),
					null);
				return methodInfo is not null
					&& methodInfo.ReturnType == c
					&& methodInfo.IsSpecialName
						? methodInfo
						: null;
			}
			return CheckType(a) ?? CheckType(b);
		}

		internal static class GetGreaterThanMethodCache<A, B, C>
		{
			internal static readonly MethodInfo? Value = GetGreaterThanMethod(typeof(A), typeof(B), typeof(C));
		}

		#endregion

		#endregion

		#region Has[Implicit|Explicit]Cast

		/// <summary>Determines if an implicit casting operator exists from one type to another.</summary>
		/// <typeparam name="From">The parameter type of the implicit casting operator.</typeparam>
		/// <typeparam name="To">The return type fo the implicit casting operator.</typeparam>
		/// <returns>True if the implicit casting operator exists or false if not.</returns>
		public static bool HasImplicitCast<From, To>() => HasCastCache<From, To>.Implicit;
		/// <summary>Determines if an implicit casting operator exists from one type to another.</summary>
		/// <typeparam name="From">The parameter type of the implicit casting operator.</typeparam>
		/// <typeparam name="To">The return type fo the implicit casting operator.</typeparam>
		/// <returns>True if the implicit casting operator exists or false if not.</returns>
		public static bool HasExplicitCast<From, To>() => HasCastCache<From, To>.Implicit;
		/// <summary>Determines if an implicit casting operator exists from one type to another.</summary>
		/// <param name="fromType">The parameter type of the implicit casting operator.</param>
		/// <param name="toType">The return type fo the implicit casting operator.</param>
		/// <returns>True if the implicit casting operator exists or false if not.</returns>
		public static bool HasImplicitCast(Type fromType, Type toType) => HasCast(fromType, toType, true);
		/// <summary>Determines if an implicit casting operator exists from one type to another.</summary>
		/// <param name="fromType">The parameter type of the implicit casting operator.</param>
		/// <param name="toType">The return type fo the implicit casting operator.</param>
		/// <returns>True if the implicit casting operator exists or false if not.</returns>
		public static bool HasExplicitCast(Type fromType, Type toType) => HasCast(fromType, toType, false);

		internal static bool HasCast(Type fromType, Type toType, bool @implicit)
		{
			_ = fromType ?? throw new ArgumentNullException(nameof(fromType));
			_ = toType ?? throw new ArgumentNullException(nameof(toType));
			string methodName = @implicit
				? "op_Implicit"
				: "op_Explicit";
			bool CheckType(Type type)
			{
				MethodInfo? methodInfo = type.GetMethod(
					methodName,
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic,
					null,
					Ɐ(fromType),
					null);
				return methodInfo is not null
					&& methodInfo.ReturnType == toType
					&& methodInfo.IsSpecialName;
			}
			if (CheckType(fromType) || CheckType(toType))
			{
				return true;
			}
			return false;
		}

		internal static class HasCastCache<From, To>
		{
			internal static readonly bool Implicit = HasCast(typeof(From), typeof(To), true);
			internal static readonly bool Explicit = HasCast(typeof(From), typeof(To), false);
		}

		#endregion

		#region System.Type.ConvertToCSharpSource

		/// <summary>Converts a <see cref="System.Type"/> into a <see cref="string"/> as it would appear in C# source code.</summary>
		/// <param name="type">The <see cref="System.Type"/> to convert to a <see cref="string"/>.</param>
		/// <param name="showGenericParameters">If the generic parameters are the generic types, whether they should be shown or not.</param>
		/// <returns>The <see cref="string"/> as the <see cref="System.Type"/> would appear in C# source code.</returns>
		public static string ConvertToCSharpSource(this Type type, bool showGenericParameters = false)
		{
			IQueue<Type> genericParameters = new QueueArray<Type>();
			type.GetGenericArguments().Stepper(x => genericParameters.Enqueue(x));
			return ConvertToCsharpSource(type);

			string ConvertToCsharpSource(Type type)
			{
				_ = type ?? throw new ArgumentNullException(nameof(type));
				string result = type.IsNested
					? ConvertToCsharpSource(type.DeclaringType ?? throw new ArgumentException($"{nameof(type)}.{nameof(Type.DeclaringType)} is null", nameof(type))) + "."
					: type.Namespace + ".";
				result += Regex.Replace(type.Name, "`.*", string.Empty);
				if (type.IsGenericType)
				{
					result += "<";
					bool firstIteration = true;
					foreach (Type generic in type.GetGenericArguments())
					{
						if (genericParameters.Count <= 0)
						{
							break;
						}
						Type correctGeneric = genericParameters.Dequeue();
						result += (firstIteration ? string.Empty : ",") +
							(correctGeneric.IsGenericParameter
								? (showGenericParameters ? (firstIteration ? string.Empty : " ") + correctGeneric.Name : string.Empty)
								: (firstIteration ? string.Empty : " ") + ConvertToCSharpSource(correctGeneric));
						firstIteration = false;
					}
					result += ">";
				}
				return result;
			}
		}

		#endregion

		#region System.Enum

		/// <summary>Gets a custom attribute on an enum value by generic type.</summary>
		/// <typeparam name="AttributeType">The type of attribute to get.</typeparam>
		/// <param name="enum">The enum value to get the attribute of.</param>
		/// <returns>The attribute on the enum value of the provided type.</returns>
		public static AttributeType? GetEnumAttribute<AttributeType>(this Enum @enum)
			where AttributeType : Attribute
		{
			Type type = @enum.GetType();
			MemberInfo memberInfo = type.GetMember(@enum.ToString())[0];
			return memberInfo.GetCustomAttribute<AttributeType>();
		}

		/// <summary>Gets custom attributes on an enum value by generic type.</summary>
		/// <typeparam name="AttributeType">The type of attribute to get.</typeparam>
		/// <param name="enum">The enum value to get the attribute of.</param>
		/// <returns>The attributes on the enum value of the provided type.</returns>
		public static System.Collections.Generic.IEnumerable<AttributeType> GetEnumAttributes<AttributeType>(this Enum @enum)
			where AttributeType : Attribute
		{
			Type type = @enum.GetType();
			MemberInfo memberInfo = type.GetMember(@enum.ToString())[0];
			return memberInfo.GetCustomAttributes<AttributeType>();
		}

		/// <summary>Gets the maximum value of an enum.</summary>
		/// <typeparam name="ENUM">The enum type to get the maximum value of.</typeparam>
		/// <returns>The maximum enum value of the provided type.</returns>
		public static ENUM GetLastEnumValue<ENUM>()
		{
			ENUM[] values = (ENUM[])Enum.GetValues(typeof(ENUM));
			if (values.Length == 0)
			{
				throw new InvalidOperationException("Attempting to get the last enum value of an enum type with no values.");
			}
			return values[^1];
		}

		#endregion

		#region System.Reflection.Assembly

		/// <summary>Enumerates through all the events with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the events of.</param>
		/// <returns>The IEnumerable of the events with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<EventInfo> GetEventInfosWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (EventInfo eventInfo in type.GetEvents(
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic))
				{
					if (eventInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
					{
						yield return eventInfo;
					}
				}
			}
		}

		/// <summary>Enumerates through all the constructors with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the constructors of.</param>
		/// <returns>The IEnumerable of the constructors with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<ConstructorInfo> GetConstructorInfosWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (ConstructorInfo constructorInfo in type.GetConstructors(
					BindingFlags.Instance |
					BindingFlags.Public |
					BindingFlags.NonPublic))
				{
					if (constructorInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
					{
						yield return constructorInfo;
					}
				}
			}
		}

		/// <summary>Enumerates through all the properties with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the properties of.</param>
		/// <returns>The IEnumerable of the properties with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<PropertyInfo> GetPropertyInfosWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (PropertyInfo propertyInfo in type.GetProperties(
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic))
				{
					if (propertyInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
					{
						yield return propertyInfo;
					}
				}
			}
		}

		/// <summary>Enumerates through all the fields with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the fields of.</param>
		/// <returns>The IEnumerable of the fields with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<FieldInfo> GetFieldInfosWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (FieldInfo fieldInfo in type.GetFields(
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic))
				{
					if (fieldInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
					{
						yield return fieldInfo;
					}
				}
			}
		}

		/// <summary>Enumerates through all the methods with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the methods of.</param>
		/// <returns>The IEnumerable of the methods with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<MethodInfo> GetMethodInfosWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (MethodInfo methodInfo in type.GetMethods(
					BindingFlags.Instance |
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic))
				{
					if (methodInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
					{
						yield return methodInfo;
					}
				}
			}
		}

		/// <summary>Enumerates through all the types with a custom attribute.</summary>
		/// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
		/// <param name="assembly">The assembly to iterate through the types of.</param>
		/// <returns>The IEnumerable of the types with the provided attribute type.</returns>
		public static System.Collections.Generic.IEnumerable<Type> GetTypesWithAttribute<AttributeType>(this Assembly assembly)
			where AttributeType : Attribute
		{
			foreach (Type type in assembly.GetTypes())
			{
				if (type.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
				{
					yield return type;
				}
			}
		}

		/// <summary>Gets all the types in an assembly that derive from a base.</summary>
		/// <typeparam name="Base">The base type to get the deriving types of.</typeparam>
		/// <param name="assembly">The assmebly to perform the search on.</param>
		/// <returns>The IEnumerable of the types that derive from the provided base.</returns>
		public static System.Collections.Generic.IEnumerable<Type> GetDerivedTypes<Base>(this Assembly assembly)
		{
			Type @base = typeof(Base);
			return assembly.GetTypes().Where(type =>
				type != @base &&
				@base.IsAssignableFrom(type));
		}

		/// <summary>Gets the file path of an assembly.</summary>
		/// <param name="assembly">The assembly to get the file path of.</param>
		/// <returns>The file path of the assembly.</returns>
		public static string? GetDirectoryPath(this Assembly assembly)
		{
			string? directoryPath = Path.GetDirectoryName(assembly.Location);
			return directoryPath == string.Empty ? null : directoryPath;
		}

		#endregion

		#region GetXmlName

		/// <summary>Gets the XML name of an <see cref="Type"/> as it appears in the XML docs.</summary>
		/// <param name="type">The field to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="type"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this Type type)
		{
			_ = type ?? throw new ArgumentNullException(nameof(type));
			_ = type.FullName ?? throw new ArgumentException($"{nameof(type)}.{nameof(Type.FullName)} is null", nameof(type));
			LoadXmlDocumentation(type.Assembly);
			return "T:" + GetXmlNameTypeSegment(type.FullName);
		}

		/// <summary>Gets the XML name of an <see cref="MethodInfo"/> as it appears in the XML docs.</summary>
		/// <param name="methodInfo">The field to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="methodInfo"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this MethodInfo methodInfo)
		{
			_ = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
			_ = methodInfo.DeclaringType ?? throw new ArgumentException($"{nameof(methodInfo)}.{nameof(Type.DeclaringType)} is null", nameof(methodInfo));
			return GetXmlNameMethodBase(methodInfo: methodInfo);
		}

		/// <summary>Gets the XML name of an <see cref="ConstructorInfo"/> as it appears in the XML docs.</summary>
		/// <param name="constructorInfo">The field to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="constructorInfo"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this ConstructorInfo constructorInfo)
		{
			_ = constructorInfo ?? throw new ArgumentNullException(nameof(constructorInfo));
			_ = constructorInfo.DeclaringType ?? throw new ArgumentException($"{nameof(constructorInfo)}.{nameof(Type.DeclaringType)} is null", nameof(constructorInfo));
			return GetXmlNameMethodBase(constructorInfo: constructorInfo);
		}

		/// <summary>Gets the XML name of an <see cref="PropertyInfo"/> as it appears in the XML docs.</summary>
		/// <param name="propertyInfo">The field to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="propertyInfo"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this PropertyInfo propertyInfo)
		{
			_ = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
			_ = propertyInfo.DeclaringType ?? throw new ArgumentException($"{nameof(propertyInfo)}.{nameof(Type.DeclaringType)} is null", nameof(propertyInfo));
			_ = propertyInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(propertyInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(propertyInfo));
			return "P:" + GetXmlNameTypeSegment(propertyInfo.DeclaringType.FullName) + "." + propertyInfo.Name;
		}

		/// <summary>Gets the XML name of an <see cref="FieldInfo"/> as it appears in the XML docs.</summary>
		/// <param name="fieldInfo">The field to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="fieldInfo"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this FieldInfo fieldInfo)
		{
			_ = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
			_ = fieldInfo.DeclaringType ?? throw new ArgumentException($"{nameof(fieldInfo)}.{nameof(Type.DeclaringType)} is null", nameof(fieldInfo));
			_ = fieldInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(fieldInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(fieldInfo));
			return "F:" + GetXmlNameTypeSegment(fieldInfo.DeclaringType.FullName) + "." + fieldInfo.Name;
		}

		/// <summary>Gets the XML name of an <see cref="EventInfo"/> as it appears in the XML docs.</summary>
		/// <param name="eventInfo">The event to get the XML name of.</param>
		/// <returns>The XML name of <paramref name="eventInfo"/> as it appears in the XML docs.</returns>
		public static string GetXmlName(this EventInfo eventInfo)
		{
			_ = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
			_ = eventInfo.DeclaringType ?? throw new ArgumentException($"{nameof(eventInfo)}.{nameof(Type.DeclaringType)} is null", nameof(eventInfo));
			_ = eventInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(eventInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(eventInfo));
			return "E:" + GetXmlNameTypeSegment(eventInfo.DeclaringType.FullName) + "." + eventInfo.Name;
		}

		internal static string GetXmlNameMethodBase(MethodInfo? methodInfo = null, ConstructorInfo? constructorInfo = null)
		{
			if (methodInfo is not null && constructorInfo is not null)
			{
				throw new TowelBugException($"{nameof(GetDocumentation)} {nameof(methodInfo)} is not null && {nameof(constructorInfo)} is not null");
			}

			MethodBase? methodBase = methodInfo ?? (MethodBase?)constructorInfo;
			_ = methodBase ?? throw new TowelBugException($"{nameof(GetDocumentation)} {nameof(methodInfo)} is null && {nameof(constructorInfo)} is null");
			_ = methodBase.DeclaringType ?? throw new ArgumentException($"{nameof(methodBase)}.{nameof(Type.DeclaringType)} is null");

			LoadXmlDocumentation(methodBase.DeclaringType.Assembly);

			MapHashLinked<int, string, StringEquate, StringHash> typeGenericMap = new();
			Type[] typeGenericArguments = methodBase.DeclaringType.GetGenericArguments();
			for (int i = 0; i < typeGenericArguments.Length; i++)
			{
				Type typeGeneric = typeGenericArguments[i];
				typeGenericMap[typeGeneric.Name] = i;
			}

			MapHashLinked<int, string, StringEquate, StringHash> methodGenericMap = new();
			if (constructorInfo is null)
			{
				Type[] methodGenericArguments = methodBase.GetGenericArguments();
				for (int i = 0; i < methodGenericArguments.Length; i++)
				{
					Type methodGeneric = methodGenericArguments[i];
					methodGenericMap[methodGeneric.Name] = i;
				}
			}

			ParameterInfo[] parameterInfos = methodBase.GetParameters();

			string memberTypePrefix = "M:";
			string declarationTypeString = GetXmlDocumenationFormattedString(methodBase.DeclaringType, false, typeGenericMap, methodGenericMap);
			string memberNameString =
				constructorInfo is not null ? "#ctor" :
				methodBase.Name;
			string methodGenericArgumentsString =
				methodGenericMap.Count > 0 ?
				"``" + methodGenericMap.Count :
				string.Empty;
			string parametersString =
				parameterInfos.Length > 0 ?
				"(" + string.Join(",", methodBase.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true, typeGenericMap, methodGenericMap))) + ")" :
				string.Empty;

			string key =
				memberTypePrefix +
				declarationTypeString +
				"." +
				memberNameString +
				methodGenericArgumentsString +
				parametersString;

			if (methodInfo is not null &&
				(methodBase.Name == "op_Implicit" ||
				methodBase.Name == "op_Explicit"))
			{
				key += "~" + GetXmlDocumenationFormattedString(methodInfo.ReturnType, true, typeGenericMap, methodGenericMap);
			}
			return key;
		}

		internal static string GetXmlDocumenationFormattedString(
			Type type,
			bool isMethodParameter,
			MapHashLinked<int, string, StringEquate, StringHash> typeGenericMap,
			MapHashLinked<int, string, StringEquate, StringHash> methodGenericMap)
		{
			if (type.IsGenericParameter)
			{
				return methodGenericMap.TryGet(type.Name, out int methodIndex)
					? "``" + methodIndex
					: "`" + typeGenericMap[type.Name];
			}
			else if (type.HasElementType)
			{
				string elementTypeString = GetXmlDocumenationFormattedString(
					type.GetElementType() ?? throw new ArgumentException($"{nameof(type)}.{nameof(Type.HasElementType)} && {nameof(type)}.{nameof(Type.GetElementType)}() is null", nameof(type)),
					isMethodParameter,
					typeGenericMap,
					methodGenericMap);

				switch (type)
				{
					case Type when type.IsPointer:
						return elementTypeString + "*";

					case Type when type.IsByRef:
						return elementTypeString + "@";

					case Type when type.IsArray:
						int rank = type.GetArrayRank();
						string arrayDimensionsString = rank > 1
							? "[" + string.Join(",", Enumerable.Repeat("0:", rank)) + "]"
							: "[]";
						return elementTypeString + arrayDimensionsString;

					default:
						throw new TowelBugException($"{nameof(GetXmlDocumenationFormattedString)} encountered an unhandled element type: {type}");
				}
			}
			else
			{
				string prefaceString = type.IsNested
					? GetXmlDocumenationFormattedString(
						type.DeclaringType ?? throw new ArgumentException($"{nameof(type)}.{nameof(Type.IsNested)} && {nameof(type)}.{nameof(Type.DeclaringType)} is null", nameof(type)),
						isMethodParameter,
						typeGenericMap,
						methodGenericMap) + "."
					: type.Namespace + ".";

				string typeNameString = isMethodParameter
					? typeNameString = Regex.Replace(type.Name, @"`\d+", string.Empty)
					: typeNameString = type.Name;

				string genericArgumentsString = type.IsGenericType && isMethodParameter
					? "{" + string.Join(",",
						type.GetGenericArguments().Select(argument =>
							GetXmlDocumenationFormattedString(
								argument,
								isMethodParameter,
								typeGenericMap,
								methodGenericMap))
						) + "}"
					: string.Empty;

				return prefaceString + typeNameString + genericArgumentsString;
			}
		}

		internal static string GetXmlNameTypeSegment(string typeFullNameString) =>
			Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty).Replace('+', '.');


		#endregion

		#region GetXmlDocumentation

		internal static ISet<Assembly> loadedAssemblies = SetHashLinked.New<Assembly>();
		internal static MapHashLinked<string, string, StringEquate, StringHash> loadedXmlDocumentation = new();

		internal static void LoadXmlDocumentation(Assembly assembly)
		{
			if (loadedAssemblies.Contains(assembly))
			{
				return;
			}
			string? directoryPath = assembly.GetDirectoryPath();
			if (directoryPath is not null)
			{
				string xmlFilePath = Path.Combine(directoryPath, assembly.GetName().Name + ".xml");
				if (File.Exists(xmlFilePath))
				{
					using StreamReader streamReader = new(xmlFilePath);
					LoadXmlDocumentation(streamReader);
				}
			}
			loadedAssemblies.Add(assembly);
		}

		/// <summary>Loads the XML code documentation into memory so it can be accessed by extension methods on reflection types.</summary>
		/// <param name="xmlDocumentation">The content of the XML code documentation.</param>
		public static void LoadXmlDocumentation(string xmlDocumentation)
		{
			using StringReader stringReader = new(xmlDocumentation);
			LoadXmlDocumentation(stringReader);
		}

		/// <summary>Loads the XML code documentation into memory so it can be accessed by extension methods on reflection types.</summary>
		/// <param name="textReader">The text reader to process in an XmlReader.</param>
		public static void LoadXmlDocumentation(TextReader textReader)
		{
			using XmlReader xmlReader = XmlReader.Create(textReader);
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
				{
					string? rawName = xmlReader["name"];
					if (!string.IsNullOrWhiteSpace(rawName))
					{
						loadedXmlDocumentation[rawName] = xmlReader.ReadInnerXml();
					}
				}
			}
		}

		/// <summary>Clears the currently loaded XML documentation.</summary>
		public static void ClearXmlDocumentation()
		{
			loadedAssemblies.Clear();
			loadedXmlDocumentation.Clear();
		}

		/// <summary>Gets the XML documentation on a type.</summary>
		/// <param name="type">The type to get the XML documentation of.</param>
		/// <returns>The XML documentation on the type.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this Type type)
		{
			_ = type ?? throw new ArgumentNullException(nameof(type));
			_ = type.FullName ?? throw new ArgumentException($"{nameof(type)}.{nameof(Type.FullName)} is null", nameof(type));
			LoadXmlDocumentation(type.Assembly);
			loadedXmlDocumentation.TryGet(type.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on a method.</summary>
		/// <param name="methodInfo">The method to get the XML documentation of.</param>
		/// <returns>The XML documentation on the method.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this MethodInfo methodInfo)
		{
			_ = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
			_ = methodInfo.DeclaringType ?? throw new ArgumentException($"{nameof(methodInfo)}.{nameof(Type.DeclaringType)} is null", nameof(methodInfo));
			LoadXmlDocumentation(methodInfo.DeclaringType.Assembly);
			loadedXmlDocumentation.TryGet(methodInfo.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on a constructor.</summary>
		/// <param name="constructorInfo">The constructor to get the XML documentation of.</param>
		/// <returns>The XML documentation on the constructor.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this ConstructorInfo constructorInfo)
		{
			_ = constructorInfo ?? throw new ArgumentNullException(nameof(constructorInfo));
			_ = constructorInfo.DeclaringType ?? throw new ArgumentException($"{nameof(constructorInfo)}.{nameof(Type.DeclaringType)} is null", nameof(constructorInfo));
			LoadXmlDocumentation(constructorInfo.DeclaringType.Assembly);
			loadedXmlDocumentation.TryGet(constructorInfo.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on a property.</summary>
		/// <param name="propertyInfo">The property to get the XML documentation of.</param>
		/// <returns>The XML documentation on the property.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this PropertyInfo propertyInfo)
		{
			_ = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
			_ = propertyInfo.DeclaringType ?? throw new ArgumentException($"{nameof(propertyInfo)}.{nameof(Type.DeclaringType)} is null", nameof(propertyInfo));
			_ = propertyInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(propertyInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(propertyInfo));
			LoadXmlDocumentation(propertyInfo.DeclaringType.Assembly);
			loadedXmlDocumentation.TryGet(propertyInfo.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on a field.</summary>
		/// <param name="fieldInfo">The field to get the XML documentation of.</param>
		/// <returns>The XML documentation on the field.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this FieldInfo fieldInfo)
		{
			_ = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
			_ = fieldInfo.DeclaringType ?? throw new ArgumentException($"{nameof(fieldInfo)}.{nameof(Type.DeclaringType)} is null", nameof(fieldInfo));
			_ = fieldInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(fieldInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(fieldInfo));
			LoadXmlDocumentation(fieldInfo.DeclaringType.Assembly);
			loadedXmlDocumentation.TryGet(fieldInfo.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on an event.</summary>
		/// <param name="eventInfo">The event to get the XML documentation of.</param>
		/// <returns>The XML documentation on the event.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this EventInfo eventInfo)
		{
			_ = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
			_ = eventInfo.DeclaringType ?? throw new ArgumentException($"{nameof(eventInfo)}.{nameof(Type.DeclaringType)} is null", nameof(eventInfo));
			_ = eventInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(eventInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(eventInfo));
			LoadXmlDocumentation(eventInfo.DeclaringType.Assembly);
			loadedXmlDocumentation.TryGet(eventInfo.GetXmlName(), out string? documentation);
			return documentation;
		}

		/// <summary>Gets the XML documentation on a member.</summary>
		/// <param name="memberInfo">The member to get the XML documentation of.</param>
		/// <returns>The XML documentation on the member.</returns>
		/// <remarks>The XML documentation must be loaded into memory for this function to work.</remarks>
		public static string? GetDocumentation(this MemberInfo memberInfo)
		{
			switch (memberInfo)
			{
				case FieldInfo fieldInfo:
					_ = fieldInfo.DeclaringType ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.DeclaringType)} is null", nameof(memberInfo));
					_ = fieldInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(memberInfo));
					return fieldInfo.GetDocumentation();
				case PropertyInfo propertyInfo:
					_ = propertyInfo.DeclaringType ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.DeclaringType)} is null", nameof(memberInfo));
					_ = propertyInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(memberInfo));
					return propertyInfo.GetDocumentation();
				case EventInfo eventInfo:
					_ = eventInfo.DeclaringType ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.DeclaringType)} is null", nameof(memberInfo));
					_ = eventInfo.DeclaringType.FullName ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(EventInfo.DeclaringType)}.{nameof(Type.FullName)} is null", nameof(memberInfo));
					return eventInfo.GetDocumentation();
				case ConstructorInfo constructorInfo:
					_ = constructorInfo.DeclaringType ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.DeclaringType)} is null", nameof(memberInfo));
					return constructorInfo.GetDocumentation();
				case MethodInfo methodInfo:
					_ = methodInfo.DeclaringType ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.DeclaringType)} is null", nameof(memberInfo));
					return methodInfo.GetDocumentation();
				case Type type:
					_ = type.FullName ?? throw new ArgumentException($"{nameof(memberInfo)}.{nameof(Type.FullName)} is null", nameof(memberInfo));
					return type.GetDocumentation();
				case null:
					throw new ArgumentNullException(nameof(memberInfo));
				default:
					throw new TowelBugException($"{nameof(GetDocumentation)} encountered an unhandled {nameof(MemberInfo)} type: {memberInfo}");
			};
		}

		/// <summary>Gets the XML documentation for a parameter.</summary>
		/// <param name="parameterInfo">The parameter to get the XML documentation for.</param>
		/// <returns>The XML documenation of the parameter.</returns>
		public static string? GetDocumentation(this ParameterInfo parameterInfo)
		{
			_ = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
			string? memberDocumentation = parameterInfo.Member.GetDocumentation();
			if (memberDocumentation is not null)
			{
				string regexPattern =
					Regex.Escape(@"<param name=" + "\"" + parameterInfo.Name + "\"" + @">") +
					".*?" +
					Regex.Escape(@"</param>");

				Match match = Regex.Match(memberDocumentation, regexPattern);
				if (match.Success)
				{
					return match.Value;
				}
			}
			return null;
		}

		#endregion

		#region System.Reflection.MethodInfo

		/// <summary>Determines if a MethodInfo is a local function.</summary>
		/// <param name="methodInfo">The method info to determine if it is a local function.</param>
		/// <returns>True if the MethodInfo is a local function. False if not.</returns>
		public static bool IsLocalFunction(this MethodInfo methodInfo) =>
			Regex.Match(methodInfo.Name, @"g__.+\|\d+_\d+").Success;

		#endregion
	}
}
