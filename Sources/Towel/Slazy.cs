using System;

namespace Towel
{
	/// <inheritdoc cref="Lazy{T}"/>
	public struct SLazy<T>
	{
		internal SLazy<T, SFunc<T>> _lazy;

		/// <summary>True if <see cref="Value"/> has been initialized.</summary>
		public bool IsValueCreated => _lazy._isValueCreated;
		/// <summary>Gets the lazily initialized value.</summary>
		public T Value => _lazy.Value;

		/// <inheritdoc cref="Lazy{T}(Func{T})"/>
		public SLazy(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_lazy = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazy{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		public static implicit operator SLazy<T>(Func<T> func) => new(func);
	}

	/// <inheritdoc cref="Lazy{T}"/>
	/// <typeparam name="TFunc">The type of method used to initialize <see cref="Value"/>.</typeparam>
#pragma warning disable CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
	public struct SLazy<T, TFunc>
#pragma warning restore CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
		where TFunc : struct, IFunc<T>
	{
		internal bool _isValueCreated;
		internal TFunc _func;
		internal T? _value;

		/// <summary>True if <see cref="Value"/> has been initialized.</summary>
		public bool IsValueCreated => _isValueCreated;

		/// <summary>Gets the lazily initialized value.</summary>
		public T Value
		{
			get
			{
				if (!_isValueCreated)
				{
					_value = _func.Invoke();
					_isValueCreated = true;
				}
				return _value!;
			}
		}

		/// <summary>Constructs a new <see cref="SLazy{T, TFunc}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		public SLazy(TFunc func)
		{
			_isValueCreated = false;
			_func = func;
			_value = default;
		}
	}
}
