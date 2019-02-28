using Towel.Mathematics;

namespace Towel.Measurements
{
	/// <summary>
	/// Contains the definitions for the various units for measuring angles.
	/// </summary>
	public static class Angle
	{
		#region Units

		public enum Units
		{
            Degrees,
			Gradians,
			Radians,
			Turns,
		}

		/// <summary>
		/// Determines unit priority. When doing math operations of values of different units, it
		/// will convert values to the highest priority unit type present in the operation. Priority
		/// is in NON-INCREASING ORDER.
		/// </summary>
		internal static int UnitOrder(Units units)
		{
			switch (units)
			{
                case Units.Degrees:
                    return 0;
				case Units.Gradians:
					return 1;
				case Units.Radians:
					return 2;
				case Units.Turns:
					return 3;
				default:
					throw new System.NotImplementedException();
			}
		}

		#endregion

		#region Unit Conversions

		internal static class Constants<T>
		{
			internal static readonly T DegreesToTurnsFactor = Compute.Divide(Compute.FromInt32<T>(1), Compute.FromInt32<T>(360));
			internal static readonly T DegreesToRadiansFactor = Compute.Divide(Constant<T>.Pi, Compute.FromInt32<T>(180));
			internal static readonly T DegreesToGradiansFactor = Compute.Divide(Compute.FromInt32<T>(10), Compute.FromInt32<T>(9));

			internal static readonly T GradiansToDegreesFactor = Compute.Divide(Compute.FromInt32<T>(9), Compute.FromInt32<T>(10));
			internal static readonly T GradiansToTurnsFactor = Compute.Divide(Compute.FromInt32<T>(1), Compute.FromInt32<T>(400));
			internal static readonly T GradiansToRadiansFactor = Compute.Divide(Constant<T>.Pi, Compute.FromInt32<T>(200));

			internal static readonly T RadiansToTurnsFactor = Compute.Invert(Compute.Multiply(Compute.FromInt32<T>(2), Constant<T>.Pi));
			internal static readonly T RadiansToDegreesFactor = Compute.Divide(Compute.FromInt32<T>(180), Constant<T>.Pi);
			internal static readonly T RadiansToGradiansFactor = Compute.Divide(Compute.FromInt32<T>(200), Constant<T>.Pi);

			internal static readonly T TurnsToDegreesFactor = Compute.FromInt32<T>(360);
			internal static readonly T TurnsToRadiansFactor = Compute.Divide(Constant<T>.Pi, Compute.FromInt32<T>(180));
			internal static readonly T TurnsToGradiansFactor = Compute.FromInt32<T>(400);
		}


		/// <summary>Converts a degrees measurement to radians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T DegreesToRadians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.DegreesToRadiansFactor); }
		/// <summary>Converts a degrees measurement to turns.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T DegreesToTurns<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.DegreesToTurnsFactor); }
		/// <summary>Converts a degrees measurement to gradians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T DegreesToGradians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.DegreesToGradiansFactor); }


		/// <summary>Converts a gradians measurement to degrees.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T GradiansToDegrees<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.GradiansToDegreesFactor); }
		/// <summary>Converts a gradians measurement to radians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T GradiansToRadians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.GradiansToRadiansFactor); }
		/// <summary>Converts a gradians measurement to turns.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T GradiansToTurns<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.GradiansToTurnsFactor); }


		/// <summary>Converts a radians measurement to degrees.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T RadiansToDegrees<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.RadiansToDegreesFactor); }
		/// <summary>Converts a radians measurement to turns.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T RadiansToTurns<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.RadiansToTurnsFactor); }
		/// <summary>Converts a radians measurement to gradians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T RadiansToGradians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.RadiansToGradiansFactor); }


		/// <summary>Converts a turns measurement to degrees.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T TurnsToDegrees<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.TurnsToDegreesFactor); }
		/// <summary>Converts a turns measurement to radians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T TurnsToRadians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.TurnsToRadiansFactor); }
		/// <summary>Converts a turns measurement to gradians.</summary>
		/// <typeparam name="T">The numeric type.</typeparam>
		/// <param name="measurement">The measurement to convert.</param>
		/// <returns>The converted measurement.</returns>
        public static T TurnsToGradians<T>(T measurement) { return Compute.Multiply(measurement, Constants<T>.TurnsToGradiansFactor); }

		#endregion
	}

	/// <summary>A measurement of an angle.</summary>
	/// <typeparam name="T">The generic numeric type used to store the angle measurement.</typeparam>
	public struct Angle<T>
	{
		internal T _measurement;
		internal Angle.Units _units;

		#region Constructors

		public Angle(T measurement, Angle.Units units)
		{
			this._measurement = measurement;
			this._units = units;
		}

