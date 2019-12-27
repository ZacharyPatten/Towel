using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using Towel;
using static Towel.Syntax;
using Towel.DataStructures;

namespace Towel.Graphics.Vulkan_Generator
{
	#region Type Declarations

	class VulkanCommand
	{
		internal int LineNumber;
		internal string Name;
		internal string ReturnType;
		internal IList<VulkanCommandParameter> Parameters;
	}

	class VulkanCommandParameter
	{
		internal int LineNumber;
		internal string Name;
		internal string Type;
	}

	class VulkanEnum
	{
		internal int LineNumber;
		internal string Name;
		internal IList<VulkanEnumValue> Values;
	}

	class VulkanEnumValue
	{
		internal int LineNumber;
		internal string Name;
		internal int Value;
	}

	class VulkanType
	{
		internal int LineNumber;
		internal string Name;
		internal string Content;
		internal string Category;
		internal IList<VulkanTypeMember> Members;
	}

	class VulkanTypeMember
	{
		internal int LineNumber;
		internal string Name;
		internal string Type;
	}

	#endregion

	static class Program
	{
		static readonly IList<VulkanEnum> VulkanEnums = new ListArray<VulkanEnum>();
		static readonly IList<VulkanCommand> VulkanCommands = new ListArray<VulkanCommand>();
		static readonly IList<VulkanType> VulkanTypes = new ListArray<VulkanType>();

		static void Main()
		{
			Console.WriteLine("This is still in heavy development. It is not ready to use...");
			Console.WriteLine("It is the work-in-progress of a generator for a Vulkan wrapper in C#.");
			Console.ReadLine();
			return;

			ParseVulkanXmlFile();
			InterpretData();
			GenerateVulkanWrapper();
		}

		#region 1) Parse

		static void ParseVulkanXmlFile()
		{
			int errorCount = 0;
			StringBuilder stringBuilder = new StringBuilder();

			#region Embedded Resource

			//Assembly assembly = Assembly.GetExecutingAssembly();
			//string resource = assembly.GetName().Name + ".vk.xml";
			//using Stream stream = assembly.GetManifestResourceStream(resource);
			//if (stream is null)
			//{
			//	Console.WriteLine("error " + ++errorCount + ": could not find " + resource + " embedded resource");
			//	return;
			//}
			//using StreamReader streamReader = new StreamReader(stream);
			//using XmlReader xmlReader = XmlReader.Create(streamReader);

			#endregion

			#region WebRequest

			var webRequest = WebRequest.Create(@"https://raw.githubusercontent.com/KhronosGroup/Vulkan-Docs/master/xml/vk.xml");
			using WebResponse webResponse = webRequest.GetResponse();
			using Stream stream = webResponse.GetResponseStream();
			using StreamReader streamReader = new StreamReader(stream);
			using XmlReader xmlReader = XmlReader.Create(streamReader);

			#endregion

			Console.WriteLine("File Found (vk.xml)...");

			#region Helpers

			int LineNumber() => ((IXmlLineInfo)xmlReader).LineNumber;

			#endregion

			bool vulkanRegistryFound = false;
			bool insideVulkanTypes = false;
			bool insideVulkanCommands = false;
			VulkanCommand currentVulkanCommand = null;
			VulkanEnum currentVulkanEnum = null;
			VulkanType currentVulkanType = null;
			VulkanTypeMember currentVulkanTypeMember = null;

			while (xmlReader.Read())
			{
				#region registry

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "registry")
				{
					vulkanRegistryFound = true;
					continue;
				}

				#endregion

				#region xml

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "xml")
				{
					continue;
				}

				#endregion

				#region comment

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "comment")
				{
					continue;
				}

				#endregion

