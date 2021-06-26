using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.Mathematics;

namespace Towel_Testing.Mathematics
{
	[TestClass] public class Symbolics_Testing
	{
		[TestMethod] public void Parse_String_Testing()
		{
			Symbolics.Constant<int> ONE = new(1);

			#region Basic Negate Tests

			{
				var A = Symbolics.Parse<int>("-1", s => (int.TryParse(s, out int value), value));
				var B = -ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("- 1", s => (int.TryParse(s, out int value), value));
				var B = -ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Basic Add Tests

			{
				var A = Symbolics.Parse<int>("1+1", s => (int.TryParse(s, out int value), value));
				var B = ONE + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + 1", s => (int.TryParse(s, out int value), value));
				var B = ONE + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + (1 + 1)", s => (int.TryParse(s, out int value), value));
				var B = ONE + (ONE + ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + (1 + (1 + 1))", s => (int.TryParse(s, out int value), value));
				var B = ONE + (ONE + (ONE + ONE));
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Basic Subtract Tests

			{
				var A = Symbolics.Parse<int>("1-1", s => (int.TryParse(s, out int value), value));
				var B = ONE - ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - 1", s => (int.TryParse(s, out int value), value));
				var B = ONE - ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - (1 - 1)", s => (int.TryParse(s, out int value), value));
				var B = ONE - (ONE - ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - (1 - (1 - 1))", s => (int.TryParse(s, out int value), value));
				var B = ONE - (ONE - (ONE - ONE));
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Basic Multiply Tests

			{
				var A = Symbolics.Parse<int>("1*1", s => (int.TryParse(s, out int value), value));
				var B = ONE * ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * 1", s => (int.TryParse(s, out int value), value));
				var B = ONE * ONE;
				Assert.IsTrue(A.Equals(B));
			}
			{
				var A = Symbolics.Parse<int>("1 * (1 * 1)", s => (int.TryParse(s, out int value), value));
				var B = ONE * (ONE * ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * (1 * (1 * 1))", s => (int.TryParse(s, out int value), value));
				var B = ONE * (ONE * (ONE * ONE));
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Basic Divide Tests

			{
				var A = Symbolics.Parse<int>("1/1", s => (int.TryParse(s, out int value), value));
				var B = ONE / ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 / 1", s => (int.TryParse(s, out int value), value));
				var B = ONE / ONE;
				Assert.IsTrue(A.Equals(B));
			}
			{
				var A = Symbolics.Parse<int>("1 / (1 / 1)", s => (int.TryParse(s, out int value), value));
				var B = ONE / (ONE / ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 / (1 / (1 / 1))", s => (int.TryParse(s, out int value), value));
				var B = ONE / (ONE / (ONE / ONE));
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Basic Factorial Tests

			{
				var A = Symbolics.Parse<int>("1!", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 !", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			#region Order Of Operations (2 operands)

			{
				var A = Symbolics.Parse<int>("1 + -1", s => (int.TryParse(s, out int value), value));
				var B = ONE + -ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("-1 + 1", s => (int.TryParse(s, out int value), value));
				var B = -ONE + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + 1 - 1", s => (int.TryParse(s, out int value), value));
				var B = ONE + ONE - ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - 1 + 1", s => (int.TryParse(s, out int value), value));
				var B = ONE - ONE + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + 1 * 1", s => (int.TryParse(s, out int value), value));
				var B = ONE + ONE * ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * 1 + 1", s => (int.TryParse(s, out int value), value));
				var B = ONE * ONE + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - 1 * 1", s => (int.TryParse(s, out int value), value));
				var B = ONE - ONE * ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * 1 - 1", s => (int.TryParse(s, out int value), value));
				var B = ONE * ONE - ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * 1 / 1", s => (int.TryParse(s, out int value), value));
				var B = ONE * ONE / ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 / 1 * 1", s => (int.TryParse(s, out int value), value));
				var B = ONE / ONE * ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 + 1!", s => (int.TryParse(s, out int value), value));
				var B = ONE + new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1! + 1", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE) + ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 - 1!", s => (int.TryParse(s, out int value), value));
				var B = ONE - new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1! - 1", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE) - ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 * 1!", s => (int.TryParse(s, out int value), value));
				var B = ONE * new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1! * 1", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE) * ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1 / 1!", s => (int.TryParse(s, out int value), value));
				var B = ONE / new Symbolics.Factorial(ONE);
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}
			{
				var A = Symbolics.Parse<int>("1! / 1", s => (int.TryParse(s, out int value), value));
				var B = new Symbolics.Factorial(ONE) / ONE;
				Assert.IsTrue(A.Equals(B));
				Assert.IsTrue(A.ToString()!.Equals(B.ToString()));
			}

			#endregion

			//Assert.Inconclusive("Test Method Not Fully Implemented");
		}
	}
}
