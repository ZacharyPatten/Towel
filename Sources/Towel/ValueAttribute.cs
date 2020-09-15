using System;
using System.Collections.Generic;
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
		internal readonly static Dictionary<object, Dictionary<object, object>> Cache = new Dictionary<object, Dictionary<object, object>>();

		internal static void LoadCache(object member, IEnumerable<ValueAttribute> valueAttributes)
		{
			lock (Cache)
			{
				foreach (ValueAttribute valueAttribute in valueAttributes)
				{
					if (!Cache.TryGetValue(member, out Dictionary<object, object> attributes))
					{
						attributes = new Dictionary<object, object>();
						Cache.Add(member, attributes);
					}
					attributes.TryAdd(valueAttribute.Attribute, valueAttribute.Value);
				}
			}
		}

		internal static bool TryGetValue(object member, object attribute, out object value)
		{
			lock (Cache)
			{
				if (Cache.TryGetValue(member, out Dictionary<object, object> attributes))
				{
					return attributes.TryGetValue(attribute, out value);
				}
				value = null;
				return false;
			}
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="Type"/>.</summary>
		/// <param name="type">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this Type type, object attribute)
		{
			_ = type ?? throw new ArgumentNullException(nameof(type));
			if (!TryGetValue(type, attribute, out object value))
			{
				LoadCache(type, type.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(type, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="MethodInfo"/>.</summary>
		/// <param name="methodInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this MethodInfo methodInfo, object attribute)
		{
			_ = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
			if (!TryGetValue(methodInfo, attribute, out object value))
			{
				LoadCache(methodInfo, methodInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(methodInfo, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="PropertyInfo"/>.</summary>
		/// <param name="propertyInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this PropertyInfo propertyInfo, object attribute)
		{
			_ = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
			if (!TryGetValue(propertyInfo, attribute, out object value))
			{
				LoadCache(propertyInfo, propertyInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(propertyInfo, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="FieldInfo"/>.</summary>
		/// <param name="fieldInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this FieldInfo fieldInfo, object attribute)
		{
			_ = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
			if (!TryGetValue(fieldInfo, attribute, out object value))
			{
				LoadCache(fieldInfo, fieldInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(fieldInfo, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="ConstructorInfo"/>.</summary>
		/// <param name="constructorInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this ConstructorInfo constructorInfo, object attribute)
		{
			_ = constructorInfo ?? throw new ArgumentNullException(nameof(constructorInfo));
			if (!TryGetValue(constructorInfo, attribute, out object value))
			{
				LoadCache(constructorInfo, constructorInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(constructorInfo, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="EventInfo"/>.</summary>
		/// <param name="eventInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this EventInfo eventInfo, object attribute)
		{
			_ = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
			if (!TryGetValue(eventInfo, attribute, out object value))
			{
				LoadCache(eventInfo, eventInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(eventInfo, attribute, out value) ? (true, value) : (false, null);
		}

		/// <summary>Gets a <see cref="ValueAttribute"/> on a <see cref="ParameterInfo"/>.</summary>
		/// <param name="parameterInfo">The type to get the <see cref="ValueAttribute"/> of.</param>
		/// <param name="attribute">The attribute to get the value of.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="object"/> Value)
		/// <para>- <see cref="bool"/> Found: True if the attribute was found; False if not.</para>
		/// <para>- <see cref="object"/> Value: The value if found or default if not.</para>
		/// </returns>
		public static (bool Found, object Value) GetValueAttribute(this ParameterInfo parameterInfo, object attribute)
		{
			_ = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
			if (!TryGetValue(parameterInfo, attribute, out object value))
			{
				LoadCache(parameterInfo, parameterInfo.GetCustomAttributes<ValueAttribute>());
			}
			else
			{
				return (true, value);
			}
			return TryGetValue(parameterInfo, attribute, out value) ? (true, value) : (false, null);
		}
	}
}
