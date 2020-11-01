using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using Towel.DataStructures;
using Towel.Measurements;
using System.Runtime.CompilerServices;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static class Statics
	{
		#region Internals

		// These are some shared internal optimizations that I don't want to expose because it might confuse people.
		// If you need to use it, just copy this code into your own project.

		/// <summary>a * b + c</summary>
		internal static class MultiplyAddImplementation<T>
		{
			internal static Func<T, T, T, T> Function = (a, b, c) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				ParameterExpression C = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Add(Expression.Multiply(A, B), C);
				Function = Expression.Lambda<Func<T, T, T, T>>(BODY, A, B, C).Compile();
				return Function(a, b, c);
			};
		}

		/// <summary>d - a * b / c</summary>
		internal static class D_subtract_A_multiply_B_divide_C<T>
		{
			internal static Func<T, T, T, T, T> Function = (a, b, c, d) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				ParameterExpression C = Expression.Parameter(typeof(T));
				ParameterExpression D = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Subtract(D, Expression.Divide(Expression.Multiply(A, B), C));
				Function = Expression.Lambda<Func<T, T, T, T, T>>(BODY, A, B, C, D).Compile();
				return Function(a, b, c, d);
			};
		}

		internal static T OperationOnStepper<T>(Action<Action<T>> stepper, Func<T, T, T> operation)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			T result = default;
			bool assigned = false;
			stepper(a =>
			{
				if (assigned)
				{
					result = operation(result!, a);
				}
				else
				{
					result = a;
					assigned = true;
				}
			});
			return assigned
				? result!
				: throw new ArgumentException($"{nameof(stepper)} is empty.", nameof(stepper));
		}

		#endregion

		#region Swap

		/// <summary>Swaps two values.</summary>
		/// <typeparam name="T">The type of values to swap.</typeparam>
		/// <param name="a">The first value of the swap.</param>
		/// <param name="b">The second value of the swap.</param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}

		#endregion

		#region source...

#pragma warning disable IDE1006 // Naming Styles

		/// <summary>Gets the file path of the current location in source code.</summary>
		/// <param name="DEFAULT">Intended to leave default. This value is set by the compiler via <see cref="CallerFilePathAttribute"/>.</param>
		/// <returns>The file path of the current location in source code.</returns>
		public static string sourcefilepath([CallerFilePath] string DEFAULT = default) => DEFAULT;

		/// <summary>Gets the member name of the current location in source code.</summary>
		/// <param name="DEFAULT">Intended to leave default. This value is set by the compiler via <see cref="CallerMemberNameAttribute"/>.</param>
		/// <returns>The member name of the current location in source code.</returns>
		public static string sourcemembername([CallerMemberName] string DEFAULT = default) => DEFAULT;

		/// <summary>Gets the line number of the current location in source code.</summary>
		/// <param name="DEFAULT">Intended to leave default. This value is set by the compiler via <see cref="CallerLineNumberAttribute"/>.</param>
		/// <returns>The line number of the current location in source code.</returns>
		public static int sourcelinenumber([CallerLineNumber] int DEFAULT = default) => DEFAULT;

#if false
		/// <summary>Gets the source code and evaluation of an expression.</summary>
		/// <typeparam name="T">The type the expression will evaluate to.</typeparam>
		/// <param name="expression">The expression to evaluate and get the source of.</param>
		/// <param name="DEFAULT">Intended to leave default. This value is set by the compiler via <see cref="CallerArgumentExpressionAttribute"/>.</param>
		/// <returns>The source code and evaluation of the expression.</returns>
		public static (T Value, string Source) sourceof<T>(T expression, [CallerArgumentExpression("expression")] string DEFAULT = default) => (expression, DEFAULT);

		/// <summary>Gets the source code and evaluation of an expression.</summary>
		/// <typeparam name="T">The type the expression will evaluate to.</typeparam>
		/// <param name="expression">The expression to evaluate and get the source of.</param>
		/// <param name="source">The source code of the expression.</param>
		/// <param name="DEFAULT">Intended to leave default. This value is set by the compiler via <see cref="CallerArgumentExpressionAttribute"/>.</param>
		/// <returns>The evaluation of the expression.</returns>
		public static T sourceof<T>(T expression, out string source, [CallerArgumentExpression("expression")] string DEFAULT = default)
		{
			source = DEFAULT;
			return expression;
		}
#endif