		private Angle(Angle<T> angle)
		{
			this._measurement = angle._measurement;
			this._units = angle._units;
		}

		#endregion

		#region Factory Methods

		/// <summary>Creates an angle from a degree measurement.</summary>
		/// <param name="degrees">The degree measurement of the angle.</param>
		/// <returns>The angle of the specified measurement.</returns>
		public static Angle<T> Factory_Degrees(T degrees)
		{
			return new Angle<T>(degrees, Angle.Units.Degrees);
		}

		/// <summary>Creates an angle from a radian measurement.</summary>
		/// <param name="radians">The radian measurement of the angle.</param>
		/// <returns>The angle of the specified measurement.</returns>
		public static Angle<T> Factory_Radians(T radians)
		{
			return new Angle<T>(radians, Angle.Units.Radians);
		}

		/// <summary>Creates an angle from a turn measurement.</summary>
		/// <param name="degrees">The turn measurement of the angle.</param>
		/// <returns>The angle of the specified measurement.</returns>
		public static Angle<T> Factory_Turns(T turns)
		{
			return new Angle<T>(turns, Angle.Units.Turns);
		}

		/// <summary>Creates an angle from a gradian measurement.</summary>
		/// <param name="degrees">The gradian measurement of the angle.</param>
		/// <returns>The angle of the specified measurement.</returns>
		public static Angle<T> Factory_Gradians(T gradians)
		{
			return new Angle<T>(gradians, Angle.Units.Gradians);
		}

		#endregion

		#region Properties

		/// <summary>The current units used to represent the angle.</summary>
		public Angle.Units Unit
		{
			get { return this._units; }
			set
			{
				if (value != this._units)
				{
					this._measurement = this[value]._measurement;
					this._units = value;
				}
			}
		}

		/// <summary>Gets the measurement in the desired units.</summary>
		/// <param name="units">The units you want the measurement to be in.</param>
		/// <returns>The measurement in the specified units.</returns>
		private Angle<T> this[Angle.Units units]
		{
			get
			{
				switch (units)
				{
					case Angle.Units.Degrees:
						switch (this._units)
						{
							case Angle.Units.Degrees:
								return new Angle<T>(this._measurement, this._units);
							case Angle.Units.Gradians:
								return new Angle<T>(Angle.GradiansToDegrees(this._measurement), Angle.Units.Degrees);
							case Angle.Units.Radians:
								return new Angle<T>(Angle.RadiansToDegrees(this._measurement), Angle.Units.Degrees);
							case Angle.Units.Turns:
								return new Angle<T>(Angle.TurnsToDegrees(this._measurement), Angle.Units.Degrees);
							default:
								throw new System.NotImplementedException();
						}
					case Angle.Units.Gradians:
						switch (this._units)
						{
							case Angle.Units.Degrees:
								return new Angle<T>(Angle.DegreesToGradians(this._measurement), Angle.Units.Gradians);
							case Angle.Units.Gradians:
								return new Angle<T>(this._measurement, this._units);
							case Angle.Units.Radians:
								return new Angle<T>(Angle.RadiansToGradians(this._measurement), Angle.Units.Gradians);
							case Angle.Units.Turns:
								return new Angle<T>(Angle.TurnsToGradians(this._measurement), Angle.Units.Gradians);
							default:
								throw new System.NotImplementedException();
						}
					case Angle.Units.Radians:
						switch (this._units)
						{
							case Angle.Units.Degrees:
								return new Angle<T>(Angle.DegreesToRadians(this._measurement), Angle.Units.Radians);
							case Angle.Units.Gradians:
								return new Angle<T>(Angle.GradiansToRadians(this._measurement), Angle.Units.Radians);
							case Angle.Units.Radians:
								return new Angle<T>(this._measurement, this._units);
							case Angle.Units.Turns:
								return new Angle<T>(Angle.TurnsToRadians(this._measurement), Angle.Units.Radians);
							default:
								throw new System.NotImplementedException();
						}
					case Angle.Units.Turns:
						switch (this._units)
						{
							case Angle.Units.Degrees:
								return new Angle<T>(Angle.DegreesToTurns(this._measurement), Angle.Units.Turns);
							case Angle.Units.Gradians:
								return new Angle<T>(Angle.GradiansToTurns(this._measurement), Angle.Units.Turns);
							case Angle.Units.Radians:
								return new Angle<T>(Angle.RadiansToTurns(this._measurement), Angle.Units.Turns);
							case Angle.Units.Turns:
								return new Angle<T>(this._measurement, this._units);
							default:
								throw new System.NotImplementedException();
						}
					default:
						throw new System.NotImplementedException();
				}
			}
		}

