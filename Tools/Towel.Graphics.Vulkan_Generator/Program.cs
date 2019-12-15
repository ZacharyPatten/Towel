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

using VulkanCommand = Towel.DataStructures.Link<string, string, Towel.DataStructures.IList<Towel.DataStructures.Link<string, string>>>; // name, return type, VulkanCommandParameters
using VulkanCommandParameter = Towel.DataStructures.Link<string, string>; // name, type
using VulkanEnum = Towel.DataStructures.Link<string, Towel.DataStructures.IList<Towel.DataStructures.Link<string, int>>>; // name, VulkanEnumValues
using VulkanEnumValue = Towel.DataStructures.Link<string, int>; // name, value
using VulkanType = Towel.DataStructures.Link<string, string, Towel.DataStructures.IList<Towel.DataStructures.Link<string, string>>>; // name, category, VulkanTypeMembers
using VulkanTypeMember = Towel.DataStructures.Link<string, string>; // name, type

namespace Towel.Graphics.Vulkan_Generator
{
	static class Program
	{
		static readonly IList<VulkanEnum> Enums = new ListArray<VulkanEnum>();
		static readonly IList<VulkanCommand> Commands = new ListArray<VulkanCommand>();
		static readonly IList<VulkanType> VulkanTypes = new ListArray<VulkanType>();

		static void Main()
		{
			Console.WriteLine("This is still in heavy development. It is not ready to use...");
			Console.WriteLine("It is the work-in-progress of a generator for a Vulkan wrapper in C#.");
			Console.ReadLine();
			return;

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

			bool vulkanRegistryFound = false;
			bool insideVulkanTypes = false;
			bool insideVulkanCommands = false;
			VulkanCommand currentVulkanCommand = null;
			VulkanEnum currentVulkanEnum = null;
			VulkanType currentVulkanType = null;

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
					if (!insideVulkanTypes && currentVulkanCommand is null)
					{
						// TODO: error when type/command logic is fixed
						//stringBuilder.AppendLine("error " + ++errorCount + " line "+ ((IXmlLineInfo)xmlReader).LineNumber + ": type declared outside types/command member");
						continue;
					}

					if (!(currentVulkanCommand is null))
					{
						string raw_value = xmlReader.Value;
						if (!(raw_value is null))
						{
							currentVulkanCommand._2 = raw_value;
							continue;
						}
					}

					if (!(currentVulkanType is null))
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": type declared inside another type");
						continue;
					}

					string raw_content = xmlReader["content"];
					if (!(raw_content is null))
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": type missing content attribute");
						continue;
					}

					currentVulkanType = new VulkanType(null, raw_content, new ListArray<VulkanTypeMember>());
					VulkanTypes.Add(currentVulkanType);
					continue;
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
					string raw_name = xmlReader["name"];
					if (raw_name is null)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": enums missing name attribute");
					}
					else
					{
						currentVulkanEnum = new VulkanEnum(raw_name, new ListArray<VulkanEnumValue>());
						Enums.Add(currentVulkanEnum);
					}
					continue;
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
						//stringBuilder.AppendLine("error " + ++errorCount + " line "+ ((IXmlLineInfo)xmlReader).LineNumber + ": enum declared outside enums member");
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
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": enum missing name attribute [" + currentVulkanEnum + "]");
						continue;
					}
					if (!(raw_value is null) && !(raw_bitpos is null))
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": enum contains both value and bitpos attributes [" + currentVulkanEnum + "." + raw_name + "]");
						continue;
					}
					if (raw_value is null && raw_bitpos is null)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": enum missing value/bitpos attribute [" + currentVulkanEnum + "." + raw_name + "]");
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
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": failed to parse value of enum [" + currentVulkanEnum + "." + raw_name + "]");
						continue;
					}

					currentVulkanEnum._2.Add(new VulkanEnumValue(raw_name, value));
					continue;
				}

				#endregion

				#region name

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "name")
				{
					// TODO: error logic

					if (!(currentVulkanCommand is null))
					{
						string raw_value = xmlReader.Value;
						currentVulkanCommand._1 = raw_value;
						continue;
					}

					if (!(currentVulkanType is null))
					{
						string raw_value = xmlReader.Value;
						currentVulkanType._1 = raw_value;
						continue;
					}

					// TODO: other

					continue;
				}

				#endregion

				#region member

				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
				{
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
					if (!insideVulkanCommands)
					{
						stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": command declared outside commands member");
						continue;
					}

					currentVulkanCommand = new VulkanCommand(null, null, new ListArray<VulkanCommandParameter>());

					continue;
				}

				if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "command")
				{
					Commands.Add(currentVulkanCommand);
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
					stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": enum missing name attribute [" + currentVulkanEnum + "]");
				}
			}

			if (!vulkanRegistryFound)
			{
				stringBuilder.AppendLine("error " + ++errorCount + " line " + ((IXmlLineInfo)xmlReader).LineNumber + ": registry not found");
			}

			File.WriteAllText("log.txt", stringBuilder.ToString());
			Console.WriteLine("Error Count: " + errorCount);
			Console.WriteLine("See log.txt...");
		}
	}
}
