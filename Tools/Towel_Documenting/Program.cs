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

namespace Towel_Documenting
{
	class Program
	{
		static void Main()
		{
			// This program generates HTML from the XML documentation of the code in the Towel project.

			// Note: Explicitly loading the XML file is not necessary if the XML file
			// is in the same location as the referenced DLL. In this case it was necessary.
			string Towel_xml_Path = @"..\..\..\..\..\Sources\Towel\Towel.xml";
			using (StreamReader reader = new(Towel_xml_Path))
			{
				Meta.LoadXmlDocumentation(reader);
			}
			Assembly assembly = typeof(Towel.Extensions).Assembly;

			#region Build Namespace Tree

			List<Namespace> rootNamespaces = new();
			Dictionary<string, Namespace> namespaceMap = new();

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

			StringBuilder output = new();

			#region CSS

			string css =
@".collapsible {
  background-color: #777;
  color: white;
  cursor: pointer;
  padding: 18px;
  width: 100%;
  border: none;
  text-align: left;
  outline: none;
  font-size: 15px;
}

.active, .collapsible:hover {
  background-color: #555;
}

.class.collapsible {
	background-color: #4ec994;
}

.active.class.collapsible, .class.collapsible:hover {
	background-color: #347F5D;
}

.struct.collapsible {
	background-color: #FFFF72;
}

.active.struct.collapsible, .struct.collapsible:hover {
	background-color: #A1A54F;
}

.enum.collapsible {
	background-color: #FF8040;
}

.active.enum.collapsible, .enum.collapsible:hover {
	background-color: #AD5C30;
}

.interface.collapsible {
	background-color: #b8d7a3;
}

.active.interface.collapsible, .interface.collapsible:hover {
	background-color: #b8d7a3;
}

.delegate.collapsible {
	background-color: #BD63C5;
}

.active.delegate.collapsible, .delegate.collapsible:hover {
	background-color: #723D77;
}

.content {
  padding: 0 18px;
  display: none;
  overflow: hidden;
  background-color: #f1f1f1;
}";

			#endregion

			#region JS

			string js =
@"var coll = document.getElementsByClassName('collapsible');
var i;

for (i = 0; i < coll.length; i++)
{
	coll[i].addEventListener('click', function() {
		this.classList.toggle('active');
		var content = this.nextElementSibling;
		if (content.style.display === 'block')
		{
			content.style.display = 'none';
		}
		else
		{
			content.style.display = 'block';
		}
	});
}";

			#endregion

			#region Convert To HTML

			output.AppendLine("<!DOCTYPE html>");
			output.AppendLine("<html>");
			output.AppendLine("<head>");

			output.AppendLine("<style>");
			output.AppendLine(css);
			output.AppendLine("</style>");

			output.AppendLine("</head>");
			output.AppendLine("<body>");

			rootNamespaces.Sort((a, b) => a.Item1.CompareTo(b.Item1));
			foreach (Namespace @namespace in rootNamespaces)
			{
				ConvertNamespaceToHtml(@namespace);
			}

			output.AppendLine("<script>");
			output.AppendLine(js);
			output.AppendLine("</script>");

			output.AppendLine("</body>");
			output.AppendLine("</html>");

			void ConvertNamespaceToHtml(Namespace @namespace)
			{
				output.Append("<button class='collapsible namespace'>");
				output.Append(@namespace.Item1 + " [Namespace]");
				output.AppendLine("</button>");

				output.AppendLine("<div class='content'>");

				// Namespaces
				@namespace.Item2.Sort();
				@namespace.Item2.ForEach(x => ConvertNamespaceToHtml(namespaceMap[x]));

				// Types
				@namespace.Item3.Sort((a, b) => a.Name.CompareTo(b.Name));
				@namespace.Item3.ForEach(ConvertTypeToHtml);

				output.AppendLine("</div>");
			}
			void ConvertTypeToHtml(Type type)
			{
				string typeToString = type.ConvertToCSharpSource(true);
				if (!string.IsNullOrEmpty(typeToString))
				{
					string typeString =
						type.IsInterface ? "interface" :
						type.IsEnum ? "enum" :
						type.IsValueType ? "struct" :
						typeof(MulticastDelegate).IsAssignableFrom(type.BaseType) ? "delegate" :
						type.IsClass ? "class" :
						throw new NotImplementedException();

					output.Append("<button class='collapsible type " + typeString + "'>");
					output.Append(HttpUtility.HtmlEncode(typeToString[(typeToString.LastIndexOf('.') + 1)..] + " [" + typeString + "]"));
					output.AppendLine("</button>");

					output.AppendLine("<div class='content'>");
					output.AppendLine(HttpUtility.HtmlEncode(type.GetDocumentation()));

					// Nested Types
					List<Type> nestedTypes = type.GetNestedTypes().ToList();
					nestedTypes.Sort((a, b) => a.Name.CompareTo(b.Name));
					nestedTypes.ForEach(ConvertTypeToHtml);

					// Fields
					foreach (FieldInfo fieldInfo in type.GetFields().Where(x => 
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null)))
					{
						output.Append("<button class='collapsible field'>");
						output.Append(fieldInfo.Name + " [Field]");
						output.AppendLine("</button>");

						output.AppendLine("<div class='content'>");
						output.AppendLine(HttpUtility.HtmlEncode(fieldInfo.GetDocumentation()));
						output.AppendLine("</div>");
					}

					// Properties
					foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null)))
					{
						output.Append("<button class='collapsible property'>");
						output.Append(propertyInfo.Name + " [Property]");
						output.AppendLine("</button>");

						output.AppendLine("<div class='content'>");
						output.AppendLine(HttpUtility.HtmlEncode(propertyInfo.GetDocumentation()));
						output.AppendLine("</div>");
					}

					// Constructors
					foreach (ConstructorInfo constructorInfo in type.GetConstructors().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null) &&
						!typeof(MulticastDelegate).IsAssignableFrom(x.DeclaringType.BaseType)))
					{
						output.Append("<button class='collapsible constructor'>");
						output.Append(constructorInfo.Name + " [Constructor]");
						output.AppendLine("</button>");

						output.AppendLine("<div class='content'>");
						output.AppendLine(HttpUtility.HtmlEncode(constructorInfo.GetDocumentation()));
						output.AppendLine("</div>");
					}

					// Methods
					foreach (MethodInfo methodInfo in type.GetMethods().Where(x =>
						x.DeclaringType == type &&
						!(x.DeclaringType.FullName is null) &&
						!typeof(MulticastDelegate).IsAssignableFrom(x.DeclaringType.BaseType) &&
						!x.IsConstructor &&
						!x.IsSpecialName &&
						!x.IsLocalFunction()))
					{
						output.Append("<button class='collapsible method'>");
						output.Append(methodInfo.Name + " [Method]");
						output.AppendLine("</button>");

						output.AppendLine("<div class='content'>");
						output.AppendLine(HttpUtility.HtmlEncode(methodInfo.GetDocumentation()));
						output.AppendLine("</div>");
					}

					output.AppendLine("</div>");
				}
			}

			#endregion

			File.WriteAllText("TowelDocumentation.html", output.ToString());
		}
	}
}
