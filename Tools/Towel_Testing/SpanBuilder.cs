namespace Towel_Testing;

[TestClass]
public class SpanBuilder_Testing
{
	[TestMethod]
	public void Testing()
	{
		{
			SpanBuilder<char> span = Span<char>.Empty;
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.IsEmpty);
			Assert.IsTrue(span._span.Length is 0);
			try
			{
				span.Append('a');
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[1];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 1);
			span.Append('a');
			Assert.IsTrue(EquateSequence<char>(span, "a"));
			Assert.IsTrue(span._i is 1);
			Assert.IsTrue(span._span.Length is 1);
			try
			{
				span.Append('b');
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[3];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 3);
			span.Append('a');
			Assert.IsTrue(span._i is 1);
			Assert.IsTrue(span._span.Length is 3);
			Assert.IsTrue(EquateSequence<char>(span, "a"));
			span.Append('b');
			Assert.IsTrue(span._i is 2);
			Assert.IsTrue(span._span.Length is 3);
			Assert.IsTrue(EquateSequence<char>(span, "ab"));
			span.Append('c');
			Assert.IsTrue(span._i is 3);
			Assert.IsTrue(span._span.Length is 3);
			Assert.IsTrue(EquateSequence<char>(span, "abc"));
			try
			{
				span.Append('d');
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[3];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 3);
			span.Append("abc");
			Assert.IsTrue(span._i is 3);
			Assert.IsTrue(span._span.Length is 3);
			Assert.IsTrue(EquateSequence<char>(span, "abc"));
			try
			{
				span.Append('d');
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[10];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 10);
			span.AppendLine('a');
			Assert.IsTrue(span._i == 1 + Environment.NewLine.Length);
			Assert.IsTrue(span._span.Length is 10);
			Assert.IsTrue(EquateSequence<char>(span, "a" + Environment.NewLine));
			span.AppendLine('b');
			Assert.IsTrue(span._i == 2 + Environment.NewLine.Length * 2);
			Assert.IsTrue(span._span.Length is 10);
			Assert.IsTrue(EquateSequence<char>(span, "a" + Environment.NewLine + "b" + Environment.NewLine));
		}
		{
			SpanBuilder<char> span = stackalloc char[20];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 20);
			span.AppendLine("ab");
			Assert.IsTrue(span._i == 2 + Environment.NewLine.Length);
			Assert.IsTrue(span._span.Length is 20);
			Assert.IsTrue(EquateSequence<char>(span, "ab" + Environment.NewLine));
			span.AppendLine("cd");
			Assert.IsTrue(span._i == 4 + Environment.NewLine.Length * 2);
			Assert.IsTrue(span._span.Length is 20);
			Assert.IsTrue(EquateSequence<char>(span, "ab" + Environment.NewLine + "cd" + Environment.NewLine));
		}
		{
			SpanBuilder<char> span = stackalloc char[10];
			Assert.IsTrue(span._i is 0);
			Assert.IsTrue(span._span.Length is 10);
			span.AppendLine();
			Assert.IsTrue(span._i == Environment.NewLine.Length);
			Assert.IsTrue(span._span.Length is 10);
			Assert.IsTrue(EquateSequence<char>(span, Environment.NewLine));
		}
		{
			SpanBuilder<char> span = stackalloc char[1];
			try
			{
				span.AppendLine('a');
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[3];
			try
			{
				span.AppendLine("abc");
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[1];
			span.Append('a');
			try
			{
				span.AppendLine();
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
		{
			SpanBuilder<char> span = stackalloc char[3];
			span.Append("abc");
			try
			{
				span.AppendLine();
				Assert.Fail();
			}
			catch (Exception exception)
			{
				Assert.IsTrue(exception is InvalidOperationException);
			}
		}
	}
}
