using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;

namespace Towel_Testing
{
	[TestClass]
	public class Extensions_Testing
	{
		#region Decimal Testing

		[TestMethod]
		public void Decimal_ToEnglishWords()
		{
			Assert.IsTrue((1m).ToEnglishWords().Equals("One"));
			Assert.IsTrue((-1m).ToEnglishWords().Equals("Negative One"));
			Assert.IsTrue((1.5m).ToEnglishWords().Equals("One And Five Tenths"));
			Assert.IsTrue((69m).ToEnglishWords().Equals("Sixty-Nine"));
			Assert.IsTrue((120m).ToEnglishWords().Equals("One Hundred Twenty"));
			Assert.IsTrue((1300m).ToEnglishWords().Equals("One Thousand Three Hundred"));
			Assert.IsTrue((7725m).ToEnglishWords().Equals("Seven Thousand Seven Hundred Twenty-Five"));
			Assert.IsTrue((12m).ToEnglishWords().Equals("Twelve"));
		}

		#endregion
	}
}

