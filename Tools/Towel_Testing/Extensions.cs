using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	[TestClass] public class TowelDotNetExtensions_Testing
	{
		#region Decimal Testing

		[TestMethod] public void Decimal_ToEnglishWords()
		{
			Assert.IsTrue((   1m).ToEnglishWords() == "One");
			Assert.IsTrue((  -1m).ToEnglishWords() == "Negative One");
			Assert.IsTrue(( 1.5m).ToEnglishWords() == "One And Five Tenths");
			Assert.IsTrue((  69m).ToEnglishWords() == "Sixty-Nine");
			Assert.IsTrue(( 120m).ToEnglishWords() == "One Hundred Twenty");
			Assert.IsTrue((1300m).ToEnglishWords() == "One Thousand Three Hundred");
			Assert.IsTrue((7725m).ToEnglishWords() == "Seven Thousand Seven Hundred Twenty-Five");
			Assert.IsTrue((  12m).ToEnglishWords() == "Twelve");
		}

		#endregion
	}
}

