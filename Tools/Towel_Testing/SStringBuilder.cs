using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
				SStringBuilder sb = Span<char>.Empty;
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append('a');
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "a"));
			}
			{
				SStringBuilder sb = stackalloc char[3];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append('a');
				sb.Append('b');
				sb.Append('c');
				Assert.IsTrue(EquateSequence<char>((string)sb, "abc"));
				sb.Append('d');
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "abcd"));
			}
			{
				SStringBuilder sb = stackalloc char[3];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append("abc");
				Assert.IsTrue(EquateSequence<char>((string)sb, "abc"));
				sb.Append('d');
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "abcd"));
			}
			{
				SStringBuilder sb = stackalloc char[3];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append("abcd");
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "abcd"));
			}
			{
				SStringBuilder sb = stackalloc char[1];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.AppendLine('a');
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "a" + Environment.NewLine));
			}
			{
				SStringBuilder sb = stackalloc char[3];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.AppendLine("abc");
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "abc" + Environment.NewLine));
			}
			{
				SStringBuilder sb = stackalloc char[1];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append("abc");
				sb.Append("def");
				Assert.IsTrue(sb._stringBuilder is not null);
				Assert.IsTrue(EquateSequence<char>((string)sb, "abcdef"));
			}
			{
				SStringBuilder sb = stackalloc char[1];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append('a');
				sb.Append('b');
				sb.Append('c');
				Assert.IsTrue(EquateSequence<char>(sb.ToString(), "abc"));
			}
			{
				SStringBuilder sb = stackalloc char[10];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append("abc");
				Assert.IsTrue(EquateSequence<char>(sb.ToString(), "abc"));
			}
			{
				SStringBuilder sb = stackalloc char[1];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.Append("abc");
				sb.Append("def");
				Assert.IsTrue(EquateSequence<char>(sb.ToString(), "abcdef"));
			}
			{
				SStringBuilder sb = stackalloc char[10];
				Assert.IsTrue(sb._stringBuilder is null);
				sb.AppendLine();
				Assert.IsTrue(EquateSequence<char>(sb.ToString(), Environment.NewLine));
			}
		}
	}
}
