using System;
using System.Collections.Generic;
using System.Text;

namespace Towel
{
	/// <summary>Definitions for Switch syntax.</summary>
	public static class SwitchSyntax
	{
		internal static SwitchDelegate<T> Do<T>(T value) =>
			possibleActions =>
			{
				foreach (var possibleAction in possibleActions)
				{
					if (possibleAction.Condition.Resolve(value))
					{
						possibleAction.Action();
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

		public enum Keyword
		{
			Default,
		}

		/// <summary>The delegate of the switch statement.</summary>
		/// <typeparam name="T">The generic type of the switch statement.</typeparam>
		/// <param name="possibleActions">The possible actions of the switch statement.</param>
		public delegate void SwitchDelegate<T>(params (Condition<T> Condition, Action Action)[] possibleActions);

		public abstract class Condition<T>
		{
			public abstract bool Resolve(T b);
			public static implicit operator Condition<T>(T value) => new Value<T> { A = value, };
			public static implicit operator Condition<T>(bool result) => new Bool<T> { Result = result, };
			public static implicit operator Condition<T>(Keyword keyword) => new Default<T>();
		}

		internal class Value<T> : Condition<T>
		{
			internal T A;
			public override bool Resolve(T b) => A.Equals(b);
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

		public abstract class Condition
		{
			public abstract bool Resolve();
			public static implicit operator Condition(bool result) => new Bool { Result = result, };
			public static implicit operator Condition(Keyword keyword) => new Default();
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
}
