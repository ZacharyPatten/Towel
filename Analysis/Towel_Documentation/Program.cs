using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Towel;

using Namespace =
	System.ValueTuple<
		string, // namespace
		System.Collections.Generic.List<string>, // nested namespaces
		System.Collections.Generic.List<System.Type>>; // nested types

namespace Towel_Documentation
{
	class Program
	{
		static void Main()
		{
			// This program generates HTML from the XML documentation of the code in the Towel project.

			// Note: Explicitly loading the XML file is not necessary if the XML file
			// is in the same location as the referenced DLL. In this case it was necessary.
			string Towel_xml_Path = @"..\..\..\..\..\Sources\Towel\Towel.xml";
			using (StreamReader reader = new StreamReader(Towel_xml_Path))
			{
				TowelDotNetExtensions.LoadXmlDocumentation(reader);
			}
			Assembly assembly = typeof(Towel.Stepper).Assembly;

			#region Build Namespace Tree

			List<Namespace> rootNamespaces = new List<Namespace>();
			Dictionary<string, Namespace> namespaceMap = new Dictionary<string, Namespace>();

			void HandleNamespace(string @namespace)
			{
				if (namespaceMap.ContainsKey(@namespace))
				{
					return;
				}
				string parentNameSpace = @namespace.Contains('.')
					?  @namespace.Substring(0, @namespace.LastIndexOf("."))
					: null;
				Namespace namespaceTuple = (@namespace, new List<string>(), new List<Type>());
				namespaceMap[@namespace] = namespaceTuple;
				if (!(parentNameSpace is null))
				{
					HandleNamespace(parentNameSpace);
					namespaceMap[parentNameSpace].Item2.Add(@namespace);
				}
				else
				{
					rootNamespaces.Add(namespaceTuple);
				}
			}

			Type[] exportedTypes = assembly.GetExportedTypes();
			foreach (Type type in exportedTypes)
			{
				string @namespace = type.Namespace;
				HandleNamespace(@namespace);
				namespaceMap[@namespace].Item3.Add(type);
			}

			#endregion

			#region Convert To HTML

			StringBuilder stringBuilder = new StringBuilder();
			rootNamespaces.Sort((a, b) => a.Item1.CompareTo(b.Item1));
			foreach (Namespace @namespace in rootNamespaces)
			{
				ConvertNamespaceToHtml(@namespace);
			}
			void ConvertNamespaceToHtml(Namespace @namespace)
			{
				stringBuilder.Append("<li class=\"namespace\">");
				stringBuilder.AppendLine(@namespace.Item1);
				stringBuilder.AppendLine("<ul>");

				// Namespaces
				@namespace.Item2.Sort();
				@namespace.Item2.ForEach(x => ConvertNamespaceToHtml(namespaceMap[x]));

				// Types
				@namespace.Item3.Sort((a, b) => a.Name.CompareTo(b.Name));
				@namespace.Item3.ForEach(ConvertTypeToHtml);

				stringBuilder.AppendLine("</ul>");
				stringBuilder.AppendLine("</li>");
			}
			void ConvertTypeToHtml(Type type)
			{
				string typeToString = type.ConvertToCsharpSource();
				if (!string.IsNullOrEmpty(typeToString))
				{
					stringBuilder.Append("<li class=\"type\">");
					stringBuilder.AppendLine(typeToString.Substring(typeToString.LastIndexOf('.') + 1));
					stringBuilder.AppendLine(HttpUtility.HtmlEncode(type.GetDocumentation()));
					stringBuilder.AppendLine("<ul>");

					// Nested Types
					List<Type> nestedTypes = type.GetNestedTypes().ToList();
					nestedTypes.Sort((a, b) => a.Name.CompareTo(b.Name));
					nestedTypes.ForEach(ConvertTypeToHtml);

					// Fields
					foreach (FieldInfo fieldInfo in type.GetFields().Where(x => 
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null)))
					{
						stringBuilder.AppendLine("<li class=\"field\">");
						stringBuilder.AppendLine(fieldInfo.Name);
						stringBuilder.AppendLine(HttpUtility.HtmlEncode(fieldInfo.GetDocumentation()));
						stringBuilder.AppendLine("</li>");
					}

					// Properties
					foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null)))
					{
						stringBuilder.AppendLine("<li class=\"property\">");
						stringBuilder.AppendLine(propertyInfo.Name);
						stringBuilder.AppendLine(HttpUtility.HtmlEncode(propertyInfo.GetDocumentation()));
						stringBuilder.AppendLine("</li>");
					}

					// Constructors
					foreach (ConstructorInfo constructorInfo in type.GetConstructors().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null) &&
						!typeof(MulticastDelegate).IsAssignableFrom(x.DeclaringType.BaseType)))
					{
						stringBuilder.AppendLine("<li class=\"constructor\">");
						stringBuilder.AppendLine(constructorInfo.Name);
						stringBuilder.AppendLine(HttpUtility.HtmlEncode(constructorInfo.GetDocumentation()));
						stringBuilder.AppendLine("</li>");
					}

					// Methods
					foreach (MethodInfo methodInfo in type.GetMethods().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null) &&
						!typeof(MulticastDelegate).IsAssignableFrom(x.DeclaringType.BaseType) &&
						!x.IsConstructor &&
						!x.IsSpecialName))
					{
						stringBuilder.AppendLine("<li class=\"method\">");
						stringBuilder.AppendLine(methodInfo.Name);
						stringBuilder.AppendLine(HttpUtility.HtmlEncode(methodInfo.GetDocumentation()));
						stringBuilder.AppendLine("</li>");
					}

					stringBuilder.AppendLine("</ul>");
					stringBuilder.AppendLine("</li>");
				}
			}

			#endregion

			File.WriteAllText("TowelDocumentation.html", stringBuilder.ToString());
		}
	}
}
