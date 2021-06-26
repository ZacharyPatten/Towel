using System;
using System.Reflection;

namespace Towel
{
	/// <summary>A value-based "tag" attribute.</summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class TagAttribute : Attribute
	{
		internal object? Tag;
		internal object? Value;

		/// <summary>Creates a new value-based "tag" attribute.</summary>
		/// <param name="tag">The tag.</param>
		/// <param name="value">The value.</param>
		public TagAttribute(object? tag, object? value)
		{
			Tag = tag;
			Value = value;
		}
	}

	/// <summary>Extension methods for reflection types and <see cref="TagAttribute"/>.</summary>
	public static class TagAttributeExtensions
	{
		/// <summary>Gets a <see cref="TagAttribute"/> on a <see cref="MemberInfo"/>.</summary>
		/// <param name="memberInfo">The type to get the <see cref="TagAttribute"/> of.</param>
		/// <param name="tag">The tag to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the tag was found; False if not or if multiple tags were found (ambiguous).</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object? Value) GetTag(this MemberInfo memberInfo, object? tag)
		{
			_ = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
			bool found = false;
			object? value = default;
			foreach (TagAttribute valueAttribute in memberInfo.GetCustomAttributes<TagAttribute>())
			{
				if (ReferenceEquals(tag, valueAttribute.Tag) || (tag is not null && tag.Equals(valueAttribute.Tag)))
				{
					if (found)
					{
						return (false, default);
					}
					found = true;
					value = valueAttribute.Value;
				}
			}
			return (found, value);
		}

		/// <summary>Gets a <see cref="TagAttribute"/> on a <see cref="ParameterInfo"/>.</summary>
		/// <param name="parameterInfo">The type to get the <see cref="TagAttribute"/> of.</param>
		/// <param name="tag">The tag to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the tag was found; False if not or if multiple tags were found (ambiguous).</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object? Value) GetTag(this ParameterInfo parameterInfo, object? tag)
		{
			_ = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
			bool found = false;
			object? value = default;
			foreach (TagAttribute valueAttribute in parameterInfo.GetCustomAttributes<TagAttribute>())
			{
				if (ReferenceEquals(tag, valueAttribute.Tag) || (tag is not null && tag.Equals(valueAttribute.Tag)))
				{
					if (found)
					{
						return (false, default);
					}
					found = true;
					value = valueAttribute.Value;
				}
			}
			return (found, value);
		}
	}
}