#pragma warning restore IDE1006 // Naming Styles

		#endregion

		#region TryParse

		/// <summary>Tries to parse a <see cref="string"/> into a value of the type <typeparamref name="A"/>.</summary>
		/// <typeparam name="A">The type to parse the <see cref="string"/> into a value of.</typeparam>
		/// <param name="string">The <see cref="string"/> to parse into a value ot type <typeparamref name="A"/>.</param>
		/// <returns>
		/// (<see cref="bool"/> Success, <typeparamref name="A"/> Value)
		/// <para>- <see cref="bool"/> Success: True if the parse was successful; False if not.</para>
		/// <para>- <typeparamref name="A"/> Value: The value if the parse was successful or default if not.</para>
		/// </returns>
		public static (bool Success, A? Value) TryParse<A>(string @string) =>
			(TryParseImplementation<A>.Function(@string, out A value), value);

		internal static class TryParseImplementation<A>
		{
			internal delegate TResult TryParse<T1, T2, TResult>(T1 arg1, out T2? arg2);

			internal static TryParse<string, A, bool> Function = (string @string, out A? value) =>
			{
				static bool Fail(string @string, out A? value)
				{
					value = default;
					return false;
				}
				if (typeof(A).IsEnum)
				{
					foreach (MethodInfo methodInfo in typeof(Enum).GetMethods(
						BindingFlags.Static |
						BindingFlags.Public))
					{
						if (methodInfo.Name == nameof(Enum.TryParse) &&
							methodInfo.IsGenericMethod &&
							methodInfo.IsStatic &&
							methodInfo.IsPublic &&
							methodInfo.ReturnType == typeof(bool))
						{
							MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeof(A));
							ParameterInfo[] parameters = genericMethodInfo.GetParameters();
							if (parameters.Length == 2 &&
								parameters[0].ParameterType == typeof(string) &&
								parameters[1].ParameterType == typeof(A).MakeByRefType())
							{
								Function = genericMethodInfo.CreateDelegate<TryParse<string, A, bool>>();
								return Function(@string, out value);
							}
						}
					}
					throw new TowelBugException("The System.Enum.TryParse method was not found via reflection.");
				}
				else
				{
					MethodInfo? methodInfo = Meta.GetTryParseMethod<A>();
					Function = methodInfo is null
						? Fail
						: methodInfo.CreateDelegate<TryParse<string, A, bool>>();
					return Function(@string, out value);
				}
			};
		}

		#endregion

		#region Hash

		/// <summary>Static wrapper for the instance based "object.GetHashCode" function.</summary>
		/// <typeparam name="T">The generic type of the hash operation.</typeparam>
		/// <param name="value">The item to get the hash code of.</param>
		/// <returns>The computed hash code using the base GetHashCode instance method.</returns>
		public static int DefaultHash<T>(T value)
		{
			_ = value ?? throw new ArgumentNullException(nameof(value));
			return value.GetHashCode();
		}

		#endregion

		#region Convert

		/// <summary>Converts <paramref name="a"/> from <typeparamref name="A"/> to <typeparamref name="B"/>.</summary>
		/// <typeparam name="A">The type of the value to convert.</typeparam>
		/// <typeparam name="B">The type to convert teh value to.</typeparam>
		/// <param name="a">The value to convert.</param>
		/// <returns>The <paramref name="a"/> value of <typeparamref name="B"/> type.</returns>
		public static B Convert<A, B>(A a) =>
			ConvertImplementation<A, B>.Function(a);

		internal static class ConvertImplementation<A, B>
		{
			internal static Func<A, B> Function = a =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				Expression BODY = Expression.Convert(A, typeof(B));
				Function = Expression.Lambda<Func<A, B>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region Join

		/// <summary>Iterates a <see cref="System.Range"/> and joins the results of a System.Func&lt;int, string&gt; seperated by a <see cref="string"/> <paramref name="seperator"/>.</summary>
		/// <param name="range">The range of values to use use on the &lt;System.Func{int, string&gt; <paramref name="func"/>.</param>
		/// <param name="func">The System.Func&lt;int, string&gt;.</param>
		/// <param name="seperator">The <see cref="string"/> seperator to join the values with.</param>
		/// <returns>The resulting <see cref="string"/> of the join.</returns>
		public static string Join(Range range, Func<int, string> func, string seperator) =>
			string.Join(seperator, range.ToIEnumerable().Select(func));

		#endregion

		#region Keywords

		/// <summary>Stepper was not broken.</summary>
		public const StepStatus Continue = StepStatus.Continue;
		/// <summary>Stepper was broken.</summary>
		public const StepStatus Break = StepStatus.Break;
		/// <summary>The Goal was found.</summary>
		public const GraphSearchStatus Goal = GraphSearchStatus.Goal;
		/// <summary>The left operand is less than the right operand.</summary>
		public const CompareResult Less = CompareResult.Less;
		/// <summary>The left operand is equal to the right operand.</summary>
		public const CompareResult Equal = CompareResult.Equal;
		/// <summary>The left operand is greater than the right operand.</summary>
		public const CompareResult Greater = CompareResult.Greater;
		/// <summary>There is no bound.</summary>
		public const Omnitree.Keyword None = Omnitree.Keyword.None;
		/// <summary>The default case in a Switch statement (true).</summary>
		public const SwitchSyntax.Keyword Default = SwitchSyntax.Keyword.Default;

		#endregion

		#region Switch

		/// <summary>Syntax sugar Switch statements.</summary>
		/// <param name="possibleActions">The possible actions of the Switch statement.</param>
		public static void Switch(params (SwitchSyntax.Condition, Action)[] possibleActions) =>
			SwitchSyntax.Do(possibleActions);

		/// <summary>Syntax sugar Switch statements.</summary>
		/// <typeparam name="T">The generic type parameter to the Switch statement.</typeparam>
		/// <param name="value">The value argument of the Switch statement.</param>
		/// <returns>The delegate for the Switch statement.</returns>
		public static SwitchSyntax.ParamsAction<SwitchSyntax.Condition<T>, Action> Switch<T>(T value) =>
			SwitchSyntax.Do<T>(value);

		/// <summary>Definitions for Switch syntax.</summary>
		public static class SwitchSyntax
		{
			/// <summary>Delegate with params intended to be used with the Switch syntax.</summary>
			public delegate void ParamsAction<A, B>(params (A, B)[] values);

			internal static ParamsAction<Condition<T>, Action> Do<T>(T value) =>
				possibleActions =>
				{
					foreach (var possibleAction in possibleActions)
					{
						if (possibleAction.Item1.Resolve(value))
						{
							possibleAction.Item2();
							return;
						}
					}
				};

			internal static void Do(params (Condition Condition, Action Action)[] possibleActions)
			{
				foreach (var possibleAction in possibleActions)
				{
					if (possibleAction.Condition)
					{
						possibleAction.Action();
						return;
					}
				}
			}

			/// <summary>Intended to be used with Switch syntax.</summary>
			public enum Keyword
			{
				/// <summary>The default keyword for the the Switch syntax.</summary>
				Default,
			}

			/// <summary>Represents the result of a conditional expression inside Switch syntax.</summary>
			/// <typeparam name="T">The generic type of the Switch condition for equality checks.</typeparam>
			public abstract class Condition<T>
			{
				/// <summary>Resolves the condition to a bool.</summary>
				public abstract bool Resolve(T b);
				/// <summary>Casts a <typeparamref name="T"/> to a bool using an equality check.</summary>
				public static implicit operator Condition<T>(T value) => new Value<T>(a: value);
				/// <summary>Uses the bool as the condition result.</summary>
				public static implicit operator Condition<T>(bool result) => new Bool<T> { Result = result, };
				/// <summary>Converts a keyword to a condition result (for "Default" case).</summary>
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0060 // Remove unused parameter
				public static implicit operator Condition<T>(Keyword keyword) => new Default<T>();
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0079 // Remove unnecessary suppression
			}

			internal class Value<T> : Condition<T>
			{
				/// <summary>The value of this condition for an equality check.</summary>
				internal T A;
				public override bool Resolve(T b) => Equate(A, b);
				public Value(T a)
				{
					A = a;
				}
			}

			internal class Bool<T> : Condition<T>
			{
				internal bool Result;
				public override bool Resolve(T b) => Result;
			}

			internal class Default<T> : Condition<T>
			{
				public override bool Resolve(T b) => true;
			}

			/// <summary>Represents the result of a conditional expression inside Switch syntax.</summary>
			public abstract class Condition
			{
				/// <summary>Resolves the condition to a bool.</summary>
				public abstract bool Resolve();
				/// <summary>Uses the bool as the condition result.</summary>
				public static implicit operator Condition(bool result) => new Bool { Result = result, };
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0060 // Remove unused parameter
				/// <summary>Converts a keyword to a condition result (for "Default" case).</summary>
				public static implicit operator Condition(Keyword keyword) => new Default();
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0079 // Remove unnecessary suppression
				/// <summary>Converts a condition to a bool using the Resolve method.</summary>
				public static implicit operator bool(Condition condition) => condition.Resolve();
			}

			internal class Bool : Condition
			{
				internal bool Result;
				public override bool Resolve() => Result;
			}

			internal class Default : Condition
			{
				public override bool Resolve() => true;
			}
		}

		#endregion

		#region Chance

#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0075 // Simplify conditional expression
#pragma warning disable IDE0060 // Remove unused parameter

		/// <summary>Allows chance syntax with "using static Towel.Syntax;".</summary>
		/// <example>25% Chance</example>
		public static ChanceSyntax Chance => default;

		/// <summary>Struct that allows percentage syntax that will be evaluated at runtime.</summary>
		public struct ChanceSyntax
		{
			/// <summary>The random algorithm currently being used by chance syntax.</summary>
			public static Random Algorithm = new Random();

			/// <summary>Creates a chance from a percentage that will be evaluated at runtime.</summary>
			/// <param name="percentage">The value of the percentage.</param>
			/// <param name="chance">The chance syntax struct object.</param>
			/// <returns>True if the the chance hits. False if not.</returns>
			public static bool operator %(double percentage, ChanceSyntax chance) =>
				percentage < 0d ? throw new ArgumentOutOfRangeException(nameof(chance)) :
				percentage > 100d ? throw new ArgumentOutOfRangeException(nameof(chance)) :
				percentage is 100d ? true :
				percentage is 0d ? false :
				Algorithm.NextDouble() < percentage / 100d;
		}

#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0075 // Simplify conditional expression
#pragma warning restore IDE0079 // Remove unnecessary suppression
#pragma warning restore CA2211 // Non-constant fields should not be visible

		#endregion

		#region Inequality

		/// <summary>Used for inequality syntax.</summary>
		/// <typeparam name="T">The generic type of elements the inequality is being used on.</typeparam>
		public struct Inequality<T>
		{
			internal bool Cast;
			internal T A;

			/// <summary>Contructs a new <see cref="Inequality{T}"/>.</summary>
			/// <param name="a">The initial value of the running inequality.</param>
			public static implicit operator Inequality<T>(T a) =>
				new Inequality<T>()
				{
					Cast = true,
					A = a,
				};
			/// <summary>Adds a greater than operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the greater than operation.</param>
			/// <returns>A running inequality with the additonal greater than operation.</returns>
			public static OperatorValidated.Inequality<T> operator >(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Compare(a.A, b) == Greater, b);
			/// <summary>Adds a less than operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the less than operation.</param>
			/// <returns>A running inequality with the additonal less than operation.</returns>
			public static OperatorValidated.Inequality<T> operator <(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Compare(a.A, b) == Less, b);
			/// <summary>Adds a greater than or equal operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the greater than or equal operation.</param>
			/// <returns>A running inequality with the additonal greater than or equal operation.</returns>
			public static OperatorValidated.Inequality<T> operator >=(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Compare(a.A, b) != Less, b);
			/// <summary>Adds a less than or equal operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the less than or equal operation.</param>
			/// <returns>A running inequality with the additonal less than or equal operation.</returns>
			public static OperatorValidated.Inequality<T> operator <=(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Compare(a.A, b) != Greater, b);
			/// <summary>Adds an equal operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the equal operation.</param>
			/// <returns>A running inequality with the additonal equal operation.</returns>
			public static OperatorValidated.Inequality<T> operator ==(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Equate(a.A, b), b);
			/// <summary>Adds an inequal operation to a running inequality.</summary>
			/// <param name="a">The current running inequality and left hand operand.</param>
			/// <param name="b">The value of the right hand operand of the inequal operation.</param>
			/// <returns>A running inequality with the additonal inequal operation.</returns>
			public static OperatorValidated.Inequality<T> operator !=(Inequality<T> a, T b) =>
				!a.Cast ? throw new InequalitySyntaxException() :
				new OperatorValidated.Inequality<T>(Inequate(a.A, b), b);

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
			/// <summary>This member is not intended to be invoked.</summary>
			/// <returns>This member is not intended to be invoked.</returns>
			[Obsolete(TowelConstants.NotIntended, true)]
			public override string ToString() => throw new InequalitySyntaxException();
			/// <summary>This member is not intended to be invoked.</summary>
			/// <param name="obj">This member is not intended to be invoked.</param>
			/// <returns>This member is not intended to be invoked.</returns>
			[Obsolete(TowelConstants.NotIntended, true)]
			public override bool Equals(object? obj) => throw new InequalitySyntaxException();
			/// <summary>This member is not intended to be invoked.</summary>
			/// <returns>This member is not intended to be invoked.</returns>
			[Obsolete(TowelConstants.NotIntended, true)]
			public override int GetHashCode() => throw new InequalitySyntaxException();
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
		}

		/// <summary>Helper type for inequality syntax. Contains an Inequality type that has been operator validated.</summary>
		public static partial class OperatorValidated
		{
			/// <summary>Used for inequality syntax.</summary>
			/// <typeparam name="T">The generic type of elements the inequality is being used on.</typeparam>
			public struct Inequality<T>
			{
				internal readonly bool Result;
				internal readonly T A;

				internal Inequality(bool result, T a)
				{
					Result = result;
					A = a;
				}
				/// <summary>Converts this running inequality into the result of the expression.</summary>
				/// <param name="inequality">The inequality to convert into the result of the expression.</param>
				public static implicit operator bool(Inequality<T> inequality) =>
					inequality.Result;
				/// <summary>Adds a greater than operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the greater than operation.</param>
				/// <returns>A running inequality with the additonal greater than operation.</returns>
				public static Inequality<T> operator >(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Compare(a.A, b) == Greater, b);
				/// <summary>Adds a less than operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the less than operation.</param>
				/// <returns>A running inequality with the additonal less than operation.</returns>
				public static Inequality<T> operator <(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Compare(a.A, b) == Less, b);
				/// <summary>Adds a greater than or equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the greater than or equal operation.</param>
				/// <returns>A running inequality with the additonal greater than or equal operation.</returns>
				public static Inequality<T> operator >=(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Compare(a.A, b) != Less, b);
				/// <summary>Adds a less than or equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the less than or equal operation.</param>
				/// <returns>A running inequality with the additonal less than or equal operation.</returns>
				public static Inequality<T> operator <=(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Compare(a.A, b) != Greater, b);
				/// <summary>Adds an equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the equal operation.</param>
				/// <returns>A running inequality with the additonal equal operation.</returns>
				public static Inequality<T> operator ==(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Equate(a.A, b), b);
				/// <summary>Adds an inequal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the inequal operation.</param>
				/// <returns>A running inequality with the additonal inequal operation.</returns>
				public static Inequality<T> operator !=(Inequality<T> a, T b) =>
					new Inequality<T>(a.Result && Inequate(a.A, b), b);
				/// <summary>Converts the result of this inequality to a <see cref="string"/>.</summary>
				/// <returns>The result of this inequality converted to a <see cref="string"/>.</returns>
				public override string ToString() => Result.ToString();
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
				/// <summary>This member is not intended to be invoked.</summary>
				/// <param name="obj">This member is not intended to be invoked.</param>
				/// <returns>This member is not intended to be invoked.</returns>
				[Obsolete(TowelConstants.NotIntended, true)]
				public override bool Equals(object? obj) => throw new InequalitySyntaxException();
				/// <summary>This member is not intended to be invoked.</summary>
				/// <returns>This member is not intended to be invoked.</returns>
				[Obsolete(TowelConstants.NotIntended, true)]
				public override int GetHashCode() => throw new InequalitySyntaxException();
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
			}
		}

		#endregion

		#region UniversalQuantification

		/// <summary>Universal Quantification Operator.</summary>
		/// <typeparam name="T">The element type of the universal quantification to declare.</typeparam>
		/// <param name="values">The values of the universal quantification.</param>
		/// <returns>The declared universal quantification.</returns>
		public static UniversalQuantification<T> Ɐ<T>(params T[] values) => new UniversalQuantification<T>(values);

		/// <summary>Universal Quantification.</summary>
		/// <typeparam name="T">The element type of the universal quantification.</typeparam>
		public struct UniversalQuantification<T> :
			System.Collections.Generic.IEnumerable<T>,
			System.Collections.Generic.IList<T>,
			Towel.DataStructures.IArray<T>
		{
			internal T[] Value;

			/// <summary>Constructs a new universal quantification from an array.</summary>
			/// <param name="array">The array value of the universal quantification.</param>
			internal UniversalQuantification(T[] array) => Value = array;

			#region Towel.Datastructures.IArray<T>
			/// <summary>The number of values in this universal quantification.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public int Length => Value.Length;
			/// <summary>Iterates each value in this universal quantification and performs an action for each element.</summary>
			/// <param name="step">The action to perform on every step of the iteration.</param>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void Stepper(Action<T> step) => Value.Stepper(step);
			/// <summary>Iterates each value in this universal quantification and performs an action for each element.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public StepStatus Stepper(Func<T, StepStatus> step) => Value.Stepper(step);
			#endregion

			#region System.Collections.Generic.IList<T>
			/// <summary>Index property for get/set operations.</summary>
			/// <param name="index">The index to get/set.</param>
			/// <returns>The value at the provided index.</returns>
			[Obsolete(TowelConstants.NotIntended, true)]
			public T this[int index]
			{
				get => Value[index];
				set => Value[index] = value;
			}
			/// <summary>Gets the number of elements in this universal quantification.</summary>
			public int Count => Value.Length;
			/// <summary>Gets a value indicating whether the <see cref="System.Collections.Generic.ICollection{T}"/> is read-only.</summary>
			public bool IsReadOnly => false;
			/// <summary>Adds an item to this universal quantifier.</summary>
			/// <param name="item">The item to add to this universal quantifier.</param>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void Add(T item)
			{
				T[] newValue = new T[Value.Length + 1];
				Array.Copy(Value, newValue, Value.Length);
				newValue[Value.Length] = item;
				Value = newValue;
			}
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void Clear() => Value = Array.Empty<T>();
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public bool Contains(T item) => Value.Contains(item);
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void CopyTo(T[] array, int arrayIndex) =>
				Array.Copy(Value, 0, array, arrayIndex, Value.Length);
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public int IndexOf(T item) => Array.IndexOf(Value, item);
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void Insert(int index, T item)
			{
				T[] newValue = new T[Value.Length + 1];
				for (int i = 0; i < newValue.Length; i++)
				{
					newValue[i] = i == index
						? item
						: i < index
							? Value[i]
							: Value[i - 1];
				}
				Value = newValue;
			}
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public bool Remove(T item)
			{
				T[] newValue = new T[Value.Length - 1];
				bool found = false;
				for (int i = 0; i < Value.Length; i++)
				{
					if (Equate(Value[i], item))
					{
						found = true;
					}
					else if (found)
					{
						newValue[i] = Value[i - 1];
					}
					else
					{
						newValue[i] = Value[i];
					}
				}
				if (!found)
				{
					return false;
				}
				Value = newValue;
				return true;
			}
			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, true)]
			public void RemoveAt(int index)
			{
				T[] newValue = new T[Value.Length - 1];
				for (int i = 0; i < Value.Length; i++)
				{
					if (i != index)
					{
						if (i < index)
						{
							newValue[i] = Value[i];
						}
						else
						{
							newValue[i] = Value[i - 1];
						}
					}
				}
				Value = newValue;
			}
			#endregion

			#region System.Collections.Generic.IEnumerable<T>
			/// <summary>Gets the <see cref="System.Collections.Generic.IEnumerator{T}"/> for this universal quantification.</summary>
			/// <returns>The <see cref="System.Collections.Generic.IEnumerator{T}"/> for this universal quantification.</returns>
			[Obsolete(TowelConstants.NotIntended, true)]
			public System.Collections.Generic.IEnumerator<T> GetEnumerator() => ((System.Collections.Generic.IEnumerable<T>)Value).GetEnumerator();
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Value.GetEnumerator();
			#endregion

			#region Implicit Casting Operators

			/// <summary>Converts a universal quantification to an array.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator T[](UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to a <see cref="System.Collections.Generic.List{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.List<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.List<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.HashSet{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.HashSet<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.HashSet<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to a <see cref="System.Collections.Generic.LinkedList{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.LinkedList<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.LinkedList<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.Stack{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.Stack<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.Stack<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.Queue{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.Queue<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.Queue<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to a sorted <see cref="System.Collections.Generic.SortedSet{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.SortedSet<T>(UniversalQuantification<T> universalQuantification) => new System.Collections.Generic.SortedSet<T>(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an Action&lt;Action&lt;T&gt;&gt;.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Action<Action<T>>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepper();
			/// <summary>Converts a universal quantification to an <see cref="StepperRef{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator StepperRef<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepperRef();
			/// <summary>Converts a universal quantification to an Func&lt;Func&lt;T, StepStatus&gt;, StepStatus&gt;.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Func<Func<T, StepStatus>, StepStatus>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepperBreak();
			/// <summary>Converts a universal quantification to an <see cref="StepperRefBreak{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator StepperRefBreak<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepperRefBreak();
			/// <summary>Converts a universal quantification to an <see cref="Array{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.Array<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to an <see cref="ListArray{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.ListArray<T>(UniversalQuantification<T> universalQuantification) => new ListArray<T>(universalQuantification.Value, universalQuantification.Value.Length);
			/// <summary>Converts a universal quantification to an <see cref="StackArray{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.StackArray<T>(UniversalQuantification<T> universalQuantification) => new StackArray<T>(universalQuantification.Value, universalQuantification.Value.Length);

			#endregion
		}

		#endregion

		#region Equate

		#if false
		/// <summary>Checks for equality of two values [<paramref name="a"/> == <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the equality.</returns>
		public static C Equate<A, B, C>(A a, B b) =>
			EquateImplementation<A, B, C>.Function(a, b);
		#endif

		/// <summary>Checks for equality of two values [<paramref name="a"/> == <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the equality check.</returns>
		public static bool Equate<T>(T a, T b) =>
			EquateImplementation<T, T, bool>.Function(a, b);

		/// <summary>Checks for equality among multiple values [<paramref name="a"/> == <paramref name="b"/> == <paramref name="c"/> == ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <param name="c">The remaining operands of the equality check.</param>
		/// <returns>True if all operands are equal or false if not.</returns>
		public static bool Equate<T>(T a, T b, params T[] c)
		{
			_ = c ?? throw new ArgumentNullException(nameof(c));
			if (c.Length == 0) throw new ArgumentException("The array is empty.", nameof(c));
			if (!Equate(a, b))
			{
				return false;
			}
			for (int i = 0; i < c.Length; i++)
			{
				if (!Equate(a, c[i]))
				{
					return false;
				}
			}
			return true;
		}

		internal static class EquateImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				// todo: add null equality checks
				// todo: I need to kill this try-catch...
				try
				{
					ParameterExpression A = Expression.Parameter(typeof(A));
					ParameterExpression B = Expression.Parameter(typeof(B));
					Expression BODY = Expression.Equal(A, B);
					Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
					return Function(a, b);
				}
				catch
				{

				}

				if (typeof(C) == typeof(bool))
				{
					EquateImplementation<A, B, bool>.Function = (_A, _B) => _A.Equals(_B);
					return Function!(a, b);
				}

				throw new NotImplementedException();
			};
		}

		/// <summary>Determines if two sequences are equal.</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="A">The first sequence of the equate.</typeparam>
		/// <typeparam name="B">The second sequence of the equate.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <param name="start">The inclusive starting index to equate from.</param>
		/// <param name="end">The inclusive ending index to equate to.</param>
		/// <param name="a">The first sequence of the equate.</param>
		/// <param name="b">The second sequence of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool Equate<T, A, B, Equate>(int start, int end, A a = default, B b = default, Equate equate = default)
			where A : struct, IFunc<int, T>
			where B : struct, IFunc<int, T>
			where Equate : struct, IFunc<T, T, bool>
		{
			for (int i = start; i <= end; i++)
			{
				if (!equate.Do(a.Do(i), b.Do(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool Equate<T>(Span<T> a, Span<T> b, Func<T, T, bool>? equate = default) =>
			Equate<T>(0, a.Length - 1, a, b, equate);

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool Equate<T, Equate>(Span<T> a, Span<T> b, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool> =>
			Equate<T, Equate>(0, a.Length - 1, a, b, equate);

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <param name="start">The inclusive starting index to equate from.</param>
		/// <param name="end">The inclusive ending index to equate to.</param>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool Equate<T>(int start, int end, Span<T> a, Span<T> b, Func<T, T, bool>? equate = default) =>
			Equate<T, FuncRuntime<T, T, bool>>(start, end, a, b, equate ?? Equate);

		/// <summary>Determines if two spans are equal.</summary>
		/// <typeparam name="T">The element type of the spans.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <param name="start">The inclusive starting index to equate from.</param>
		/// <param name="end">The inclusive ending index to equate to.</param>
		/// <param name="a">The first span of the equate.</param>
		/// <param name="b">The second span of the equate.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the spans are equal; False if not.</returns>
		public static bool Equate<T, Equate>(int start, int end, Span<T> a, Span<T> b, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(start), start, $@"{nameof(start)} < 0");
			}
			if (end >= Minimum(a.Length, b.Length))
			{
				throw new ArgumentOutOfRangeException(nameof(end), end, $@"{nameof(end)} >= Min({nameof(a)}.{nameof(a.Length)} {a.Length}, {nameof(b)}.{nameof(b.Length)} {b.Length})");
			}
			if (end < start)
			{
				throw new ArgumentOutOfRangeException($@"{nameof(end)} {end} < {nameof(start)} {start}");
			}
			if (a.IsEmpty && b.IsEmpty)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = start; i <= end; i++)
			{
				if (!equate.Do(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Inequate

		/// <summary>Checks for inequality of two values [<paramref name="a"/> != <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the inequality.</returns>
		public static C Inequate<A, B, C>(A a, B b) =>
			InequateImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks for inequality of two values [<paramref name="a"/> != <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the inequality check.</param>
		/// <param name="b">The second operand of the inequality check.</param>
		/// <returns>The result of the inequality check.</returns>
		public static bool Inequate<T>(T a, T b) =>
			Inequate<T, T, bool>(a, b);

		internal static class InequateImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.NotEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region LessThan

		/// <summary>Checks if one value is less than another [<paramref name="a"/> &lt; <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than operation.</returns>
		public static C LessThan<A, B, C>(A a, B b) =>
			LessThanImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is less than another [<paramref name="a"/> &lt; <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the less than check.</param>
		/// <param name="b">The second operand of the less than check.</param>
		/// <returns>The result of the less than check.</returns>
		public static bool LessThan<T>(T a, T b) =>
			LessThan<T, T, bool>(a, b);

		internal static class LessThanImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.LessThan(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region GreaterThan

		/// <summary>Checks if one value is greater than another [<paramref name="a"/> &gt; <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than operation.</returns>
		public static C GreaterThan<A, B, C>(A a, B b) =>
			GreaterThanImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is greater than another [<paramref name="a"/> &gt; <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the greater than check.</param>
		/// <param name="b">The second operand of the greater than check.</param>
		/// <returns>The result of the greater than check.</returns>
		public static bool GreaterThan<T>(T a, T b) =>
			GreaterThan<T, T, bool>(a, b);

		internal static class GreaterThanImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.GreaterThan(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region LessThanOrEqual

		/// <summary>Checks if one value is less than or equal to another [<paramref name="a"/> &lt;= <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the less than or equal to operation.</returns>
		public static C LessThanOrEqual<A, B, C>(A a, B b) =>
			LessThanOrEqualImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is less than or equal to another [<paramref name="a"/> &lt;= <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the less than or equal to check.</param>
		/// <param name="b">The second operand of the less than or equal to check.</param>
		/// <returns>The result of the less than or equal to check.</returns>
		public static bool LessThanOrEqual<T>(T a, T b) =>
			LessThanOrEqual<T, T, bool>(a, b);

		internal static class LessThanOrEqualImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.LessThanOrEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region GreaterThanOrEqual

		/// <summary>Checks if one value is less greater or equal to another [<paramref name="a"/> &gt;= <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the greater than or equal to operation.</returns>
		public static C GreaterThanOrEqual<A, B, C>(A a, B b) =>
			GreaterThanOrEqualImplementation<A, B, C>.Function(a, b);

		/// <summary>Checks if one value is greater than or equal to another [<paramref name="a"/> &gt;= <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the greater than or equal to check.</param>
		/// <param name="b">The second operand of the greater than or equal to check.</param>
		/// <returns>The result of the greater than or equal to check.</returns>
		public static bool GreaterThanOrEqual<T>(T a, T b) =>
			GreaterThanOrEqual<T, T, bool>(a, b);

		internal static class GreaterThanOrEqualImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.GreaterThanOrEqual(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Compare

		#if false
		/// <summary>Compares two values.</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the comparison.</returns>
		public static C Compare<A, B, C>(A a, B b) =>
			CompareImplementation<A, B, C>.Function(a, b);
		#endif

		/// <summary>Compares two values.</summary>
		/// <typeparam name="T">The type of values to compare.</typeparam>
		/// <param name="a">The first value of the comparison.</param>
		/// <param name="b">The second value of the comparison.</param>
		/// <returns>The result of the comparison.</returns>
		public static CompareResult Compare<T>(T a, T b) =>
			CompareImplementation<T, T, CompareResult>.Function(a, b);

		internal static class CompareImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				if (typeof(C) == typeof(CompareResult))
				{
					if (typeof(A) == typeof(B) && a is IComparable<B> &&
						!(typeof(A).IsPrimitive && typeof(B).IsPrimitive))
					{
						CompareImplementation<A, A, CompareResult>.Function =
							(a, b) => System.Collections.Generic.Comparer<A>.Default.Compare(a, b).ToCompareResult();
					}
					else
					{
						ParameterExpression A = Expression.Parameter(typeof(A));
						ParameterExpression B = Expression.Parameter(typeof(B));

						Expression? lessThanPredicate =
							typeof(A).IsPrimitive && typeof(B).IsPrimitive
							? Expression.LessThan(A, B)
							: Meta.GetLessThanMethod<A, B, bool>() is not null
								? Expression.LessThan(A, B)
								: Meta.GetGreaterThanMethod<B, A, bool>() is not null
									? Expression.GreaterThan(B, A)
									: null;

						Expression? greaterThanPredicate =
							typeof(A).IsPrimitive && typeof(B).IsPrimitive
							? Expression.GreaterThan(A, B)
							: Meta.GetGreaterThanMethod<A, B, bool>() is not null
								? Expression.GreaterThan(A, B)
								: Meta.GetLessThanMethod<B, A, bool>() is not null
									? Expression.LessThan(B, A)
									: null;

						if (lessThanPredicate is null || greaterThanPredicate is null)
						{
							throw new NotSupportedException("You attempted a comparison operation with unsupported types.");
						}

						LabelTarget RETURN = Expression.Label(typeof(CompareResult));
						Expression BODY = Expression.Block(
							Expression.IfThen(
									lessThanPredicate,
									Expression.Return(RETURN, Expression.Constant(Less, typeof(CompareResult)))),
								Expression.IfThen(
									greaterThanPredicate,
									Expression.Return(RETURN, Expression.Constant(Greater, typeof(CompareResult)))),
								Expression.Return(RETURN, Expression.Constant(Equal, typeof(CompareResult))),
								Expression.Label(RETURN, Expression.Constant(default(CompareResult), typeof(CompareResult))));
						CompareImplementation<A, B, CompareResult>.Function = Expression.Lambda<Func<A, B, CompareResult>>(BODY, A, B).Compile();
					}
					return Function!(a, b);
				}
				throw new NotImplementedException();
			};
		}

		#endregion

		#region Negation

		/// <summary>Negates a value [-<paramref name="a"/>].</summary>
		/// <typeparam name="A">The type of the value to negate.</typeparam>
		/// <typeparam name="B">The resulting type of the negation.</typeparam>
		/// <param name="a">The value to negate.</param>
		/// <returns>The result of the negation [-<paramref name="a"/>].</returns>
		public static B Negation<A, B>(A a) =>
			NegationImplementation<A, B>.Function(a);

		/// <summary>Negates a value [-<paramref name="a"/>].</summary>
		/// <typeparam name="T">The type of the value to negate.</typeparam>
		/// <param name="a">The value to negate.</param>
		/// <returns>The result of the negation [-<paramref name="a"/>].</returns>
		public static T Negation<T>(T a) =>
			Negation<T, T>(a);

		internal static class NegationImplementation<A, B>
		{
			internal static Func<A, B> Function = a =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				Expression BODY = Expression.Negate(A);
				Function = Expression.Lambda<Func<A, B>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region Addition

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static C Addition<A, B, C>(A a, B b) =>
			AdditionImplementation<A, B, C>.Function(a, b);

		/// <summary>Adds two values [<paramref name="a"/> + <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/>].</returns>
		public static T Addition<T>(T a, T b) =>
			Addition<T, T, T>(a, b);

		/// <summary>Adds multiple values [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand of the addition.</param>
		/// <param name="b">The second operand of the addition.</param>
		/// <param name="c">The third operand of the addition.</param>
		/// <param name="d">The remaining operands of the addition.</param>
		/// <returns>The result of the addition [<paramref name="a"/> + <paramref name="b"/> + <paramref name="c"/> + ...].</returns>
		public static T Addition<T>(T a, T b, T c, params T[] d) =>
			Addition<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Adds multiple values [step1 + step2 + step3 + ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper of the values to add.</param>
		/// <returns>The result of the addition [step1 + step2 + step3 + ...].</returns>
		public static T Addition<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Addition);

		internal static class AdditionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Add(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Subtraction

		/// <summary>Subtracts two values [<paramref name="a"/> - <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/>].</returns>
		public static C Subtraction<A, B, C>(A a, B b) =>
			SubtractionImplementation<A, B, C>.Function(a, b);

		/// <summary>Subtracts two values [<paramref name="a"/> - <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/>].</returns>
		public static T Subtraction<T>(T a, T b) =>
			Subtraction<T, T, T>(a, b);

		/// <summary>Subtracts multiple values [<paramref name="a"/> - <paramref name="b"/> - <paramref name="c"/> - ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <param name="c">The third operand.</param>
		/// <param name="d">The remaining values.</param>
		/// <returns>The result of the subtraction [<paramref name="a"/> - <paramref name="b"/> - <paramref name="c"/> - ...].</returns>
		public static T Subtraction<T>(T a, T b, T c, params T[] d) =>
			Subtraction<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Subtracts multiple numeric values [step1 - step2 - step3 - ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the subtraction [step1 - step2 - step3 - ...].</returns>
		public static T Subtraction<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Subtraction);

		internal static class SubtractionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Subtract(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Multiplication

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static C Multiplication<A, B, C>(A a, B b) =>
			MultiplicationImplementation<A, B, C>.Function(a, b);

		/// <summary>Multiplies two values [<paramref name="a"/> * <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/>].</returns>
		public static T Multiplication<T>(T a, T b) =>
			Multiplication<T, T, T>(a, b);

		/// <summary>Multiplies multiple values [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand.</param>
		/// <param name="b">The second operand.</param>
		/// <param name="c">The third operand.</param>
		/// <param name="d">The remaining values.</param>
		/// <returns>The result of the multiplication [<paramref name="a"/> * <paramref name="b"/> * <paramref name="c"/> * ...].</returns>
		public static T Multiplication<T>(T a, T b, T c, params T[] d) =>
			Multiplication<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Multiplies multiple values [step1 * step2 * step3 * ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the multiplication [step1 * step2 * step3 * ...].</returns>
		public static T Multiplication<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Multiplication);

		internal static class MultiplicationImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Multiply(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Division

		/// <summary>Divides two values [<paramref name="a"/> / <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/>].</returns>
		public static C Division<A, B, C>(A a, B b) =>
			DivisionImplementation<A, B, C>.Function(a, b);

		/// <summary>Divides two values [<paramref name="a"/> / <paramref name="b"/>].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/>].</returns>
		public static T Division<T>(T a, T b) =>
			Division<T, T, T>(a, b);

		/// <summary>Divides multiple values [<paramref name="a"/> / <paramref name="b"/> / <paramref name="c"/> / ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="a">The first operand of the division.</param>
		/// <param name="b">The second operand of the division.</param>
		/// <param name="c">The third operand of the division.</param>
		/// <param name="d">The remaining values of the division.</param>
		/// <returns>The result of the division [<paramref name="a"/> / <paramref name="b"/> / <paramref name="c"/> / ...].</returns>
		public static T Division<T>(T a, T b, T c, params T[] d) =>
			Division<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Divides multiple values [step1 / step2 / step3 / ...].</summary>
		/// <typeparam name="T">The type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the division [step1 / step2 / step3 / ...].</returns>
		public static T Division<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Division);

		internal static class DivisionImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Divide(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Remainder

		/// <summary>Remainders two values [<paramref name="a"/> % <paramref name="b"/>].</summary>
		/// <typeparam name="A">The type of the left operand.</typeparam>
		/// <typeparam name="B">The type of the right operand.</typeparam>
		/// <typeparam name="C">The type of the return.</typeparam>
		/// <param name="a">The left operand.</param>
		/// <param name="b">The right operand.</param>
		/// <returns>The result of the remainder operation [<paramref name="a"/> % <paramref name="b"/>].</returns>
		public static C Remainder<A, B, C>(A a, B b) =>
			RemainderImplementation<A, B, C>.Function(a, b);

		/// <summary>Modulos two numeric values [<paramref name="a"/> % <paramref name="b"/>].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the modulation.</param>
		/// <param name="b">The second operand of the modulation.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(T a, T b) =>
			Remainder<T, T, T>(a, b);

		/// <summary>Modulos multiple numeric values [<paramref name="a"/> % <paramref name="b"/> % <paramref name="c"/> % ...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the modulation.</param>
		/// <param name="b">The second operand of the modulation.</param>
		/// <param name="c">The third operand of the modulation.</param>
		/// <param name="d">The remaining values of the modulation.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(T a, T b, T c, params T[] d) =>
			Remainder<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Modulos multiple numeric values [step_1 % step_2 % step_3...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the modulation.</returns>
		public static T Remainder<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Remainder);

		internal static class RemainderImplementation<A, B, C>
		{
			internal static Func<A, B, C> Function = (a, b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(A));
				ParameterExpression B = Expression.Parameter(typeof(B));
				Expression BODY = Expression.Modulo(A, B);
				Function = Expression.Lambda<Func<A, B, C>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		#endregion

		#region Inversion

		/// <summary>Inverts a numeric value [1 / a].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The numeric value to invert.</param>
		/// <returns>The result of the inversion.</returns>
		public static T Inversion<T>(T a) =>
			Division(Constant<T>.One, a);

		#endregion

		#region Power

		/// <summary>Powers two numeric values [a ^ b].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the power.</param>
		/// <param name="b">The first operand of the power.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(T a, T b) =>
			PowerImplementation<T>.Function(a, b);

		/// <summary>Powers multiple numeric values [a ^ b ^ c...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the power.</param>
		/// <param name="b">The second operand of the power.</param>
		/// <param name="c">The third operand of the power.</param>
		/// <param name="d">The remaining values of the power.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(T a, T b, T c, params T[] d) =>
			Power<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Powers multiple numeric values [step_1 ^ step_2 ^ step_3...].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The stepper containing the values.</param>
		/// <returns>The result of the power.</returns>
		public static T Power<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Power);

		internal static class PowerImplementation<T>
		{
			internal static Func<T, T, T> Function = (a, b) =>
			{
				// Note: this code needs to die.. but this works until it gets a better version

				// optimization for specific known types
				if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
				{
					ParameterExpression A = Expression.Parameter(typeof(T));
					ParameterExpression B = Expression.Parameter(typeof(T));
					MethodInfo? Math_Pow = typeof(Math).GetMethod(nameof(Math.Pow));
					if (Math_Pow is not null)
					{
						Expression BODY = Expression.Convert(Expression.Call(Math_Pow, Expression.Convert(A, typeof(double)), Expression.Convert(B, typeof(double))), typeof(T));
						Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
						return Function(a, b);
					}
				}
				
				Function = (A, B) =>
				{
					if (IsInteger(B) && IsPositive(B) && LessThan(B, Convert<int, T>(int.MaxValue)))
					{
						T result = A;
						int power = Convert<T, int>(B);
						for (int i = 0; i < power; i++)
						{
							result = Multiplication(result, A);
						}
						return result;
					}
					else
					{
						throw new NotImplementedException("This feature is still in development.");
					}
				};

				return Function(a, b);
			};
		}

		#endregion

		#region SquareRoot

		/// <summary>Square roots a numeric value [√a].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The numeric value to square root.</param>
		/// <returns>The result of the square root.</returns>
		public static T SquareRoot<T>(T a) =>
			SquareRootImplementation<T>.Function(a);

		internal static class SquareRootImplementation<T>
		{
			internal static Func<T, T> Function = a =>
			{
				#region Optimization(int)

				if (typeof(T) == typeof(int))
				{
					static int SquareRoot(int x)
					{
						if (x == 0 || x == 1)
						{
							return x;
						}
						int start = 1, end = x, ans = 0;
						while (start <= end)
						{
							int mid = (start + end) / 2;
							if (mid * mid == x)
							{
								return mid;
							}
							if (mid * mid < x)
							{
								start = mid + 1;
								ans = mid;
							}
							else
							{
								end = mid - 1;
							}
						}
						return ans;
					}
					SquareRootImplementation<int>.Function = SquareRoot;
					return Function!(a);
				}

				#endregion

				return Root(a, Constant<T>.Two);
			};
		}

		#endregion

		#region Root

		/// <summary>Roots two numeric values [a ^ (1 / b)].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The base of the root.</param>
		/// <param name="b">The root of the operation.</param>
		/// <returns>The result of the root.</returns>
		public static T Root<T>(T a, T b) =>
			Power(a, Inversion(b));

		#endregion

		#region Logarithm

		/// <summary>Computes the logarithm of a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="value">The value to compute the logarithm of.</param>
		/// <param name="base">The base of the logarithm to compute.</param>
		/// <returns>The computed logarithm value.</returns>
		public static T Logarithm<T>(T value, T @base) =>
			LogarithmImplementation<T>.Function(value, @base);

		internal static class LogarithmImplementation<T>
		{
			internal static Func<T, T, T> Function = (a, b) =>
			{
				throw new NotImplementedException();

				//ParameterExpression A = Expression.Parameter(typeof(T));
				//ParameterExpression B = Expression.Parameter(typeof(T));
				//Expression BODY = ;
				//Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
				//return Function(a, b);
			};
		}

		#endregion

		#region IsInteger

		/// <summary>Determines if a numerical value is an integer.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine integer status of.</param>
		/// <returns>Whether or not the value is an integer.</returns>
		public static bool IsInteger<T>(T a) =>
			IsIntegerImplementation<T>.Function(a);

		internal static class IsIntegerImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsIntegerMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Equal(
					Expression.Modulo(A, Expression.Constant(Constant<T>.One)),
					Expression.Constant(Constant<T>.Zero));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsNonNegative

		/// <summary>Determines if a numerical value is non-negative.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine non-negative status of.</param>
		/// <returns>Whether or not the value is non-negative.</returns>
		public static bool IsNonNegative<T>(T a) =>
			IsNonNegativeImplementation<T>.Function(a);

		internal static class IsNonNegativeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsNonNegativeMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				Expression BODY = Expression.GreaterThanOrEqual(A, Expression.Constant(Constant<T>.Zero));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsNegative

		/// <summary>Determines if a numerical value is negative.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine negative status of.</param>
		/// <returns>Whether or not the value is negative.</returns>
		public static bool IsNegative<T>(T a) =>
			IsNegativeImplementation<T>.Function(a);

		internal static class IsNegativeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsNegativeMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				LabelTarget RETURN = Expression.Label(typeof(bool));
				Expression BODY = Expression.LessThan(A, Expression.Constant(Constant<T>.Zero));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsPositive

		/// <summary>Determines if a numerical value is positive.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine positive status of.</param>
		/// <returns>Whether or not the value is positive.</returns>
		public static bool IsPositive<T>(T a) =>
			IsPositiveImplementation<T>.Function(a);

		internal static class IsPositiveImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsPositiveMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				Expression BODY = Expression.GreaterThan(A, Expression.Constant(Constant<T>.Zero));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsEven

		/// <summary>Determines if a numerical value is even.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine even status of.</param>
		/// <returns>Whether or not the value is even.</returns>
		public static bool IsEven<T>(T a) =>
			IsEvenImplementation<T>.Function(a);

		internal static class IsEvenImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsEvenMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Equal(Expression.Modulo(A, Expression.Constant(Constant<T>.Two)), Expression.Constant(Constant<T>.Zero));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsOdd

		/// <summary>Determines if a numerical value is odd.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine odd status of.</param>
		/// <returns>Whether or not the value is odd.</returns>
		public static bool IsOdd<T>(T a) =>
			IsOddImplementation<T>.Function(a);

		internal static class IsOddImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsOddMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				ParameterExpression A = Expression.Parameter(typeof(T));
				Expression BODY =
					Expression.Block(
						Expression.IfThen(
							Expression.LessThan(A, Expression.Constant(Constant<T>.Zero)),
							Expression.Assign(A, Expression.Negate(A))),
						Expression.Equal(Expression.Modulo(A, Expression.Constant(Constant<T>.Two)), Expression.Constant(Constant<T>.One)));
				Function = Expression.Lambda<Func<T, bool>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region IsPrime

		/// <summary>Determines if a numerical value is prime.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to determine prime status of.</param>
		/// <returns>Whether or not the value is prime.</returns>
		public static bool IsPrime<T>(T a) =>
			IsPrimeImplementation<T>.Function(a);

		internal static class IsPrimeImplementation<T>
		{
			internal static Func<T, bool> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetIsPrimeMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, bool>>();
					return Function(a);
				}

				// This can be optimized
				Function = A =>
				{
					if (IsInteger(A) && !LessThan(A, Constant<T>.Two))
					{
						if (Equate(A, Constant<T>.Two))
						{
							return true;
						}
						if (IsEven(A))
						{
							return false;
						}
						T squareRoot = SquareRoot(A);
						for (T divisor = Constant<T>.Three; LessThanOrEqual(divisor, squareRoot); divisor = Addition(divisor, Constant<T>.Two))
						{
							if (Equate(Remainder<T>(A, divisor), Constant<T>.Zero))
							{
								return false;
							}
						}
						return true;
					}
					else
					{
						return false;
					}
				};
				return Function(a);
			};
		}

		#endregion

		#region AbsoluteValue

		/// <summary>Gets the absolute value of a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to get the absolute value of.</param>
		/// <returns>The absolute value of the provided value.</returns>
		public static T AbsoluteValue<T>(T a) =>
			AbsoluteValueImplementation<T>.Function(a);

		internal static class AbsoluteValueImplementation<T>
		{
			internal static Func<T, T> Function = a =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				LabelTarget RETURN = Expression.Label(typeof(T));
				Expression BODY = Expression.Block(
					Expression.IfThenElse(
						Expression.LessThan(A, Expression.Constant(Constant<T>.Zero)),
						Expression.Return(RETURN, Expression.Negate(A)),
						Expression.Return(RETURN, A)),
					Expression.Label(RETURN, Expression.Constant(default(T), typeof(T))));
				Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
				return Function(a);
			};
		}

		#endregion

		#region Maximum

		/// <summary>Computes the maximum of two numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the maximum operation.</param>
		/// <param name="b">The second operand of the maximum operation.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T Maximum<T>(T a, T b) =>
			GreaterThanOrEqual(a, b) ? a : b;

		/// <summary>Computes the maximum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the maximum operation.</param>
		/// <param name="b">The second operand of the maximum operation.</param>
		/// <param name="c">The third operand of the maximum operation.</param>
		/// <param name="d">The remaining operands of the maximum operation.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T Maximum<T>(T a, T b, T c, params T[] d) =>
			Maximum<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Computes the maximum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the maximum of.</param>
		/// <returns>The computed maximum of the provided values.</returns>
		public static T Maximum<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Maximum);

		#endregion

		#region Minimum

		/// <summary>Computes the minimum of two numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b) =>
			LessThanOrEqual(a, b) ? a : b;

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <param name="c">The third operand of the minimum operation.</param>
		/// <param name="d">The remaining operands of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(T a, T b, T c, params T[] d) =>
			Minimum<T>(step => { step(a); step(b); step(c); d.ToStepper()(step); });

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the minimum of.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static T Minimum<T>(Action<Action<T>> stepper) =>
			OperationOnStepper(stepper, Minimum);

		/// <summary>Computes the minimum of multiple numeric values.</summary>
		/// <param name="a">The first operand of the minimum operation.</param>
		/// <param name="b">The second operand of the minimum operation.</param>
		/// <param name="c">The third operand of the minimum operation.</param>
		/// <returns>The computed minimum of the provided values.</returns>
		public static int Minimum(int a, int b, int c) => Math.Min(Math.Min(a, b), c);

		#endregion

		#region Clamp

		/// <summary>Gets a value restricted to a minimum and maximum range.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="value">The value to clamp.</param>
		/// <param name="minimum">The minimum of the range to clamp the value by.</param>
		/// <param name="maximum">The maximum of the range to clamp the value by.</param>
		/// <returns>The value restricted to the provided range.</returns>
		public static T Clamp<T>(T value, T minimum, T maximum) =>
			LessThan(value, minimum)
			? minimum
			: GreaterThan(value, maximum)
				? maximum
				: value;

		#endregion

		#region EqualToLeniency

		/// <summary>Checks for equality between two numeric values with a range of possibly leniency.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the equality check.</param>
		/// <param name="b">The second operand of the equality check.</param>
		/// <param name="leniency">The allowed distance between the values to still be considered equal.</param>
		/// <returns>True if the values are within the allowed leniency of each other. False if not.</returns>
		public static bool EqualToLeniency<T>(T a, T b, T leniency) =>
			// TODO: add an ArgumentOutOfBounds check on leniency
			EqualToLeniencyImplementation<T>.Function(a, b, leniency);

		internal static class EqualToLeniencyImplementation<T>
		{
			internal static Func<T, T, T, bool> Function = (T a, T b, T c) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				ParameterExpression C = Expression.Parameter(typeof(T));
				ParameterExpression D = Expression.Variable(typeof(T));
				LabelTarget RETURN = Expression.Label(typeof(bool));
				Expression BODY = Expression.Block(Ɐ(D),
					Expression.Assign(D, Expression.Subtract(A, B)),
					Expression.IfThenElse(
						Expression.LessThan(D, Expression.Constant(Constant<T>.Zero)),
						Expression.Assign(D, Expression.Negate(D)),
						Expression.Assign(D, D)),
					Expression.Return(RETURN, Expression.LessThanOrEqual(D, C), typeof(bool)),
					Expression.Label(RETURN, Expression.Constant(default(bool))));
				Function = Expression.Lambda<Func<T, T, T, bool>>(BODY, A, B, C).Compile();
				return Function(a, b, c);
			};
		}

		#endregion

		#region GreatestCommonFactor

		/// <summary>Computes the greatest common factor of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="a">The first operand of the greatest common factor computation.</param>
		/// <param name="b">The second operand of the greatest common factor computation.</param>
		/// <param name="c">The remaining operands of the greatest common factor computation.</param>
		/// <returns>The computed greatest common factor of the set of numbers.</returns>
		public static T GreatestCommonFactor<T>(T a, T b, params T[] c) =>
			GreatestCommonFactor<T>(step => { step(a); step(b); c.ToStepper()(step); });

		/// <summary>Computes the greatest common factor of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the greatest common factor of.</param>
		/// <returns>The computed greatest common factor of the set of numbers.</returns>
		public static T GreatestCommonFactor<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			bool assigned = false;
			T answer = Constant<T>.Zero;
			stepper((T n) =>
			{
				_ = n ?? throw new ArgumentNullException(nameof(n));
				if (Equate(n, Constant<T>.Zero))
				{
					throw new MathematicsException("Encountered Zero (0) while computing the " + nameof(GreatestCommonFactor));
				}
				else if (!IsInteger(n))
				{
					throw new MathematicsException(nameof(stepper) + " contains non-integer value(s).");
				}
				if (!assigned)
				{
					answer = AbsoluteValue(n);
					assigned = true;
				}
				else
				{
					if (GreaterThan(answer, Constant<T>.One))
					{
						T a = answer;
						T b = n;
						while (Inequate(b, Constant<T>.Zero))
						{
							T remainder = Remainder(a, b);
							a = b;
							b = remainder;
						}
						answer = AbsoluteValue(a);
					}
				}
			});
			if (!assigned)
			{
				throw new ArgumentException(nameof(stepper) + " is empty.", nameof(stepper));
			}
			return answer;
		}

		#endregion

		#region LeastCommonMultiple

		/// <summary>Computes the least common multiple of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first operand of the least common muiltiple computation.</param>
		/// <param name="b">The second operand of the least common muiltiple computation.</param>
		/// <param name="c">The remaining operands of the least common muiltiple computation.</param>
		/// <returns>The computed least common least common multiple of the set of numbers.</returns>
		public static T LeastCommonMultiple<T>(T a, T b, params T[] c) =>
			LeastCommonMultiple<T>(step => { step(a); step(b); c.ToStepper()(step); });

		/// <summary>Computes the least common multiple of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the least common multiple of.</param>
		/// <returns>The computed least common least common multiple of the set of numbers.</returns>
		public static T LeastCommonMultiple<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			bool assigned = false;
			T answer = default;
			stepper(parameter =>
			{
				if (Equate(parameter, Constant<T>.Zero))
				{
					throw new MathematicsException(nameof(stepper) + " contains 0 value(s).");
				}
				if (!IsInteger(parameter))
				{
					throw new MathematicsException(nameof(stepper) + " contains non-integer value(s).");
				}
				parameter = AbsoluteValue(parameter);
				if (!assigned)
				{
					answer = parameter;
					assigned = true;
				}
				else
				{
					answer = Division(Multiplication(answer, parameter), GreatestCommonFactor(answer, parameter));
				}
			});
			if (!assigned)
			{
				throw new ArgumentException(nameof(stepper) + " is empty.", nameof(stepper));
			}
			return answer!;
		}

		#endregion

		#region LinearInterpolation

		/// <summary>Linearly interpolations a value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="x">The value along the first dimension to compute the linear interpolation for.</param>
		/// <param name="x0">A known starting point along the first dimension.</param>
		/// <param name="x1">A known ending point along the first dimension.</param>
		/// <param name="y0">A known starting point along the second dimension.</param>
		/// <param name="y1">A known ending point along the second dimension.</param>
		/// <returns>The linearly interpolated value.</returns>
		public static T LinearInterpolation<T>(T x, T x0, T x1, T y0, T y1)
		{
			if (GreaterThan(x0, x1) ||
				GreaterThan(x, x1) ||
				LessThan(x, x0))
			{
				throw new MathematicsException($"!({nameof(x0)}[{x0}] <= {nameof(x)}[{x}] <= {nameof(x1)}[{x1}])");
			}
			if (Equate(x0, x1))
			{
				if (Inequate(y0, y1))
				{
					throw new MathematicsException($"{nameof(x0)}[{x0}] == {nameof(x1)}[{x1}] && {nameof(y0)}[{y0}] != {nameof(y1)}[{y1}]");
				}
				else
				{
					return y0;
				}
			}
			return Addition(y0, Division(Multiplication(Subtraction(x, x0), Subtraction(y1, y0)), Subtraction(x1, x0)));
		}

		#endregion

		#region Factorial

		/// <summary>Computes the factorial of a numeric value [<paramref name="a"/>!] == [<paramref name="a"/> * (<paramref name="a"/> - 1) * (<paramref name="a"/> - 2) * ... * 1].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The integer value to compute the factorial of.</param>
		/// <returns>The computed factorial value.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is not an integer value.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the parameter is less than zero.</exception>
		public static T Factorial<T>(T a) =>
			FactorialImplementation<T>.Function(a);

		internal static class FactorialImplementation<T>
		{
			internal static Func<T, T> Function = a =>
			{
				MethodInfo? methodInfo = Meta.GetFactorialMethod<T>();
				if (methodInfo is not null)
				{
					Function = methodInfo.CreateDelegate<Func<T, T>>();
					return Function(a);
				}

				Function = A =>
				{
					if (!IsInteger(A))
					{
						throw new ArgumentOutOfRangeException(nameof(A), A, $"!{nameof(A)}[{A}].{nameof(IsInteger)}()");
					}
					if (LessThan(A, Constant<T>.Zero))
					{
						throw new ArgumentOutOfRangeException(nameof(A), A, $"!({nameof(A)}[{A}] >= 0)");
					}
					T result = Constant<T>.One;
					for (; GreaterThan(A, Constant<T>.One); A = Subtraction(A, Constant<T>.One))
						result = Multiplication(A, result);
					return result;
				};
				return Function(a);
			};
		}

		#endregion

		#region Combinations

		/// <summary>Computes the combinations of <paramref name="N"/> values using the <paramref name="n"/> grouping definitions.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="N">The number of values to compute the combinations of.</param>
		/// <param name="n">The groups and how many values fall into each group.</param>
		/// <returns>The computed number of combinations.</returns>
		public static T Combinations<T>(T N, T[] n)
		{
			if (!IsInteger(N))
			{
				throw new ArgumentOutOfRangeException(nameof(N), N, $"!({nameof(N)}.{nameof(IsInteger)})");
			}
			T result = Factorial(N);
			T sum = Constant<T>.Zero;
			for (int i = 0; i < n.Length; i++)
			{
				if (!IsInteger(n[i]))
				{
					throw new ArgumentOutOfRangeException(nameof(n) + "[" + i + "]", n[i], "!(" + nameof(n) + "[" + i + "]." + nameof(IsInteger) + ")");
				}
				result = Division(result, Factorial(n[i]));
				sum = Addition(sum, n[i]);
			}
			if (GreaterThan(sum, N))
			{
				throw new MathematicsException("Aurguments out of range !(" + nameof(N) + " < Add(" + nameof(n) + ") [" + N + " < " + sum + "].");
			}
			return result;
		}

		#endregion

		#region BinomialCoefficient

		/// <summary>Computes the Binomial coefficient (N choose n).</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="N">The size of the entire set (N choose n).</param>
		/// <param name="n">The size of the subset (N choose n).</param>
		/// <returns>The computed binomial coefficient (N choose n).</returns>
		public static T BinomialCoefficient<T>(T N, T n)
		{
			if (LessThan(N, Constant<T>.Zero))
			{
				throw new ArgumentOutOfRangeException(nameof(N), N, "!(" + nameof(N) + " >= 0)");
			}
			if (!IsInteger(N))
			{
				throw new ArgumentOutOfRangeException(nameof(N), N, "!(" + nameof(N) + "." + nameof(IsInteger) + ")");
			}
			if (!IsInteger(n))
			{
				throw new ArgumentOutOfRangeException(nameof(n), n, "!(" + nameof(n) + "." + nameof(IsInteger) + ")");
			}
			if (LessThan(N, n))
			{
				throw new MathematicsException("Arguments out of range !(" + nameof(N) + " <= " + nameof(n) + ") [" + N + " <= " + n + "].");
			}
			return Division(Factorial(N), Multiplication(Factorial(n), Factorial(Subtraction(N, n))));
		}

		#endregion

		#region Occurences

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); });

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, T, bool> equate, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, null);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, int> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, null, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <param name="a">The first value in the data.</param>
		/// <param name="b">The rest of the data.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Func<T, T, bool> equate, Func<T, int> hash, T a, params T[] b) =>
			Occurences<T>(step => { step(a); b.ToStepper()(step); }, equate, hash);

		/// <summary>Counts the number of occurences of each item.</summary>
		/// <typeparam name="T">The generic type to count the occerences of.</typeparam>
		/// <param name="stepper">The data to count the occurences of.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate.</param>
		/// <returns>The occurence map of the data.</returns>
		public static IMap<int, T> Occurences<T>(Action<Action<T>> stepper, Func<T, T, bool>? equate = null, Func<T, int>? hash = null)
		{
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					map[a]++;
				}
				else
				{
					map[a] = 1;
				}
			});
			return map;
		}

		#endregion

		#region Mode

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, T, bool> equate, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, null);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, int> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, null, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <param name="a">The first value of the data set.</param>
		/// <param name="b">The rest of the data set.</param>
		public static void Mode<T>(Action<T> step, Func<T, T, bool> equate, Func<T, int> hash, T a, params T[] b) =>
			Mode<T>(x => { x(a); b.ToStepper()(x); }, step, equate, hash);

		/// <summary>Gets the mode(s) of a data set.</summary>
		/// <typeparam name="T">The generic type of the data.</typeparam>
		/// <param name="stepper">The data set.</param>
		/// <param name="step">The action to perform on every mode value found.</param>
		/// <param name="equate">The equality delegate.</param>
		/// <param name="hash">The hash code delegate</param>
		/// <returns>The modes of the data set.</returns>
		public static void Mode<T>(Action<Action<T>> stepper, Action<T> step, Func<T, T, bool>? equate = null, Func<T, int>? hash = null)
		{
			int maxOccurences = -1;
			IMap<int, T> map = new MapHashLinked<int, T>(equate, hash);
			stepper(a =>
			{
				if (map.Contains(a))
				{
					int occurences = ++map[a];
					maxOccurences = Math.Max(occurences, maxOccurences);
				}
				else
				{
					map[a] = 1;
					maxOccurences = Math.Max(1, maxOccurences);
				}
			});
			map.Stepper((value, key) =>
			{
				if (value == maxOccurences)
				{
					step(key);
				}
			});
		}

		#endregion

		#region Mean

		/// <summary>Computes the mean of a set of numerical values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The first value of the set of data to compute the mean of.</param>
		/// <param name="b">The remaining values in the data set to compute the mean of.</param>
		/// <returns>The computed mean of the set of data.</returns>
		public static T Mean<T>(T a, params T[] b) =>
			Mean<T>(step => { step(a); b.ToStepper()(step); });

		/// <summary>Computes the mean of a set of numerical values.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the mean of.</param>
		/// <returns>The computed mean of the set of data.</returns>
		public static T Mean<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			T i = Constant<T>.Zero;
			T sum = Constant<T>.Zero;
			stepper(step =>
			{
				i = Addition(i, Constant<T>.One);
				sum = Addition(sum, step);
			});
			if (Equate(i, Constant<T>.Zero))
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			return Division(sum, i);
		}

		#endregion

		#region Median

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="compare">The comparison algorithm to sort the data by.</param>
		/// <param name="values">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Func<T, T, CompareResult> compare, params T[] values)
		{
			_ = compare ?? throw new ArgumentNullException(nameof(compare));
			_ = values ?? throw new ArgumentNullException(nameof(values));
			// standard algorithm (sort and grab middle value)
			SortMerge(values, compare);
			if (values.Length % 2 == 1) // odd... just grab middle value
			{
				return values[values.Length / 2];
			}
			else // even... must perform a mean of the middle two values
			{
				T leftMiddle = values[(values.Length / 2) - 1];
				T rightMiddle = values[values.Length / 2];
				return Division(Addition(leftMiddle, rightMiddle), Constant<T>.Two);
			}
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="compare">The comparison algorithm to sort the data by.</param>
		/// <param name="stepper">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Func<T, T, CompareResult> compare, Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			return Median<T>(compare, stepper.ToArray());
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="values">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(params T[] values)
		{
			return Median(Compare, values);
		}

		/// <summary>Computes the median of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="stepper">The set of data to compute the median of.</param>
		/// <returns>The computed median value of the set of data.</returns>
		public static T Median<T>(Action<Action<T>> stepper)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			return Median(Compare, stepper.ToArray());
		}

		#region Possible Optimization (Still in Development)

		//public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, params T[] values)
		//{
		//    // this is an optimized median algorithm, but it only works on odd sets without duplicates
		//    if (hash is not null && equate is not null && values.Length % 2 == 1 && !values.ToStepper().ContainsDuplicates(equate, hash))
		//    {
		//        int medianIndex = 0;
		//        OddNoDupesMedianImplementation(values, values.Length, ref medianIndex, compare);
		//        return values[medianIndex];
		//    }
		//    else
		//    {
		//        return Median(compare, values);
		//    }
		//}

		//public static T Median<T>(Func<T, T, CompareResult> compare, Hash<T> hash, Func<T, T, bool> equate, Stepper<T> stepper)
		//{
		//    return Median(compare, hash, equate, stepper.ToArray());
		//}

		///// <summary>Fast algorithm for median computation, but only works on data with an odd number of values without duplicates.</summary>
		//internal static void OddNoDupesMedianImplementation<T>(T[] a, int n, ref int k, Func<T, T, CompareResult> compare)
		//{
		//    int L = 0;
		//    int R = n - 1;
		//    k = n / 2;
		//    int i; int j;
		//    while (L < R)
		//    {
		//        T x = a[k];
		//        i = L; j = R;
		//        OddNoDupesMedianImplementation_Split(a, n, x, ref i, ref j, compare);
		//        if (j <= k) L = i;
		//        if (i >= k) R = j;
		//    }
		//}

		//internal static void OddNoDupesMedianImplementation_Split<T>(T[] a, int n, T x, ref int i, ref int j, Func<T, T, CompareResult> compare)
		//{
		//    do
		//    {
		//        while (compare(a[i], x) == Comparison.Less) i++;
		//        while (compare(a[j], x) == Comparison.Greater) j--;
		//        T t = a[i];
		//        a[i] = a[j];
		//        a[j] = t;
		//    } while (i < j);
		//}

		#endregion

		#endregion

		#region GeometricMean

		/// <summary>Computes the geometric mean of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbres to compute the geometric mean of.</param>
		/// <returns>The computed geometric mean of the set of numbers.</returns>
		public static T GeometricMean<T>(Action<Action<T>> stepper)
		{
			T multiple = Constant<T>.One;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				count = Addition(count, Constant<T>.One);
				multiple = Multiplication(multiple, i);
			});
			return Root(multiple, count);
		}

		#endregion

		#region Variance

		/// <summary>Computes the variance of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the variance of.</param>
		/// <returns>The computed variance of the set of numbers.</returns>
		public static T Variance<T>(Action<Action<T>> stepper)
		{
			T mean = Mean(stepper);
			T variance = Constant<T>.Zero;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				T i_minus_mean = Subtraction(i, mean);
				variance = Addition(variance, Multiplication(i_minus_mean, i_minus_mean));
				count = Addition(count, Constant<T>.One);
			});
			return Division(variance, count);
		}

		#endregion

		#region StandardDeviation

		/// <summary>Computes the standard deviation of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the standard deviation of.</param>
		/// <returns>The computed standard deviation of the set of numbers.</returns>
		public static T StandardDeviation<T>(Action<Action<T>> stepper) =>
			SquareRoot(Variance(stepper));

		#endregion

		#region MeanDeviation

		/// <summary>The mean deviation of a set of numbers.</summary>
		/// <typeparam name="T">The numeric type of the computation.</typeparam>
		/// <param name="stepper">The set of numbers to compute the mean deviation of.</param>
		/// <returns>The computed mean deviation of the set of numbers.</returns>
		public static T MeanDeviation<T>(Action<Action<T>> stepper)
		{
			T mean = Mean(stepper);
			T temp = Constant<T>.Zero;
			T count = Constant<T>.Zero;
			stepper(i =>
			{
				temp = Addition(temp, AbsoluteValue(Subtraction(i, mean)));
				count = Addition(count, Constant<T>.One);
			});
			return Division(temp, count);
		}

		#endregion

		#region Range

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// /// <param name="stepper">The set of data to get the range of.</param>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(out T minimum, out T maximum, Action<Action<T>> stepper) =>
			Range(stepper, out minimum, out maximum);

		/// <summary>Gets the range (minimum and maximum) of a set of data.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// /// <param name="stepper">The set of data to get the range of.</param>
		/// <param name="minimum">The minimum of the set of data.</param>
		/// <param name="maximum">The maximum of the set of data.</param>
		/// <exception cref="ArgumentNullException">Throws when stepper is null.</exception>
		/// <exception cref="ArgumentException">Throws when stepper is empty.</exception>
		public static void Range<T>(Action<Action<T>> stepper, out T minimum, out T maximum)
		{
			_ = stepper ?? throw new ArgumentNullException(nameof(stepper));
			// Note: can't use out parameters as capture variables
			T min = default;
			T max = default;
			bool assigned = false;
			stepper(a =>
			{
				if (assigned)
				{
					min = LessThan(a, min) ? a : min;
					max = LessThan(max, a) ? a : max;
				}
				else
				{
					min = a;
					max = a;
					assigned = true;
				}
			});
			if (!assigned)
			{
				throw new ArgumentException("The argument is empty.", nameof(stepper));
			}
			minimum = min!;
			maximum = max!;
		}

		#endregion

		#region Quantiles

		/// <summary>Computes the quantiles of a set of data.</summary>
		/// <typeparam name="T">The generic data type.</typeparam>
		/// <param name="quantiles">The number of quantiles to compute.</param>
		/// <param name="stepper">The data stepper.</param>
		/// <returns>The computed quantiles of the data set.</returns>
		public static T[] Quantiles<T>(int quantiles, Action<Action<T>> stepper)
		{
			if (quantiles < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(quantiles), quantiles, "!(" + nameof(quantiles) + " >= 1)");
			}
			int count = stepper.Count();
			T[] ordered = new T[count];
			int a = 0;
			stepper(i => { ordered[a++] = i; });
			SortQuick<T>(ordered, Compare);
			T[] resultingQuantiles = new T[quantiles + 1];
			resultingQuantiles[0] = ordered[0];
			resultingQuantiles[^1] = ordered[^1];
			T QUANTILES_PLUS_1 = Convert<int, T>(quantiles + 1);
			T ORDERED_LENGTH = Convert<int, T>(ordered.Length);
			for (int i = 1; i < quantiles; i++)
			{
				T I = Convert<int, T>(i);
				T temp = Division(ORDERED_LENGTH, Multiplication<T>(QUANTILES_PLUS_1, I));
				if (IsInteger(temp))
				{
					resultingQuantiles[i] = ordered[Convert<T, int>(temp)];
				}
				else
				{
					resultingQuantiles[i] = Division(Addition(ordered[Convert<T, int>(temp)], ordered[Convert<T, int>(temp) + 1]), Constant<T>.Two);
				}
			}
			return resultingQuantiles;
		}

		#endregion

		#region Correlation

		//        /// <summary>Computes the median of a set of values.</summary>
		//        internal static Compute.Delegates.Correlation Correlation_internal = (Stepper<T> a, Stepper<T> b) =>
		//        {
		//            throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon");

		//            Compute.Correlation_internal =
		//        Meta.Compile<Compute.Delegates.Correlation>(
		//        string.Concat(
		//        @"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> _a, Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _b) =>
		//{
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), " a_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_a);
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), " b_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_b);
		//	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> a_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
		//	_a((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { a_temp.Add(i - b_mean); });
		//	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> b_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
		//	_b((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { b_temp.Add(i - a_mean); });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] a_cross_b = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[a_temp.Count * b_temp.Count];
		//	int count = 0;
		//	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_a) =>
		//	{
		//		b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_b) =>
		//		{
		//			a_cross_b[count++] = i_a * i_b;
		//		});
		//	});
		//	a_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
		//	b_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_cross_b = 0;
		//	foreach (", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i in a_cross_b)
		//		sum_a_cross_b += i;
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_temp = 0;
		//	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_a_temp += i; });
		//	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_b_temp = 0;
		//	b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_b_temp += i; });
		//	return sum_a_cross_b / Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.sqrt(sum_a_temp * sum_b_temp);
		//}"));

		//            return Compute.Correlation_internal(a, b);
		//        };

		//        public static T Correlation(Stepper<T> a, Stepper<T> b)
		//        {
		//            return Correlation_internal(a, b);
		//        }
		#endregion

		#region Exponential

		/// <summary>Computes the exponentional of a value [e ^ <paramref name="a"/>].</summary>
		/// <typeparam name="T">The generic type of the operation.</typeparam>
		/// <param name="a">The value to compute the exponentional of.</param>
		/// <returns>The exponential of the value [e ^ <paramref name="a"/>].</returns>
		public static T Exponential<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region NaturalLogarithm

		/// <summary>Computes the natural logarithm of a value [ln(<paramref name="a"/>)].</summary>
		/// <typeparam name="T">The generic type of the operation.</typeparam>
		/// <param name="a">The value to compute the natural log of.</param>
		/// <returns>The natural log of the provided value [ln(<paramref name="a"/>)].</returns>
		public static T NaturalLogarithm<T>(T a) =>
			NaturalLogarithmImplementation<T>.Function(a);

		internal static class NaturalLogarithmImplementation<T>
		{
			internal static Func<T, T> Function = a =>
			{
				// optimization for specific known types
				if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
				{
					ParameterExpression A = Expression.Parameter(typeof(T));
					MethodInfo? Math_Log = typeof(Math).GetMethod("Log");
					if (Math_Log is not null)
					{
						Expression BODY = Expression.Call(Math_Log, A);
						Function = Expression.Lambda<Func<T, T>>(BODY, A).Compile();
						return Function(a);
					}
				}
				throw new NotImplementedException();
			};
		}

		#endregion

		#region Trigonometry Functions

		#region Sine

		/// <summary>Computes the sine ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed sine ratio of the provided angle.</returns>
		public static T SineTaylorSeries<T>(Angle<T> a, Predicate<T>? predicate = null)
		{
			// Series: sine(x) = x - (x^3 / 3!) + (x^5 / 5!) - (x^7 / 7!) + (x^9 / 9!) + ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T sine = x;
			T previous;
			bool isAddTerm = false;
			T i = Constant<T>.Three;
			T xSquared = Multiplication(x, x);
			T xRunningPower = x;
			T xRunningFactorial = Constant<T>.One;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = sine;
				if (isAddTerm)
				{
					sine = Addition(sine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					sine = Subtraction(sine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequate(sine, previous) && (predicate is null || !predicate(sine)));
			return sine;
		}

		/// <summary>Computes the sine ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <returns>The sine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T SineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Sin(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the sine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated sine ratio of.</param>
		/// <returns>The quadratic estimation of the sine ratio of the provided angle.</returns>
		public static T SineQuadratic<T>(Angle<T> a)
		{
			// Piecewise Functions:
			// y = (-4/π^2)(x - (π/2))^2 + 1
			// y = (4/π^2)(x - (3π/2))^2 - 1

			T adjusted = Remainder(a[Angle.Units.Radians], Constant<T>.Pi2);
			if (IsNegative(adjusted))
			{
				adjusted = Addition(adjusted, Constant<T>.Pi2);
			}
			if (LessThan(adjusted, Constant<T>.Pi))
			{
				T xMinusPiOver2 = Subtraction(adjusted, Constant<T>.PiOver2);
				T xMinusPiOver2Squared = Multiplication(xMinusPiOver2, xMinusPiOver2);
				return Addition(Multiplication(Constant<T>.Negative4OverPiSquared, xMinusPiOver2Squared), Constant<T>.One);
			}
			else
			{
				T xMinus3PiOver2 = Subtraction(adjusted, Constant<T>.Pi3Over2);
				T xMinus3PiOver2Squared = Multiplication(xMinus3PiOver2, xMinus3PiOver2);
				return Subtraction(Multiplication(Constant<T>.FourOverPiSquared, xMinus3PiOver2Squared), Constant<T>.One);
			}
		}

		#endregion

		#region Cosine

		/// <summary>Computes the cosine ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed cosine ratio of the provided angle.</returns>
		public static T CosineTaylorSeries<T>(Angle<T> a, Predicate<T>? predicate = null)
		{
			// Series: cosine(x) = 1 - (x^2 / 2!) + (x^4 / 4!) - (x^6 / 6!) + (x^8 / 8!) - ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T cosine = Constant<T>.One;
			T previous;
			T xSquared = Multiplication(x, x);
			T xRunningPower = Constant<T>.One;
			T xRunningFactorial = Constant<T>.One;
			bool isAddTerm = false;
			T i = Constant<T>.Two;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = cosine;
				if (isAddTerm)
				{
					cosine = Addition(cosine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					cosine = Subtraction(cosine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequate(cosine, previous) && (predicate is null || !predicate(cosine)));
			return cosine;
		}

		/// <summary>Computes the cosine ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <returns>The cosine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CosineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Cos(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the cosine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cosine ratio of.</param>
		/// <returns>The quadratic estimation of the cosine ratio of the provided angle.</returns>
		public static T CosineQuadratic<T>(Angle<T> a)
		{
			Angle<T> piOver2Radians = new Angle<T>(Constant<T>.PiOver2, Angle.Units.Radians);
			return SineQuadratic(a - piOver2Radians);
		}

		#endregion

		#region Tangent

		/// <summary>Computes the tangent ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The taylor series computed tangent ratio of the provided angle.</returns>
		public static T TangentTaylorSeries<T>(Angle<T> a)
		{
			return Division(SineTaylorSeries(a), CosineTaylorSeries(a));
		}

		/// <summary>Computes the tangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The tangent ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T TangentSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Tan(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the tangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated tangent ratio of.</param>
		/// <returns>The quadratic estimation of the tangent ratio of the provided angle.</returns>
		public static T TangentQuadratic<T>(Angle<T> a)
		{
			return Division(SineQuadratic(a), CosineQuadratic(a));
		}

		#endregion

		#region Cosecant

		/// <summary>Computes the cosecant ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosecant ratio of.</param>
		/// <returns>The cosecant ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CosecantSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, SineSystem(a));
		}

		/// <summary>Estimates the cosecant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cosecant ratio of.</param>
		/// <returns>The quadratic estimation of the cosecant ratio of the provided angle.</returns>
		public static T CosecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, SineQuadratic(a));
		}

		#endregion

		#region Secant

		/// <summary>Computes the secant ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the secant ratio of.</param>
		/// <returns>The secant ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T SecantSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, CosineSystem(a));
		}

		/// <summary>Estimates the secant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated secant ratio of.</param>
		/// <returns>The quadratic estimation of the secant ratio of the provided angle.</returns>
		public static T SecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, CosineQuadratic(a));
		}

		#endregion

		#region Cotangent

		/// <summary>Computes the cotangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cotangent ratio of.</param>
		/// <returns>The cotangent ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CotangentSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, TangentSystem(a));
		}

		/// <summary>Estimates the cotangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cotangent ratio of.</param>
		/// <returns>The quadratic estimation of the cotangent ratio of the provided angle.</returns>
		public static T CotangentQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, TangentQuadratic(a));
		}

		#endregion

		#region InverseSine

		//public static Angle<T> InverseSine<T>(T a)
		//{
		//    return InverseSineImplementation<T>.Function(a);
		//}

		//internal static class InverseSineImplementation<T>
		//{
		//    internal static Func<T, Angle<T>> Function = a =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Asin)), A));
		//            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region InverseCosine

		//public static Angle<T> InverseCosine<T>(T a)
		//{
		//    return InverseCosineImplementation<T>.Function(a);
		//}

		//internal static class InverseCosineImplementation<T>
		//{
		//    internal static Func<T, Angle<T>> Function = a =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Acos)), A));
		//            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region InverseTangent

		//public static Angle<T> InverseTangent<T>(T a)
		//{
		//    return InverseTangentImplementation<T>.Function(a);
		//}

		//internal static class InverseTangentImplementation<T>
		//{
		//    internal static Func<T, Angle<T>> Function = a =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Atan)), A));
		//            Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region InverseCosecant

		//public static Angle<T> InverseCosecant<T>(T a)
		//{
		//    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseSine(a).Radians));
		//}

		#endregion

		#region InverseSecant

		//public static Angle<T> InverseSecant<T>(T a)
		//{
		//    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseCosine(a).Radians));
		//}

		#endregion

		#region InverseCotangent

		//public static Angle<T> InverseCotangent<T>(T a)
		//{
		//    return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseTangent(a).Radians));
		//}

		#endregion

		#region HyperbolicSine

		//public static T HyperbolicSine<T>(Angle<T> a)
		//{
		//    return HyperbolicSineImplementation<T>.Function(a);
		//}

		//internal static class HyperbolicSineImplementation<T>
		//{
		//    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sinh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region HyperbolicCosine

		//public static T HyperbolicCosine<T>(Angle<T> a)
		//{
		//    return HyperbolicCosineImplementation<T>.Function(a);
		//}

		//internal static class HyperbolicCosineImplementation<T>
		//{
		//    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Cosh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region HyperbolicTangent

		//public static T HyperbolicTangent<T>(Angle<T> a)
		//{
		//    return HyperbolicTangentImplementation<T>.Function(a);
		//}

		//internal static class HyperbolicTangentImplementation<T>
		//{
		//    internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//    {
		//        // optimization for specific known types
		//        if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//        {
		//            ParameterExpression A = Expression.Parameter(typeof(T));
		//            Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Tanh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//            Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//            return Function(a);
		//        }
		//        throw new NotImplementedException();
		//    };
		//}

		#endregion

		#region HyperbolicCosecant

		//public static T HyperbolicCosecant<T>(Angle<T> a)
		//{
		//    return Divide(Constant<T>.One, HyperbolicSine(a));
		//}

		#endregion

		#region HyperbolicSecant

		//public static T HyperbolicSecant<T>(Angle<T> a)
		//{
		//    return Divide(Constant<T>.One, HyperbolicCosine(a));
		//}

		#endregion

		#region HyperbolicCotangent

		//public static T HyperbolicCotangent<T>(Angle<T> a)
		//{
		//    return Divide(Constant<T>.One, HyperbolicTangent(a));
		//}

		#endregion

		#region InverseHyperbolicSine

		public static Angle<T> InverseHyperbolicSine<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region InverseHyperbolicCosine

		public static Angle<T> InverseHyperbolicCosine<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region InverseHyperbolicTangent

		public static Angle<T> InverseHyperbolicTangent<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region InverseHyperbolicCosecant

		public static Angle<T> InverseHyperbolicCosecant<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region InverseHyperbolicSecant

		public static Angle<T> InverseHyperbolicSecant<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region InverseHyperbolicCotangent

		public static Angle<T> InverseHyperbolicCotangent<T>(T a)
		{
			throw new NotImplementedException();
		}

		#endregion

		#endregion

		#region LinearRegression2D

		/// <summary>Computes the best fit line from a set of points in 2D space [y = slope * x + y_intercept].</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="points">The points to compute the best fit line of.</param>
		/// <param name="slope">The slope of the computed best fit line [y = slope * x + y_intercept].</param>
		/// <param name="y_intercept">The y intercept of the computed best fit line [y = slope * x + y_intercept].</param>
		public static void LinearRegression2D<T>(Action<Action<T, T>> points, out T slope, out T y_intercept)
		{
			_ = points ?? throw new ArgumentNullException(nameof(points));
			int count = 0;
			T sumx = Constant<T>.Zero;
			T sumy = Constant<T>.Zero;
			points((T x, T y) =>
			{
				sumx = Addition(sumx, x);
				sumy = Addition(sumy, y);
				count++;
			});
			if (count < 2)
			{
				throw new MathematicsException("At least 3 points must be provided for linear regressions");
			}
			T tcount = Convert<int, T>(count);
			T meanx = Division(sumx, tcount);
			T meany = Division(sumy, tcount);
			T variancex = Constant<T>.Zero;
			T variancey = Constant<T>.Zero;
			points((T x, T y) =>
			{
				T offset = Subtraction(x, meanx);
				variancey = Addition(variancey, Multiplication(offset, Subtraction(y, meany)));
				variancex = Addition(variancex, Multiplication(offset, offset));
			});
			slope = Division(variancey, variancex);
			y_intercept = Subtraction(meany, Multiplication(slope, meanx));
		}

		#endregion

		#region FactorPrimes

		/// <summary>Factors the primes numbers of a numeric integer value.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The value to factor the prime numbers of.</param>
		/// <param name="step">The action to perform on all found prime factors.</param>
		public static void FactorPrimes<T>(T a, Action<T> step) =>
			FactorPrimesImplementation<T>.Function(a, step);

		internal static class FactorPrimesImplementation<T>
		{
			internal static Action<T, Action<T>> Function = (a, x) =>
			{
				Function = (A, step) =>
				{
					if (!IsInteger(A))
					{
						throw new ArgumentOutOfRangeException(nameof(A), A, "!(" + nameof(A) + "." + nameof(IsInteger) + ")");
					}
					if (IsNegative(A))
					{
						A = AbsoluteValue(A);
						step(Convert<int, T>(-1));
					}
					while (IsEven(A))
					{
						step(Constant<T>.Two);
						A = Division(A, Constant<T>.Two);
					}
					for (T i = Constant<T>.Three; LessThanOrEqual(i, SquareRoot(A)); i = Addition(i, Constant<T>.Two))
					{
						while (Equate(Remainder(A, i), Constant<T>.Zero))
						{
							step(i);
							A = Division(A, i);
						}
					}
					if (GreaterThan(A, Constant<T>.Two))
					{
						step(A);
					}
				};
				Function(a, x);
			};
		}

		#endregion

		#region Hamming Distance

		/// <summary>Computes the Hamming distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="length">The length of the sequences.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, GetA, GetB, Equals>(
			int length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, $@"{nameof(length)} < 0");
			}
			int distance = 0;
			for (int i = 0; i < length; i++)
			{
				if (!equals.Do(a.Do(i), b.Do(i)))
				{
					distance++;
				}
			}
			return distance;
		}

		/// <summary>Computes the Hamming distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			HammingDistance<char, CharEquate>(a, b);

		/// <summary>Computes the Hamming distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Hamming distance of the two sequences.</returns>
		public static int HammingDistance<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
		{
			if (a.Length != b.Length)
			{
				throw new ArgumentException($@"{nameof(a)}.{nameof(a.Length)} ({a.Length}) != {nameof(b)}.{nameof(b.Length)} ({b.Length})");
			}
			int distance = 0;
			for (int i = 0; i < a.Length; i++)
			{
				if (!equals.Do(a[i], b[i]))
				{
					distance++;
				}
			}
			return distance;
		}

		#endregion

		#region Levenshtein distance

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
		{
			if (a_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(a_length), a_length, $@"{nameof(a_length)} < 0");
			}
			if (b_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(b_length), b_length, $@"{nameof(b_length)} < 0");
			}
			int LDR(int ai, int bi)
			{
				int _ai = ai + 1;
				int _bi = bi + 1;
				return
					ai >= a_length ?
					bi >= b_length ? 0 :
					1 + LDR(ai, _bi) :
				bi >= b_length ? 1 + LDR(_ai, bi) :
				equals.Do(a.Do(ai), b.Do(bi)) ? LDR(_ai, _bi) :
				1 + Minimum(LDR(ai, _bi), LDR(_ai, bi), LDR(_ai, _bi));
			}
			return LDR(0, 0);
		}

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			LevenshteinDistanceRecursive<char, CharEquate>(a, b);

		/// <summary>Computes the Levenshtein distance (using an recursive algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceRecursive<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
		{
			int LDR(
				ReadOnlySpan<T> a,
				ReadOnlySpan<T> b,
				int ai, int bi)
			{
				int _ai = ai + 1;
				int _bi = bi + 1;
				return
					ai >= a.Length ?
					bi >= b.Length ? 0 :
					1 + LDR(a, b, ai, _bi) :
				bi >= b.Length ? 1 + LDR(a, b, _ai, bi) :
				equals.Do(a[ai], b[bi]) ? LDR(a, b, _ai, _bi) :
				1 + Minimum(LDR(a, b, ai, _bi), LDR(a, b, _ai, bi), LDR(a, b, _ai, _bi));
			}
			return LDR(a, b, 0, 0);
		}

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="GetA">The get index function for the first sequence.</typeparam>
		/// <typeparam name="GetB">The get index function for the second sequence.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a_length">The length of the first sequence.</param>
		/// <param name="b_length">The length of the second sequence.</param>
		/// <param name="a">The get index function for the first sequence.</param>
		/// <param name="b">The get index function for the second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, GetA, GetB, Equals>(
			int a_length,
			int b_length,
			GetA a = default,
			GetB b = default,
			Equals equals = default)
			where GetA : struct, IFunc<int, T>
			where GetB : struct, IFunc<int, T>
			where Equals : struct, IFunc<T, T, bool>
		{
			if (a_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(a_length), a_length, $@"{nameof(a_length)} < 0");
			}
			if (b_length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(b_length), b_length, $@"{nameof(b_length)} < 0");
			}
			a_length++;
			b_length++;
			int[,] matrix = new int[a_length, b_length];
			for (int i = 1; i < a_length; i++)
			{
				matrix[i, 0] = i;
			}
			for (int i = 1; i < b_length; i++)
			{
				matrix[0, i] = i;
			}
			for (int bi = 1; bi < b_length; bi++)
			{
				for (int ai = 1; ai < a_length; ai++)
				{
					int _ai = ai - 1;
					int _bi = bi - 1;
					matrix[ai, bi] = Minimum(
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equals.Do(a.Do(_ai), b.Do(_bi)) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
				}
			}
			return matrix[a_length - 1, b_length - 1];
		}

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <param name="a">The first sequence of the operation.</param>
		/// <param name="b">The second sequence of the operation.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b) =>
			LevenshteinDistanceIterative<char, CharEquate>(a, b);

		/// <summary>Computes the Levenshtein distance (using an iterative algorithm).</summary>
		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <typeparam name="Equals">The equality check function.</typeparam>
		/// <param name="a">The first sequence.</param>
		/// <param name="b">The second sequence.</param>
		/// <param name="equals">The equality check function.</param>
		/// <returns>The computed Levenshtein distance of the two sequences.</returns>
		public static int LevenshteinDistanceIterative<T, Equals>(
			ReadOnlySpan<T> a,
			ReadOnlySpan<T> b,
			Equals equals = default)
			where Equals : struct, IFunc<T, T, bool>
		{
			int a_length = a.Length + 1;
			int b_length = b.Length + 1;
			int[,] matrix = new int[a_length, b_length];
			for (int i = 1; i < a_length; i++)
			{
				matrix[i, 0] = i;
			}
			for (int i = 1; i < b_length; i++)
			{
				matrix[0, i] = i;
			}
			for (int bi = 1; bi < b_length; bi++)
			{
				for (int ai = 1; ai < a_length; ai++)
				{
					int _ai = ai - 1;
					int _bi = bi - 1;
					matrix[ai, bi] = Minimum(
						matrix[_ai, bi] + 1,
						matrix[ai, _bi] + 1,
						!equals.Do(a[_ai], b[_bi]) ? matrix[_ai, _bi] + 1 : matrix[_ai, _bi]);
				}
			}
			return matrix[a_length - 1, b_length - 1];
		}

		#endregion

		#region Permute

		#region XML

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="T">The generic element type of the indexed collection.</typeparam>
		/// <typeparam name="Action">The action to perform on each permutation.</typeparam>
		/// <typeparam name="Status">The status checker for cancellation.</typeparam>
		/// <typeparam name="Get">The get index operation of the collection.</typeparam>
		/// <typeparam name="Set">The set index operation of the collection.</typeparam>
		/// <param name="start">The starting index of the values to permute.</param>
		/// <param name="end">The ending index of the values to permute.</param>
		/// <param name="action">The action to perform on each permutation.</param>
		/// <param name="status">The status checker for cancellation.</param>
		/// <param name="get">The get index operation of the collection.</param>
		/// <param name="set">The set index operation of the collection.</param>
		/// <param name="array">The array to iterate the permutations of.</param>
		/// <param name="list">The list to iterate the permutations of.</param>
		/// <param name="span">The span of the permutation.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void PermuteXML() => throw new DocumentationMethodException();
		/// <summary>Iterates through all the permutations of an indexed collection (using a recursive algorithm).</summary>
		/// <inheritdoc cref="PermuteXML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void PermuteRecursive_XML() => throw new DocumentationMethodException();
		/// <summary>Iterates through all the permutations of an indexed collection (using an iterative algorithm).</summary>
		/// <inheritdoc cref="PermuteXML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void PermuteIterative_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, ActionRuntime, StepStatusContinue, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteRecursive<T, ActionRuntime, FuncRuntime<StepStatus>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			PermuteRecursive<T, Action, StepStatusContinue, Get, Set>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			Permute(start, end);
			StepStatus Permute(int a, int b)
			{
				if (a == b)
				{
					action.Do();
					return status.Do();
				}
				for (int i = a; i <= b; i++)
				{
					Swap(a, i);
					if (Permute(a + 1, b) is Break)
					{
						return Break;
					}
					Swap(a, i);
				}
				return Continue;
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T>(Span<T> span, Action action) =>
			PermuteRecursive<T, ActionRuntime, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action>(Span<T> span, Action action = default)
			where Action : struct, IAction =>
			PermuteRecursive<T, Action, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Status>(Span<T> span, Action action, Status status = default)
			where Status : struct, IFunc<StepStatus> =>
			PermuteRecursive<T, ActionRuntime, Status>(span, action, status);

		/// <inheritdoc cref="PermuteRecursive_XML"/>
		public static void PermuteRecursive<T, Action, Status>(Span<T> span, Action action = default, Status status = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
		{
			Permute(span, 0, span.Length - 1);
			StepStatus Permute(Span<T> span, int a, int b)
			{
				if (a == b)
				{
					action.Do();
					return status.Do();
				}
				for (int i = a; i <= b; i++)
				{
					Swap(span, a, i);
					if (Permute(span, a + 1, b) is Break)
					{
						return Break;
					}
					Swap(span, a, i);
				}
				return Continue;
			}
			static void Swap(Span<T> span, int a, int b)
			{
				T temp = span[a];
				span[a] = span[b];
				span[b] = temp;
			}
		}

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, ActionRuntime, StepStatusContinue, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(int start, int end, Action action, Func<StepStatus> status, Func<int, T> get, Action<int, T> set) =>
			PermuteIterative<T, ActionRuntime, FuncRuntime<StepStatus>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, action, status, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Get, Set>(int start, int end, Action action = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			PermuteIterative<T, Action, StepStatusContinue, Get, Set>(start, end, action, default, get, set);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Status, Get, Set>(int start, int end, Action action = default, Status status = default, Get get = default, Set set = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			action.Do();
			int[] indeces = new int[end + 2 - start];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = start + 1; i < end + 1 && status.Do() is Continue; action.Do())
			{
				indeces[i]--;
				Swap(i, i % 2 == 1 ? indeces[i] : 0);
				for (i = 1; indeces[i] == 0; i++)
				{
					indeces[i] = i;
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T>(Span<T> span, Action action) =>
			PermuteIterative<T, ActionRuntime, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action>(Span<T> span, Action action = default)
			where Action : struct, IAction =>
			PermuteIterative<T, Action, StepStatusContinue>(span, action);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Status>(Span<T> span, Action action, Status status = default)
			where Status : struct, IFunc<StepStatus> =>
			PermuteIterative<T, ActionRuntime, Status>(span, action, status);

		/// <inheritdoc cref="PermuteIterative_XML"/>
		public static void PermuteIterative<T, Action, Status>(Span<T> span, Action action = default, Status status = default)
			where Action : struct, IAction
			where Status : struct, IFunc<StepStatus>
		{
			action.Do();
			int[] indeces = new int[span.Length - 1 + 2];
			for (int i = 0; i < indeces.Length; i++)
			{
				indeces[i] = i;
			}
			for (int i = 1; i < span.Length && status.Do() is Continue; action.Do())
			{
				indeces[i]--;
				Swap(span, i, i % 2 == 1 ? indeces[i] : 0);
				for (i = 1; indeces[i] == 0; i++)
				{
					indeces[i] = i;
				}
			}
			static void Swap(Span<T> span, int a, int b)
			{
				T temp = span[a];
				span[a] = span[b];
				span[b] = temp;
			}
		}

		#endregion

		#region SearchBinary

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
		/// <summary>Performs a binary search on sorted indexed data.</summary>
		/// <typeparam name="T">The type of elements to search through.</typeparam>
		/// <typeparam name="Get">The function for getting an element at an index.</typeparam>
		/// <typeparam name="Sift">The function for sifting through the elements.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <param name="index">The starting index of the binary search.</param>
		/// <param name="length">The number of elements to be searched after the starting <paramref name="index"/>.</param>
		/// <param name="get">The function for getting an element at an index.</param>
		/// <param name="sift">The function for comparing the the elements to th desired target.</param>
		/// <param name="array">The array search.</param>
		/// <param name="element">The element to search for.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="span">The span of the binary search.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="int"/> Index, <typeparamref name="T"/> Value)
		/// <para>- <see cref="bool"/> Found: True if a match was found; False if not.</para>
		/// <para>- <see cref="int"/> Index: The resulting index of the search that will always be &lt;= the desired match.</para>
		/// <para>- <typeparamref name="T"/> Value: The resulting value of the binary search if a match was found or default if not.</para>
		/// </returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SearchBinary_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#if false // keeping this for future reference; array types are supports by the ReadOnlySpan<T> overloads

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T Value) SearchBinary<T>(T[] array, T element, Func<T, T, CompareResult> compare = default)
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return SearchBinary<T, GetIndexArray<T>, SiftFromCompareAndValue<T, FuncRuntime<T, T, CompareResult>>>(0, array.Length, array, new SiftFromCompareAndValue<T, FuncRuntime<T, T, CompareResult>>(element, compare ?? Compare));
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T Value) SearchBinary<T, Compare>(T[] array, T element, Compare compare = default)
			where Compare : IFunc<T, T, CompareResult>
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return SearchBinary<T, GetIndexArray<T>, SiftFromCompareAndValue<T, Compare>>(0, array.Length, array, new SiftFromCompareAndValue<T, Compare>(element, compare));
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T Value) SearchBinary<T>(T[] array, Func<T, CompareResult> sift)
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return SearchBinary<T, GetIndexArray<T>, FuncRuntime<T, CompareResult>>(0, array.Length, array, sift);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T Value) SearchBinary<T, Sift>(T[] array, Sift sift = default)
			where Sift : IFunc<T, CompareResult>
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return SearchBinary<T, GetIndexArray<T>, Sift>(0, array.Length, array, sift);
		}

		#endif

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(int length, Func<int, T> get, Func<T, CompareResult> sift)
		{
			_ = get ?? throw new ArgumentNullException(nameof(get));
			_ = sift ?? throw new ArgumentNullException(nameof(sift));
			return SearchBinary<T, FuncRuntime<int, T>, FuncRuntime<T, CompareResult>>(0, length, get, sift);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, Get, Sift>(int index, int length, Get get = default, Sift sift = default)
			where Get : struct, IFunc<int, T>
			where Sift : struct, IFunc<T, CompareResult>
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, "!(" + nameof(length) + " > 0)");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index), index, "!(" + nameof(index) + " > 0)");
			}
			if (length > int.MaxValue / 2 && index > int.MaxValue / 2)
			{
				throw new ArgumentOutOfRangeException($"{nameof(length)} > {int.MaxValue / 2} && {nameof(index)} / {int.MaxValue / 2}", default(Exception));
			}
			int low = index;
			int hi = index + length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low) / 2;
				T value = get.Do(median);
				CompareResult compareResult = sift.Do(value);
				switch (compareResult)
				{
					case Less: low = median + 1; break;
					case Greater: hi = median - 1; break;
					case Equal: return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(Sift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, T element, Func<T, T, CompareResult>? compare = default) =>
			SearchBinary<T, SiftFromCompareAndValue<T, FuncRuntime<T, T, CompareResult>>>(span, new SiftFromCompareAndValue<T, FuncRuntime<T, T, CompareResult>>(element, compare ?? Compare));

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, Func<T, CompareResult> sift)
		{
			_ = sift ?? throw new ArgumentNullException(nameof(sift));
			return SearchBinary<T, FuncRuntime<T, CompareResult>>(span, sift);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, Compare>(ReadOnlySpan<T> span, T element, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			SearchBinary<T, SiftFromCompareAndValue<T, Compare>>(span, new SiftFromCompareAndValue<T, Compare>(element, compare));

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, Sift>(ReadOnlySpan<T> span, Sift sift = default)
			where Sift : struct, IFunc<T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($@"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int low = 0;
			int hi = span.Length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low) / 2;
				T value = span[median];
				CompareResult compareResult = sift.Do(value);
				switch (compareResult)
				{
					case Less: low = median + 1; break;
					case Greater: hi = median - 1; break;
					case Equal: return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(Sift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		#endregion

		#region Search Graph Algorithms

		#region Publics

		/// <summary>The status of a graph search algorithm.</summary>
		public enum GraphSearchStatus
		{
			/// <summary>Graph search was not broken.</summary>
			Continue = StepStatus.Continue,
			/// <summary>Graph search was broken.</summary>
			Break = StepStatus.Break,
			/// <summary>Graph search found the goal.</summary>
			Goal = 2,
		}

		/// <summary>Syntax sugar hacks.</summary>
		public static class GraphSyntax
		{
			/// <summary>This is a syntax sugar hack to allow implicit conversions from StepStatus to GraphSearchStatus.</summary>
			public struct GraphSearchStatusStruct
			{
				internal readonly GraphSearchStatus Value;
				/// <summary>Constructs a new graph search status.</summary>
				/// <param name="value">The status of the graph search.</param>
				public GraphSearchStatusStruct(GraphSearchStatus value) => Value = value;
				/// <summary>Converts a <see cref="GraphSearchStatus"/> into a <see cref="GraphSearchStatusStruct"/>.</summary>
				/// <param name="value">The <see cref="GraphSearchStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatusStruct(GraphSearchStatus value) => new GraphSearchStatusStruct(value);
				/// <summary>Converts a <see cref="StepStatus"/> into a <see cref="GraphSearchStatusStruct"/>.</summary>
				/// <param name="value">The <see cref="StepStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatusStruct(StepStatus value) => new GraphSearchStatusStruct((GraphSearchStatus)value);
				/// <summary>Converts a <see cref="GraphSearchStatusStruct"/> into a <see cref="GraphSearchStatus"/>.</summary>
				/// <param name="value">The <see cref="GraphSearchStatusStruct"/> to convert.</param>
				public static implicit operator GraphSearchStatus(GraphSearchStatusStruct value) => value.Value;
			}
		}

		/// <summary>Step function for all neigbors of a given node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The node to step through all the neighbors of.</param>
		/// <param name="neighbors">Step function to perform on all neighbors.</param>
		public delegate void SearchNeighbors<Node>(Node current, Action<Node> neighbors);
		/// <summary>Computes the heuristic value of a given node in a graph (smaller values mean closer to goal node).</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="node">The node to compute the heuristic value of.</param>
		/// <returns>The computed heuristic value for this node.</returns>
		public delegate Numeric SearchHeuristic<Node, Numeric>(Node node);
		/// <summary>Computes the cost of moving from the current node to a specific neighbor.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="current">The current (starting) node.</param>
		/// <param name="neighbor">The node to compute the cost of movign to.</param>
		/// <returns>The computed cost value of movign from current to neighbor.</returns>
		public delegate Numeric SearchCost<Node, Numeric>(Node current, Node neighbor);
		/// <summary>Predicate for determining if we have reached the goal node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The current node.</param>
		/// <returns>True if the current node is a/the goal node; False if not.</returns>
		public delegate bool SearchGoal<Node>(Node current);
		/// <summary>Checks the status of a graph search.</summary>
		/// <typeparam name="Node">The node type of the search.</typeparam>
		/// <param name="current">The current node of the search.</param>
		/// <returns>The status of the search.</returns>
		public delegate GraphSyntax.GraphSearchStatusStruct SearchCheck<Node>(Node current);

		#endregion

		#region Internals

		internal abstract class BaseAlgorithmNode<AlgorithmNode, Node>
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			internal AlgorithmNode? Previous;
			internal Node Value;

			internal BaseAlgorithmNode(Node value, AlgorithmNode? previous = null)
			{
				Value = value;
				Previous = previous;
			}
		}

		internal class BreadthFirstSearch<Node> : BaseAlgorithmNode<BreadthFirstSearch<Node>, Node>
		{
			internal BreadthFirstSearch(Node value, BreadthFirstSearch<Node>? previous = null)
				: base(value: value, previous: previous) { }
		}

		internal class DijkstraNode<Node, Numeric> : BaseAlgorithmNode<DijkstraNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;

			internal DijkstraNode(Node value, Numeric priority, DijkstraNode<Node, Numeric>? previous = null)
				: base(value: value, previous: previous)
			{
				Priority = priority;
			}
		}

		internal class AstarNode<Node, Numeric> : BaseAlgorithmNode<AstarNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;
			internal Numeric Cost;

			internal AstarNode(Node value, Numeric priority, Numeric cost, AstarNode<Node, Numeric>? previous = null)
				: base(value: value, previous: previous)
			{
				Priority = priority;
				Cost = cost;
			}
		}

		internal class PathNode<Node>
		{
			internal Node Value;
			internal PathNode<Node>? Next;

			internal PathNode(Node value, PathNode<Node>? next = null)
			{
				Value = value;
				Next = next;
			}
		}

		internal static Action<Action<Node>> BuildPath<AlgorithmNode, Node>(BaseAlgorithmNode<AlgorithmNode, Node> node)
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			PathNode<Node>? start = null;
			for (BaseAlgorithmNode<AlgorithmNode, Node>? current = node; current is not null; current = current.Previous)
			{
				PathNode<Node>? temp = start;
				start = new PathNode<Node>(
					value: current.Value,
					next: temp);
			}
			return step =>
			{
				PathNode<Node>? current = start;
				while (current is not null)
				{
					step(current.Value);
					current = current.Next;
				}
			};
		}

		internal struct AStarPriorityCompare<Node, Numeric> : IFunc<AstarNode<Node, Numeric>, AstarNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Do(AstarNode<Node, Numeric> a, AstarNode<Node, Numeric> b) =>
				Compare(b.Priority, a.Priority);
		}

		internal struct DijkstraPriorityCompare<Node, Numeric> : IFunc<DijkstraNode<Node, Numeric>, DijkstraNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Do(DijkstraNode<Node, Numeric> a, DijkstraNode<Node, Numeric> b) =>
				Compare(b.Priority, a.Priority);
		}

		#endregion

		#region Graph (Shared)

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="check">Checks the status of the search.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <param name="goal">The goal of the search.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SearchGraph_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		#region A* Algorithm

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Astar_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchGoal<Node> goal, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchGoal<Node> goal, out Numeric? totalCost) =>
			SearchGraph(start, graph.Neighbors, heuristic, cost, goal, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, goal, Equate, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, Func<Node, Node, bool> equate, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, out Numeric? totalCost) =>
			SearchGraph(start, graph, heuristic, cost, goal, Equate, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, Func<Node, Node, bool> equate, out Numeric? totalCost) =>
			SearchGraph(start, graph.Neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchCheck<Node> check, out Numeric? totalCost)
		{
			// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
			IHeap<AstarNode<Node, Numeric>> fringe = new HeapArray<AstarNode<Node, Numeric>, AStarPriorityCompare<Node, Numeric>>();

			// push starting node
			fringe.Enqueue(
				new AstarNode<Node, Numeric>(
					value: start,
					priority: Constant<Numeric>.Zero,
					cost: Constant<Numeric>.Zero,
					previous: null));

			// run the algorithm
			while (fringe.Count != 0)
			{
				AstarNode<Node, Numeric> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				else if (status is GraphSearchStatus.Goal)
				{
					totalCost = current.Cost;
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							Numeric costValue = Addition(current.Cost, cost(current.Value, neighbor));
							fringe.Enqueue(
								new AstarNode<Node, Numeric>(
									value: neighbor,
									priority: Addition(heuristic(neighbor), costValue),
									cost: costValue,
									previous: current));
						});
				}
			}
			totalCost = default;
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#region Dijkstra Algorithm

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Dijkstra_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchGoal<Node> goal) =>
			SearchGraph(start, neighbors, heuristic, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, Node goal) =>
			SearchGraph(start, neighbors, heuristic, goal, Equate);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, Node goal) =>
			SearchGraph(start, graph, heuristic, goal, Equate);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, graph.Neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchGoal<Node> goal) =>
			SearchGraph(start, graph.Neighbors, heuristic, goal);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCheck<Node> check)
		{
			// using a heap (aka priority queue) to store nodes based on their computed heuristic value
			IHeap<DijkstraNode<Node, Numeric>> fringe = new HeapArray<DijkstraNode<Node, Numeric>, DijkstraPriorityCompare<Node, Numeric>>();

			// push starting node
			fringe.Enqueue(
				new DijkstraNode<Node, Numeric>(
					value: start,
					priority: Constant<Numeric>.Zero,
					previous: null));

			// run the algorithm
			while (fringe.Count != 0)
			{
				DijkstraNode<Node, Numeric> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				if (status is GraphSearchStatus.Goal)
				{
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							fringe.Enqueue(
								new DijkstraNode<Node, Numeric>(
									value: neighbor,
									priority: heuristic(neighbor),
									previous: current));
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#region BreadthFirstSearch Algorithm

		/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_BreadthFirstSearch_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, SearchGoal<Node> goal) =>
			SearchGraph(start, neighbors, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, Node goal) =>
			SearchGraph(start, neighbors, goal, Equate);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, Node goal) =>
			SearchGraph(start, graph, goal, Equate);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, graph.Neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, SearchGoal<Node> goal) =>
			SearchGraph(start, graph.Neighbors, goal);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, SearchCheck<Node> check)
		{
			IQueue<BreadthFirstSearch<Node>> fringe = new QueueLinked<BreadthFirstSearch<Node>>();

			// push starting node
			fringe.Enqueue(
				new BreadthFirstSearch<Node>(
					value: start,
					previous: null));

			// run the algorithm
			while (fringe.Count != 0)
			{
				BreadthFirstSearch<Node> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				if (status is GraphSearchStatus.Goal)
				{
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							fringe.Enqueue(
								new BreadthFirstSearch<Node>(
									value: neighbor,
									previous: current));
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#endregion

		#region Sort

		#region XML
#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="span">The span to be sorted.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Sort_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
		#endregion

		#region SortBubble

		/// <summary>
		/// Sorts values using the bubble sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortBubble_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Do(get.Do(j), get.Do(j + 1)) is Greater)
					{
						// Swap
						T temp = get.Do(j + 1);
						set.Do(j + 1, get.Do(j));
						set.Do(j, temp);
					}
				}
			}
		}

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i <= span.Length - 1; i++)
			{
				for (int j = 0; j <= span.Length - 2; j++)
				{
					if (compare.Do(span[j], span[j + 1]) is Greater)
					{
						Swap(ref span[j], ref span[j + 1]);
					}
				}
			}
		}

		#endregion

		#region SortSelection

		/// <summary>
		/// Sorts values using the selection sort algoritm.
		/// <para>Runtime: Ω(n^2), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortSelection_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				int min = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare.Do(get.Do(j), get.Do(min)) is Less)
					{
						min = j;
					}
				}
				// Swap
				T temp = get.Do(min);
				set.Do(min, get.Do(i));
				set.Do(i, temp);
			}
		}

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i <= span.Length - 1; i++)
			{
				int min = i;
				for (int j = i + 1; j <= span.Length - 2; j++)
				{
					if (compare.Do(span[j], span[min]) is Less)
					{
						min = j;
					}
				}
				Swap(ref span[min], ref span[i]);
			}
		}

		#endregion

		#region SortInsertion

		/// <summary>
		/// Sorts values using the insertion sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortInsertion_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start + 1; i <= end; i++)
			{
				T temp = get.Do(i);
				int j = i;
				for (; j > start && compare.Do(get.Do(j - 1), temp) is Greater; j--)
				{
					set.Do(j, get.Do(j - 1));
				}
				set.Do(j, temp);
			}
		}

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 1; i <= span.Length - 1; i++)
			{
				T temp = span[i];
				int j;
				for (j = i; j > 0 && compare.Do(span[j - 1], temp) is Greater; j--)
				{
					span[j] = span[j - 1];
				}
				span[j] = temp;
			}
		}

		#endregion

		#region SortQuick

		/// <summary>
		/// Sorts values using the quick sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</para>
		/// <para>Memory: ln(n)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortQuick_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortQuick_Recursive(start, end - start + 1);

			void SortQuick_Recursive(int start, int length)
			{
				if (length > 1)
				{
					T pivot = get.Do(start);
					int i = start;
					int j = start + length - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Do(get.Do(j), pivot) is Less)
						{
							Swap(i++, j);
						}
						else if (compare.Do(get.Do(j), pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(k--, j--);
						}
					}
					SortQuick_Recursive(start, i - start);
					SortQuick_Recursive(k + 1, start + length - (k + 1));
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortQuick_Recursive(span, 0, span.Length);

			void SortQuick_Recursive(Span<T> span, int startIndex, int len)
			{
				if (len > 1)
				{
					T pivot = span[startIndex];
					int i = startIndex;
					int j = startIndex + len - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Do(span[j], pivot) is Less)
						{
							Swap(ref span[i++], ref span[j]);
						}
						else if (compare.Do(span[j], pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(ref span[k--], ref span[j--]);
						}
					}
					SortQuick_Recursive(span, startIndex, i - startIndex);
					SortQuick_Recursive(span, k + 1, startIndex + len - (k + 1));
				}
			}
		}

		#endregion

		#region SortMerge

		/// <summary>
		/// Sorts values using the merge sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</para>
		/// <para>Memory: Θ(n)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortMerge_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortMerge_Recursive(start, end - start + 1);

			void SortMerge_Recursive(int start, int length)
			{
				if (length > 1)
				{
					int half = length / 2;
					SortMerge_Recursive(start, half);
					SortMerge_Recursive(start + half, length - half);
					T[] sorted = new T[length];
					int i = start;
					int j = start + half;
					int k = 0;
					while (i < start + half && j < start + length)
					{
						if (compare.Do(get.Do(i), get.Do(j)) is Greater)
						{
							sorted[k++] = get.Do(j++);
						}
						else
						{
							sorted[k++] = get.Do(i++);
						}
					}
					for (int h = 0; h < start + half - i; h++)
					{
						sorted[k + h] = get.Do(i + h);
					}
					for (int h = 0; h < start + length - j; h++)
					{
						sorted[k + h] = get.Do(j + h);
					}
					for (int h = 0; h < length; h++)
					{
						set.Do(start + h, sorted[0 + h]);
					}
				}
			}
		}

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortMerge_Recursive(span);

			void SortMerge_Recursive(Span<T> span)
			{
				if (span.Length > 1)
				{
					int half = span.Length / 2;
					SortMerge_Recursive(span[..half]);
					SortMerge_Recursive(span[half..]);
					T[] sorted = new T[span.Length];
					int i = 0;
					int j = half;
					int k = 0;
					while (i < half && j < span.Length)
					{
						if (compare.Do(span[i], span[j]) is Greater)
						{
							sorted[k++] = span[j++];
						}
						else
						{
							sorted[k++] = span[i++];
						}
					}
					for (int h = 0; h < half - i; h++)
					{
						sorted[k + h] = span[i + h];
					}
					for (int h = 0; h < span.Length - j; h++)
					{
						sorted[k + h] = span[j + h];
					}
					for (int h = 0; h < span.Length; h++)
					{
						span[h] = sorted[0 + h];
					}
				}
			}
		}

		#endregion

		#region SortHeap

		/// <summary>
		/// Sorts values using the heap sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortHeap_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxSortHeapify(heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(start, i);
				heapSize--;
				MaxSortHeapify(heapSize + start, 0, start);
			}
			void MaxSortHeapify(int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Do(get.Do(left), get.Do(largest)) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Do(get.Do(right), get.Do(largest)) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					Swap(index, largest);
					MaxSortHeapify(heapSize, largest - offset, offset);
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int start = 0;
			int end = span.Length - 1;
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxSortHeapify(span, heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(ref span[start], ref span[i]);
				heapSize--;
				MaxSortHeapify(span, heapSize + start, 0, start);
			}
			void MaxSortHeapify(Span<T> span, int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Do(span[left], span[largest]) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Do(span[right], span[largest]) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					Swap(ref span[index], ref span[largest]);
					MaxSortHeapify(span, heapSize, largest - offset, offset);
				}
			}
		}

		#endregion

		#region SortOddEven

		/// <summary>
		/// Sorts values using the odd even sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortOddEven_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = start; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				for (int i = start + 1; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				void Swap(int a, int b)
				{
					T temp = get.Do(a);
					set.Do(a, get.Do(b));
					set.Do(b, temp);
				}
			}
		}

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = 0; i < span.Length - 1; i += 2)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						sorted = false;
					}
				}
				for (int i = 1; i < span.Length - 1; i += 2)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region SortCounting

