using System;

namespace Towel
{
	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazy<T>
	{
		internal class Reference
		{
			internal Func<T>? _func;
			internal T? _value;
			internal Reference(Func<T> func) => _func = func;
		}

		internal Reference? _reference;
		internal T? _value;

		/// <summary>True if <see cref="Value"/> has been initialized.</summary>
		public bool IsValueCreated => _reference is null ? true : GetIsValueCreated();

		/// <summary>Gets the lazily initialized value.</summary>
		public T Value => _reference is null ? _value! : GetValue();

		/// <summary>Constructs a new <see cref="SLazy{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazy(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazy{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazy(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_reference = new(func);
			_value = default;
		}

		internal bool GetIsValueCreated()
		{
			Reference? reference = _reference;
			if (reference is not null)
			{
				if (reference._func is null)
				{
					_value = reference._value;
					_reference = null;
					return true;
				}
			}
			return false;
		}

		internal T GetValue()
		{
			Reference? reference = _reference;
			if (reference is not null)
			{
				if (reference._func is null)
				{
					_value = reference._value;
					_reference = null;
				}
				else
				{
					lock (reference)
					{
						if (_reference is not null)
						{
							if (reference._func is null)
							{
								_value = reference._value;
							}
							else
							{
								try
								{
									T value = reference._func.Invoke();
									reference._func = default;
									reference._value = value;
									_value = value;
								}
								catch (Exception exception)
								{
									reference._func = () => throw exception;
									throw;
								}
							}
							_reference = null;
						}
					}
				}
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="SLazy{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazy<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazy{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazy<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazy<T> slazy)
			{
				obj = slazy.Value;
			}
			return (Value, obj) switch
			{
				(null, null) => true,
				(_,    null) => false,
				(null,    _) => false,
				_ => Value!.Equals(obj),
			};
		}

		/// <summary>Returns a string that represents <see cref="Value"/>.</summary>
		/// <returns>A string that represents <see cref="Value"/></returns>
		public override string? ToString() => Value?.ToString();

		/// <summary>Gets the hash code of <see cref="Value"/>.</summary>
		/// <returns>The hash code of <see cref="Value"/>.</returns>
		public override int GetHashCode() => Value?.GetHashCode() ?? default;
	}
}
