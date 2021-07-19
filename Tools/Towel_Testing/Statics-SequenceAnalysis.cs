using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		#region IsPalindrome

		[TestMethod]
		public void IsPalindrome_Span_Testing()
		{
			// char----------------

			// odd
			Assert.IsTrue(IsPalindrome("a"));
			Assert.IsTrue(IsPalindrome("aaa"));
			Assert.IsTrue(IsPalindrome("bab"));
			Assert.IsTrue(IsPalindrome("ababa"));
			Assert.IsTrue(IsPalindrome("cbabc"));
			// even
			Assert.IsTrue(IsPalindrome("aa"));
			Assert.IsTrue(IsPalindrome("aabbaa"));
			Assert.IsTrue(IsPalindrome("aabbccbbaa"));
			// false
			Assert.IsFalse(IsPalindrome("ab"));
			Assert.IsFalse(IsPalindrome("abc"));
			Assert.IsFalse(IsPalindrome("abaa"));
			Assert.IsFalse(IsPalindrome("aaba"));
			Assert.IsFalse(IsPalindrome("baaa"));
			Assert.IsFalse(IsPalindrome("aaab"));

			// non-char------------

			// odd
			Assert.IsTrue(IsPalindrome<int>(new[] { 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 2, 1, 2 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 2, 1, 2, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 3, 2, 1, 2, 3 }));
			// even
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1 }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 2, 2, 1, 1, }));
			Assert.IsTrue(IsPalindrome<int>(new[] { 1, 1, 2, 2, 3, 3, 2, 2, 1, 1, }));
			// false
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2, 3 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 2, 1, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 1, 2, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 2, 1, 1, 1 }));
			Assert.IsFalse(IsPalindrome<int>(new[] { 1, 1, 1, 2 }));
		}

		[TestMethod]
		public void IsPalindrome_NonSpan_Testing()
		{
			// char-----------------

			// odd
			string a = "a";
			Assert.IsTrue(IsPalindrome(0, a.Length - 1, i => a[i]));
			string aaa = "aaa";
			Assert.IsTrue(IsPalindrome(0, aaa.Length - 1, i => aaa[i]));
			string bab = "bab";
			Assert.IsTrue(IsPalindrome(0, bab.Length - 1, i => bab[i]));
			string ababa = "ababa";
			Assert.IsTrue(IsPalindrome(0, ababa.Length - 1, i => ababa[i]));
			string cbabc = "cbabc";
			Assert.IsTrue(IsPalindrome(0, cbabc.Length - 1, i => cbabc[i]));
			// even
			string aa = "aa";
			Assert.IsTrue(IsPalindrome(0, aa.Length - 1, i => aa[i]));
			string aabbaa = "aabbaa";
			Assert.IsTrue(IsPalindrome(0, aabbaa.Length - 1, i => aabbaa[i]));
			string aabbccbbaa = "aabbccbbaa";
			Assert.IsTrue(IsPalindrome(0, aabbccbbaa.Length - 1, i => aabbccbbaa[i]));
			// false
			string ab = "ab";
			Assert.IsFalse(IsPalindrome(0, ab.Length - 1, i => ab[i]));
			string abc = "abc";
			Assert.IsFalse(IsPalindrome(0, abc.Length - 1, i => abc[i]));
			string abaa = "abaa";
			Assert.IsFalse(IsPalindrome(0, abaa.Length - 1, i => abaa[i]));
			string aaba = "aaba";
			Assert.IsFalse(IsPalindrome(0, aaba.Length - 1, i => aaba[i]));
			string baaa = "baaa";
			Assert.IsFalse(IsPalindrome(0, baaa.Length - 1, i => baaa[i]));
			string aaab = "aaab";
			Assert.IsFalse(IsPalindrome(0, aaab.Length - 1, i => aaab[i]));
			// partials
			string bbb = "bbb";
			Assert.IsTrue(IsPalindrome(1, bbb.Length - 2, i => bbb[i]));
			string babab = "babab";
			Assert.IsTrue(IsPalindrome(1, babab.Length - 2, i => babab[i]));
			string babb = "babb";
			Assert.IsFalse(IsPalindrome(1, babb.Length - 2, i => babb[i]));
			string babbb = "babbb";
			Assert.IsFalse(IsPalindrome(1, babbb.Length - 2, i => babbb[i]));

			// non char-------------

			// odd
			int[] _1 = { 1 };
			Assert.IsTrue(IsPalindrome(0, _1.Length - 1, i => _1[i]));
			int[] _1_1_1 = { 1, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_1.Length - 1, i => _1_1_1[i]));
			int[] _2_1_2 = { 2, 1, 2 };
			Assert.IsTrue(IsPalindrome(0, _2_1_2.Length - 1, i => _2_1_2[i]));
			int[] _1_2_1_2_1 = { 1, 2, 1, 2, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_2_1_2_1.Length - 1, i => _1_2_1_2_1[i]));
			int[] _3_2_1_2_3 = { 3, 2, 1, 2, 3 };
			Assert.IsTrue(IsPalindrome(0, _3_2_1_2_3.Length - 1, i => _3_2_1_2_3[i]));
			// even
			int[] _1_1 = { 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1.Length - 1, i => _1_1[i]));
			int[] _1_1_2_2_1_1 = { 1, 1, 2, 2, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_2_2_1_1.Length - 1, i => _1_1_2_2_1_1[i]));
			int[] _1_1_2_2_3_3_2_2_1_1 = { 1, 1, 2, 2, 3, 3, 2, 2, 1, 1 };
			Assert.IsTrue(IsPalindrome(0, _1_1_2_2_3_3_2_2_1_1.Length - 1, i => _1_1_2_2_3_3_2_2_1_1[i]));
			// false
			int[] _1_2 = { 1, 2 };
			Assert.IsFalse(IsPalindrome(0, _1_2.Length - 1, i => _1_2[i]));
			int[] _1_2_3 = { 1, 2, 3 };
			Assert.IsFalse(IsPalindrome(0, _1_2_3.Length - 1, i => _1_2_3[i]));
			int[] _1_2_1_1 = { 1, 2, 1, 1 };
			Assert.IsFalse(IsPalindrome(0, _1_2_1_1.Length - 1, i => _1_2_1_1[i]));
			int[] _1_1_2_1 = { 1, 1, 2, 1 };
			Assert.IsFalse(IsPalindrome(0, _1_1_2_1.Length - 1, i => _1_1_2_1[i]));
			int[] _2_1_1_1 = { 2, 1, 1, 1 };
			Assert.IsFalse(IsPalindrome(0, _2_1_1_1.Length - 1, i => _2_1_1_1[i]));
			int[] _1_1_1_2 = { 1, 1, 1, 2 };
			Assert.IsFalse(IsPalindrome(0, _1_1_1_2.Length - 1, i => _1_1_1_2[i]));
			// partials
			int[] _2_2_2 = { 2, 2, 2 };
			Assert.IsTrue(IsPalindrome(1, _2_2_2.Length - 2, i => _2_2_2[i]));
			int[] _2_1_2_1_2 = { 2, 1, 2, 1, 2 };
			Assert.IsTrue(IsPalindrome(1, _2_1_2_1_2.Length - 2, i => _2_1_2_1_2[i]));
			int[] _2_1_2_2 = { 2, 1, 2, 2 };
			Assert.IsFalse(IsPalindrome(1, _2_1_2_2.Length - 2, i => _2_1_2_2[i]));
			int[] _2_1_2_2_2 = { 2, 1, 2, 2, 2 };
			Assert.IsFalse(IsPalindrome(1, _2_1_2_2_2.Length - 2, i => _2_1_2_2_2[i]));
		}

		#endregion

		#region IsInterleaved

		[TestMethod]
		public void IsInterleavedRecursive_Testing()
		{
			Assert.IsTrue(IsInterleavedRecursive("a", "z", "az"));
			Assert.IsTrue(IsInterleavedRecursive("ab", "yz", "aybz"));
			Assert.IsTrue(IsInterleavedRecursive("abc", "xyz", "axbycz"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "awbxcydz"));

			Assert.IsTrue(IsInterleavedRecursive("a", "z", "za"));
			Assert.IsTrue(IsInterleavedRecursive("ab", "yz", "yazb"));
			Assert.IsTrue(IsInterleavedRecursive("abc", "xyz", "xaybzc"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "waxbyczd"));

			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "aazz"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "azaz"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "zaza"));
			Assert.IsTrue(IsInterleavedRecursive("aa", "zz", "zzaa"));

			Assert.IsTrue(IsInterleavedRecursive("", "", ""));

			Assert.IsFalse(IsInterleavedRecursive("a", "", ""));
			Assert.IsFalse(IsInterleavedRecursive("", "a", ""));
			Assert.IsFalse(IsInterleavedRecursive("", "", "a"));
			Assert.IsFalse(IsInterleavedRecursive("a", "a", ""));
			Assert.IsFalse(IsInterleavedRecursive("a", "a", "aaa"));
		}

		[TestMethod]
		public void IsInterleavedIterative_Testing()
		{
			Assert.IsTrue(IsInterleavedIterative("a", "z", "az"));
			Assert.IsTrue(IsInterleavedIterative("ab", "yz", "aybz"));
			Assert.IsTrue(IsInterleavedIterative("abc", "xyz", "axbycz"));
			Assert.IsTrue(IsInterleavedRecursive("abcd", "wxyz", "awbxcydz"));

			Assert.IsTrue(IsInterleavedIterative("a", "z", "za"));
			Assert.IsTrue(IsInterleavedIterative("ab", "yz", "yazb"));
			Assert.IsTrue(IsInterleavedIterative("abc", "xyz", "xaybzc"));
			Assert.IsTrue(IsInterleavedIterative("abcd", "wxyz", "waxbyczd"));

			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "aazz"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "azaz"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "zaza"));
			Assert.IsTrue(IsInterleavedIterative("aa", "zz", "zzaa"));

			Assert.IsTrue(IsInterleavedIterative("", "", ""));

			Assert.IsFalse(IsInterleavedIterative("a", "", ""));
			Assert.IsFalse(IsInterleavedIterative("", "a", ""));
			Assert.IsFalse(IsInterleavedIterative("", "", "a"));
			Assert.IsFalse(IsInterleavedIterative("a", "a", ""));
			Assert.IsFalse(IsInterleavedIterative("a", "a", "aaa"));
		}

		#endregion

		#region IsReorderOf

		[TestMethod]
		public void IsReorderOf_Testing()
		{
			Assert.IsTrue(IsReorderOf<char>("a", "a"));
			Assert.IsTrue(IsReorderOf<char>("ab", "ba"));
			Assert.IsTrue(IsReorderOf<char>("abc", "cba"));

			Assert.IsTrue(IsReorderOf<char>("aab", "baa"));
			Assert.IsTrue(IsReorderOf<char>("aab", "aba"));
			Assert.IsTrue(IsReorderOf<char>("aab", "aab"));

			Assert.IsTrue(IsReorderOf<char>("aabb", "bbaa"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "abab"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "abba"));
			Assert.IsTrue(IsReorderOf<char>("aabb", "aabb"));

			Assert.IsFalse(IsReorderOf<char>("a", "b"));
			Assert.IsFalse(IsReorderOf<char>("aa", "bb"));
			Assert.IsFalse(IsReorderOf<char>("ab", "aa"));
			Assert.IsFalse(IsReorderOf<char>("ab", "bb"));

			Assert.IsFalse(IsReorderOf<char>("aa", "a"));
			Assert.IsFalse(IsReorderOf<char>("a", "aa"));
			Assert.IsFalse(IsReorderOf<char>("ab", "aab"));
			Assert.IsFalse(IsReorderOf<char>("aab", "ab"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aaabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabbbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabbccc"));
			Assert.IsFalse(IsReorderOf<char>("aaabbcc", "aabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbbcc", "aabbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbccc", "aabbcc"));

			Assert.IsFalse(IsReorderOf<char>("aabb", "aaab"));
			Assert.IsFalse(IsReorderOf<char>("bbaa", "aaab"));

			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aaabcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "abbbcc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "aabccc"));
			Assert.IsFalse(IsReorderOf<char>("aabbcc", "abbccc"));

			Assert.IsFalse(IsReorderOf<char>("a", ""));
			Assert.IsFalse(IsReorderOf<char>("", "a"));
			Assert.IsFalse(IsReorderOf<char>(null, "a"));
			Assert.IsFalse(IsReorderOf<char>("a", null));

			Assert.IsTrue(IsReorderOf<char>(null, null));
			Assert.IsTrue(IsReorderOf<char>("", ""));
			Assert.IsTrue(IsReorderOf<char>(null, ""));
			Assert.IsTrue(IsReorderOf<char>("", null));
		}

		#endregion

		#region SetEquals

		[TestMethod]
		public void SetEquals_Testing()
		{
			Assert.IsTrue(EquateSet<char>("a", "a"));
			Assert.IsTrue(EquateSet<char>("ab", "ba"));
			Assert.IsTrue(EquateSet<char>("abc", "cba"));

			Assert.IsTrue(EquateSet<char>("aab", "baa"));
			Assert.IsTrue(EquateSet<char>("aab", "aba"));
			Assert.IsTrue(EquateSet<char>("aab", "aab"));

			Assert.IsTrue(EquateSet<char>("aabb", "bbaa"));
			Assert.IsTrue(EquateSet<char>("aabb", "abab"));
			Assert.IsTrue(EquateSet<char>("aabb", "abba"));
			Assert.IsTrue(EquateSet<char>("aabb", "aabb"));

			Assert.IsFalse(EquateSet<char>("a", "b"));
			Assert.IsFalse(EquateSet<char>("aa", "bb"));
			Assert.IsFalse(EquateSet<char>("ab", "aa"));
			Assert.IsFalse(EquateSet<char>("ab", "bb"));

			Assert.IsTrue(EquateSet<char>("aa", "a"));
			Assert.IsTrue(EquateSet<char>("a", "aa"));
			Assert.IsTrue(EquateSet<char>("ab", "aab"));
			Assert.IsTrue(EquateSet<char>("aab", "ab"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "aaabbcc"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "aabbbcc"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "aabbccc"));
			Assert.IsTrue(EquateSet<char>("aaabbcc", "aabbcc"));
			Assert.IsTrue(EquateSet<char>("aabbbcc", "aabbcc"));
			Assert.IsTrue(EquateSet<char>("aabbccc", "aabbcc"));

			Assert.IsTrue(EquateSet<char>("aabb", "aaab"));
			Assert.IsTrue(EquateSet<char>("bbaa", "aaab"));

			Assert.IsTrue(EquateSet<char>("aabbcc", "aaabcc"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "abbbcc"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "aabccc"));
			Assert.IsTrue(EquateSet<char>("aabbcc", "abbccc"));

			Assert.IsFalse(EquateSet<char>("a", ""));
			Assert.IsFalse(EquateSet<char>("", "a"));
			Assert.IsFalse(EquateSet<char>(null, "a"));
			Assert.IsFalse(EquateSet<char>("a", null));

			Assert.IsTrue(EquateSet<char>(null, null));
			Assert.IsTrue(EquateSet<char>("", ""));
			Assert.IsTrue(EquateSet<char>(null, ""));
			Assert.IsTrue(EquateSet<char>("", null));

			Assert.IsTrue(EquateSet<int>(Array.Empty<int>(), Array.Empty<int>()));
			Assert.IsTrue(EquateSet<int>(new[] { 1 }, new[] { 1 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2 }, new[] { 1, 2 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2 }, new[] { 2, 1 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 3, 2, 1 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 2, 1, 3 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 2, 3, 1 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 1, 3, 2 }));
			Assert.IsTrue(EquateSet<int>(new[] { 1, 2, 3 }, new[] { 3, 1, 2 }));
		}

		#endregion

		#region ContainsDuplicates

		[TestMethod]
		public void ContainsDuplicates_Testing()
		{
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 0 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { 0, 1 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1, 0 }));
			Assert.IsFalse(ContainsDuplicates<int>(stackalloc int[] { -1, 0, 1 }));

			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 0, 0 }));
			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 0, 1, 0 }));
			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 1, 0, 1 }));
			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 0, 1, 1 }));
			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 1, 1, 0 }));
			Assert.IsTrue(ContainsDuplicates<int>(stackalloc int[] { 1, 1, 1 }));
		}

		#endregion

		#region Contains

		[TestMethod]
		public void Contains_Testing()
		{
			{
				Span<int> span = stackalloc int[] { };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(stackalloc int[] { }, 0));
				Assert.IsFalse(Contains(stackalloc int[] { }, 1));
			}
			{
				Span<int> span = stackalloc int[] { 1, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsFalse(Contains(span, 2));
			}
			{
				Span<int> span = stackalloc int[] { 1, 2, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsTrue(Contains(span, 2));
				Assert.IsFalse(Contains(span, 3));
			}
			{
				Span<int> span = stackalloc int[] { 1, 2, 3, };
				Assert.IsFalse(Contains(span, -1));
				Assert.IsFalse(Contains(span, 0));
				Assert.IsTrue(Contains(span, 1));
				Assert.IsTrue(Contains(span, 2));
				Assert.IsTrue(Contains(span, 3));
				Assert.IsFalse(Contains(span, 4));
			}
		}

		#endregion

		#region Any

		[TestMethod]
		public void Any_Testing()
		{
			Assert.IsFalse(Any(stackalloc int[] { }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { }, i => false));

			Assert.IsTrue(Any(stackalloc int[] { 0 }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { 0 }, i => false));
			Assert.IsTrue(Any(stackalloc int[] { 0 }, i => i is 0));

			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => true));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => false));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => i is -1));
			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => i is 0));
			Assert.IsTrue(Any(stackalloc int[] { 0, 1 }, i => i is 1));
			Assert.IsFalse(Any(stackalloc int[] { 0, 1 }, i => i is 2));

			Assert.ThrowsException<ArgumentNullException>(() => Any(stackalloc int[] { }, default!));
		}

		#endregion

		#region CombineRanges

		[TestMethod]
		public void CombineRanges_Testing()
		{
			{
				Assert.IsTrue(EquateSet<(int, int)>(Array.Empty<(int, int)>(), Array.Empty<(int, int)>()));
			}
			{
				(int, int)[] a = new[]
				{
					(1,   5),
					(4,   7),
					(15, 18),
					(3,  10),
				};
				(int, int)[] b = new[]
				{
					(1,  10),
					(15, 18),
				};
				Assert.IsTrue(EquateSet<(int, int)>(CombineRanges(a).ToArray(), b));
			}
			{
				(DateTime, DateTime)[] a =
				{
					(new DateTime(2000, 1, 1), new DateTime(2002, 1, 1)),
					(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2003, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2011, 1, 1), new DateTime(2016, 1, 1)),
				};
				(DateTime, DateTime)[] b =
				{
					(new DateTime(2000, 1, 1), new DateTime(2009, 1, 1)),
					(new DateTime(2011, 1, 1), new DateTime(2016, 1, 1)),
				};
				Assert.IsTrue(EquateSet<(DateTime, DateTime)>(CombineRanges(a).ToArray(), b));
			}
			{
				(string, string)[] a = new[]
				{
					("tux", "zebra"),
					("a",   "hippo"),
					("boy", "joust"),
					("car", "dog"),
				};
				(string, string)[] b = new[]
				{
					("a", "joust"),
					("tux", "zebra"),
				};
				Assert.IsTrue(EquateSet<(string, string)>(CombineRanges(a).ToArray(), b));
			}
		}

		#endregion

		#region IsOrdered

		[TestMethod]
		public void IsOrdered_Testing()
		{
			{
				Assert.IsTrue(IsOrdered<int>(Ɐ<int>()));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 2)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 2, 3)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 1)));
				Assert.IsTrue(IsOrdered<int>(Ɐ(1, 1, 2)));

				Assert.IsFalse(IsOrdered<int>(Ɐ(2, 1)));
				Assert.IsFalse(IsOrdered<int>(Ɐ(3, 1, 2)));
				Assert.IsFalse(IsOrdered<int>(Ɐ(1, 3, 2)));

				Assert.IsTrue(IsOrdered<char>(Ɐ<char>()));
				Assert.IsTrue(IsOrdered<char>("a"));
				Assert.IsTrue(IsOrdered<char>("ab"));
				Assert.IsTrue(IsOrdered<char>("abc"));
				Assert.IsTrue(IsOrdered<char>("aa"));
				Assert.IsTrue(IsOrdered<char>("aab"));

				Assert.IsFalse(IsOrdered<char>("ba"));
				Assert.IsFalse(IsOrdered<char>("cab"));
				Assert.IsFalse(IsOrdered<char>("acb"));
			}
			{
				Assert.IsTrue(Ɐ<int>().IsOrdered());
				Assert.IsTrue(Ɐ(1).IsOrdered());
				Assert.IsTrue(Ɐ(1, 2).IsOrdered());
				Assert.IsTrue(Ɐ(1, 2, 3).IsOrdered());
				Assert.IsTrue(Ɐ(1, 1).IsOrdered());
				Assert.IsTrue(Ɐ(1, 1, 2).IsOrdered());

				Assert.IsFalse(Ɐ(2, 1).IsOrdered());
				Assert.IsFalse(Ɐ(3, 1, 2).IsOrdered());
				Assert.IsFalse(Ɐ(1, 3, 2).IsOrdered());

				Assert.IsTrue(Ɐ<char>().IsOrdered());
				Assert.IsTrue("a".IsOrdered());
				Assert.IsTrue("ab".IsOrdered());
				Assert.IsTrue("abc".IsOrdered());
				Assert.IsTrue("aa".IsOrdered());
				Assert.IsTrue("aab".IsOrdered());

				Assert.IsFalse("ba".IsOrdered());
				Assert.IsFalse("cab".IsOrdered());
				Assert.IsFalse("acb".IsOrdered());
			}
			{
				int[] ints0 = Array.Empty<int>();
				Assert.IsTrue(IsOrdered(0, ints0.Length - 1, i => ints0[i]));
				int[] ints1 = { 1 };
				Assert.IsTrue(IsOrdered(0, ints1.Length - 1, i => ints1[i]));
				int[] ints2 = { 1, 2 };
				Assert.IsTrue(IsOrdered(0, ints2.Length - 1, i => ints2[i]));
				int[] ints3 = { 1, 2, 3 };
				Assert.IsTrue(IsOrdered(0, ints3.Length - 1, i => ints3[i]));

				int[] ints4 = { 2, 1 };
				Assert.IsFalse(IsOrdered(0, ints4.Length - 1, i => ints4[i]));
				int[] ints5 = { 3, 1, 2 };
				Assert.IsFalse(IsOrdered(0, ints5.Length - 1, i => ints5[i]));
				int[] ints6 = { 1, 3, 2 };
				Assert.IsFalse(IsOrdered(0, ints6.Length - 1, i => ints6[i]));

				string s0 = "";
				Assert.IsTrue(IsOrdered(0, s0.Length - 1, i => s0[i]));
				string s1 = "a";
				Assert.IsTrue(IsOrdered(0, ints1.Length - 1, i => s1[i]));
				string s2 = "ab";
				Assert.IsTrue(IsOrdered(0, s2.Length - 1, i => s2[i]));
				string s3 = "abc";
				Assert.IsTrue(IsOrdered(0, s3.Length - 1, i => s3[i]));

				string s4 = "ba";
				Assert.IsFalse(IsOrdered(0, s4.Length - 1, i => s4[i]));
				string s5 = "cab";
				Assert.IsFalse(IsOrdered(0, s5.Length - 1, i => s5[i]));
				string s6 = "acb";
				Assert.IsFalse(IsOrdered(0, s6.Length - 1, i => s6[i]));
			}
		}

		#endregion

		#region FilterOrdered

		[TestMethod]
		public void FilterOrdered_Testing()
		{
			Assert.IsTrue(EquateSequence<int>(Ɐ<int>().FilterOrdered().ToArray(), Ɐ<int>()));
			Assert.IsTrue(EquateSequence<int>(Ɐ(1).FilterOrdered().ToArray(), Ɐ(1)));
			Assert.IsTrue(EquateSequence<int>(Ɐ(1, 2).FilterOrdered().ToArray(), Ɐ(1, 2)));
			Assert.IsTrue(EquateSequence<int>(Ɐ(2, 1).FilterOrdered().ToArray(), Ɐ(2)));
			Assert.IsTrue(EquateSequence<int>(Ɐ(1, 2, 3).FilterOrdered().ToArray(), Ɐ(1, 2, 3)));
			Assert.IsTrue(EquateSequence<int>(Ɐ(1, -1, 2, -2, 3).FilterOrdered().ToArray(), Ɐ(1, 2, 3)));
		}

		#endregion

		#region GetGreatest

		[TestMethod]
		public void GetGreatest_IEnumerable_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetGreatest(Ɐ<int>(), 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest(Ɐ(1), 2));

			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 2), 1), Ɐ(2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(2, 1), 1), Ɐ(2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 2, 3), 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(3, 2, 1), 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 3, 2), 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 1, 1), 2), Ɐ(1, 1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 2, 3), 2), Ɐ(3, 2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(3, 2, 1), 2), Ɐ(3, 2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(1, 2, 3), 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest(Ɐ(3, 2, 1), 3), Ɐ(3, 2, 1)));
		}

		[TestMethod]
		public void GetGreatest_Span_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetGreatest<int, Int32Compare>(stackalloc int[] { }, 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest<int, Int32Compare>(stackalloc int[] { 1 }, 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest<int, Int32Compare>(stackalloc int[] { 1 }, -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetGreatest<int, Int32Compare>(stackalloc int[] { 1 }, 2));

			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 2 }, 1), Ɐ(2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 2, 1 }, 1), Ɐ(2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 3, 2 }, 1), Ɐ(3)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 1, 1 }, 2), Ɐ(1, 1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 2), Ɐ(3, 2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 2), Ɐ(3, 2)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(EquateSet<int>(GetGreatest<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 3), Ɐ(3, 2, 1)));
		}

		#endregion

		#region GetLeast

		[TestMethod]
		public void GetLeast_IEnumerable_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetLeast(Ɐ<int>(), 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast(Ɐ(1), 2));

			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1, 2), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(2, 1), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1, 2, 3), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(3, 2, 1), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(3, 1, 2), 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1, 1, 1), 2), Ɐ(1, 1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1, 2, 3), 2), Ɐ(1, 2)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(3, 2, 1), 2), Ɐ(1, 2)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(1, 2, 3), 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(EquateSet<int>(GetLeast(Ɐ(3, 2, 1), 3), Ɐ(3, 2, 1)));
		}

		[TestMethod]
		public void GetLeast_Span_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => GetLeast<int, Int32Compare>(stackalloc int[] {  }, 1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast<int, Int32Compare>(stackalloc int[] { 1 }, 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast<int, Int32Compare>(stackalloc int[] { 1 }, -1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => GetLeast<int, Int32Compare>(stackalloc int[] { 1 }, 2));

			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1, 2 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 2, 1 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 3, 1, 2 }, 1), Ɐ(1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1, 1, 1 }, 2), Ɐ(1, 1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 2), Ɐ(1, 2)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 2), Ɐ(1, 2)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 1, 2, 3 }, 3), Ɐ(3, 2, 1)));
			Assert.IsTrue(EquateSet<int>(GetLeast<int, Int32Compare>(stackalloc int[] { 3, 2, 1 }, 3), Ɐ(3, 2, 1)));
		}

		#endregion

		#region HammingDistance

		[TestMethod]
		public void HammingDistance_Testing()
		{
			Assert.ThrowsException<ArgumentException>(() => HammingDistance("a", ""));
			Assert.ThrowsException<ArgumentException>(() => HammingDistance("", "a"));

			Assert.IsTrue(HammingDistance("", "") is 0);
			Assert.IsTrue(HammingDistance("a", "a") is 0);
			Assert.IsTrue(HammingDistance("a", "b") is 1);
			Assert.IsTrue(HammingDistance("aa", "aa") is 0);
			Assert.IsTrue(HammingDistance("aa", "ab") is 1);
			Assert.IsTrue(HammingDistance("aa", "bb") is 2);
			Assert.IsTrue(HammingDistance("aaa", "aaa") is 0);
			Assert.IsTrue(HammingDistance("aaa", "aab") is 1);
			Assert.IsTrue(HammingDistance("aaa", "aba") is 1);
			Assert.IsTrue(HammingDistance("aaa", "baa") is 1);
			Assert.IsTrue(HammingDistance("aaa", "bab") is 2);
			Assert.IsTrue(HammingDistance("aaa", "bba") is 2);
			Assert.IsTrue(HammingDistance("aaa", "bbb") is 3);
		}

		#endregion
	}
}
