using System.Reflection;

namespace Towel_Testing;

[TestClass]
public class XML_Methods
{
	[TestMethod]
	public void Test()
	{
		foreach (Type type in typeof(Statics).Assembly.GetTypes())
		{
			foreach (MethodInfo method in type.GetMethods().Where(m => m.Name.StartsWith("XML_")))
			{
				Assert.IsTrue(method.IsStatic, method.Name);
				Assert.IsTrue(method.IsPublic, method.Name);
				Assert.IsTrue(!method.IsGenericMethod, method.Name);
				Assert.IsTrue(method.ReturnType == typeof(void), method.Name);
				Assert.IsTrue(method.GetParameters().Length is 0, method.Name);
				ObsoleteAttribute? obsolete = method.GetCustomAttribute<ObsoleteAttribute>();
				Assert.IsTrue(obsolete is not null, method.Name);
				Assert.IsTrue(obsolete!.IsError, method.Name);
				Assert.IsTrue(obsolete!.Message is NotIntended, method.Name);
				try
				{
					MethodInfo invoke = method;
					if (method.DeclaringType!.IsGenericType)
					{
						Type declaringType = method.DeclaringType;
						Type[] generics = declaringType.GetGenericArguments();
						for (int i = 0; i < generics.Length; i++)
						{
							if (generics[i].GetGenericParameterConstraints().Length > 1)
							{
								generics[i] = generics[i].GetGenericParameterConstraints()[^1];
							}
							else
							{
								generics[i] = typeof(int);
							}
						}
						declaringType = declaringType.MakeGenericType(generics);
						invoke = declaringType.GetMethods().FirstOrDefault(m => m.MetadataToken == method.MetadataToken)!;
					}
					invoke.Invoke(null, null);
					Assert.Fail(method.Name);
				}
				catch (TargetInvocationException exception)
				{
					Assert.IsTrue(exception.InnerException is not null, method.Name);
					Assert.IsTrue(exception.InnerException is DocumentationMethodException, method.Name);
				}
			}
		}
	}
}
