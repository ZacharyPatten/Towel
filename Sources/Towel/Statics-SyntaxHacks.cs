using System;
using System.Linq;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
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
			/// <typeparam name="TA">The first type in the value tuples in the array.</typeparam>
			/// <typeparam name="TB">The second type in the value tuples in the array.</typeparam>
			/// <param name="values">The param values.</param>
			public delegate void ParamsAction<TA, TB>(params (TA, TB)[] values);

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
				/// <param name="b">The right value to compare this condition to.</param>
				/// <returns>The result of the condition.</returns>
				public abstract bool Resolve(T b);

				/// <summary>Casts a <typeparamref name="T"/> to a bool using an equality check.</summary>
				/// <param name="value">The value this condition will compare with.</param>
				public static implicit operator Condition<T>(T value) => new Value<T>(a: value);

				/// <summary>Uses the bool as the condition result.</summary>
				/// <param name="result">The <see cref="bool"/>result of this condition.</param>
				public static implicit operator Condition<T>(bool result) => new Bool<T> { Result = result, };

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0060 // Remove unused parameter

				/// <summary>Converts a keyword to a condition result (for "Default" case).</summary>
				/// <param name="keyword"><see cref="Keyword.Default"/></param>
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
				/// <returns>The result of the condition.</returns>
				public abstract bool Resolve();

				/// <summary>Uses the bool as the condition result.</summary>
				/// <param name="result">The <see cref="bool"/>result of this condition.</param>
				public static implicit operator Condition(bool result) => new Bool { Result = result, };

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0060 // Remove unused parameter

				/// <summary>Converts a keyword to a condition result (for "Default" case).</summary>
				/// <param name="keyword"><see cref="Keyword.Default"/></param>
				public static implicit operator Condition(Keyword keyword) => new Default();

#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0079 // Remove unnecessary suppression

				/// <summary>Converts a condition to a bool using the Resolve method.</summary>
				/// <param name="condition">The condition to resolve to a <see cref="bool"/> value.</param>
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
			public static Random Algorithm = new();

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
				new()
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
					new(a.Result && Compare(a.A, b) == Greater, b);
				/// <summary>Adds a less than operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the less than operation.</param>
				/// <returns>A running inequality with the additonal less than operation.</returns>
				public static Inequality<T> operator <(Inequality<T> a, T b) =>
					new(a.Result && Compare(a.A, b) == Less, b);
				/// <summary>Adds a greater than or equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the greater than or equal operation.</param>
				/// <returns>A running inequality with the additonal greater than or equal operation.</returns>
				public static Inequality<T> operator >=(Inequality<T> a, T b) =>
					new(a.Result && Compare(a.A, b) != Less, b);
				/// <summary>Adds a less than or equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the less than or equal operation.</param>
				/// <returns>A running inequality with the additonal less than or equal operation.</returns>
				public static Inequality<T> operator <=(Inequality<T> a, T b) =>
					new(a.Result && Compare(a.A, b) != Greater, b);
				/// <summary>Adds an equal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the equal operation.</param>
				/// <returns>A running inequality with the additonal equal operation.</returns>
				public static Inequality<T> operator ==(Inequality<T> a, T b) =>
					new(a.Result && Equate(a.A, b), b);
				/// <summary>Adds an inequal operation to a running inequality.</summary>
				/// <param name="a">The current running inequality and left hand operand.</param>
				/// <param name="b">The value of the right hand operand of the inequal operation.</param>
				/// <returns>A running inequality with the additonal inequal operation.</returns>
				public static Inequality<T> operator !=(Inequality<T> a, T b) =>
					new(a.Result && Inequate(a.A, b), b);
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

#pragma warning disable CS0618 // Type or member is obsolete

		/// <summary>Universal Quantification Operator.</summary>
		/// <typeparam name="T">The element type of the universal quantification to declare.</typeparam>
		/// <param name="values">The values of the universal quantification.</param>
		/// <returns>The declared universal quantification.</returns>
		public static UniversalQuantification<T> Ɐ<T>(params T[] values) => new(values);

