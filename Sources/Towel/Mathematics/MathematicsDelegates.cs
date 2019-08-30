using Towel.Measurements;

namespace Towel.Mathematics
{
	public static class MathematicsDelegates
	{
		/// <summary>Computes the sine ratio of an angle.</summary>
		/// <typeparam name="T">The numeric type of the opertion.</typeparam>
		/// <param name="angle">The angle to compute the sine ratio of.</param>
		/// <returns>The sine ratio of the angle.</returns>
		public delegate T Sine<T>(Angle<T> angle);

		/// <summary>Computes the cosine ratio of an angle.</summary>
		/// <typeparam name="T">The numeric type of the opertion.</typeparam>
		/// <param name="angle">The angle to compute the cosine ratio of.</param>
		/// <returns>The cosine ratio of the angle.</returns>
		public delegate T Cosine<T>(Angle<T> angle);

		/// <summary>Computes the cosine ratio of an angle.</summary>
		/// <typeparam name="T">The numeric type of the opertion.</typeparam>
		/// <param name="angle">The angle to compute the tangent ratio of.</param>
		/// <returns>The cosine ratio of the angle.</returns>
		public delegate T Tangent<T>(Angle<T> angle);

		/// <summary>Computes the angle measurement from a sine ratio.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="ratio">The sine ratio to compute the angle of.</param>
		/// <returns>The angle of the sine ratio.</returns>
		public delegate Angle<T> InverseSine<T>(T ratio);

		/// <summary>Computes the angle measurement from a cosine ratio.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="ratio">The cosine ratio to compute the angle of.</param>
		/// <returns>The angle of the cosine ratio.</returns>
		public delegate Angle<T> InverseCosine<T>(T ratio);

		/// <summary>Computes the angle measurement from a tangent ratio.</summary>
		/// <typeparam name="T">The numeric type of the operation.</typeparam>
		/// <param name="ratio">The tangent ratio to compute the angle of.</param>
		/// <returns>The angle of the tangent ratio.</returns>
		public delegate Angle<T> InverseTangent<T>(T ratio);
	}
}
