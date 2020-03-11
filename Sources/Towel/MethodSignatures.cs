namespace Towel
{
	/// <summary>Contains internal static method signatures for reflection look ups.</summary>
	internal static class MethodSignatures
	{
		internal static class System
		{
			internal static class Enum
			{
				internal delegate bool TryParse<TEnum>(string value, out TEnum result);
			}
		}
	}
}
