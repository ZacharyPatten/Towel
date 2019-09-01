using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Measurements
{
	/// <summary>Static class with methods regarding measurements.</summary>
	public static class Measurement
	{
		/// <summary>Interface for unit conversion.</summary>
		/// <typeparam name="UNITSTYPE">The unit type of the interface.</typeparam>
		public interface IUnits<UNITSTYPE>
		{
			/// <summary>Converts the units of measurement of a value.</summary>
			/// <typeparam name="T">The generic type of the value to convert.</typeparam>
			/// <param name="value">The value to be converted.</param>
			/// <param name="from">The current units of the value.</param>
			/// <param name="to">The desired units of the value.</param>
			/// <returns>The value converted into the desired units.</returns>
			T Convert<T>(T value, UNITSTYPE from, UNITSTYPE to);
		}

		/// <summary>Converts the units of measurement of a value.</summary>
		/// <typeparam name="T">The generic type of the value to convert.</typeparam>
		/// <typeparam name="UNITSTYPE">The type of units to be converted.</typeparam>
		/// <param name="value">The value to be converted.</param>
		/// <param name="from">The current units of the value.</param>
		/// <param name="to">The desired units of the value.</param>
		/// <returns>The value converted into the desired units.</returns>
		public static T Convert<T, UNITSTYPE>(T value, UNITSTYPE from, UNITSTYPE to)
			where UNITSTYPE : IUnits<UNITSTYPE>
		{
			return from.Convert(value, from, to);
		}
	}
}
