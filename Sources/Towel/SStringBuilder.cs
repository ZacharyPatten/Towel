using System;
using System.Text;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	/// <summary>Represents a <see cref="string"/> in the process of being built.</summary>
	public ref struct SStringBuilder
	{
		internal SpanBuilder<char> _spanBuilder;
		internal StringBuilder? _stringBuilder;

		/// <summary>Constructs a new <see cref="SpanBuilder{T}"/>.</summary>
		/// <param name="span">The <see cref="Span{T}"/> to initialize.</param>
		public SStringBuilder(Span<char> span)
		{
			_spanBuilder = span;
			_stringBuilder = null;
		}

		/// <summary>Appends a <see cref="char"/> to this <see cref="SpanBuilder{T}"/>.</summary>
		/// <param name="value">The <see cref="char"/> to append to this <see cref="SpanBuilder{T}"/>.</param>
		public void Append(char value)
		{
			if (_stringBuilder is not null)
			{
				_stringBuilder.Append(value);
			}
			else if (_spanBuilder._i + 1 > _spanBuilder._span.Length)
			{
				_stringBuilder = new(_spanBuilder._i + 1);
				_stringBuilder.Append(_spanBuilder);
				_stringBuilder.Append(value);
			}
			else
			{
				_spanBuilder.Append(value);
			}
		}

		/// <summary>Appends a <see cref="char"/> span to this <see cref="SpanBuilder{T}"/>.</summary>
		/// <param name="span">The <see cref="char"/> span to append to this <see cref="SpanBuilder{T}"/>.</param>
		public void Append(ReadOnlySpan<char> span)
		{
			if (_stringBuilder is not null)
			{
				_stringBuilder.Append(span);
			}
			else if (_spanBuilder._i + span.Length > _spanBuilder._span.Length)
			{
				_stringBuilder = new(_spanBuilder._i + span.Length);
				_stringBuilder.Append(_spanBuilder);
				_stringBuilder.Append(span);
			}
			else
			{
				_spanBuilder.Append(span);
			}
		}

		/// <summary>Appends a <see cref="char"/> followed by <see cref="Environment.NewLine"/>.</summary>
		/// <param name="value">The <see cref="char"/> to append.</param>
		public void AppendLine(char value)
		{
			Append(value);
			Append(Environment.NewLine);
		}

		/// <summary>Appends a <see cref="char"/> span followed by <see cref="Environment.NewLine"/>.</summary>
		/// <param name="span">The <see cref="char"/> span to append.</param>
		public void AppendLine(ReadOnlySpan<char> span)
		{
			Append(span);
			Append(Environment.NewLine);
		}

		/// <summary>Appends a <see cref="Environment.NewLine"/>.</summary>
		public void AppendLine()
		{
			Append(Environment.NewLine);
		}

		/// <summary>Converts a <see cref="Span{T}"/> to a <see cref="SpanBuilder{T}"/>.</summary>
		/// <param name="span">The <see cref="Span{T}"/> to convert to a <see cref="SpanBuilder{T}"/>.</param>
		public static implicit operator SStringBuilder(Span<char> span) => new(span);

		/// <summary>Converts a <see cref="SStringBuilder"/> to a <see cref="string"/>.</summary>
		/// <param name="sStringBuilder">The <see cref="SStringBuilder"/> to convert to a <see cref="string"/>.</param>
		public static implicit operator string(SStringBuilder sStringBuilder) =>
			sStringBuilder._stringBuilder is null
				? new(sStringBuilder._spanBuilder)
				: sStringBuilder._stringBuilder.ToString();

		/// <inheritdoc cref="StringBuilder.ToString()"/>
		public override string ToString() => _stringBuilder is null ? new(_spanBuilder) : _stringBuilder.ToString();
	}
}
