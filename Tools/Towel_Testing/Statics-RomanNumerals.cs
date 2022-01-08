namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		#region TryParseRomanNumeral

		[TestMethod]
		public void TryParseRomanNumeral_Testing()
		{
			Assert.IsTrue(TryParseRomanNumeral("I")       is (true,    1));
			Assert.IsTrue(TryParseRomanNumeral("II")      is (true,    2));
			Assert.IsTrue(TryParseRomanNumeral("III")     is (true,    3));
			Assert.IsTrue(TryParseRomanNumeral("IV")      is (true,    4));
			Assert.IsTrue(TryParseRomanNumeral("V")       is (true,    5));
			Assert.IsTrue(TryParseRomanNumeral("VI")      is (true,    6));
			Assert.IsTrue(TryParseRomanNumeral("VII")     is (true,    7));
			Assert.IsTrue(TryParseRomanNumeral("VIII")    is (true,    8));
			Assert.IsTrue(TryParseRomanNumeral("IX")      is (true,    9));
			Assert.IsTrue(TryParseRomanNumeral("X")       is (true,   10));
			Assert.IsTrue(TryParseRomanNumeral("XI")      is (true,   11));
			Assert.IsTrue(TryParseRomanNumeral("XII")     is (true,   12));
			Assert.IsTrue(TryParseRomanNumeral("XIII")    is (true,   13));
			Assert.IsTrue(TryParseRomanNumeral("XIV")     is (true,   14));
			Assert.IsTrue(TryParseRomanNumeral("XV")      is (true,   15));
			Assert.IsTrue(TryParseRomanNumeral("XVI")     is (true,   16));
			Assert.IsTrue(TryParseRomanNumeral("XVII")    is (true,   17));
			Assert.IsTrue(TryParseRomanNumeral("XVIII")   is (true,   18));
			Assert.IsTrue(TryParseRomanNumeral("XIX")     is (true,   19));
			Assert.IsTrue(TryParseRomanNumeral("XX")      is (true,   20));
			Assert.IsTrue(TryParseRomanNumeral("XXI")     is (true,   21));
			Assert.IsTrue(TryParseRomanNumeral("XXII")    is (true,   22));
			Assert.IsTrue(TryParseRomanNumeral("XXIII")   is (true,   23));
			Assert.IsTrue(TryParseRomanNumeral("XXIV")    is (true,   24));
			Assert.IsTrue(TryParseRomanNumeral("XXV")     is (true,   25));
			Assert.IsTrue(TryParseRomanNumeral("XXVI")    is (true,   26));
			Assert.IsTrue(TryParseRomanNumeral("XXVII")   is (true,   27));
			Assert.IsTrue(TryParseRomanNumeral("XXVIII")  is (true,   28));
			Assert.IsTrue(TryParseRomanNumeral("XXIX")    is (true,   29));
			Assert.IsTrue(TryParseRomanNumeral("XXX")     is (true,   30));
			Assert.IsTrue(TryParseRomanNumeral("XXXI")    is (true,   31));
			Assert.IsTrue(TryParseRomanNumeral("XXXII")   is (true,   32));
			Assert.IsTrue(TryParseRomanNumeral("XXXIII")  is (true,   33));
			Assert.IsTrue(TryParseRomanNumeral("XXXIV")   is (true,   34));
			Assert.IsTrue(TryParseRomanNumeral("XXXV")    is (true,   35));
			Assert.IsTrue(TryParseRomanNumeral("XXXVI")   is (true,   36));
			Assert.IsTrue(TryParseRomanNumeral("XXXVII")  is (true,   37));
			Assert.IsTrue(TryParseRomanNumeral("XXXVIII") is (true,   38));
			Assert.IsTrue(TryParseRomanNumeral("XXXIX")   is (true,   39));
			Assert.IsTrue(TryParseRomanNumeral("XL")      is (true,   40));
			Assert.IsTrue(TryParseRomanNumeral("XLI")     is (true,   41));
			Assert.IsTrue(TryParseRomanNumeral("XLII")    is (true,   42));
			Assert.IsTrue(TryParseRomanNumeral("XLIII")   is (true,   43));
			Assert.IsTrue(TryParseRomanNumeral("XLIV")    is (true,   44));
			Assert.IsTrue(TryParseRomanNumeral("XLV")     is (true,   45));
			Assert.IsTrue(TryParseRomanNumeral("XLVI")    is (true,   46));
			Assert.IsTrue(TryParseRomanNumeral("XLVII")   is (true,   47));
			Assert.IsTrue(TryParseRomanNumeral("XLVIII")  is (true,   48));
			Assert.IsTrue(TryParseRomanNumeral("XLIX")    is (true,   49));
			Assert.IsTrue(TryParseRomanNumeral("L")       is (true,   50));
			Assert.IsTrue(TryParseRomanNumeral("C")       is (true,  100));
			Assert.IsTrue(TryParseRomanNumeral("D")       is (true,  500));
			Assert.IsTrue(TryParseRomanNumeral("M")       is (true, 1000));

			Assert.IsTrue(TryParseRomanNumeral(null)  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("")    is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("a")   is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("aI")  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("Ia")  is (false, default(int)));
			Assert.IsTrue(TryParseRomanNumeral("aIa") is (false, default(int)));
		}

		#endregion

		#region TryToRomanNumeral

		[TestMethod]
		public void TryToRomanNumeral_Testing()
		{
			Assert.IsTrue(TryToRomanNumeral(   1) is (true,       "I"));
			Assert.IsTrue(TryToRomanNumeral(   2) is (true,      "II"));
			Assert.IsTrue(TryToRomanNumeral(   3) is (true,     "III"));
			Assert.IsTrue(TryToRomanNumeral(   4) is (true,      "IV"));
			Assert.IsTrue(TryToRomanNumeral(   5) is (true,       "V"));
			Assert.IsTrue(TryToRomanNumeral(   6) is (true,      "VI"));
			Assert.IsTrue(TryToRomanNumeral(   7) is (true,     "VII"));
			Assert.IsTrue(TryToRomanNumeral(   8) is (true,    "VIII"));
			Assert.IsTrue(TryToRomanNumeral(   9) is (true,      "IX"));
			Assert.IsTrue(TryToRomanNumeral(  10) is (true,       "X"));
			Assert.IsTrue(TryToRomanNumeral(  11) is (true,      "XI"));
			Assert.IsTrue(TryToRomanNumeral(  12) is (true,     "XII"));
			Assert.IsTrue(TryToRomanNumeral(  13) is (true,    "XIII"));
			Assert.IsTrue(TryToRomanNumeral(  14) is (true,     "XIV"));
			Assert.IsTrue(TryToRomanNumeral(  15) is (true,      "XV"));
			Assert.IsTrue(TryToRomanNumeral(  16) is (true,     "XVI"));
			Assert.IsTrue(TryToRomanNumeral(  17) is (true,    "XVII"));
			Assert.IsTrue(TryToRomanNumeral(  18) is (true,   "XVIII"));
			Assert.IsTrue(TryToRomanNumeral(  19) is (true,     "XIX"));
			Assert.IsTrue(TryToRomanNumeral(  20) is (true,      "XX"));
			Assert.IsTrue(TryToRomanNumeral(  21) is (true,     "XXI"));
			Assert.IsTrue(TryToRomanNumeral(  22) is (true,    "XXII"));
			Assert.IsTrue(TryToRomanNumeral(  23) is (true,   "XXIII"));
			Assert.IsTrue(TryToRomanNumeral(  24) is (true,    "XXIV"));
			Assert.IsTrue(TryToRomanNumeral(  25) is (true,     "XXV"));
			Assert.IsTrue(TryToRomanNumeral(  26) is (true,    "XXVI"));
			Assert.IsTrue(TryToRomanNumeral(  27) is (true,   "XXVII"));
			Assert.IsTrue(TryToRomanNumeral(  28) is (true,  "XXVIII"));
			Assert.IsTrue(TryToRomanNumeral(  29) is (true,    "XXIX"));
			Assert.IsTrue(TryToRomanNumeral(  30) is (true,     "XXX"));
			Assert.IsTrue(TryToRomanNumeral(  31) is (true,    "XXXI"));
			Assert.IsTrue(TryToRomanNumeral(  32) is (true,   "XXXII"));
			Assert.IsTrue(TryToRomanNumeral(  33) is (true,  "XXXIII"));
			Assert.IsTrue(TryToRomanNumeral(  34) is (true,   "XXXIV"));
			Assert.IsTrue(TryToRomanNumeral(  35) is (true,    "XXXV"));
			Assert.IsTrue(TryToRomanNumeral(  36) is (true,   "XXXVI"));
			Assert.IsTrue(TryToRomanNumeral(  37) is (true,  "XXXVII"));
			Assert.IsTrue(TryToRomanNumeral(  38) is (true, "XXXVIII"));
			Assert.IsTrue(TryToRomanNumeral(  39) is (true,   "XXXIX"));
			Assert.IsTrue(TryToRomanNumeral(  40) is (true,      "XL"));
			Assert.IsTrue(TryToRomanNumeral(  41) is (true,     "XLI"));
			Assert.IsTrue(TryToRomanNumeral(  42) is (true,    "XLII"));
			Assert.IsTrue(TryToRomanNumeral(  43) is (true,   "XLIII"));
			Assert.IsTrue(TryToRomanNumeral(  44) is (true,    "XLIV"));
			Assert.IsTrue(TryToRomanNumeral(  45) is (true,     "XLV"));
			Assert.IsTrue(TryToRomanNumeral(  46) is (true,    "XLVI"));
			Assert.IsTrue(TryToRomanNumeral(  47) is (true,   "XLVII"));
			Assert.IsTrue(TryToRomanNumeral(  48) is (true,  "XLVIII"));
			Assert.IsTrue(TryToRomanNumeral(  49) is (true,    "XLIX"));
			Assert.IsTrue(TryToRomanNumeral(  50) is (true,       "L"));
			Assert.IsTrue(TryToRomanNumeral( 100) is (true,       "C"));
			Assert.IsTrue(TryToRomanNumeral( 500) is (true,       "D"));
			Assert.IsTrue(TryToRomanNumeral(1000) is (true,       "M"));

			Assert.IsTrue(TryToRomanNumeral(    0) is (false, null));
			Assert.IsTrue(TryToRomanNumeral( 4000) is (false, null));
			Assert.IsTrue(TryToRomanNumeral(   -1) is (false, null));
			Assert.IsTrue(TryToRomanNumeral(-3999) is (false, null));
		}

		#endregion

		#region RomanNumeralSynch

		[TestMethod]
		public void RomanNumeralSynch_Testing()
		{
			for (int i = 1; i < 4000; i++)
			{
				var (toSuccess, romanNumerals) = TryToRomanNumeral(i);
				Assert.IsTrue(toSuccess);
				var (fromSuccess, value) = TryParseRomanNumeral(romanNumerals);
				Assert.IsTrue(fromSuccess);
				Assert.IsTrue(value == i);
			}
		}

		#endregion
	}
}
