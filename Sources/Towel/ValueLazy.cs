using System;

namespace Towel
{
	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <summary>True if <see cref="Value"/> has been initialized.</summary>
		public bool IsValueCreated => _func is null;

		/// <summary>Gets the lazily initialized value.</summary>
		public T Value => _func is null ? _value! : SafeGetValue();

		internal T SafeGetValue()
		{
			Func<T>? func = _func;
			if (func is not null)
			{
				lock (func)
				{
					if (_func is not null)
					{
						try
						{
							_value = _func();
							_func = null;
						}
						catch (Exception exception)
						{
							_func = () => throw exception;
							throw;
						}
					}
				}
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazy{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazy(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazy{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazy(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazy{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazy<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazy{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazy<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazy<T> slazy)
			{
				obj = slazy.Value;
			}
			return (Value, obj) switch
			{
				(null, null) => true,
				(_, null) => false,
				(null, _) => false,
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
