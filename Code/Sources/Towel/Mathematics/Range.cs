namespace Towel.Mathematics
{
	/// <summary>Represents a range denoted by a minimum and maximum point.</summary>
	/// <typeparam name="T">The generic type </typeparam>
	public class Range<T>
	{
		private T _min;
		private T _max;
		private bool _inclusiveMin;
		private bool _inclusiveMax;

		#region Properties

		public T Min
		{
			get { return this._min; }
			set
			{
				if (value == null)
					throw new System.ArgumentNullException("value");
				if (!Validate(value, this._max))
					throw new System.ArithmeticException("invalid vectors when setting range minimum !(min <= max)");
				this._min = value;
			}
		}

		public T Max
		{
			get { return this._max; }
			set
			{
				if (value == null)
					throw new System.ArgumentNullException("value");
				if (!Validate(this._min, value))
					throw new System.ArithmeticException("invalid vectors when setting range maximum !(min <= max)");
				this._max = value;
			}
		}

		public T MidPoint
		{
			get
			{
				return Compute.Mean(this._min, this._max);
			}
		}

		#endregion

		#region Constructors

		public Range(T min, T max)
		{
			if (object.ReferenceEquals(null, min))
				throw new System.ArgumentNullException("min");
			if (object.ReferenceEquals(null, max))
				throw new System.ArgumentNullException("max");
			if (Compute.GreaterThan(min, max))
				throw new System.ArithmeticException("invalid vectors during range construction !(min <= max)");
			this._min = min;
			this._max = max;
		}

		internal Range(Range<T> range)
		{
			this._min = range._min;
			this._max = range._max;
		}

        #endregion

        #region Static

        private static bool Validate(T a, T b)
        {
            if (a == null)
                throw new System.ArgumentNullException("a");
            if (b == null)
                throw new System.ArgumentNullException("b");
            if (Compute.LessThan(b, a))
                return false;
            return true;
        }

        public static bool Contains(Range<T> range, T value)
		{
			if (range == null)
				throw new System.ArgumentNullException("range");
			if (value == null)
				throw new System.ArgumentNullException("value");
			if (Compute.LessThan(value, range._min) || Compute.GreaterThan(value, range._max))
				return false;
			return true;
		}

		public static bool Contains(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (Compute.LessThan(b._min, a._min) || Compute.GreaterThan(b._max, a._max))
				return false;
			return true;
		}

		public static bool Overlaps(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (Compute.LessThan(a._max, b._min) || Compute.GreaterThan(a._min, b._max))
				return false;
			return true;
		}

		public static Range<T> Intersect(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (!Range<T>.Overlaps(a, b))
				return null;
			T min;
			T max;
			if (Compute.GreaterThan(a._min, b._min))
				min = a._min;
			else
				min = b._min;
			if (Compute.LessThan(a._max, b._max))
				max = a._max;
			else
				max = b._max;
			return new Range<T>(min, max);
		}

		public static Range<T>[] Union(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (Range<T>.Overlaps(a, b))
			{
				T min;
				T max;
				if (Compute.LessThan(a._min, b._min))
					min = a._min;
				else
					min = b._min;
				if (Compute.GreaterThan(a._max, b._max))
					max = a._max;
				else
					max = b._max;
				return new Range<T>[] { new Range<T>(min, max) };
			}
			else
			{
				return new Range<T>[] { new Range<T>(a), new Range<T>(b) };
			}
		}

		public static Range<T>[] Complement(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (!Range<T>.Overlaps(a, b))
				return new Range<T>[] { new Range<T>(a) };
			if (Compute.Equal(a._min, b._min) && Compute.Equal(a._max, b._max))
				return null;
			T min;
			T max;
			if (Compute.LessThan(a._min, b._min) && Compute.GreaterThan(a._max, b._max))
			{
				return new Range<T>[] { new Range<T>(a._min, b._min), new Range<T>(b._max, a._max) };
			}
			if (Compute.LessThan(a._min, b._min))
			{
				min = a._min;
				max = b._min;
			}
			else
			{
				min = b._max;
				max = a._max;
			}
			return new Range<T>[] { new Range<T>(min, max) };
		}

		public static Range<T>[] Split(Range<T> range, bool inclusiveEdges, params T[] values)
		{
			throw new System.NotImplementedException();
			//T[] ordered = values.Clone() as T[]; // in case they pass in an array they use later
			//Algorithms.Sort<T>.Merge(Compute.Compare, ordered);
			//if 
		}
		
		public new static bool Equals(Range<T> a, Range<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			return object.ReferenceEquals(a, b) || (Compute.Equal(a._min, b._min) && Compute.Equal(a._max, b._max));
		}

		#endregion

		#region Instance

		public bool Contains(T value)
		{ return Range<T>.Contains(this, value); }
		public bool Contains(Range<T> b)
		{ return Range<T>.Contains(this, b); }
		public bool Overlaps(Range<T> b)
		{ return Range<T>.Overlaps(this, b); }
		public Range<T> Intersect(Range<T> b)
		{ return Range<T>.Intersect(this, b); }
		public Range<T>[] Union(Range<T> b)
		{ return Range<T>.Union(this, b); }
		public Range<T>[] Complement(Range<T> b)
		{ return Range<T>.Complement(this, b); }
		public Range<T>[] Split(bool inclusiveEdges, params T[] values)
		{ return Range<T>.Split(this, inclusiveEdges, values); }

		#endregion

		#region Operators
		
		public static bool operator ==(Range<T> a, Range<T> b)
		{ return Equals(a, b); }
		public static bool operator !=(Range<T> a, Range<T> b)
		{ return !Equals(a, b); }
		/// <summary>Complement</summary>
		public static Range<T>[] operator ^(Range<T> a, Range<T> b)
		{ return Range<T>.Complement(a, b); }
		/// <summary>Union</summary>
		public static Range<T>[] operator |(Range<T> a, Range<T> b)
		{ return Range<T>.Union(a, b); }
		/// <summary>Intersection</summary>
		public static Range<T> operator &(Range<T> a, Range<T> b)
		{ return Range<T>.Intersect(a, b); }

		#endregion

		#region Overrides

		public override string ToString()
		{
			return string.Concat(this._min, "->", this._max);
		}

		public override bool Equals(object b)
		{
			if (!(b is Range<T>))
				return false;
			return Range<T>.Equals(this, b as Range<T>);
		}

		public override int GetHashCode()
		{
			return this._min.GetHashCode() ^ this._max.GetHashCode();
		}
		#endregion
	}
}
