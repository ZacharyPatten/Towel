using System.Threading;

namespace Towel
{
	#region ILazy<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public interface ILazy<T>
	{
		/// <summary>The pattern of thread safety this lazy is using.</summary>
		LazyThreadSafetyMode ThreadSafety { get; }
		/// <summary>Gets the lazily initialized value.</summary>
		T Value { get; }
		/// <summary>True if <see cref="Value"/> has been initialized.</summary>
		bool IsValueCreated { get; }
		/// <summary>True if exceptions thrown by the factory delegate are being cached.</summary>
		bool IsCachingExceptions { get; }
		/// <summary>True if the lazy is safe from struct copies.</summary>
		bool IsStructCopySafe { get; }
	}

	#endregion

	#region SLazyReference<T>

	internal class SLazyReference<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		internal SLazyReference(Func<T> func) => _func = func;

		internal T GetValueSafe()
		{
			lock (this)
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
			return _value!;
		}

		internal T GetValuePublicationLock()
		{
			Func<T>? func = _func;
			if (func is not null)
			{
				try
				{
					T value = func();
					lock (this)
					{
						if (_func is not null)
						{
							if (ReferenceEquals(_func, func))
							{
								_value = value;
								_func = null;
							}
							else
							{
								_ = _func();
							}
						}
					}
				}
				catch (Exception exception)
				{
					lock (this)
					{
						if (_func is not null)
						{
							if (ReferenceEquals(_func, func))
							{
								_func = () => throw exception;
								throw;
							}
							else
							{
								_ = _func();
							}
						}
					}
				}
			}
			return _value!;
		}

		internal T GetValuePublicationLockNoCatch()
		{
			Func<T>? func = _func;
			if (func is not null)
			{
				T value = func();
				lock (this)
				{
					if (_func is not null)
					{
						_value = value;
						_func = null;
					}
				}
			}
			return _value!;
		}