		public T Degrees { get { return this[Angle.Units.Degrees]._measurement; } }

		public T Radians { get { return this[Angle.Units.Radians]._measurement; } }

		public T Turns { get { return this[Angle.Units.Turns]._measurement; } }

		public T Gradians { get { return this[Angle.Units.Gradians]._measurement; } }

		#endregion

		#region Operators

		public static Angle<T> operator +(Angle<T> left, Angle<T> right) { return Angle<T>.Add(left, right); }
		public static Angle<T> operator -(Angle<T> left, Angle<T> right) { return Angle<T>.Subtract(left, right); }
		public static Angle<T> operator /(Angle<T> angle, T constant) { return Angle<T>.Divide(angle, constant); }
		public static Angle<T> operator *(Angle<T> angle, T constant) { return Angle<T>.Multiply(angle, constant); }
		public static Angle<T> operator *(T constant, Angle<T> angle) { return Angle<T>.Multiply(constant, angle); }
		public static bool operator <(Angle<T> left, Angle<T> right) { return Angle<T>.LessThan(left, right); }
		public static bool operator >(Angle<T> left, Angle<T> right) { return Angle<T>.GreaterThan(left, right); }
		public static bool operator <=(Angle<T> left, Angle<T> right) { return Angle<T>.LessThanOrEqual(left, right); }
		public static bool operator >=(Angle<T> left, Angle<T> right) { return Angle<T>.GreaterThanOrEqual(left, right); }
		public static bool operator ==(Angle<T> left, Angle<T> right) { return Angle<T>.Equal(left, right); }
		public static bool operator !=(Angle<T> left, Angle<T> right) { return Angle<T>.EqualNot(left, right); }

		#endregion

		#region Static Math

		public static Angle<T> Add(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return new Angle<T>(Compute.Add(left._measurement, right._measurement), left._units);
		}

		public static Angle<T> Subtract(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return new Angle<T>(Compute.Subtract(left._measurement, right._measurement), left._units);
		}

		public static Angle<T> Divide(Angle<T> angle, T constant)
		{
			return new Angle<T>(Compute.Divide(angle._measurement, constant), angle._units);
		}

		public static Angle<T> Multiply(Angle<T> angle, T constant)
		{
			return new Angle<T>(Compute.Multiply(angle._measurement, constant), angle._units);
		}

		public static Angle<T> Multiply(T constant, Angle<T> angle)
		{
			return new Angle<T>(Compute.Multiply(angle._measurement, constant), angle._units);
		}

		public static bool LessThan(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.LessThan(left._measurement, right._measurement);
		}

		public static bool GreaterThan(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.GreaterThan(left._measurement, right._measurement);
		}

		public static bool LessThanOrEqual(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.LessThanOrEqual(left._measurement, right._measurement);
		}

		public static bool GreaterThanOrEqual(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.GreaterThanOrEqual(left._measurement, right._measurement);
		}

		public static bool Equal(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.Equal(left._measurement, right._measurement);
		}

		public static bool EqualNot(Angle<T> left, Angle<T> right)
		{
			GetLikeUnits(left, right, out left, out right);
			return Compute.NotEqual(left._measurement, right._measurement);
		}

		#endregion

		#region Helpers

		/// <summary>Gets both values to be in the same units.</summary>
		internal static void GetLikeUnits(Angle<T> left, Angle<T> right, out Angle<T> left_prime, out Angle<T> right_prime)
		{
			if (left._units == right._units) // no unit conversion required
			{
				left_prime = left;
				right_prime = right;
			}
			else // requires unit conversion
			{
				Angle.Units units = GetPriorityUnits(left, right); // convert the units to the smallest in hopes of not losing accuracy
				left_prime = left[units];
				right_prime = right[units];
			}
		}

		/// <summary>Gets the smallest unit between two operands.</summary>
		internal static Angle.Units GetPriorityUnits(Angle<T> left, Angle<T> right)
		{
			if (Angle.UnitOrder(left._units) < Angle.UnitOrder(right._units))
				return left._units;
			else
				return right._units;
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			switch (this._units)
			{
				case Angle.Units.Degrees:
					return this._measurement.ToString() + "°";
				case Angle.Units.Gradians:
					return this._measurement.ToString() + "ᵍ";
				case Angle.Units.Radians:
					return this._measurement.ToString() + "rad";
				case Angle.Units.Turns:
					return this._measurement.ToString() + "turn";
				default:
					throw new System.NotImplementedException();
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is Angle<T>)
				return this == ((Angle<T>)obj);
			return false;
		}

		public override int GetHashCode()
		{
			return this._measurement.GetHashCode() ^ this._units.GetHashCode();
		}

		#endregion
	}
}
