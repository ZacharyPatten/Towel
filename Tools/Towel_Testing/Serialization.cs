namespace Towel_Testing;

[TestClass]
public class Serialization_Testing
{
	internal class ExpectedException : Exception { }

	public static decimal StaticFunction(int a, string? b, object? c) => throw new ExpectedException();

	public static void StaticAction(int a, string? b, object? c) => throw new ExpectedException();

#pragma warning disable CA1822 // Mark members as static
	public void NonStaticMethod() => throw new ExpectedException();
#pragma warning restore CA1822 // Mark members as static

	[TestMethod]
	public void StaticDelegateToXml_Testing()
	{
		{ // Should Succeed
			Func<int, string?, object?, decimal> functionToSerialize = StaticFunction;
			string serialization = Serialization.StaticDelegateToXml(functionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
		}
		{ // Should Succeed
			Action<int, string?, object?> functionToSerialize = StaticAction;
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

	[TestMethod]
	public void StaticDelegateFromXml_Testing()
	{
		{ // Should Succeed
			Func<int, string?, object?, decimal> functionToSerialize = StaticFunction;
			string serialization = Serialization.StaticDelegateToXml(functionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
			var deserialization = Serialization.StaticDelegateFromXml<Func<int, string?, object?, decimal>>(serialization);
			Assert.IsFalse(deserialization is null);
			Assert.ThrowsException<ExpectedException>(() => deserialization!(default, default, default));
		}
		{ // Should Succeed
			Action<int, string?, object?> actionToSerialize = StaticAction;
			string serialization = Serialization.StaticDelegateToXml(actionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
			var deserialization = Serialization.StaticDelegateFromXml<Action<int, string?, object?>>(serialization);
			Assert.IsFalse(deserialization is null);
			Assert.ThrowsException<ExpectedException>(() => deserialization!(default, default, default));
		}
	}

	[TestMethod]
	public void StaticDelegateToJson_Testing()
	{
		{ // Should Succeed
			Func<int, string?, object?, decimal> functionToSerialize = StaticFunction;
			string serialization = Serialization.StaticDelegateToJson(functionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
		}
		{ // Should Succeed
			Action<int, string?, object?> functionToSerialize = StaticAction;
			string serialization = Serialization.StaticDelegateToJson(functionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
		}
		{ // Should Fail (Non-Static)
			Action nonStaticDelegate = NonStaticMethod;
			Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToJson(nonStaticDelegate));
		}
		{ // Should Fail (Local Function)
			Exception notImplemented = new NotImplementedException();
			void LocalFunctionToSerializeFail() => throw notImplemented;
			Action nonStaticDelegate = LocalFunctionToSerializeFail;
			Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToJson(nonStaticDelegate));
		}
		{ // Should Fail (Static Local Function)
			static void StaticLocalFunctionToSerialize() => throw new NotImplementedException();
			Action nonStaticDelegate = StaticLocalFunctionToSerialize;
			Assert.ThrowsException<NotSupportedException>(() => Serialization.StaticDelegateToJson(nonStaticDelegate));
		}
	}

	[TestMethod]
	public void StaticDelegateFromJson_Testing()
	{
		{ // Should Succeed
			Func<int, string?, object?, decimal> functionToSerialize = StaticFunction;
			string serialization = Serialization.StaticDelegateToJson(functionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
			var deserialization = Serialization.StaticDelegateFromJson<Func<int, string?, object?, decimal>>(serialization);
			Assert.IsFalse(deserialization is null);
			Assert.ThrowsException<ExpectedException>(() => deserialization!(default, default, default));
		}
		{ // Should Succeed
			Action<int, string?, object?> actionToSerialize = StaticAction;
			string serialization = Serialization.StaticDelegateToJson(actionToSerialize);
			Assert.IsFalse(string.IsNullOrWhiteSpace(serialization));
			var deserialization = Serialization.StaticDelegateFromJson<Action<int, string?, object?>>(serialization);
			Assert.IsFalse(deserialization is null);
			Assert.ThrowsException<ExpectedException>(() => deserialization!(default, default, default));
		}
	}
}
