using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	public static class LazyTesting
	{
		public static void Test<TLazyString, TLazyInt, TLazyObject>(
			Func<Func<string?>, TLazyString> newLazyString,
			Func<string?, TLazyString> newLazyStringValue,
			Func<Func<int>, TLazyInt> newLazyInt,
			Func<int, TLazyInt> newLazyIntValue,
			Func<Func<object?>, TLazyObject> newLazyObject,
			Func<object, TLazyObject> newLazyObjectValue)
			where TLazyString : struct, ILazy<string?>
			where TLazyInt : struct, ILazy<int>
			where TLazyObject : struct, ILazy<object?>
		{
			#region ILazy<T> test cases

			{
				Assert.ThrowsException<ArgumentNullException>(() => newLazyString(default(Func<string>)!));
				Assert.ThrowsException<ArgumentNullException>(() => newLazyInt(default(Func<int>)!));
				Assert.ThrowsException<ArgumentNullException>(() => newLazyObject(default(Func<object>)!));
			}
			{
				TLazyString a = newLazyString(() => null);
				Assert.IsTrue(!a.IsValueCreated);
			}
			{
				TLazyString a = newLazyString(() => null);
				Assert.IsTrue(!a.Equals("not null"));
			}
			{
				TLazyString a = newLazyString(() => "not null");
				Assert.IsTrue(!a.Equals(null));
			}
			{
				TLazyInt a = newLazyIntValue(1);
				TLazyInt b = newLazyIntValue(1);
				Assert.IsTrue(a.Equals(b));
			}
			{
				TLazyString a = newLazyStringValue("hello world");
				TLazyString b = newLazyStringValue("hello world");
				Assert.IsTrue(a.Equals(b));
			}
			{
				TLazyString a = newLazyStringValue(default(string));
				TLazyString b = newLazyStringValue(default(string));
				Assert.IsTrue(a.Equals(b));
			}
			{
				TLazyString a = newLazyStringValue(default(string));
				Assert.IsTrue(a.Equals(a));
			}
			{
				TLazyObject a = newLazyObject(() => new());
				TLazyObject b = newLazyObject(() => new());
				Assert.IsFalse(a.Equals(b));
			}
			{
				TLazyInt a = newLazyIntValue(1);
				Assert.IsTrue(a.Equals(1));
			}
			{
				TLazyString a = newLazyStringValue("hello world");
				Assert.IsTrue(a.Equals("hello world"));
			}
			{
				TLazyString a = newLazyStringValue(default(string));
				Assert.IsTrue(a.Equals(null));
			}
			if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.ExecutionAndPublication)
			{
				{
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazy = newLazyInt(() =>
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
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazy.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazy.Value is 2);
					Assert.IsTrue(value is 2);
				}
			}
			else if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.PublicationOnly)
			{
				{
					object @lock = new();
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazy = newLazyInt(() =>
					{
						while (!ready)
						{
							default(SpinWait).SpinOnce();
						}
						lock (@lock)
						{
							return ++value;
						}
					});
					Thread thread1 = new(() => { thread1Ready = true; _ = slazy.Value; });
					Thread thread2 = new(() => { thread2Ready = true; _ = slazy.Value; });
					thread1.Start();
					thread2.Start();
					SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

					// just some extra waits for good measure to try to ensure
					// thread1 and thread2 reach "_ = slazy.Value"
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazy.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazy.Value is 2);
					Assert.IsTrue(value is 3);
				}
			}
			else if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.None)
			{
				{
					object @lock = new();
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazy = newLazyInt(() =>
					{
						while (!ready)
						{
							default(SpinWait).SpinOnce();
						}
						lock (@lock)
						{
							return ++value;
						}
					});
					Thread thread1 = new(() => { thread1Ready = true; _ = slazy.Value; });
					Thread thread2 = new(() => { thread2Ready = true; _ = slazy.Value; });
					thread1.Start();
					thread2.Start();
					SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

					// just some extra waits for good measure to try to ensure
					// thread1 and thread2 reach "_ = slazy.Value"
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazy.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazy.Value is 3);
					Assert.IsTrue(value is 3);
				}
			}
			if (default(TLazyObject).IsCachingExceptions)
			{
				{
					int test = 1;
					TLazyObject a = newLazyObject(() => { test++; throw new Exception("Expected"); });
					Exception? exception = null;
					Assert.IsTrue(test is 1);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { exception = e; throw; } });
					Assert.IsTrue(test is 2);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(ReferenceEquals(exception, e)); throw; } });
					Assert.IsTrue(test is 2);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(ReferenceEquals(exception, e)); throw; } });
					Assert.IsTrue(test is 2);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(ReferenceEquals(exception, e)); throw; } });
				}
			}
			else
			{
				{
					int test = 1;
					TLazyObject a = newLazyObject(() => { test++; throw new Exception("Expected"); });
					Exception? exception = null;
					Assert.IsTrue(test is 1);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { exception = e; throw; } });
					Assert.IsTrue(test is 2);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(!ReferenceEquals(exception, e)); throw; } });
					Assert.IsTrue(test is 3);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(!ReferenceEquals(exception, e)); throw; } });
					Assert.IsTrue(test is 4);
					Assert.ThrowsException<Exception>(() => { try { _ = a.Value; } catch (Exception e) { Assert.IsTrue(!ReferenceEquals(exception, e)); throw; } });
				}
			}
			if (default(TLazyString).IsStructCopySafe)
			{
				{
					TLazyString a = newLazyStringValue("hello world");
					TLazyString b = a;
					Assert.IsTrue(a.Equals(b));
				}
				{
					TLazyInt a = newLazyIntValue(1);
					Assert.IsTrue(a.Equals(a));
				}
				{
					TLazyString a = newLazyStringValue("hello world");
					Assert.IsTrue(a.Equals(a));
				}
				{
					TLazyObject a = newLazyObject(() => new());
					TLazyObject b = a;
					Assert.IsTrue(a.Equals(b));
				}
				{
					TLazyObject a = newLazyObject(() => new());
					Assert.IsTrue(a.Equals(a));
				}
				{
					TLazyInt a = newLazyInt(() => 1);
					Assert.IsTrue(a.Equals(a));
				}
				if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.ExecutionAndPublication)
				{
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazyA = newLazyInt(() =>
					{
						while (!ready)
						{
							default(SpinWait).SpinOnce();
						}
						return ++value;
					});
					TLazyInt slazyB = slazyA;
					Thread thread1 = new(() => { thread1Ready = true; _ = slazyA.Value; });
					Thread thread2 = new(() => { thread2Ready = true; _ = slazyB.Value; });
					thread1.Start();
					thread2.Start();
					SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

					// just some extra waits for good measure to ensure thread1 and thread2
					// reach "_ = slazyA.Value" and "_ = slazyB.Value" respectively
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazyA.IsValueCreated);
					Assert.IsTrue(!slazyB.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazyA.Value is 2);
					Assert.IsTrue(slazyB.Value is 2);
					Assert.IsTrue(value is 2);
				}
				else if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.PublicationOnly)
				{
					object @lock = new();
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazyA = newLazyInt(() =>
					{
						while (!ready)
						{
							default(SpinWait).SpinOnce();
						}
						lock (@lock)
						{
							return ++value;
						}
					});
					TLazyInt slazyB = slazyA;
					Thread thread1 = new(() => { thread1Ready = true; _ = slazyA.Value; });
					Thread thread2 = new(() => { thread2Ready = true; _ = slazyB.Value; });
					thread1.Start();
					thread2.Start();
					SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

					// just some extra waits for good measure to ensure thread1 and thread2
					// reach "_ = slazyA.Value" and "_ = slazyB.Value" respectively
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazyA.IsValueCreated);
					Assert.IsTrue(!slazyB.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazyA.Value is 2);
					Assert.IsTrue(slazyB.Value is 2);
					Assert.IsTrue(value is 3);
				}
				else if (default(TLazyInt).ThreadSafety is LazyThreadSafetyMode.None)
				{
					object @lock = new();
					int value = 1;
					bool ready = false;
					bool thread1Ready = false;
					bool thread2Ready = false;
					TLazyInt slazyA = newLazyInt(() =>
					{
						while (!ready)
						{
							default(SpinWait).SpinOnce();
						}
						lock (@lock)
						{
							return ++value;
						}
					});
					TLazyInt slazyB = slazyA;
					Thread thread1 = new(() => { thread1Ready = true; _ = slazyA.Value; });
					Thread thread2 = new(() => { thread2Ready = true; _ = slazyB.Value; });
					thread1.Start();
					thread2.Start();
					SpinWait.SpinUntil(() => thread1Ready && thread2Ready);

					// just some extra waits for good measure to ensure thread1 and thread2
					// reach "_ = slazyA.Value" and "_ = slazyB.Value" respectively
					SpinWait sw = new();
					for (int i = 0; i < 10; i++)
					{
						sw.SpinOnce();
					}

					Assert.IsTrue(!slazyA.IsValueCreated);
					Assert.IsTrue(!slazyB.IsValueCreated);
					ready = true;
					thread1.Join();
					thread2.Join();
					Assert.IsTrue(slazyA.Value is 2 or 3);
					Assert.IsTrue(slazyB.Value is 2 or 3);
					Assert.IsTrue(value is 2 or 3);
				}
				{
					TLazyInt a = newLazyInt(() => 1);
					TLazyInt b = a;
					Assert.IsTrue(a.Value is 1);
					Assert.IsTrue(b.IsValueCreated);
					Assert.IsTrue(b.Value is 1);
				}
			}

			#endregion

			#region ToString() test cases

			{
				TLazyString a = newLazyStringValue(default(string));
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				TLazyString a = newLazyString(() => null);
				Assert.IsTrue(a.ToString() is null);
			}
			{
				TLazyInt a = newLazyInt(() => 1);
				Assert.IsTrue(a.ToString() == 1.ToString());
			}
			{
				TLazyString a = newLazyString(() => "hello world");
				Assert.IsTrue(a.ToString() is "hello world");
			}
			{
				TLazyInt a = newLazyInt(() => 1);
				Assert.IsTrue(a.Value is 1);
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.ToString() == 1.ToString());
			}
			{
				TLazyString a = newLazyString(() => "hello world");
				Assert.IsTrue(a.Value is "hello world");
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.ToString() is "hello world");
			}

			#endregion

			#region GetHashCode() test cases

			{
				TLazyObject a = newLazyObjectValue(default(object)!);
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				TLazyObject a = newLazyObject(() => null);
				Assert.IsTrue(a.GetHashCode() is default(int));
			}
			{
				TLazyInt a = newLazyInt(() => 1);
				Assert.IsTrue(a.GetHashCode() == 1.GetHashCode());
			}
			{
				TLazyString a = newLazyString(() => "hello world");
				Assert.IsTrue(a.GetHashCode() == "hello world".GetHashCode());
			}
			{
				TLazyInt a = newLazyInt(() => 1);
				Assert.IsTrue(a.Value is 1);
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.GetHashCode() == 1.GetHashCode());
			}
			{
				TLazyString a = newLazyString(() => "hello world");
				Assert.IsTrue(a.Value is "hello world");
				Assert.IsTrue(a.IsValueCreated);
				Assert.IsTrue(a.GetHashCode() == "hello world".GetHashCode());
			}

			#endregion
		}
	}

	[TestClass]
	public class SLazy_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazy<string?>, SLazy<int>, SLazy<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class SLazyNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazyNoCatch<string?>, SLazyNoCatch<int>, SLazyNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class SLazyPublicationLock_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazyPublicationLock<string?>, SLazyPublicationLock<int>, SLazyPublicationLock<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class SLazyPublicationLockNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazyPublicationLockNoCatch<string?>, SLazyPublicationLockNoCatch<int>, SLazyPublicationLockNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class SLazyNoLock_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazyNoLock<string?>, SLazyNoLock<int>, SLazyNoLock<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class SLazyNoLockNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<SLazyNoLockNoCatch<string?>, SLazyNoLockNoCatch<int>, SLazyNoLockNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazy_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazy<string?>, ValueLazy<int>, ValueLazy<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazyNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazyNoCatch<string?>, ValueLazyNoCatch<int>, ValueLazyNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazyPublicationLock_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazyPublicationLock<string?>, ValueLazyPublicationLock<int>, ValueLazyPublicationLock<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazyPublicationLockNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazyPublicationLockNoCatch<string?>, ValueLazyPublicationLockNoCatch<int>, ValueLazyPublicationLockNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazyNoLock_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazyNoLock<string?>, ValueLazyNoLock<int>, ValueLazyNoLock<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}

	[TestClass]
	public class ValueLazyNoLockNoCatch_Testing
	{
		[TestMethod]
		public void Testing()
		{
			LazyTesting.Test<ValueLazyNoLockNoCatch<string?>, ValueLazyNoLockNoCatch<int>, ValueLazyNoLockNoCatch<object?>>(
				func => func,
				value => value,
				func => func,
				value => value,
				func => func,
				value => value);
		}
	}
}
