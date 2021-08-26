using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	[TestClass]
	public class SStringBuilder_Testing
	{
		[TestMethod]
		public void Testing()
		{
			{
				SStringBuilder span = Span<char>.Empty;
				Assert.IsTrue(span._stringBuilder is null);
				span.Append('a');
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "a"));
			}
			{
				SStringBuilder span = stackalloc char[3];
				Assert.IsTrue(span._stringBuilder is null);
				span.Append('a');
				span.Append('b');
				span.Append('c');
				Assert.IsTrue(EquateSequence<char>((string)span, "abc"));
				span.Append('d');
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "abcd"));
			}
			{
				SStringBuilder span = stackalloc char[3];
				Assert.IsTrue(span._stringBuilder is null);
				span.Append("abc");
				Assert.IsTrue(EquateSequence<char>((string)span, "abc"));
				span.Append('d');
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "abcd"));
			}
			{
				SStringBuilder span = stackalloc char[3];
				Assert.IsTrue(span._stringBuilder is null);
				span.Append("abcd");
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "abcd"));
			}
			{
				SStringBuilder span = stackalloc char[1];
				Assert.IsTrue(span._stringBuilder is null);
				span.AppendLine('a');
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "a" + Environment.NewLine));
			}
			{
				SStringBuilder span = stackalloc char[3];
				Assert.IsTrue(span._stringBuilder is null);
				span.AppendLine("abc");
				Assert.IsTrue(span._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)span, "abc" + Environment.NewLine));
			}
		}
	}
}
