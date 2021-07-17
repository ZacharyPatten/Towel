using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.Measurements;

namespace Towel_Testing.Measurements
{
	[TestClass]
	public class Time_Testing
	{
		#region Millisecond_Second_Testing

		[TestMethod]
		public void Millisecond_Second_Testing()
		{
			{ // int
				Time<int> a = new(1000, Time.Units.Milliseconds);
				Time<int> b = new(1, Time.Units.Seconds);
				Assert.IsTrue(a == b);
			}
			{ // float
				Time<float> a = new(1000f, Time.Units.Milliseconds);
				Time<float> b = new(1f, Time.Units.Seconds);
				Assert.IsTrue(a == b);
			}
			{ // double
				Time<double> a = new(1000d, Time.Units.Milliseconds);
				Time<double> b = new(1d, Time.Units.Seconds);
				Assert.IsTrue(a == b);
			}
			{ // decimal
				Time<decimal> a = new(1000m, Time.Units.Milliseconds);
				Time<decimal> b = new(1m, Time.Units.Seconds);
				Assert.IsTrue(a == b);
			}
		}

		#endregion

		#region Second_Minutes_Testing

		[TestMethod]
		public void Second_Minutes_Testing()
		{
			{ // int
				Time<int> a = new(60, Time.Units.Seconds);
				Time<int> b = new(1, Time.Units.Minutes);
				Assert.IsTrue(a == b);
			}
			{ // float
				Time<float> a = new(60f, Time.Units.Seconds);
				Time<float> b = new(1f, Time.Units.Minutes);
				Assert.IsTrue(a == b);
			}
			{ // double
				Time<double> a = new(60d, Time.Units.Seconds);
				Time<double> b = new(1d, Time.Units.Minutes);
				Assert.IsTrue(a == b);
			}
			{ // decimal
				Time<decimal> a = new(60m, Time.Units.Seconds);
				Time<decimal> b = new(1m, Time.Units.Minutes);
				Assert.IsTrue(a == b);
			}
		}

		#endregion

		#region Minutes_Hours_Testing

		[TestMethod]
		public void Minutes_Hours_Testing()
		{
			{ // int
				Time<int> a = new(60, Time.Units.Minutes);
				Time<int> b = new(1, Time.Units.Hours);
				Assert.IsTrue(a == b);
			}
			{ // float
				Time<float> a = new(60f, Time.Units.Minutes);
				Time<float> b = new(1f, Time.Units.Hours);
				Assert.IsTrue(a == b);
			}
			{ // double
				Time<double> a = new(60d, Time.Units.Minutes);
				Time<double> b = new(1d, Time.Units.Hours);
				Assert.IsTrue(a == b);
			}
			{ // decimal
				Time<decimal> a = new(60m, Time.Units.Minutes);
				Time<decimal> b = new(1m, Time.Units.Hours);
				Assert.IsTrue(a == b);
			}
		}

		#endregion

		#region Hours_Days_Testing

		[TestMethod]
		public void Hours_Days_Testing()
		{
			{ // int
				Time<int> a = new(24, Time.Units.Hours);
				Time<int> b = new(1, Time.Units.Days);
				Assert.IsTrue(a == b);
			}
			{ // float
				Time<float> a = new(24f, Time.Units.Hours);
				Time<float> b = new(1f, Time.Units.Days);
				Assert.IsTrue(a == b);
			}
			{ // double
				Time<double> a = new(24d, Time.Units.Hours);
				Time<double> b = new(1d, Time.Units.Days);
				Assert.IsTrue(a == b);
			}
			{ // decimal
				Time<decimal> a = new(24m, Time.Units.Hours);
				Time<decimal> b = new(1m, Time.Units.Days);
				Assert.IsTrue(a == b);
			}
		}

		#endregion
	}
}
