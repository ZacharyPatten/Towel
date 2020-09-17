using System;
using System.Reflection;

namespace Towel
{
	/// <summary>A value-based attribute.</summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class ValueAttribute : Attribute
	{
		internal object Attribute;
		internal object Value;

		/// <summary>Creates a new value-based attribute.</summary>
		/// <param name="attribute">The attribute.</param>
		/// <param name="value">The value.</param>
		public ValueAttribute(object attribute, object value)
		{
			Attribute = attribute;
			Value = value;
		}
	}

	/// <summary>Extension methods for reflection types and <see cref="ValueAttribute"/>.</summary>
	public static class ValueAttributeExtensions
	{
		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="MemberInfo"/>.</summary>
		/// <param name="memberInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not or if multiple attributes were found (ambiguous).</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this MemberInfo memberInfo, object attribute)
		{
			_ = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
			bool found = false;
			object value = default;
			foreach (ValueAttribute valueAttribute in memberInfo.GetCustomAttributes<ValueAttribute>())
			{
				if (attribute.Equals(valueAttribute.Attribute))
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

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="ParameterInfo"/>.</summary>
		/// <param name="parameterInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not or if multiple attributes were found (ambiguous).</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this ParameterInfo parameterInfo, object attribute)
		{
			_ = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
			bool found = false;
			object value = default;
			foreach (ValueAttribute valueAttribute in parameterInfo.GetCustomAttributes<ValueAttribute>())
			{
				if (attribute.Equals(valueAttribute.Attribute))
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