#pragma warning restore CS0618 // Type or member is obsolete

		/// <summary>Universal Quantification.</summary>
		/// <typeparam name="T">The element type of the universal quantification.</typeparam>
		public struct UniversalQuantification<T> :
			System.Collections.Generic.IEnumerable<T>,
			System.Collections.Generic.IList<T>,
			Towel.DataStructures.IArray<T>
		{
			internal T[] Value;

			/// <summary>Not intended to be invoked directly.</summary>
			/// <param name="array">The array value of the universal quantification.</param>
			[Obsolete(TowelConstants.NotIntended, false)]
			internal UniversalQuantification(T[] array) => Value = array;

			#region Towel.Datastructures.IArray<T>

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public int Length => Value.Length;

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public StepStatus StepperBreak<TStep>(TStep step = default)
				where TStep : struct, IFunc<T, StepStatus> =>
				Value.StepperBreak(step);

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public T[] ToArray() => Value;

			#endregion

			#region System.Collections.Generic.IList<T>

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public T this[int index]
			{
				get => Value[index];
				set => Value[index] = value;
			}

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public int Count => Value.Length;

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public bool IsReadOnly => false;

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public void Add(T item)
			{
				T[] newValue = new T[Value.Length + 1];
				Array.Copy(Value, newValue, Value.Length);
				newValue[Value.Length] = item;
				Value = newValue;
			}

			/// <summary>Not intended to be invoked directly.</summary>
			[Obsolete(TowelConstants.NotIntended, false)]
			public void Clear() => Value = Array.Empty<T>();

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public bool Contains(T item) => Value.Contains(item);

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public void CopyTo(T[] array, int arrayIndex) =>
				Array.Copy(Value, 0, array, arrayIndex, Value.Length);

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public int IndexOf(T item) => Array.IndexOf(Value, item);

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
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
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
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
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
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

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			public System.Collections.Generic.IEnumerator<T> GetEnumerator() => ((System.Collections.Generic.IEnumerable<T>)Value).GetEnumerator();

			/// <summary>Not intended to be invoked directly.</summary>
			/// <inheritdoc/>
			[Obsolete(TowelConstants.NotIntended, false)]
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Value.GetEnumerator();

			#endregion

			#region Implicit Casting Operators

			/// <summary>Converts a universal quantification to a memory.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator ReadOnlyMemory<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to a memory.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Memory<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to a span.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator ReadOnlySpan<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to a span.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Span<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to an array.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator T[](UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to a <see cref="System.Collections.Generic.List{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.List<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.HashSet{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.HashSet<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to a <see cref="System.Collections.Generic.LinkedList{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.LinkedList<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.Stack{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.Stack<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an <see cref="System.Collections.Generic.Queue{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.Queue<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to a sorted <see cref="System.Collections.Generic.SortedSet{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator System.Collections.Generic.SortedSet<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value);
			/// <summary>Converts a universal quantification to an Action&lt;Action&lt;T&gt;&gt;.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Action<Action<T>>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepper();
			/// <summary>Converts a universal quantification to an Func&lt;Func&lt;T, StepStatus&gt;, StepStatus&gt;.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Func<Func<T, StepStatus>, StepStatus>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value.ToStepperBreak();
			/// <summary>Converts a universal quantification to an <see cref="Array{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.Array<T>(UniversalQuantification<T> universalQuantification) => universalQuantification.Value;
			/// <summary>Converts a universal quantification to an <see cref="ListArray{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.ListArray<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value, universalQuantification.Value.Length);
			/// <summary>Converts a universal quantification to an <see cref="StackArray{T}"/>.</summary>
			/// <param name="universalQuantification">The universal quantification to be converted.</param>
			public static implicit operator Towel.DataStructures.StackArray<T>(UniversalQuantification<T> universalQuantification) => new(universalQuantification.Value, universalQuantification.Value.Length, default);

			#endregion
		}

		#endregion
	}
}
