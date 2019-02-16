namespace Towel.Diagnostics
{
	public static class Performance
	{
		public static System.TimeSpan Time_DateTimNow(System.Action action)
		{
			System.DateTime a = System.DateTime.Now;
			action();
			System.DateTime b = System.DateTime.Now;
			return b - a;
		}

		public static System.TimeSpan Time_StopWatch(System.Action action)
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Restart();
			action();
			watch.Stop();
			return watch.Elapsed;
		}
	}
}
