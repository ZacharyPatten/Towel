namespace Towel;

/// <summary>Represents a <see cref="Span{T}"/> in the process of being initialized.</summary>
/// <typeparam name="T">The type of values in the <see cref="Span{T}"/>.</typeparam>
public ref struct SpanBuilder<T>
{
	internal Span<T> _span;
	internal int _i;

	/// <summary>Constructs a new <see cref="SpanBuilder{T}"/>.</summary>
	/// <param name="span">The <see cref="Span{T}"/> to initialize.</param>
	public SpanBuilder(Span<T> span)
	{
		_span = span;
		_i = 0;
	}

	/// <summary>Appends a <typeparamref name="T"/> to this <see cref="SpanBuilder{T}"/>.</summary>
	/// <param name="value">The <typeparamref name="T"/> to append to this <see cref="SpanBuilder{T}"/>.</param>
	public void Append(T value)
	{
		if (_i >= _span.Length) throw new InvalidOperationException("The span to append was larger than span being appended to could support.");
		AppendInternal(value);
	}

	/// <summary>Appends a <typeparamref name="T"/> span to this <see cref="SpanBuilder{T}"/>.</summary>
	/// <param name="span">The <typeparamref name="T"/> span to append to this <see cref="SpanBuilder{T}"/>.</param>
	public void Append(ReadOnlySpan<T> span)
	{
		if (_i + span.Length > _span.Length) throw new InvalidOperationException("The span to append was larger than span being appended to could support.");
		AppendInternal(span);
	}

	internal void AppendInternal(T value) => _span[_i++] = value;
	internal void AppendInternal(ReadOnlySpan<T> span)
	{
		span.CopyTo(_span[_i..(_i + span.Length)]);
		_i += span.Length;
	}

	/// <summary>Converts a <see cref="SpanBuilder{T}"/> to a <see cref="ReadOnlySpan{T}"/>.</summary>
	/// <param name="spanBuilder">The <see cref="SpanBuilder{T}"/> to convert to a <see cref="ReadOnlySpan{T}"/>.</param>
	public static implicit operator ReadOnlySpan<T>(SpanBuilder<T> spanBuilder) => spanBuilder._span[..spanBuilder._i];

	/// <summary>Converts a <see cref="SpanBuilder{T}"/> to a <see cref="Span{T}"/>.</summary>
	/// <param name="spanBuilder">The <see cref="SpanBuilder{T}"/> to convert to a <see cref="Span{T}"/>.</param>
	public static implicit operator Span<T>(SpanBuilder<T> spanBuilder) => spanBuilder._span[..spanBuilder._i];

	/// <summary>Converts a <see cref="Span{T}"/> to a <see cref="SpanBuilder{T}"/>.</summary>
	/// <param name="span">The <see cref="Span{T}"/> to convert to a <see cref="SpanBuilder{T}"/>.</param>
	public static implicit operator SpanBuilder<T>(Span<T> span) => new(span);
}

/// <summary>Root type of the static functional methods in Towel.</summary>
public static partial class Statics
{
	/// <summary>Appends a <see cref="char"/> followed by <see cref="Environment.NewLine"/> to a <paramref name="spanBuilder"/>.</summary>
	/// <param name="spanBuilder">The <see cref="SpanBuilder{T}"/> to append to.</param>
	/// <param name="value">The <see cref="char"/> to append to the <paramref name="spanBuilder"/>.</param>
	public static void AppendLine(this ref SpanBuilder<char> spanBuilder, char value)
	{
		if (spanBuilder._i + 1 + Environment.NewLine.Length > spanBuilder._span.Length) throw new InvalidOperationException("The span to append was larger than span being appended to could support.");
		spanBuilder.AppendInternal(value);
		spanBuilder.AppendInternal(Environment.NewLine);
	}

	/// <summary>Appends a <see cref="char"/> span followed by <see cref="Environment.NewLine"/> to a <paramref name="spanBuilder"/>.</summary>
	/// <param name="spanBuilder">The <see cref="SpanBuilder{T}"/> to append to.</param>
	/// <param name="span">The <see cref="char"/> span to append to the <paramref name="spanBuilder"/>.</param>
	public static void AppendLine(this ref SpanBuilder<char> spanBuilder, ReadOnlySpan<char> span)
	{
		if (spanBuilder._i + span.Length + Environment.NewLine.Length > spanBuilder._span.Length) throw new InvalidOperationException("The span to append was larger than span being appended to could support.");
		spanBuilder.AppendInternal(span);
		spanBuilder.AppendInternal(Environment.NewLine);
	}

	/// <summary>Appends a <see cref="Environment.NewLine"/> to a <paramref name="spanBuilder"/>.</summary>
	/// <param name="spanBuilder">The <see cref="SpanBuilder{T}"/> to append to.</param>
	public static void AppendLine(this ref SpanBuilder<char> spanBuilder)
	{
		spanBuilder.Append(Environment.NewLine);
	}
}