		internal T GetValueNoLock()
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
			return _value!;
		}

		internal T GetValueNoCatch()
		{
			lock (this)
			{
				if (_func is not null)
				{
					_value = _func();
					_func = null;
				}
			}
			return _value!;
		}

		internal T GetValueNoLockNoCatch()
		{
			if (_func is not null)
			{
				_value = _func();
				_func = null;
			}
			return _value!;
		}
	}

	#endregion

	#region SLazy<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazy<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValueSafe();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.ExecutionAndPublication;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

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
			_value = default;
			_reference = new(func);
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazy<T> left, SLazy<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazy<T> left, SLazy<T> right) => !(left == right);
	}

	#endregion

	#region SLazyNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazyNoCatch<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValueNoCatch();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.ExecutionAndPublication;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazyNoCatch(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazyNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_reference = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazyNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazyNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazyNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazyNoCatch<T> left, SLazyNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazyNoCatch<T> left, SLazyNoCatch<T> right) => !(left == right);
	}

	#endregion

	#region SLazyNoLock<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazyNoLock<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValueNoLock();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.None;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

		/// <summary>Constructs a new <see cref="SLazyNoLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazyNoLock(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazyNoLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazyNoLock(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_reference = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazyNoLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazyNoLock<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazyNoLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazyNoLock<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazyNoLock<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazyNoLock<T> left, SLazyNoLock<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazyNoLock<T> left, SLazyNoLock<T> right) => !(left == right);
	}

	#endregion

	#region SLazyNoLockNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazyNoLockNoCatch<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValueNoLockNoCatch();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.None;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

		/// <summary>Constructs a new <see cref="SLazyNoLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazyNoLockNoCatch(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazyNoLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazyNoLockNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_reference = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazyNoLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazyNoLockNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazyNoLockNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazyNoLockNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazyNoLockNoCatch<T> left, SLazyNoLockNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazyNoLockNoCatch<T> left, SLazyNoLockNoCatch<T> right) => !(left == right);
	}

	#endregion

	#region SLazyPublicationLock<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazyPublicationLock<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValuePublicationLock();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.PublicationOnly;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

		/// <summary>Constructs a new <see cref="SLazyPublicationLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazyPublicationLock(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazyPublicationLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazyPublicationLock(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_reference = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazyPublicationLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazyPublicationLock<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazyPublicationLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazyPublicationLock<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazyPublicationLock<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazyPublicationLock<T> left, SLazyPublicationLock<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazyPublicationLock<T> left, SLazyPublicationLock<T> right) => !(left == right);
	}

	#endregion

	#region SLazyPublicationLockNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct SLazyPublicationLockNoCatch<T> : ILazy<T>
	{
		internal SLazyReference<T>? _reference;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated
		{
			get
			{
				SLazyReference<T>? reference = _reference;
				if (reference is not null)
				{
					if (reference._func is null)
					{
						_value = reference._value;
						_reference = null;
						return true;
					}
					return false;
				}
				return true;
			}
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				if (_reference is not null)
				{
					_value = _reference.GetValuePublicationLockNoCatch();
					_reference = null;
				}
				return _value!;
			}
		}

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.PublicationOnly;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => true;

		/// <summary>Constructs a new <see cref="SLazyPublicationLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public SLazyPublicationLockNoCatch(T value)
		{
			_reference = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="SLazyPublicationLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public SLazyPublicationLockNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_reference = new(func);
		}

		/// <summary>Constructs a new <see cref="SLazyPublicationLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator SLazyPublicationLockNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="SLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator SLazyPublicationLockNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is SLazyPublicationLockNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(SLazyPublicationLockNoCatch<T> left, SLazyPublicationLockNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(SLazyPublicationLockNoCatch<T> left, SLazyPublicationLockNoCatch<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazy<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazy<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValueSafe();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.ExecutionAndPublication;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValueSafe()
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazy<T> left, ValueLazy<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazy<T> left, ValueLazy<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazyNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazyNoCatch<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValueNoCatch();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.ExecutionAndPublication;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValueNoCatch()
		{
			Func<T>? func = _func;
			if (func is not null)
			{
				lock (func)
				{
					if (_func is not null)
					{
						_value = _func();
						_func = null;
					}
				}
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazyNoCatch(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazyNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazyNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazyNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazyNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazyNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazyNoCatch<T> left, ValueLazyNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazyNoCatch<T> left, ValueLazyNoCatch<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazyNoLock<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazyNoLock<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValueNoLock();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.None;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValueNoLock()
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
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazyNoLock(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazyNoLock(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazyNoLock<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazyNoLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazyNoLock<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazyNoLock<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazyNoLock<T> left, ValueLazyNoLock<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazyNoLock<T> left, ValueLazyNoLock<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazyNoLockNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazyNoLockNoCatch<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValueNoLockNoCatch();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.None;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValueNoLockNoCatch()
		{
			if (_func is not null)
			{
				_value = _func();
				_func = null;
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazyNoLockNoCatch(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazyNoLockNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazyNoLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazyNoLockNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazyNoLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazyNoLockNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazyNoLockNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazyNoLockNoCatch<T> left, ValueLazyNoLockNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazyNoLockNoCatch<T> left, ValueLazyNoLockNoCatch<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazyPublicationLock<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazyPublicationLock<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValuePublicationLock();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.PublicationOnly;

		/// <inheritdoc />
		public bool IsCachingExceptions => true;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValuePublicationLock()
		{
			Func<T>? func = _func;
			if (func is not null)
			{
				try
				{
					T value = func();
					lock (func)
					{
						if (_func is not null)
						{
							if (ReferenceEquals(_func, func))
							{
								_value = value;
								_func = null;
							}
							else
							{
								_ = _func();
							}
						}
					}
				}
				catch (Exception exception)
				{
					lock (func)
					{
						if (_func is not null)
						{
							if (ReferenceEquals(_func, func))
							{
								_func = () => throw exception;
								throw;
							}
							else
							{
								_ = _func();
							}
						}
					}
				}
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazyPublicationLock(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazyPublicationLock(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLock{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazyPublicationLock<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLock{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazyPublicationLock<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazyPublicationLock<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazyPublicationLock<T> left, ValueLazyPublicationLock<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazyPublicationLock<T> left, ValueLazyPublicationLock<T> right) => !(left == right);
	}

	#endregion

	#region ValueLazyPublicationLockNoCatch<T>

	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of value that is being lazily initialized.</typeparam>
	public struct ValueLazyPublicationLockNoCatch<T> : ILazy<T>
	{
		internal Func<T>? _func;
		internal T? _value;

		/// <inheritdoc />
		public bool IsValueCreated => _func is null;

		/// <inheritdoc />
		public T Value => _func is null ? _value! : GetValuePublicationLockNoCatch();

		/// <inheritdoc />
		public LazyThreadSafetyMode ThreadSafety => LazyThreadSafetyMode.PublicationOnly;

		/// <inheritdoc />
		public bool IsCachingExceptions => false;

		/// <inheritdoc />
		public bool IsStructCopySafe => false;

		internal T GetValuePublicationLockNoCatch()
		{

			Func<T>? func = _func;
			if (func is not null)
			{
				T value = func();
				lock (func)
				{
					if (_func is not null)
					{
						_value = value;
						_func = null;
					}
				}
			}
			return _value!;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public ValueLazyPublicationLockNoCatch(T value)
		{
			_func = null;
			_value = value;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public ValueLazyPublicationLockNoCatch(Func<T> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));
			_value = default;
			_func = func;
		}

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLockNoCatch{T}"/> from a <see cref="Func{T}"/>.</summary>
		/// <param name="func">The method used to initialize <see cref="Value"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="func"/> is null.</exception>
		public static implicit operator ValueLazyPublicationLockNoCatch<T>(Func<T> func) => new(func);

		/// <summary>Constructs a new <see cref="ValueLazyPublicationLockNoCatch{T}"/> from a <typeparamref name="T"/>.</summary>
		/// <param name="value">The value to initialize <see cref="Value"/> with.</param>
		public static implicit operator ValueLazyPublicationLockNoCatch<T>(T value) => new(value);

		/// <summary>Checks for equality between <see cref="Value"/> and <paramref name="obj"/>.</summary>
		/// <param name="obj">The value to compare to <see cref="Value"/>.</param>
		/// <returns>True if <see cref="Value"/> and <paramref name="obj"/> are equal or False if not.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is ValueLazyPublicationLockNoCatch<T> slazy)
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

		/// <summary>Checks for equality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the equality check.</param>
		/// <param name="right">The second value of the equality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are equal or False if not.</returns>
		public static bool operator ==(ValueLazyPublicationLockNoCatch<T> left, ValueLazyPublicationLockNoCatch<T> right) => left.Equals(right);

		/// <summary>Checks for inequality between <paramref name="left"/> and <paramref name="right"/>.</summary>
		/// <param name="left">The first value of the inequality check.</param>
		/// <param name="right">The second value of the inequality check.</param>
		/// <returns>True if <paramref name="left"/> and <paramref name="right"/> are not equal or False if not.</returns>
		public static bool operator !=(ValueLazyPublicationLockNoCatch<T> left, ValueLazyPublicationLockNoCatch<T> right) => !(left == right);
	}

	#endregion
}
