using System.IO;
using static Towel.CommandLine;

namespace Towel_Testing
{
	[TestClass]
	public class CommandLine_Testing
	{
		#region Test: public static void A()

		internal static bool ACalled;

		[Command]
		public static void A()
		{
			ACalled = true;
		}

		[TestMethod]
		public void TestA()
		{
			{
				ACalled = false;
				HandleArguments(new[] { "A" });
				Assert.IsTrue(ACalled);
			}
			{
				ACalled = false;
				HandleArguments(new[] { "A", "--a", "1" });
				Assert.IsTrue(!ACalled);
			}
		}

		#endregion

		#region Test: public static void B(int a)

		internal static bool BCalled;
		internal static int BAParameter;

		[Command]
		public static void B(int a)
		{
			BCalled = true;
			BAParameter = a;
		}

		[TestMethod]
		public void TestB()
		{
			{
				BCalled = false;
				BAParameter = 0;
				HandleArguments(new[] { "B", "--a", "1" });
				Assert.IsTrue(BCalled);
				Assert.IsTrue(BAParameter is 1);
			}
			{
				BCalled = false;
				BAParameter = 0;
				HandleArguments(new[] { "B", "--a", "2" });
				Assert.IsTrue(BCalled);
				Assert.IsTrue(BAParameter is 2);
			}
			{
				BCalled = false;
				BAParameter = 0;
				HandleArguments(new[] { "B" });
				Assert.IsTrue(!BCalled);
				Assert.IsTrue(BAParameter is 0);
			}
			{
				BCalled = false;
				BAParameter = 0;
				HandleArguments(new[] { "B", "--b", "1" });
				Assert.IsTrue(!BCalled);
				Assert.IsTrue(BAParameter is 0);
			}
		}

		#endregion

		#region Test: public static void C(int a = -1)

		internal static bool CCalled;
		internal static int CAParameter;

		[Command]
		public static void C(int a = -1)
		{
			CCalled = true;
			CAParameter = a;
		}

		[TestMethod]
		public void TestC()
		{
			{
				CCalled = false;
				CAParameter = 0;
				HandleArguments(new[] { "C", "--a", "1" });
				Assert.IsTrue(CCalled);
				Assert.IsTrue(CAParameter is 1);
			}
			{
				CCalled = false;
				CAParameter = 0;
				HandleArguments(new[] { "C", "--a", "2" });
				Assert.IsTrue(CCalled);
				Assert.IsTrue(CAParameter is 2);
			}
			{
				CCalled = false;
				CAParameter = 0;
				HandleArguments(new[] { "C" });
				Assert.IsTrue(CCalled);
				Assert.IsTrue(CAParameter is -1);
			}
			{
				CCalled = false;
				CAParameter = 0;
				HandleArguments(new[] { "C", "--b", "1" });
				Assert.IsTrue(!CCalled);
				Assert.IsTrue(CAParameter is 0);
			}
		}

		#endregion

		#region Test: public static void D(int a, int b)

		internal static bool DCalled;
		internal static int DAParameter;
		internal static int DBParameter;

		[Command]
		public static void D(int a, int b)
		{
			DCalled = true;
			DAParameter = a;
			DBParameter = b;
		}

		[TestMethod]
		public void TestD()
		{
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D" });
				Assert.IsTrue(!DCalled);
				Assert.IsTrue(DAParameter is 0);
				Assert.IsTrue(DBParameter is 0);
			}
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D", "--a", "1" });
				Assert.IsTrue(!DCalled);
				Assert.IsTrue(DAParameter is 0);
				Assert.IsTrue(DBParameter is 0);
			}
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D", "--b", "1" });
				Assert.IsTrue(!DCalled);
				Assert.IsTrue(DAParameter is 0);
				Assert.IsTrue(DBParameter is 0);
			}
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D", "--a", "1", "--b", "2" });
				Assert.IsTrue(DCalled);
				Assert.IsTrue(DAParameter is 1);
				Assert.IsTrue(DBParameter is 2);
			}
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D", "--b", "1", "--a", "2" });
				Assert.IsTrue(DCalled);
				Assert.IsTrue(DAParameter is 2);
				Assert.IsTrue(DBParameter is 1);
			}
			{
				DCalled = false;
				DAParameter = 0;
				DBParameter = 0;
				HandleArguments(new[] { "D", "--a", "1", "--b", "2", "--c", "3" });
				Assert.IsTrue(!DCalled);
				Assert.IsTrue(DAParameter is 0);
				Assert.IsTrue(DBParameter is 0);
			}
		}

		#endregion

		#region Test: public static void E(int a, int b = -1)

		internal static bool ECalled;
		internal static int EAParameter;
		internal static int EBParameter;

		[Command]
		public static void E(int a, int b = -1)
		{
			ECalled = true;
			EAParameter = a;
			EBParameter = b;
		}

		[TestMethod]
		public void TestE()
		{
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E" });
				Assert.IsTrue(!ECalled);
				Assert.IsTrue(EAParameter is 0);
				Assert.IsTrue(EBParameter is 0);
			}
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E", "--a", "1" });
				Assert.IsTrue(ECalled);
				Assert.IsTrue(EAParameter is 1);
				Assert.IsTrue(EBParameter is -1);
			}
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E", "--b", "1" });
				Assert.IsTrue(!ECalled);
				Assert.IsTrue(EAParameter is 0);
				Assert.IsTrue(EBParameter is 0);
			}
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E", "--a", "1", "--b", "2" });
				Assert.IsTrue(ECalled);
				Assert.IsTrue(EAParameter is 1);
				Assert.IsTrue(EBParameter is 2);
			}
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E", "--b", "1", "--a", "2" });
				Assert.IsTrue(ECalled);
				Assert.IsTrue(EAParameter is 2);
				Assert.IsTrue(EBParameter is 1);
			}
			{
				ECalled = false;
				EAParameter = 0;
				EBParameter = 0;
				HandleArguments(new[] { "E", "--a", "1", "--b", "2", "--c", "3" });
				Assert.IsTrue(!ECalled);
				Assert.IsTrue(EAParameter is 0);
				Assert.IsTrue(EBParameter is 0);
			}
		}

		#endregion

		#region Test: public static void F(FileInfo a)

		internal static bool FCalled;
		internal static FileInfo? FAParameter;

		[Command]
		public static void F(FileInfo a)
		{
			FCalled = true;
			FAParameter = a;
		}

		[TestMethod]
		public void TestF()
		{
			{
				FCalled = false;
				FAParameter = null;
				const string fileName = "Towel_Testing.xml";
				HandleArguments(new[] { "F", "--a", fileName });
				Assert.IsTrue(FCalled);
				Assert.IsTrue(FAParameter is not null && FAParameter.Name == fileName);
			}
			{
				FCalled = false;
				FAParameter = null;
				HandleArguments(new[] { "F" });
				Assert.IsTrue(!FCalled);
				Assert.IsTrue(FAParameter is null);
			}
		}

		#endregion
	}
}