#if false

		///// <summary>Method specifically for computing object keys in the SortCounting Sort algorithm.</summary>
		///// <typeparam name="T">The type of instances in the array to be sorted.</typeparam>
		///// <param name="instance">The instance to compute a counting key for.</param>
		///// <returns>The counting key computed from the provided instance.</returns>
		//public delegate int ComputeSortCountingKey(T instance);

		///// <summary>
		///// Sorts an entire array in non-decreasing order using the heap sort algorithm.
		///// <para>Runtime: Θ(Max(key))</para>
		///// <para>Memory: Max(Key)</para>
		///// <para>Stable: True</para>
		///// </summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="computeSortCountingKey">Method specifically for computing object keys in the SortCounting Sort algorithm.</param>
		///// <param name="array">The array to be sorted</param>
		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, T[] array)
		//{
		//	throw new System.NotImplementedException();

		//	// This code needs revision and conversion
		//	int[] count = new int[array.Length];
		//	int maxKey = 0;
		//	for (int i = 0; i < array.Length; i++)
		//	{
		//		int key = computeSortCountingKey(array[i]) / array.Length;
		//		count[key] += 1;
		//		if (key > maxKey)
		//			maxKey = key;
		//	}

		//	int total = 0;
		//	for (int i = 0; i < maxKey; i++)
		//	{
		//		int oldCount = count[i];
		//		count[i] = total;
		//		total += oldCount;
		//	}

		//	T[] output = new T[maxKey];
		//	for (int i = 0; i < array.Length; i++)
		//	{
		//		int key = computeSortCountingKey(array[i]);
		//		output[count[key]] = array[i];
		//		count[computeSortCountingKey(array[i])] += 1;
		//	}
		//}

		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, T[] array, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, Get<T> get, Assign<T> set, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

