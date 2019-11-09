using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;

namespace Towel_Testing
{
	[TestClass] public class Serialization_Testing
	{
		#region Static Delegate

		public static decimal StaticMethod(int a, string b, object c) => throw new NotImplementedException();

		public void NonStaticMethod() => throw new NotImplementedException();

		[TestMethod] public void StaticDelegateToXml_Testing()
		{
			{ // Should Succeed
				Func<int, string, object, decimal> functionToSerialize = StaticMethod;
				string serialization = Serialization.StaticDelegateToXml(functionToSerialize);
				Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
			}
			{ // Should Fail (Non-Static)
				Action nonStaticDelegate = NonStaticMethod;
				Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToXml(nonStaticDelegate));
			}
			{ // Should Fail (Local Function)
				Exception notImplemented = new NotImplementedException();
				void LocalFunctionToSerializeFail() => throw notImplemented;
				Action nonStaticDelegate = LocalFunctionToSerializeFail;
				Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToXml(nonStaticDelegate));
			}
			{ // Should Fail (Static Local Function)
				static void StaticLocalFunctionToSerialize() => throw new NotImplementedException();
				Action nonStaticDelegate = StaticLocalFunctionToSerialize;
				Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToXml(nonStaticDelegate));
			}
		}

		[TestMethod] public void StaticDelegateFromXml_Testing()
		{
			{ // Should Succeed
				Func<int, string, object, decimal> functionToSerialize = StaticMethod;
				string serialization = Serialization.StaticDelegateToXml(functionToSerialize);
				Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
				var deserialization = Serialization.StaticDelegateFromXml<Func<int, string, object, decimal>>(serialization);
				Assert.IsFalse(deserialization is null);
				Assert.ThrowsException<NotImplementedException>(() => deserialization(default, default, default));
			}
		}

		#endregion
	}
}