				#region platforms

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "platforms")
				{
					continue;
				}

				#endregion

				#region platform

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "platform")
				{
					continue;
				}

				#endregion

				#region tags

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "tags")
				{
					continue;
				}

				#endregion

				#region tag

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "tag")
				{
					continue;
				}

				#endregion

				#region types

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "types")
				{
					insideVulkanTypes = true;
					continue;
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "types")
				{
					insideVulkanTypes = false;
					continue;
				}

				#endregion

				#region type

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "type")
				{
					try
					{
						if (!insideVulkanTypes && currentVulkanCommand is null)
						{
							// TODO: error when type/command logic is fixed
							//stringBuilder.AppendLine("error " + ++errorCount + " line "+ LineNumber() + ": type declared outside types/command member");
							continue;
						}

						if (!(currentVulkanCommand is null))
						{
							string raw_value = xmlReader.Value;
							if (!(raw_value is null))
							{
								currentVulkanCommand.ReturnType = raw_value;
								continue;
							}
						}

						//if (!(currentVulkanType is null))
						//{
						//	// TODO: kill this
						//	currentVulkanType = null;
						//	//stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": type declared inside another type");
						//	//continue;
						//}

						string raw_name = xmlReader["name"];
						string raw_content = xmlReader["content"];
						string raw_category = xmlReader["category"];
						if (!(raw_content is null))
						{
							stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": type missing content attribute");
							continue;
						}
						currentVulkanType = new VulkanType()
						{
							LineNumber = LineNumber(),
							Name = raw_name,
							Content = raw_content,
							Category = raw_category,
							Members = new ListArray<VulkanTypeMember>()
						};
						VulkanTypes.Add(currentVulkanType);
						continue;
					}
					finally
					{
						if (xmlReader.IsEmptyElement)
						{
							currentVulkanType = null;
						}
					}
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "type")
				{
					currentVulkanType = null;
					continue;
				}

				#endregion

				#region enums

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "enums")// && xmlReader["type"] == "enum")
				{
					try
					{
						string raw_name = xmlReader["name"];
						if (raw_name is null)
						{
							stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": enums missing name attribute");
						}
						else
						{
							currentVulkanEnum = new VulkanEnum()
							{
								LineNumber = LineNumber(),
								Name = raw_name,
								Values = new ListArray<VulkanEnumValue>(),
							};
							VulkanEnums.Add(currentVulkanEnum);
						}
						continue;
					}
					finally
					{
						if (xmlReader.IsEmptyElement)
						{
							currentVulkanEnum = null;
						}
					}
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "enums")// && xmlReader["type"] == "enum")
				{
					currentVulkanEnum = null;
				}

				#endregion

				#region enum

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "enum")
				{
					if (currentVulkanEnum is null)
					{
						//stringBuilder.AppendLine("error " + ++errorCount + " line "+ LineNumber() + ": enum declared outside enums member");
						continue;
					}

					string raw_name = xmlReader["name"];
					string raw_value = xmlReader["value"];
					string raw_bitpos = xmlReader["bitpos"];
					string raw_alias = xmlReader["alias"];

					if (!(raw_alias is null))
					{
						// ignored alias
						continue;
					}
					if (raw_name is null)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": enum missing name attribute [" + currentVulkanEnum + "]");
						continue;
					}
					if (!(raw_value is null) && !(raw_bitpos is null))
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": enum contains both value and bitpos attributes [" + currentVulkanEnum + "." + raw_name + "]");
						continue;
					}
					if (raw_value is null && raw_bitpos is null)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": enum missing value/bitpos attribute [" + currentVulkanEnum + "." + raw_name + "]");
						continue;
					}

					string valueString = raw_value ?? raw_bitpos;
					bool isHexadecimal = Regex.Match(valueString, "0[xX][0-9a-fA-F]+").Success;
					int value;
					if (isHexadecimal)
					{
						valueString = valueString.Substring(2);
					}
					bool success = isHexadecimal
						? int.TryParse(valueString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value)
						: int.TryParse(valueString, out value);
					if (!success)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": failed to parse value of enum [" + currentVulkanEnum + "." + raw_name + "]");
						continue;
					}
					currentVulkanEnum.Values.Add(new VulkanEnumValue()
					{
						LineNumber = LineNumber(),
						Name = raw_name,
						Value = value,
					});
					continue;
				}

				#endregion

				#region name

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "name")
				{
					// TODO: error logic

					if (!(currentVulkanTypeMember is null))
					{
						string raw_value = xmlReader.Value;

						if (string.IsNullOrWhiteSpace(raw_value))
						{
							raw_value = xmlReader.ReadInnerXml();
						}

						currentVulkanTypeMember.Name = raw_value;

						if (string.IsNullOrWhiteSpace(raw_value))
						{
							Console.WriteLine("Break");
						}

						continue;
					}

					if (!(currentVulkanCommand is null))
					{
						string raw_value = xmlReader.Value;
						currentVulkanCommand.Name = raw_value;
						continue;
					}

					if (!(currentVulkanType is null))
					{
						string raw_value = xmlReader.ReadInnerXml();

						if (raw_value is null || raw_value == string.Empty)
						{
							Console.WriteLine("Break");
						}

						currentVulkanType.Name = raw_value;
						continue;
					}
					
					


					// TODO: other

					continue;
				}

				#endregion

				#region member

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
				{
					try
					{
						if (!(currentVulkanTypeMember is null))
						{
							stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": member declared inside another member");
							//continue;
						}
						if (currentVulkanType is null)
						{
							stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": member declared outside a type");
							//continue;
						}
						currentVulkanTypeMember = new VulkanTypeMember()
						{
							LineNumber = LineNumber(),
						};
						if (!(currentVulkanType is null))
						{
							currentVulkanType.Members.Add(currentVulkanTypeMember);
							continue;
						}
						continue;
					}
					finally
					{
						if (xmlReader.IsEmptyElement)
						{
							currentVulkanTypeMember = null;
						}
					}
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "member")
				{
					currentVulkanTypeMember = null;
					continue;
				}

				#endregion

				#region unused

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "unused")
				{
					continue;
				}

				#endregion

				#region commands

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "commands")
				{
					insideVulkanCommands = true;
					continue;
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "commands")
				{
					insideVulkanCommands = false;
					continue;
				}

				#endregion

				#region command

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "command")
				{
					try
					{
						if (!insideVulkanCommands)
						{
							stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": command declared outside commands member");
							continue;
						}
						currentVulkanCommand = new VulkanCommand()
						{
							LineNumber = LineNumber(),
							Parameters = new ListArray<VulkanCommandParameter>()
						};
						VulkanCommands.Add(currentVulkanCommand);
						continue;
					}
					finally
					{
						currentVulkanCommand = null;
					}
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "command")
				{
					currentVulkanCommand = null;
					continue;
				}

				#endregion

				#region proto

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "proto")
				{
					continue;
				}

				#endregion

				#region param

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "param")
				{
					continue;
				}

				#endregion

				#region implicitexternsyncparams

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "implicitexternsyncparams")
				{
					continue;
				}

				#endregion

				#region feature

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "feature")
				{
					continue;
				}

				#endregion

				#region require

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "require")
				{
					continue;
				}

				#endregion

				#region extensions

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "extensions")
				{
					continue;
				}

				#endregion

				#region extension

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "extension")
				{
					continue;
				}

				#endregion

				if (xmlReader.NodeType == XmlNodeType.Element && !(xmlReader.Name is null))
				{
					stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": enum missing name attribute [" + currentVulkanEnum + "]");
				}
			}

			if (!vulkanRegistryFound)
			{
				stringBuilder.AppendLine("error " + ++errorCount + " line " + LineNumber() + ": registry not found");
			}

			File.WriteAllText("log.txt", stringBuilder.ToString());
			Console.WriteLine("Error Count: " + errorCount);
			Console.WriteLine("File Parsing Complete...");
		}

		#endregion

		#region 2) Interpret

		static void InterpretData()
		{
			Console.WriteLine("Interpretation Started...");

			static string InterpretIdentifier(string identifier) => identifier is null
				? null
				: identifier.Replace(' ', '_');

			VulkanEnums.Stepper(Enum =>
			{
				Enum.Name = InterpretIdentifier(Enum.Name);
				Enum.Values.Stepper(Value =>
				{
					Value.Name = InterpretIdentifier(Value.Name);
				});
			});
			VulkanCommands.Stepper(Command =>
			{
				Command.Name = InterpretIdentifier(Command.Name);
				Command.Parameters.Stepper(Parameter =>
				{
					Parameter.Name = InterpretIdentifier(Parameter.Name);
				});
			});
			VulkanTypes.Stepper(Type =>
			{
				Type.Name = InterpretIdentifier(Type.Name);
				Type.Members.Stepper(Member =>
				{
					Member.Name = InterpretIdentifier(Member.Name);
				});
			});

			Console.WriteLine("Interpretation Complete...");
		}

		#endregion

		#region 3) Generate

		static void GenerateVulkanWrapper()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("//-----------------------------------------------------------------------------------");
			stringBuilder.AppendLine("// <auto-generated>");
			stringBuilder.AppendLine("//	This code was generated from the \"Tools\\Towel.Graphics.Vulkan_Generator\" project.");
			stringBuilder.AppendLine("// </auto-generated>");
			stringBuilder.AppendLine("//-----------------------------------------------------------------------------------");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(@"namespace Towel {");
			VulkanEnums.Stepper(Enum =>
			{
				stringBuilder.AppendLine("\tpublic enum " + Enum.Name + " { // Line Number: " + Enum.LineNumber);
				Enum.Values.Stepper(Value =>
				{
					stringBuilder.AppendLine("\t\t" + Value.Name + " = " + Value.Value + ", // Line Number: " + Value.LineNumber);
				});
				stringBuilder.AppendLine("\t}");
			});
			VulkanCommands.Stepper(Command =>
			{

			});
			VulkanTypes.Stepper(Type =>
			{
				if (!(Type.Name is null || Type.Name == string.Empty))
				{
					stringBuilder.AppendLine("\tpublic struct " + Type.Name + " { // Line Number: " + Type.LineNumber);
					Type.Members.Stepper(Member =>
					{
						stringBuilder.AppendLine("\t\t" + Member.Type + " " + Member.Name + "; // Line Number: " + Member.LineNumber);
					});
					stringBuilder.AppendLine("\t}");
				}
			});
			stringBuilder.AppendLine(@"}");
			File.WriteAllText("output.cs", stringBuilder.ToString());
		}

		#endregion
	}
}
