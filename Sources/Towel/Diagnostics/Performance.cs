using System;

namespace Towel.Diagnostics
{
	public static class Performance
	{
		public static TimeSpan Time_DateTimNow(Action action)
		{
			DateTime a = DateTime.Now;
			action();
			DateTime b = DateTime.Now;
			return b - a;
		}

		public static TimeSpan Time_StopWatch(Action action)
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Restart();
			action();
			watch.Stop();
			return watch.Elapsed;
		}
	}
}
