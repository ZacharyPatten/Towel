using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Towel
{
	/// <summary>Static class containing serialization code.</summary>
	public static partial class Serialization
	{
		#region Shared

		internal static readonly XmlWriterSettings DefaultXmlWriterSettings =
			new XmlWriterSettings() { OmitXmlDeclaration = true, };

		#endregion

		#region System.Xml.Serialization.XmlSerializer .NET Wrapper (Default)

		#region To XML

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <returns>The XML serialzation of the value.</returns>
		public static string DefaultToXml<T>(T value) =>
			DefaultToXml(value, DefaultXmlWriterSettings);

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <returns>The XML serialzation of the value.</returns>
		public static string DefaultToXml<T>(T value, XmlWriterSettings xmlWriterSettings)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				DefaultToXml(value, stringWriter, xmlWriterSettings);
				return stringWriter.ToString();
			}
		}

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		public static void DefaultToXml<T>(T value, TextWriter textWriter) =>
			DefaultToXml(value, textWriter, DefaultXmlWriterSettings);

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		public static void DefaultToXml<T>(T value, TextWriter textWriter, XmlWriterSettings xmlWriterSettings)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(xmlWriter, value);
			}
		}

		#endregion

		#region From XML

		/// <summary>Wrapper for the default XML deserialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="string">The string containing the XML content to deserialize.</param>
		/// <returns>The deserialized value.</returns>
		public static T DefaultFromXml<T>(string @string)
		{
			using (StringReader stringReader = new StringReader(@string))
			{
				return DefaultFromXml<T>(stringReader);
			}
		}

		/// <summary>Wrapper for the default XML deserialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="textReader">The text reader providing the XML to deserialize.</param>
		/// <returns>The deserialized value.</returns>
		public static T DefaultFromXml<T>(TextReader textReader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(textReader);
		}

		#endregion

		#endregion

		#region Static Delegates

		#region Shared

		internal static partial class StaticDelegateConstants
		{
			internal const string NAME = "Delegate";
			internal const string DECLARING_TYPE = "Method.DeclaringType.AssemblyQualifiedName";
			internal const string METHOD_NAME = "Method.Name";
			internal const string PARAMETERS = "Method.GetParameters";
			internal const string PARAMETER_TYPE = "ParameterType.AssemblyQualifiedName";
		}

		#endregion

		#region To XML

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <returns>The XML serialization of the delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static string StaticDelegateToXml<T>(T @delegate) where T : Delegate =>
			StaticDelegateToXml(@delegate, DefaultXmlWriterSettings);

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <returns>The XML serialization of the delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static string StaticDelegateToXml<T>(T @delegate, XmlWriterSettings xmlWriterSettings) where T : Delegate
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				StaticDelegateToXml(@delegate, stringWriter, xmlWriterSettings);
				return stringWriter.ToString();
			}
		}

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter) where T : Delegate =>
			StaticDelegateToXml(@delegate, textWriter, DefaultXmlWriterSettings);

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter, XmlWriterSettings xmlWriterSettings) where T : Delegate
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				MethodInfo methodInfo = @delegate.Method;
				if (methodInfo.IsLocalFunction())
				{
					throw new NotSupportedException("delegates assigned to local functions are not supported");
				}
				if (!methodInfo.IsStatic)
				{
					throw new NotSupportedException("delegates assigned to non-static methods are not supported");
				}

				ParameterInfo[] parameterInfos = methodInfo.GetParameters();
				xmlWriter.WriteStartElement(StaticDelegateConstants.NAME);
				{
					xmlWriter.WriteStartElement(StaticDelegateConstants.DECLARING_TYPE);
					xmlWriter.WriteString(methodInfo.DeclaringType.AssemblyQualifiedName);
					xmlWriter.WriteEndElement();

					xmlWriter.WriteStartElement(StaticDelegateConstants.METHOD_NAME);
					xmlWriter.WriteString(methodInfo.Name);
					xmlWriter.WriteEndElement();

					xmlWriter.WriteStartElement(StaticDelegateConstants.PARAMETERS);
					for (int i = 0; i < parameterInfos.Length; i++)
					{
						xmlWriter.WriteStartElement(StaticDelegateConstants.PARAMETER_TYPE);
						xmlWriter.WriteString(parameterInfos[i].ParameterType.AssemblyQualifiedName);
						xmlWriter.WriteEndElement();
					}
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
			}
		}

		#endregion

		#region From XML

		/// <summary>Deserializes a static delegate from XML.</summary>
		/// <typeparam name="T">The type of the delegate to deserialize.</typeparam>
		/// <param name="string">The string of XML content to deserialize.</param>
		/// <returns>The deserialized delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static T StaticDelegateFromXml<T>(string @string) where T : Delegate
		{
			using (StringReader stringReader = new StringReader(@string))
			{
				return StaticDelegateFromXml<T>(stringReader);
			}
		}

		/// <summary>Deserializes a static delegate from XML.</summary>
		/// <typeparam name="T">The type of the delegate to deserialize.</typeparam>
		/// <param name="textReader">The text reader providing the XML to deserialize.</param>
		/// <returns>The deserialized delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static T StaticDelegateFromXml<T>(TextReader textReader) where T : Delegate
		{
			try
			{
				string declaringTypeString = null;
				string methodNameString = null;
				List<string> parameterTypeStrings = new List<string>();
				using (XmlReader xmlReader = XmlReader.Create(textReader))
				{
					while (xmlReader.Read())
					{
						CONTINUE:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							switch (xmlReader.Name)
							{
								case StaticDelegateConstants.DECLARING_TYPE:
									declaringTypeString = xmlReader.ReadInnerXml();
									goto CONTINUE;
								case StaticDelegateConstants.METHOD_NAME:
									methodNameString = xmlReader.ReadInnerXml();
									goto CONTINUE;
								case StaticDelegateConstants.PARAMETER_TYPE:
									parameterTypeStrings.Add(xmlReader.ReadInnerXml());
									goto CONTINUE;
							}
						}
					}
				}
				Type declaringType = Type.GetType(declaringTypeString);
				MethodInfo methodInfo = null;
				if (parameterTypeStrings.Count > 0)
				{
					Type[] parameterTypes = parameterTypeStrings.Select(x => Type.GetType(x)).ToArray();
					methodInfo = declaringType.GetMethod(methodNameString, parameterTypes);
				}
				else
				{
					methodInfo = declaringType.GetMethod(methodNameString);
				}
				if (methodInfo.IsLocalFunction())
				{
					goto ThrowLocalFunctionException;
				}
				if (!methodInfo.IsStatic)
				{
					goto ThrowNonStaticException;
				}
				return (T)methodInfo.CreateDelegate(typeof(T));
			}
			catch (Exception exception)
			{
				throw new Exception("deserialization failed", exception);
			}
			ThrowNonStaticException:
			throw new NotSupportedException("delegates assigned to non-static methods are not supported");
			ThrowLocalFunctionException:
			throw new NotSupportedException("delegates assigned to local functions are not supported");
		}

		#endregion

		#endregion
	}
}
