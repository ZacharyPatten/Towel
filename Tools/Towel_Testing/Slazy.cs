using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
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
				SLazy<string> a = default;
				SLazy<string> b = default;
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
