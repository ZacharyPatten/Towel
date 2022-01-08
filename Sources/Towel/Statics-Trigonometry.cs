namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Sine

		/// <summary>Computes the sine ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed sine ratio of the provided angle.</returns>
		public static T SineTaylorSeries<T>(Angle<T> a, Predicate<T>? predicate = null)
		{
			// Series: sine(x) = x - (x^3 / 3!) + (x^5 / 5!) - (x^7 / 7!) + (x^9 / 9!) + ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T sine = x;
			T previous;
			bool isAddTerm = false;
			T i = Constant<T>.Three;
			T xSquared = Multiplication(x, x);
			T xRunningPower = x;
			T xRunningFactorial = Constant<T>.One;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = sine;
				if (isAddTerm)
				{
					sine = Addition(sine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					sine = Subtraction(sine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequate(sine, previous) && (predicate is null || !predicate(sine)));
			return sine;
		}

		/// <summary>Computes the sine ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the sine ratio of.</param>
		/// <returns>The sine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T SineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Sin(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the sine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated sine ratio of.</param>
		/// <returns>The quadratic estimation of the sine ratio of the provided angle.</returns>
		public static T SineQuadratic<T>(Angle<T> a)
		{
			// Piecewise Functions:
			// y = (-4/π^2)(x - (π/2))^2 + 1
			// y = (4/π^2)(x - (3π/2))^2 - 1

			T adjusted = Remainder(a[Angle.Units.Radians], Constant<T>.Pi2);
			if (IsNegative(adjusted))
			{
				adjusted = Addition(adjusted, Constant<T>.Pi2);
			}
			if (LessThan(adjusted, Constant<T>.Pi))
			{
				T xMinusPiOver2 = Subtraction(adjusted, Constant<T>.PiOver2);
				T xMinusPiOver2Squared = Multiplication(xMinusPiOver2, xMinusPiOver2);
				return Addition(Multiplication(Constant<T>.Negative4OverPiSquared, xMinusPiOver2Squared), Constant<T>.One);
			}
			else
			{
				T xMinus3PiOver2 = Subtraction(adjusted, Constant<T>.Pi3Over2);
				T xMinus3PiOver2Squared = Multiplication(xMinus3PiOver2, xMinus3PiOver2);
				return Subtraction(Multiplication(Constant<T>.FourOverPiSquared, xMinus3PiOver2Squared), Constant<T>.One);
			}
		}

		#endregion

		#region Cosine

		/// <summary>Computes the cosine ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <param name="predicate">Determines if coputation should continue or is accurate enough.</param>
		/// <returns>The taylor series computed cosine ratio of the provided angle.</returns>
		public static T CosineTaylorSeries<T>(Angle<T> a, Predicate<T>? predicate = null)
		{
			// Series: cosine(x) = 1 - (x^2 / 2!) + (x^4 / 4!) - (x^6 / 6!) + (x^8 / 8!) - ...
			// more terms in computation inproves accuracy

			// Note: there is room for optimization (custom runtime compilation)

			T x = a[Angle.Units.Radians];
			T cosine = Constant<T>.One;
			T previous;
			T xSquared = Multiplication(x, x);
			T xRunningPower = Constant<T>.One;
			T xRunningFactorial = Constant<T>.One;
			bool isAddTerm = false;
			T i = Constant<T>.Two;
			do
			{
				xRunningPower = Multiplication(xRunningPower, xSquared);
				xRunningFactorial = Multiplication(xRunningFactorial, Multiplication(i, Subtraction(i, Constant<T>.One)));
				previous = cosine;
				if (isAddTerm)
				{
					cosine = Addition(cosine, Division(xRunningPower, xRunningFactorial));
				}
				else
				{
					cosine = Subtraction(cosine, Division(xRunningPower, xRunningFactorial));
				}
				isAddTerm = !isAddTerm;
				i = Addition(i, Constant<T>.Two);
			} while (Inequate(cosine, previous) && (predicate is null || !predicate(cosine)));
			return cosine;
		}

		/// <summary>Computes the cosine ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosine ratio of.</param>
		/// <returns>The cosine ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CosineSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Cos(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the cosine ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cosine ratio of.</param>
		/// <returns>The quadratic estimation of the cosine ratio of the provided angle.</returns>
		public static T CosineQuadratic<T>(Angle<T> a)
		{
			Angle<T> piOver2Radians = new(Constant<T>.PiOver2, Angle.Units.Radians);
			return SineQuadratic(a - piOver2Radians);
		}

		#endregion

		#region Tangent

		/// <summary>Computes the tangent ratio of an angle using the relative talor series. Accurate but slow.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The taylor series computed tangent ratio of the provided angle.</returns>
		public static T TangentTaylorSeries<T>(Angle<T> a)
		{
			return Division(SineTaylorSeries(a), CosineTaylorSeries(a));
		}

		/// <summary>Computes the tangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the tangent ratio of.</param>
		/// <returns>The tangent ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T TangentSystem<T>(Angle<T> a)
		{
			T b = a[Angle.Units.Radians];
			double c = Convert<T, double>(b);
			double d = Math.Tan(c);
			T e = Convert<double, T>(d);
			return e;
		}

		/// <summary>Estimates the tangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated tangent ratio of.</param>
		/// <returns>The quadratic estimation of the tangent ratio of the provided angle.</returns>
		public static T TangentQuadratic<T>(Angle<T> a)
		{
			return Division(SineQuadratic(a), CosineQuadratic(a));
		}

		#endregion

		#region Cosecant

		/// <summary>Computes the cosecant ratio of an angle using the system's sine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cosecant ratio of.</param>
		/// <returns>The cosecant ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CosecantSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, SineSystem(a));
		}

		/// <summary>Estimates the cosecant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cosecant ratio of.</param>
		/// <returns>The quadratic estimation of the cosecant ratio of the provided angle.</returns>
		public static T CosecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, SineQuadratic(a));
		}

		#endregion

		#region Secant

		/// <summary>Computes the secant ratio of an angle using the system's cosine function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the secant ratio of.</param>
		/// <returns>The secant ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T SecantSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, CosineSystem(a));
		}

		/// <summary>Estimates the secant ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated secant ratio of.</param>
		/// <returns>The quadratic estimation of the secant ratio of the provided angle.</returns>
		public static T SecantQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, CosineQuadratic(a));
		}

		#endregion

		#region Cotangent

		/// <summary>Computes the cotangent ratio of an angle using the system's tangent function. WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the cotangent ratio of.</param>
		/// <returns>The cotangent ratio of the provided angle.</returns>
		/// <remarks>WARNING! CONVERSION TO/FROM DOUBLE (possible loss of significant figures).</remarks>
		public static T CotangentSystem<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, TangentSystem(a));
		}

		/// <summary>Estimates the cotangent ratio using piecewise quadratic equations. Fast but NOT very accurate.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="a">The angle to compute the quadratic estimated cotangent ratio of.</param>
		/// <returns>The quadratic estimation of the cotangent ratio of the provided angle.</returns>
		public static T CotangentQuadratic<T>(Angle<T> a)
		{
			return Division(Constant<T>.One, TangentQuadratic(a));
		}

		#endregion

		#region InverseSine

		// public static Angle<T> InverseSine<T>(T a)
		// {
		//     return InverseSineImplementation<T>.Function(a);
		// }

		// internal static class InverseSineImplementation<T>
		// {
		//     internal static Func<T, Angle<T>> Function = a =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Asin)), A));
		//             Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region InverseCosine

		// public static Angle<T> InverseCosine<T>(T a)
		// {
		//     return InverseCosineImplementation<T>.Function(a);
		// }

		// internal static class InverseCosineImplementation<T>
		// {
		//     internal static Func<T, Angle<T>> Function = a =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Acos)), A));
		//             Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region InverseTangent

		// public static Angle<T> InverseTangent<T>(T a)
		// {
		//     return InverseTangentImplementation<T>.Function(a);
		// }

		// internal static class InverseTangentImplementation<T>
		// {
		//     internal static Func<T, Angle<T>> Function = a =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Angle<T>).GetMethod(nameof(Angle<T>.Factory_Radians), BindingFlags.Static), Expression.Call(typeof(Math).GetMethod(nameof(Math.Atan)), A));
		//             Function = Expression.Lambda<Func<T, Angle<T>>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region InverseCosecant

		// public static Angle<T> InverseCosecant<T>(T a)
		// {
		//     return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseSine(a).Radians));
		// }

		#endregion

		#region InverseSecant

		// public static Angle<T> InverseSecant<T>(T a)
		// {
		//     return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseCosine(a).Radians));
		// }

		#endregion

		#region InverseCotangent

		// public static Angle<T> InverseCotangent<T>(T a)
		// {
		//     return Angle<T>.Factory_Radians(Divide(Constant<T>.One, InverseTangent(a).Radians));
		// }

		#endregion

		#region HyperbolicSine

		// public static T HyperbolicSine<T>(Angle<T> a)
		// {
		//     return HyperbolicSineImplementation<T>.Function(a);
		// }

		// internal static class HyperbolicSineImplementation<T>
		// {
		//     internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Sinh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//             Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region HyperbolicCosine

		// public static T HyperbolicCosine<T>(Angle<T> a)
		// {
		//     return HyperbolicCosineImplementation<T>.Function(a);
		// }

		// internal static class HyperbolicCosineImplementation<T>
		// {
		//     internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Cosh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//             Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region HyperbolicTangent

		// public static T HyperbolicTangent<T>(Angle<T> a)
		// {
		//     return HyperbolicTangentImplementation<T>.Function(a);
		// }

		// internal static class HyperbolicTangentImplementation<T>
		// {
		//     internal static Func<Angle<T>, T> Function = (Angle<T> a) =>
		//     {
		//         // optimization for specific known types
		//         if (TypeDescriptor.GetConverter(typeof(T)).CanConvertTo(typeof(double)))
		//         {
		//             ParameterExpression A = Expression.Parameter(typeof(T));
		//             Expression BODY = Expression.Call(typeof(Math).GetMethod(nameof(Math.Tanh)), Expression.Convert(Expression.Property(A, typeof(Angle<T>).GetProperty(nameof(a.Radians))), typeof(double)));
		//             Function = Expression.Lambda<Func<Angle<T>, T>>(BODY, A).Compile();
		//             return Function(a);
		//         }
		//         throw new NotImplementedException();
		//     };
		// }

		#endregion

		#region HyperbolicCosecant

		// public static T HyperbolicCosecant<T>(Angle<T> a)
		// {
		//     return Divide(Constant<T>.One, HyperbolicSine(a));
		// }

		#endregion

		#region HyperbolicSecant

		// public static T HyperbolicSecant<T>(Angle<T> a)
		// {
		//     return Divide(Constant<T>.One, HyperbolicCosine(a));
		// }

		#endregion

		#region HyperbolicCotangent

		// public static T HyperbolicCotangent<T>(Angle<T> a)
		// {
		//     return Divide(Constant<T>.One, HyperbolicTangent(a));
		// }

		#endregion

		#region InverseHyperbolicSine

		// public static Angle<T> InverseHyperbolicSine<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion

		#region InverseHyperbolicCosine

		// public static Angle<T> InverseHyperbolicCosine<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion

		#region InverseHyperbolicTangent

		// public static Angle<T> InverseHyperbolicTangent<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion

		#region InverseHyperbolicCosecant

		// public static Angle<T> InverseHyperbolicCosecant<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion

		#region InverseHyperbolicSecant

		// public static Angle<T> InverseHyperbolicSecant<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion

		#region InverseHyperbolicCotangent

		// public static Angle<T> InverseHyperbolicCotangent<T>(T a)
		// {
		//     throw new NotImplementedException();
		// }

		#endregion
	}
}
