using System;
using System.Threading;

namespace Towel.Parallels
{
	/// <summary>Contains static methods for running threads in the background.</summary>
	public static class Parallel
	{
		#region Delegates

		/// <summary>A delegate to be run in parallel on a seperate thread.</summary>
		public delegate void Operation();

		/// <summary>A delegate to be run in parallel on a seperate thread with callback capabilities.</summary>
		/// <param name="report">The delegate to report back to the thread that created the current thread.</param>
		public delegate void Operation_Report(Callback report);

		/// <summary>A delegate for sending a message back to the thread that created the current thread.</summary>
		public delegate void Callback();

		/// <summary>Factory delegate for constructing delegates to be run in parallel of each other.</summary>
		/// <param name="current">The current index out of the max number of delegates to create.</param>
		/// <param name="max">The number of delegates that will be created.</param>
		/// <returns>The "current" delegate to be run in parallel out of "max" delegates.</returns>
		public delegate Operation Operation_Factory(int current, int max);

		/// <summary>A delegate for resolving a multi-threaded event.</summary>
		public delegate void Resolve(IAsyncResult result);

		#endregion

		#region Thread

		/// <summary>Runs a delegate in a seperate thread.</summary>
		/// <param name="run">The delegate to run in the background.</param>
		public static IAsyncResult Thread(
			Operation run)
		{
			if (run == null)
				throw new System.ArgumentNullException("run");

			return run.BeginInvoke(
				(IAsyncResult ar) => { },
				null);
		}

		/// <summary>Runs a delegate in a seperate thread.</summary>
		/// <param name="run">The delegate to run in the background.</param>
		/// <param name="resolve">The delegate for handling the completion of the background thread.</param>
		public static IAsyncResult Thread(
			Operation run,
			Resolve resolve)
		{
			if (run == null)
				throw new System.ArgumentNullException("run");
			if (resolve == null)
				throw new System.ArgumentNullException("resolve");

			SynchronizationContext context = SynchronizationContext.Current;

			return run.BeginInvoke(
				(IAsyncResult ar) => { context.Post((object state) => { resolve(ar); }, null); },
				null);
		}

		/// <summary>Runs a delegate in a seperate thread.</summary>
		/// <param name="run">The delegate to run in the background with reporting.</param>
		/// <param name="report">The delegate for reporting</param>
		public static IAsyncResult Thread(
			Operation_Report run,
			Callback report)
		{
			if (run == null)
				throw new System.ArgumentNullException("run");
			if (report == null)
				throw new System.ArgumentNullException("report");

			SynchronizationContext context = SynchronizationContext.Current;

			return run.BeginInvoke(
				() => 
				{ 
					context.Post((object state) => { report(); }, null); 
				},
				(IAsyncResult ar) => { },
				null);
		}

		/// <summary>Runs a delegate in a seperate thread.</summary>
		/// <param name="run">The delegate to run in the background with reporting.</param>
		/// <param name="report">The delegate for reporting</param>
		/// <param name="resolve">The delegate for handling the completion of the background thread.</param>
		public static IAsyncResult Thread(
			Operation_Report run,
			Callback report,
			Resolve resolve)
		{
			if (run == null)
				throw new System.ArgumentNullException("run");
			if (report == null)
				throw new System.ArgumentNullException("report");
			if (resolve == null)
				throw new System.ArgumentNullException("resolve");

			SynchronizationContext context = SynchronizationContext.Current;

			return run.BeginInvoke(
				() => { context.Post((object state) => { report(); }, null); },
				(IAsyncResult ar) => { context.Post((object state) => { resolve(ar); }, null); },
				null);
		}

		#endregion
	}
}
