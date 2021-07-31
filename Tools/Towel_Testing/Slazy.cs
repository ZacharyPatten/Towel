using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	[TestClass]
	public class Slazy_Testing
	{
		[TestMethod]
		public void Test()
		{
			{
				Assert.ThrowsException<ArgumentNullException>(() => new SLazy<int>(default(Func<int>)!));
			}
			{
				SLazy<int> a = 1;
				SLazy<int> b = 1;
				Assert.IsTrue(a.Equals(b));
			}
			{
				SLazy<string> a = "hello world";
				SLazy<string> b = "hello world";
				Assert.IsTrue(a.Equals(b));
			}
			{
				SLazy<string> a = "hello world";
				SLazy<string> b = a;
				Assert.IsTrue(a.Equals(b));
			}
			{
				SLazy<string?> a = default(string);
				SLazy<string?> b = default(string);
				Assert.IsTrue(a.Equals(b));
			}
			{
				SLazy<int> a = 1;
				Assert.IsTrue(a.Equals(a));
			}
			{
				SLazy<string> a = "hello world";
				Assert.IsTrue(a.Equals(a));
			}
			{
				SLazy<string?> a = default(string);
				Assert.IsTrue(a.Equals(a));
			}
			{
				SLazy<object> a = new(() => new());
				Assert.IsTrue(a.Equals(a));
			}
			{
				SLazy<object> a = new(() => new());
				SLazy<object> b = new(() => new());
				Assert.IsFalse(a.Equals(b));
			}
			{
				SLazy<object> a = new(() => new());
				SLazy<object> b = a;
				Assert.IsTrue(a.Equals(b));
			}
			{
				SLazy<int> a = 1;
				Assert.IsTrue(a.Equals(1));
			}
			{
				SLazy<string> a = "hello world";
				Assert.IsTrue(a.Equals("hello world"));
			}
			{
				SLazy<string?> a = default(string);
				Assert.IsTrue(a.Equals(null));
			}
			{
				int value = 1;
				bool ready = false;
				bool thread1Ready = false;
				bool thread2Ready = false;
				SLazy<int> slazy = new(() =>
				{
					while (!ready)
					{
						default(SpinWait).SpinOnce();
					}
					return ++value;
				});
				Thread thread1 = new(() => { thread1Ready = true; _ = slazy.Value; });
				Thread thread2 = new(() => { thread2Ready = true; _ = slazy.Value; });
				thread1.Start();
				thread2.Start();
				SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

				// just some extra waits for good measure to try to ensure
				// thread1 and thread2 reach "_ = slazy.Value"
				default(SpinWait).SpinOnce();
				default(SpinWait).SpinOnce();
				default(SpinWait).SpinOnce();

				Assert.IsTrue(!slazy.IsValueCreated);
				ready = true;
				thread1.Join();
				thread2.Join();
				Assert.IsTrue(slazy.Value is 2);
				Assert.IsTrue(value is 2);
			}
			{
				int value = 1;
				bool ready = false;
				bool thread1Ready = false;
				bool thread2Ready = false;
				SLazy<int> slazyA = new(() =>
				{
					while (!ready)
					{
						default(SpinWait).SpinOnce();
					}
					return ++value;
				});
				SLazy<int> slazyB = slazyA;
				Thread thread1 = new(() => { thread1Ready = true; _ = slazyA.Value; });
				Thread thread2 = new(() => { thread2Ready = true; _ = slazyB.Value; });
				thread1.Start();
				thread2.Start();
				SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

				// just some extra waits for good measure to ensure thread1 and thread2
				// reach "_ = slazyA.Value" and "_ = slazyB.Value" respectively
				default(SpinWait).SpinOnce();
				default(SpinWait).SpinOnce();
				default(SpinWait).SpinOnce();

				Assert.IsTrue(!slazyA.IsValueCreated);
				Assert.IsTrue(!slazyB.IsValueCreated);
				ready = true;
				thread1.Join();
				thread2.Join();
				Assert.IsTrue(slazyA.Value is 2);
				Assert.IsTrue(slazyB.Value is 2);
				Assert.IsTrue(value is 2);
			}
		}
	}
}
