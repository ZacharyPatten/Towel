using System;
using System.Threading;

namespace Towel
{
    /// <summary>Contains static methods for running threads in the background.</summary>
    public static class ParallelThread
    {
        /// <summary>A delegate to be run in parallel on a seperate thread.</summary>
        public delegate void Operation();

        /// <summary>A delegate to be run in parallel on a seperate thread with callback capabilities.</summary>
        /// <param name="report">The delegate to report back to the thread that created the current thread.</param>
        public delegate void OperationReport(Callback report);

        /// <summary>A delegate for sending a message back to the thread that created the current thread.</summary>
        public delegate void Callback();

        /// <summary>A delegate for resolving a multi-threaded event.</summary>
        public delegate void Resolve(IAsyncResult result);

        /// <summary>Runs a delegate in a seperate thread.</summary>
        /// <param name="run">The delegate to run in the background.</param>
        public static IAsyncResult Run(
            Operation run)
        {
            if (run is null)
            {
                throw new ArgumentNullException(nameof(run));
            }

            return run.BeginInvoke(
                asyncResult => { },
                null);
        }

        /// <summary>Runs a delegate in a seperate thread.</summary>
        /// <param name="run">The delegate to run in the background.</param>
        /// <param name="resolve">The delegate for handling the completion of the background thread.</param>
        public static IAsyncResult Run(
            Operation run,
            Resolve resolve)
        {
            if (run is null)
            {
                throw new ArgumentNullException(nameof(run));
            }
            if (resolve is null)
            {
                throw new ArgumentNullException(nameof(resolve));
            }

            SynchronizationContext context = SynchronizationContext.Current;

            return run.BeginInvoke(
                asyncResult => context.Post(x => resolve(asyncResult), null),
                null);
        }

        /// <summary>Runs a delegate in a seperate thread.</summary>
        /// <param name="run">The delegate to run in the background with reporting.</param>
        /// <param name="report">The delegate for reporting</param>
        public static IAsyncResult Run(
            OperationReport run,
            Callback report)
        {
            if (run is null)
            {
                throw new ArgumentNullException(nameof(run));
            }
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            SynchronizationContext context = SynchronizationContext.Current;

            return run.BeginInvoke(
                () =>
                {
                    context.Post((object state) => { report(); }, null);
                },
                asyncResult => { },
                null);
        }

        /// <summary>Runs a delegate in a seperate thread.</summary>
        /// <param name="run">The delegate to run in the background with reporting.</param>
        /// <param name="report">The delegate for reporting</param>
        /// <param name="resolve">The delegate for handling the completion of the background thread.</param>
        public static IAsyncResult Run(
            OperationReport run,
            Callback report,
            Resolve resolve)
        {
            if (run is null)
            {
                throw new ArgumentNullException(nameof(run));
            }
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }
            if (resolve is null)
            {
                throw new ArgumentNullException(nameof(resolve));
            }

            SynchronizationContext context = SynchronizationContext.Current;

            return run.BeginInvoke(
                () => context.Post(x => report(), null),
                asyncResult => context.Post(x => resolve(asyncResult), null),
                null);
        }
    }
}
