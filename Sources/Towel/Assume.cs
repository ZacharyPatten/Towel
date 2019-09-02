using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace Towel
{
	/// <summary>Breaks type safeness by making assumptions about types.</summary>
	public static class Assume
	{
		public static T PropertyGet<T>(object @object, Type type, string propertyName, BindingFlags? bindingFlags) where T : Delegate
		{
			PropertyInfo[] propertyInfos =
				!bindingFlags.HasValue ?
				type.GetProperties() :
				type.GetProperties(bindingFlags.Value);
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				if (propertyInfo.Name.Equals(propertyName))
				{
					try
					{
						return
							@object is null ?
							(T)propertyInfo.GetGetMethod().CreateDelegate(typeof(T)) :
							(T)propertyInfo.GetGetMethod().CreateDelegate(typeof(T), @object);
					}
					catch
					{
						continue;
					}
				}
			}
			return null;
		}

		public static T PropertySet<T>(object @object, Type type, string propertyName, BindingFlags? bindingFlags) where T : Delegate
		{
			PropertyInfo[] propertyInfos =
				!bindingFlags.HasValue ?
				type.GetProperties() :
				type.GetProperties(bindingFlags.Value);
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				if (propertyInfo.Name.Equals(propertyName))
				{
					try
					{
						return
							@object is null ?
							(T)propertyInfo.GetSetMethod().CreateDelegate(typeof(T)) :
							(T)propertyInfo.GetSetMethod().CreateDelegate(typeof(T), @object);
					}
					catch
					{
						continue;
					}
				}
			}
			return null;
		}

		public static T Method<T>(object @object, Type type, string methodName, BindingFlags? bindingFlags) where T : Delegate
		{
			MethodInfo[] methodInfos =
				!bindingFlags.HasValue ?
				type.GetMethods() :
				type.GetMethods(bindingFlags.Value);
			foreach (MethodInfo methodInfo in methodInfos)
			{
				if (methodInfo.Name.Equals(methodName))
				{
					try
					{
						return
							@object is null ?
							(T)methodInfo.CreateDelegate(typeof(T)) :
							(T)methodInfo.CreateDelegate(typeof(T), @object);
					}
					catch
					{
						continue;
					}
				}
			}
			return null;
		}
	}
}
