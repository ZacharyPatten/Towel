using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

using Namespace = 
    System.ValueTuple<
        string,
        System.Collections.Generic.List<string>,
        System.Collections.Generic.List<System.Type>>;

namespace Towel_Documentation
{
    class Program
    {
        static void Main()
        {
            // This program generates HTML from the XML documentation of the code in the Towel project.

            TowelSystemExtensions.LoadXmlDocumentation(File.ReadAllText(@"..\..\..\..\..\Sources\Towel\Towel.xml"));
            Assembly assembly = typeof(Towel.Stepper).Assembly;
            Type[] exportedTypes = assembly.GetExportedTypes();

            // First Pass: Build Namespace Hierarchy
            List<Namespace> namespaceHierarchy = new List<Namespace>();
            Dictionary<string, Namespace> namespaceMap = new Dictionary<string, Namespace>();
            foreach (string fullNamespace in exportedTypes.Select(x => x.Namespace).Distinct())
            {
                if (!namespaceMap.ContainsKey(fullNamespace))
                {
                    string[] namespaces = fullNamespace.Split('.');
                    string currentFullNamespace = null;
                    Namespace? parent = null;
                    foreach (string @namespace in namespaces)
                    {
                        currentFullNamespace = currentFullNamespace is null ? @namespace :
                            currentFullNamespace + "." + @namespace;
                        if (!namespaceMap.TryGetValue(currentFullNamespace, out Namespace current))
                        {
                            current = (@namespace, new List<string>(), new List<Type>());
                            if (parent.HasValue)
                            {
                                parent.Value.Item2.Add(currentFullNamespace);
                            }
                            else
                            {
                                namespaceHierarchy.Add(current);
                            }
                            namespaceMap[currentFullNamespace] = current;
                        }
                        parent = current;
                    }
                }
            }

            // Second Pass: Add Types To the Namespace Hierarchy
            foreach (Type type in exportedTypes)
            {
                if (!type.IsNested)
                {
                    namespaceMap[type.Namespace].Item3.Add(type);
                }
            }

            // Third Pass: Convert The Namespace Hierarchy To HTML (And Sort Alphabetically)
            StringBuilder stringBuilder = new StringBuilder();
            namespaceHierarchy.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            foreach (Namespace @namespace in namespaceHierarchy)
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
                    foreach (FieldInfo fieldInfo in type.GetFields().Where(x => x.DeclaringType == type))
                    {
                        if (!(fieldInfo.DeclaringType.FullName is null))
                        {
                            stringBuilder.AppendLine("<li class=\"field\">");
                            stringBuilder.AppendLine(fieldInfo.Name);
                            stringBuilder.AppendLine(HttpUtility.HtmlEncode(fieldInfo.GetDocumentation()));
                            stringBuilder.AppendLine("</li>");
                        }
                    }

                    // Properties
                    foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x => x.DeclaringType == type))
                    {
                        if (!(propertyInfo.DeclaringType.FullName is null))
                        {
                            stringBuilder.AppendLine("<li class=\"property\">");
                            stringBuilder.AppendLine(propertyInfo.Name);
                            stringBuilder.AppendLine(HttpUtility.HtmlEncode(propertyInfo.GetDocumentation()));
                            stringBuilder.AppendLine("</li>");
                        }
                    }

                    // Constructors
                    foreach (ConstructorInfo constructorInfo in type.GetConstructors().Where(x => x.DeclaringType == type))
                    {
                        if (!(constructorInfo.DeclaringType.FullName is null))
                        {
                            stringBuilder.AppendLine("<li class=\"constructor\">");
                            stringBuilder.AppendLine(constructorInfo.Name);
                            stringBuilder.AppendLine(HttpUtility.HtmlEncode(constructorInfo.GetDocumentation()));
                            stringBuilder.AppendLine("</li>");
                        }
                    }

                    // Methods
                    foreach (MethodInfo methodInfo in type.GetMethods().Where(x => x.DeclaringType == type))
                    {
                        if (!(methodInfo.DeclaringType.FullName is null))
                        {
                            stringBuilder.AppendLine("<li class=\"method\">");
                            stringBuilder.AppendLine(methodInfo.Name);
                            stringBuilder.AppendLine(HttpUtility.HtmlEncode(methodInfo.GetDocumentation()));
                            stringBuilder.AppendLine("</li>");
                        }
                    }

                    stringBuilder.AppendLine("</ul>");
                    stringBuilder.AppendLine("</li>");
                }
            }

            File.WriteAllText("TowelDocumentation.html", stringBuilder.ToString());
        }
    }
}
