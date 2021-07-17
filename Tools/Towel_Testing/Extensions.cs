using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	[TestClass]
	public partial class Extensions_Testing
	{
		#region string.Replace (multiple)

		[TestMethod]
		public void String_Replace()
		{
			Assert.AreEqual("aaa bbb c ddd e", "a b c d e".Replace(("a", "aaa"), ("b", "bbb"), ("d", "ddd")));

			Assert.AreEqual("aaa bbb c d e", "a b c d e".Replace(("a", "aaa"), ("b", "bbb"), ("aaa", "ERROR")));

			Assert.ThrowsException<ArgumentNullException>(() => Extensions.Replace(null!, ("a", "b")));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace(null!));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace((null!, "a")));
			Assert.ThrowsException<ArgumentNullException>(() => string.Empty.Replace(("a", null!)));

			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace());
			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace(("a", "b"), ("a", "c")));
			Assert.ThrowsException<ArgumentException>(() => string.Empty.Replace((string.Empty, "a")));
		}

		#endregion

		#region System.Range.ToIEnumerable

		[TestMethod]
		public void ToIEnumerable_Range()
		{
			Assert.IsTrue(Equate<int>((0..4).ToIEnumerable().ToArray(), new[] { 0, 1, 2, 3 }));
			Assert.IsTrue(Equate<int>((4..0).ToIEnumerable().ToArray(), new[] { 4, 3, 2, 1 }));

			Assert.IsTrue(Equate<int>((11..14).ToIEnumerable().ToArray(), new[] { 11, 12, 13 }));
			Assert.IsTrue(Equate<int>((14..11).ToIEnumerable().ToArray(), new[] { 14, 13, 12 }));

			//// unfortuntately System.Range syntax does not support negative values
			// Assert.IsTrue(Equate<int>((0..-3).ToIEnumerable().ToArray(), new[] {  0, -1, -2 }));
			// Assert.IsTrue(Equate<int>((-3..0).ToIEnumerable().ToArray(), new[] { -3, -2, -1 }));

			Assert.ThrowsException<ArgumentException>(() => (^0..4).ToIEnumerable().ToArray());
			Assert.ThrowsException<ArgumentException>(() => (0..^4).ToIEnumerable().ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^0..^4).ToIEnumerable().ToArray());
		}

		#endregion

		#region System.Range.ToArray

		[TestMethod]
		public void ToArray_Range()
		{
			Assert.IsTrue(Equate<int>(( ..0).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((0..0).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((1..1).ToArray(), Ɐ<int>()));
			Assert.IsTrue(Equate<int>((2..2).ToArray(), Ɐ<int>()));

			// least to greatest
			Assert.IsTrue(Equate<int>(( ..1).ToArray(), Ɐ(0)));
			Assert.IsTrue(Equate<int>(( ..2).ToArray(), Ɐ(0, 1)));
			Assert.IsTrue(Equate<int>((0..1).ToArray(), Ɐ(0)));
			Assert.IsTrue(Equate<int>((0..2).ToArray(), Ɐ(0, 1)));
			Assert.IsTrue(Equate<int>((1..2).ToArray(), Ɐ(1)));
			Assert.IsTrue(Equate<int>((1..3).ToArray(), Ɐ(1, 2)));
			Assert.IsTrue(Equate<int>((1..4).ToArray(), Ɐ(1, 2, 3)));
			Assert.IsTrue(Equate<int>((1..5).ToArray(), Ɐ(1, 2, 3, 4)));
			Assert.IsTrue(Equate<int>((2..3).ToArray(), Ɐ(2)));
			Assert.IsTrue(Equate<int>((2..4).ToArray(), Ɐ(2, 3)));

			// greatest to least
			Assert.IsTrue(Equate<int>((2..1).ToArray(), Ɐ(2)));
			Assert.IsTrue(Equate<int>((3..1).ToArray(), Ɐ(3, 2)));
			Assert.IsTrue(Equate<int>((4..1).ToArray(), Ɐ(4, 3, 2)));
			Assert.IsTrue(Equate<int>((5..1).ToArray(), Ɐ(5, 4, 3, 2)));
			Assert.IsTrue(Equate<int>((3..2).ToArray(), Ɐ(3)));
			Assert.IsTrue(Equate<int>((4..2).ToArray(), Ɐ(4, 3)));

			Assert.ThrowsException<ArgumentException>(() => (..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (1..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (..^1).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..^1).ToArray());
		}

		#endregion

		#region System.Range.Select

		[TestMethod]
		public void Select_Range()
		{
			Assert.IsTrue(( ..0).Select(x => x).SequenceEqual(Ɐ<int>()));
			Assert.IsTrue((0..0).Select(x => x).SequenceEqual(Ɐ<int>()));
			Assert.IsTrue((1..1).Select(x => x).SequenceEqual(Ɐ<int>()));
			Assert.IsTrue((2..2).Select(x => x).SequenceEqual(Ɐ<int>()));

			// least to greatest
			Assert.IsTrue(( ..1).Select(x => x).SequenceEqual(Ɐ(0)));
			Assert.IsTrue(( ..2).Select(x => x).SequenceEqual(Ɐ(0, 1)));
			Assert.IsTrue((0..1).Select(x => x).SequenceEqual(Ɐ(0)));
			Assert.IsTrue((0..2).Select(x => x).SequenceEqual(Ɐ(0, 1)));
			Assert.IsTrue((1..2).Select(x => x).SequenceEqual(Ɐ(1)));
			Assert.IsTrue((1..3).Select(x => x).SequenceEqual(Ɐ(1, 2)));
			Assert.IsTrue((1..4).Select(x => x).SequenceEqual(Ɐ(1, 2, 3)));
			Assert.IsTrue((1..5).Select(x => x).SequenceEqual(Ɐ(1, 2, 3, 4)));
			Assert.IsTrue((2..3).Select(x => x).SequenceEqual(Ɐ(2)));
			Assert.IsTrue((2..4).Select(x => x).SequenceEqual(Ɐ(2, 3)));

			// greatest to least
			Assert.IsTrue((2..1).Select(x => x).SequenceEqual(Ɐ(2)));
			Assert.IsTrue((3..1).Select(x => x).SequenceEqual(Ɐ(3, 2)));
			Assert.IsTrue((4..1).Select(x => x).SequenceEqual(Ɐ(4, 3, 2)));
			Assert.IsTrue((5..1).Select(x => x).SequenceEqual(Ɐ(5, 4, 3, 2)));
			Assert.IsTrue((3..2).Select(x => x).SequenceEqual(Ɐ(3)));
			Assert.IsTrue((4..2).Select(x => x).SequenceEqual(Ɐ(4, 3)));

			// some types other than int
			Assert.IsTrue(('a'..'f').Select(x => (char)x).SequenceEqual(Ɐ('a', 'b', 'c', 'd', 'e')));
			Assert.IsTrue(('a'..'f').Select(x => ((char)x).ToString()).SequenceEqual(Ɐ("a", "b", "c", "d", "e")));

			Assert.ThrowsException<ArgumentException>(() => (..).Select(x => x).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (1..).Select(x => x).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..).Select(x => x).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (..^1).Select(x => x).ToArray());
			Assert.ThrowsException<ArgumentException>(() => (^1..^1).Select(x => x).ToArray());
		}

		#endregion
	}
}
