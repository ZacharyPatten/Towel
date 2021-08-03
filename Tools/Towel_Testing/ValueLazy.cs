using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	[TestClass]
	public class ValueLazy_Testing
	{
		[TestMethod]
		public void Testing()
		{
			{
				Assert.ThrowsException<ArgumentNullException>(() => new ValueLazy<int>(default(Func<int>)!));
			}
			{
				ValueLazy<string?> a = new(() => null);
				Assert.IsTrue(!a.IsValueCreated);
			}
			{
				ValueLazy<string?> a = new(() => null);
				Assert.IsTrue(!a.Equals("not null"));
			}
			{
				ValueLazy<string?> a = new(() => "not null");
				Assert.IsTrue(!a.Equals(null));
			}
			{
				ValueLazy<int> a = 1;
				ValueLazy<int> b = 1;
				Assert.IsTrue(a.Equals(b));
			}
			{
				ValueLazy<string> a = "hello world";
				ValueLazy<string> b = "hello world";
				Assert.IsTrue(a.Equals(b));
			}
			{
				ValueLazy<string> a = "hello world";
				ValueLazy<string> b = a;
				Assert.IsTrue(a.Equals(b));
			}
			{
				ValueLazy<string?> a = default(string);
				ValueLazy<string?> b = default(string);
				Assert.IsTrue(a.Equals(b));
			}
			{
				ValueLazy<int> a = 1;
				Assert.IsTrue(a.Equals(a));
			}
			{
				ValueLazy<string> a = "hello world";
				Assert.IsTrue(a.Equals(a));
			}
			{
				ValueLazy<object> a = new(() => new());
				ValueLazy<object> b = new(() => new());
				Assert.IsFalse(a.Equals(b));
			}
			{
				ValueLazy<int> a = 1;
				Assert.IsTrue(a.Equals(1));
			}
			{
				ValueLazy<string> a = "hello world";
				Assert.IsTrue(a.Equals("hello world"));
			}
			{
				ValueLazy<string?> a = default(string);
				Assert.IsTrue(a.Equals(null));
			}
			{
				ValueLazy<int> a = new Func<int>(() => 1);
				Assert.IsTrue(a.Equals(a));
			}
			{
				int value = 1;
				bool ready = false;
				bool thread1Ready = false;
				bool thread2Ready = false;
				ValueLazy<int> slazy = new(() =>
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
				int test = 1;
				ValueLazy<object> a = new(() => { test++; throw new Exception("Expected"); });
				Assert.IsTrue(test is 1);
				Assert.ThrowsException<Exception>(() => _ = a.Value);
				Assert.IsTrue(test is 2);
				Assert.ThrowsException<Exception>(() => _ = a.Value);
				Assert.IsTrue(test is 2);
				Assert.ThrowsException<Exception>(() => _ = a.Value);
				Assert.IsTrue(test is 2);
				Assert.ThrowsException<Exception>(() => _ = a.Value);
			}
		}

		[TestMethod]
		public void ToString_Testing()
		{
			{
				ValueLazy<string?> a = default(string);
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				ValueLazy<string?> a = new(() => null);
				Assert.IsTrue(a.ToString() is null);
			}
			{
				ValueLazy<int> a = new(() => 1);
				Assert.IsTrue(a.ToString() == 1.ToString());
			}
			{
				ValueLazy<string> a = new(() => "hello world");
				Assert.IsTrue(a.ToString() is "hello world");
			}
			{
				ValueLazy<int> a = new(() => 1);
				Assert.IsTrue(a.Value is 1);
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.ToString() == 1.ToString());
			}
			{
				ValueLazy<string> a = new(() => "hello world");
				Assert.IsTrue(a.Value is "hello world");
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.ToString() is "hello world");
			}
		}

		[TestMethod]
		public void GetHashCode_Testing()
		{
			{
				ValueLazy<object?> a = default(object);
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				ValueLazy<object?> a = new(() => null);
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				ValueLazy<int> a = new(() => 1);
				Assert.IsTrue(a.GetHashCode() == 1.GetHashCode());
			}
			{
				ValueLazy<string> a = new(() => "hello world");
				Assert.IsTrue(a.GetHashCode() == "hello world".GetHashCode());
			}
			{
				ValueLazy<int> a = new(() => 1);
				Assert.IsTrue(a.Value is 1);
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.GetHashCode() == 1.GetHashCode());
			}
			{
				ValueLazy<string> a = new(() => "hello world");
				Assert.IsTrue(a.Value is "hello world");
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.GetHashCode() == "hello world".GetHashCode());
			}
		}
	}
}