#endif

		#endregion

		#region Shuffle

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>
		/// Sorts values into a randomized order.
		/// <para>Runtime: O(n)</para>
		/// <para>Memory: O(1)</para>
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The array to shuffle.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Shuffle_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Random? random = null) =>
			Shuffle<T, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, get, set, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set>(int start, int end, Get get = default, Set set = default, Random? random = null)
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			Shuffle<T, Get, Set, RandomNextIntMinValueIntMaxValue>(start, end, get, set, random ?? new Random());

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set, Random>(int start, int end, Get get = default, Set set = default, Random random = default)
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
			where Random : struct, IFunc<int, int, int>
		{
			for (int i = start; i <= end; i++)
			{
				int randomIndex = random.Do(start, end);
				// Swap
				T temp = get.Do(i);
				set.Do(i, get.Do(randomIndex));
				set.Do(randomIndex, temp);
			}
		}

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(Span<T> span, Random? random = null) =>
			Shuffle<T, RandomNextIntMinValueIntMaxValue>(span, random ?? new Random());

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Random>(Span<T> span, Random random = default)
			where Random : struct, IFunc<int, int, int>
		{
			for (int i = 0; i < span.Length; i++)
			{
				int index = random.Do(0, span.Length);
				Swap(ref span[i], ref span[index]);
			}
		}

		#endregion

		#region SortBogo

		/// <summary>
		/// Sorts values using the bogo sort algorithm.
		/// <para>Runtime: Ω(n), ε(n*n!), O(∞)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortBogo_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set, random);

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random? random = null)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			SortBogo<T, Compare, Get, Set, RandomNextIntMinValueIntMaxValue>(start, end, compare, get, set, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Get, Set, Random>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random random = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
			where Random : struct, IFunc<int, int, int>
		{
			while (!SortBogoCheck(start, end))
			{
				Shuffle<T, Get, Set, Random>(start, end, get, set, random);
			}
			bool SortBogoCheck(int start, int end)
			{
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T>(Span<T> span, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare>(Span<T> span, Compare compare = default, Random? random = null)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			SortBogo<T, Compare, RandomNextIntMinValueIntMaxValue>(span, compare, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Random>(Span<T> span, Compare compare = default, Random random = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Random : struct, IFunc<int, int, int>
		{
			while (!SortBogoCheck(span))
			{
				Shuffle<T, Random>(span, random);
			}
			bool SortBogoCheck(Span<T> span)
			{
				for (int i = 1; i < span.Length; i++)
				{
					if (compare.Do(span[i - 1], span[i]) is Greater)
					{
						return false;
					}
				}
				return true;
			}
		}

		#endregion

		#region SortSlow

		/// <summary>Sorts values using the slow sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortSlow_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortSlowRecursive(start, end);
			void SortSlowRecursive(int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SortSlowRecursive(i, mid);
				SortSlowRecursive(mid + 1, j);
				if (compare.Do(get.Do(j), get.Do(mid)) is Less)
				{
					// Swap
					T temp = get.Do(j);
					set.Do(j, get.Do(mid));
					set.Do(mid, temp);
				}
				SortSlowRecursive(i, j - 1);
			}
		}

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortSlowRecursive(span, 0, span.Length - 1);
			void SortSlowRecursive(Span<T> span, int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SortSlowRecursive(span, i, mid);
				SortSlowRecursive(span, mid + 1, j);
				if (compare.Do(span[j], span[mid]) is Less)
				{
					Swap(ref span[j], ref span[mid]);
				}
				SortSlowRecursive(span, i, j - 1);
			}
		}

		#endregion

		#region SortGnome

		/// <summary>Sorts values using the gnome sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortGnome_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int i = start;
			while (i <= end)
			{
				if (i == start || compare.Do(get.Do(i), get.Do(i - 1)) != Less)
				{
					i++;
				}
				else
				{
					// Swap
					T temp = get.Do(i);
					set.Do(i, get.Do(i - 1));
					set.Do(i - 1, temp);
					i--;
				}
			}
		}

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int i = 0;
			while (i < span.Length)
			{
				if (i == 0 || compare.Do(span[i], span[i - 1]) != Less)
				{
					i++;
				}
				else
				{
					Swap(ref span[i], ref span[i - 1]);
					i--;
				}
			}
		}

		#endregion

		#region SortComb

		/// <summary>Sorts values using the comb sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortComb_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			const double shrink = 1.3;
			int gap = end - start + 1;
			bool sorted = false;
			while (!sorted)
			{
				gap = (int)(gap / shrink);
				if (gap <= 1)
				{
					gap = 1;
					sorted = true;
				}
				for (int i = start; i + gap <= end; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + gap)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + gap));
						set.Do(i + gap, temp);
						sorted = false;
					}
				}
			}
		}

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			const double shrink = 1.3;
			int gap = span.Length;
			bool sorted = false;
			while (!sorted)
			{
				gap = (int)(gap / shrink);
				if (gap <= 1)
				{
					gap = 1;
					sorted = true;
				}
				for (int i = 0; i + gap < span.Length; i++)
				{
					if (compare.Do(span[i], span[i + gap]) is Greater)
					{
						Swap(ref span[i], ref span[i + gap]);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region SortShell

		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortShell_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap + start; i <= end; i++)
				{
					T temp = get.Do(i);
					int j = i;
					for (; j >= gap && compare.Do(get.Do(j - gap), temp) is Greater; j -= gap)
					{
						set.Do(j, get.Do(j - gap));
					}
					set.Do(j, temp);
				}
			}
		}

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap; i < span.Length; i++)
				{
					T temp = span[i];
					int j = i;
					for (; j >= gap && compare.Do(span[j - gap], temp) is Greater; j -= gap)
					{
						span[j] = span[j - gap];
					}
					span[j] = temp;
				}
			}
		}

		#endregion

		#region SortCocktail

		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortCocktail_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
				swapped = false;
				for (int i = end - 1; i >= start; i--)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
			}
		}

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, FuncRuntime<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = 0; i < span.Length - 1; i++)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
				swapped = false;
				for (int i = span.Length - 2; i >= 0; i--)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
			}
		}

		#endregion

		#endregion

		#region NextUnique

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
		{
#pragma warning disable CS0618
			if (count < Math.Sqrt(maxValue - minValue))
			{
				NextUniqueRollTracking(count, minValue, maxValue, random, step);
			}
			else
			{
				NextUniquePoolTracking(count, minValue, maxValue, random, step);
			}
#pragma warning restore CS0618
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		[Obsolete("It is recommended you use " + nameof(NextUnique) + " method instead.", false)]
		public static void NextUniqueRollTracking<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			// Algorithm B: O(.5*count^2), Ω(count), ε(.5*count^2)
			Node? head = null;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int roll = random.Do(minValue, maxValue - i);
				if (roll < minValue || roll >= maxValue - i)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				Node? node = head;
				Node? previous = null;
				while (node is not null && node.Value <= roll) // O(count / 2), Ω(0), ε(count / 2)
				{
					roll++;
					previous = node;
					node = node.Next;
				}
				step.Do(roll);
				if (previous is null)
				{
					head = new Node() { Value = roll, Next = head, };
				}
				else
				{
					previous.Next = new Node() { Value = roll, Next = previous.Next };
				}
			}
		}
		internal class Node
		{
			internal int Value;
			internal Node? Next;
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Step">The function to perform on each generated <see cref="int"/> value.</typeparam>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		[Obsolete("It is recommended you use " + nameof(NextUnique) + " method instead.", false)]
		public static void NextUniquePoolTracking<Step, Random>(int count, int minValue, int maxValue, Random random = default, Step step = default)
			where Step : struct, IAction<int>
			where Random : struct, IFunc<int, int, int>
		{
			if (maxValue < minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(maxValue), $"{nameof(minValue)} > {nameof(maxValue)}");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} < 0");
			}
			if (maxValue - minValue < count)
			{
				throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} is larger than {nameof(maxValue)} - {nameof(minValue)}.");
			}
			// Algorithm B: Θ(range + count)
			int pool = maxValue - minValue;
			int[] array = new int[pool];
			for (int i = 0, j = minValue; j < maxValue; i++, j++) // Θ(range)
				array[i] = j;
			for (int i = 0; i < count; i++) // Θ(count)
			{
				int rollIndex = random.Do(0, pool);
				if (rollIndex < 0 || rollIndex >= pool)
				{
					throw new ArgumentException("The Random provided returned a value outside the requested range.");
				}
				int roll = array[rollIndex];
				array[rollIndex] = array[--pool];
				step.Do(roll);
			}
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		/// <param name="step">The function to perform on each generated <see cref="int"/> value.</param>
		public static void NextUnique<Random>(int count, int minValue, int maxValue, Action<int> step, Random random = default)
			where Random : struct, IFunc<int, int, int>
		{
			_ = step ?? throw new ArgumentNullException(nameof(step));
			NextUnique<ActionRuntime<int>, Random>(count, minValue, maxValue, random, step);
		}

		/// <summary>
		/// Generates <paramref name="count"/> unique random <see cref="int"/> values in the
		/// [<paramref name="minValue"/>..<paramref name="maxValue"/>] range where <paramref name="minValue"/> is
		/// inclusive and <paramref name="maxValue"/> is exclusive.
		/// </summary>
		/// <typeparam name="Random">The random to generation algorithm.</typeparam>
		/// <param name="random">The random to generation algorithm.</param>
		/// <param name="count">The number of <see cref="int"/> values to generate.</param>
		/// <param name="minValue">Inclusive endpoint of the random generation range.</param>
		/// <param name="maxValue">Exclusive endpoint of the random generation range.</param>
		public static int[] NextUnique<Random>(int count, int minValue, int maxValue, Random random = default)
			where Random : struct, IFunc<int, int, int>
		{
			int[] values = new int[count];
			int i = 0;
			NextUnique<ActionRuntime<int>, Random>(count, minValue, maxValue, random, new Action<int>(value => values[i++] = value));
			return values;
		}

		#endregion

		#region IsPalindrome

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T>(int start, int end, Func<int, T> get, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, FuncRuntime<int, T> , FuncRuntime<T, T, bool>>(start, end, get, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Get">The get index function of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Get>(int start, int end, Get get = default, Func<T, T, bool>? equate = default)
			where Get : struct, IFunc<int, T> =>
			IsPalindrome<T, Get, FuncRuntime<T, T, bool>>(start, end, get, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <typeparam name="Get">The get index function of the sequence.</typeparam>
		/// <param name="start">The inclusive starting index of the palindrome check.</param>
		/// <param name="end">The inclusive ending index of the palindrome check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <param name="get">The get index function of the sequence.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Get, Equate>(int start, int end, Get get = default, Equate equate = default)
			where Get : struct, IFunc<int, T>
			where Equate : struct, IFunc<T, T, bool>
		{
			int middle = (end - start) / 2 + start;
			for (int i = start; i <= middle; i++)
			{
				if (!equate.Do(get.Do(i), get.Do(end - i + start)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <param name="span">The span to check.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome(ReadOnlySpan<char> span) =>
			IsPalindrome<char, CharEquate>(span);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <param name="span">The span to check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T>(ReadOnlySpan<T> span, Func<T, T, bool>? equate = default) =>
			IsPalindrome<T, FuncRuntime<T, T, bool>>(span, equate ?? Equate);

		/// <summary>Determines if a sequence is a palindrome.</summary>
		/// <typeparam name="T">The element type of the sequence.</typeparam>
		/// <typeparam name="Equate">The element equate function.</typeparam>
		/// <param name="span">The span to check.</param>
		/// <param name="equate">The element equate function.</param>
		/// <returns>True if the sequence is a palindrome; False if not.</returns>
		public static bool IsPalindrome<T, Equate>(ReadOnlySpan<T> span, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
		{
			int middle = span.Length / 2;
			for (int i = 0; i < middle; i++)
			{
				if(!equate.Do(span[i], span[^(i + 1)]))
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region IsInterleaved

		#region XML

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name

		/// <typeparam name="T">The element type of the sequences.</typeparam>
		/// <param name="a">The first sequence to determine if <paramref name="c"/> is interleaved of.</param>
		/// <param name="b">The second sequence to determine if <paramref name="c"/> is interleaved of.</param>
		/// <param name="c">The sequence to determine if it is interleaved from <paramref name="a"/> and <paramref name="b"/>.</param>
		/// <param name="equate">The function for equating <typeparamref name="T"/> values.</param>
		/// <returns>True if <paramref name="c"/> is interleaved of <paramref name="a"/> and <paramref name="b"/> or False if not.</returns>
		/// <example>
		/// <code>
		/// IsInterleaved("abc", "xyz", "axbycz") // True
		/// IsInterleaved("abc", "xyz", "cbazyx") // False (order not preserved)
		/// IsInterleaved("abc", "xyz", "012345") // False
		/// </code>
		/// </example>
		internal static void IsInterleaved_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// <para>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all elements of <paramref name="a"/> and <paramref name="b"/> 
		/// while retaining the order of the respective elements. Uses a recursive algorithm.
		/// </para>
		/// <para>Runtime: O(2^(Min(<paramref name="a"/>.Length + <paramref name="b"/>.Length, <paramref name="c"/>.Length))), Ω(1)</para>
		/// <para>Memory: O(1)</para>
		/// </summary>
		/// <inheritdoc cref="IsInterleaved_XML"/>
		internal static void IsInterleavedRecursive_XML() => throw new DocumentationMethodException();

		/// <summary>
		/// Determines if <paramref name="c"/> is interleved of <paramref name="a"/> and <paramref name="b"/>,
		/// meaning that <paramref name="c"/> it contains all elements of <paramref name="a"/> and <paramref name="b"/> 
		/// while retaining the order of the respective elements. Uses a interative algorithm.
		/// <para>Runtime: O(Min(<paramref name="a"/>.Length * <paramref name="b"/>.Length))), Ω(1)</para>
		/// <para>Memory: O(<paramref name="a"/>.Length * <paramref name="b"/>.Length)</para>
		/// </summary>
		/// <inheritdoc cref="IsInterleaved_XML"/>
		internal static void IsInterleavedIterative_XML() => throw new DocumentationMethodException();

#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedRecursive<T, FuncRuntime<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedRecursive<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedRecursive<T, Equate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
		{
			if (a.Length + b.Length != c.Length)
			{
				return false;
			}

			bool Implementation(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c) =>
				a.IsEmpty && b.IsEmpty && c.IsEmpty ||
				(
					!c.IsEmpty &&
					(
						(!a.IsEmpty && equate.Do(a[0], c[0]) && Implementation(a[1..], b, c[1..])) ||
						(!b.IsEmpty && equate.Do(b[0], c[0]) && Implementation(a, b[1..], c[1..]))
					)
				);

			return Implementation(a, b, c);
		}

		/// <inheritdoc cref="IsInterleavedIterative_XML"/>
		public static bool IsInterleavedIterative<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Func<T, T, bool>? equate = default) =>
			IsInterleavedIterative<T, FuncRuntime<T, T, bool>>(a, b, c, equate ?? Equate);

		/// <inheritdoc cref="IsInterleavedRecursive_XML"/>
		public static bool IsInterleavedIterative(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c) =>
			IsInterleavedIterative<char, CharEquate>(a, b, c);

		/// <inheritdoc cref="IsInterleavedIterative_XML"/>
		public static bool IsInterleavedIterative<T, Equate>(ReadOnlySpan<T> a, ReadOnlySpan<T> b, ReadOnlySpan<T> c, Equate equate = default)
			where Equate : struct, IFunc<T, T, bool>
		{
			if (a.Length + b.Length != c.Length)
			{
				return false;
			}
			bool[,] d = new bool[a.Length + 1, b.Length + 1];
			for (int i = 0; i <= a.Length; ++i)
			{
				for (int j = 0; j <= b.Length; ++j)
				{
					d[i, j] =
						(i == 0 && j == 0) ||
						(
							i == 0 ? (equate.Do(b[j - 1], c[j - 1]) && d[i, j - 1]) :
							j == 0 ? (equate.Do(a[i - 1], c[i - 1]) && d[i - 1, j]) :
							equate.Do(a[i - 1], c[i + j - 1]) && !equate.Do(b[j - 1], c[i + j - 1]) ? d[i - 1, j] :
							!equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]) ? d[i, j - 1] :
							equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]) && (d[i - 1, j] || d[i, j - 1])
						);

					#region expanded version

					//if (i == 0 && j == 0)
					//{
					//	d[i, j] = true;
					//}
					//else if (i == 0)
					//{
					//	if (equate.Do(b[j - 1], c[j - 1]))
					//	{
					//		d[i, j] = d[i, j - 1];
					//	}
					//}
					//else if (j == 0)
					//{
					//	if (equate.Do(a[i - 1], c[i - 1]))
					//	{
					//		d[i, j] = d[i - 1, j];
					//	}
					//}
					//else if (equate.Do(a[i - 1], c[i + j - 1]) && !equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i - 1, j];
					//}
					//else if (!equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i, j - 1];
					//}
					//else if (equate.Do(a[i - 1], c[i + j - 1]) && equate.Do(b[j - 1], c[i + j - 1]))
					//{
					//	d[i, j] = d[i - 1, j] || d[i, j - 1];
					//}

					#endregion
				}
			}
			return d[a.Length, b.Length];
		}

		#endregion

		#region Structs

		/// <summary>Default int compare.</summary>
		public struct IntCompare : IFunc<int, int, CompareResult>
		{
			/// <summary>Default int compare.</summary>
			/// <param name="a">The left hand side of the compare.</param>
			/// <param name="b">The right ahnd side of the compare.</param>
			/// <returns>The result of the comparison.</returns>
			public CompareResult Do(int a, int b) => a.CompareTo(b).ToCompareResult();
		}

		/// <summary>Compares two char values for equality.</summary>
		public struct CharEquate : IFunc<char, char, bool>
		{
			/// <summary>Compares two char values for equality.</summary>
			/// <param name="a">The first operand of the equality check.</param>
			/// <param name="b">The second operand of the equality check.</param>
			/// <returns>True if equal; False if not.</returns>
			public bool Do(char a, char b) => a == b;
		}

		/// <summary>Built in Compare struct for runtime computations.</summary>
		/// <typeparam name="T">The generic type of the values to compare.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		public struct SiftFromCompareAndValue<T, Compare> : IFunc<T, CompareResult>
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			internal Compare CompareFunction;
			internal T Value;

			/// <summary>The invocation of the compile time delegate.</summary>
			public CompareResult Do(T a) => CompareFunction.Do(a, Value);

			/// <summary>Creates a compile-time-resolved sifting function to be passed into another type.</summary>
			/// <param name="value">The value for future values to be compared against.</param>
			/// <param name="compare">The compare function.</param>
			public SiftFromCompareAndValue(T value, Compare compare = default)
			{
				Value = value;
				CompareFunction = compare;
			}
		}

		/// <summary>Compile time resulution to the <see cref="StepStatus.Continue"/> value.</summary>
		public struct StepStatusContinue : IFunc<StepStatus>
		{
			/// <summary>Returns <see cref="StepStatus.Continue"/>.</summary>
			/// <returns><see cref="StepStatus.Continue"/></returns>
			public StepStatus Do() => Continue;
		}

		/// <summary>Struct wrapper for the <see cref="Random.Next(int, int)"/> method as an <see cref="IFunc{T1, T2, TResult}"/>.</summary>
		public struct RandomNextIntMinValueIntMaxValue : IFunc<int, int, int>
		{
			internal Random _random;
			/// <inheritdoc cref="Random.Next(int, int)"/>
			public int Do(int minValue, int maxValue) => _random.Next(minValue, maxValue);
			/// <summary>Casts a <see cref="Random"/> to a struct wrapper.</summary>
			public static implicit operator RandomNextIntMinValueIntMaxValue(Random random) => new RandomNextIntMinValueIntMaxValue() { _random = random, };
		}

		#endregion
	}
}
