using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;

namespace Towel_Testing
{
	[TestClass] public class MethodSignatures_Testing
	{
		[TestMethod] public void System_Enum_TryParse() =>
			Assert.IsFalse(typeof(Enum).GetMethod<MethodSignatures.System.Enum.TryParse<ConsoleColor>>() is null);
	}
}
