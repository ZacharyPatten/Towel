using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Towel.Serialization
{
  public static partial class Serialization
  {
    #region Shared

    private static readonly XmlWriterSettings DefaultXmlWriterSettings =
        new XmlWriterSettings()
        {
          OmitXmlDeclaration = true,
        };

    #endregion

    #region System.Xml.Serialization.XmlSerializer .NET Wrapper (Default)

    #region To XML

    public static string DefaultToXml<T>(T value)
    {
      return DefaultToXml(value, DefaultXmlWriterSettings);
    }

    public static string DefaultToXml<T>(T value, XmlWriterSettings xmlWriterSettings)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        DefaultToXml(value, stringWriter, xmlWriterSettings);
        return stringWriter.ToString();
      }
    }

    public static void DefaultToXml<T>(T value, TextWriter textWriter)
    {
      DefaultToXml(value, textWriter, DefaultXmlWriterSettings);
    }

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

    public static T DefaultFromXml<T>(string @string)
    {
      using (StringReader stringReader = new StringReader(@string))
      {
        return DefaultFromXml<T>(stringReader);
      }
    }

    public static T DefaultFromXml<T>(TextReader textReader)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T));
      return (T)serializer.Deserialize(textReader);
    }

    #endregion

    #endregion

    #region Static Delegates

    #region Shared

    private static partial class StaticDelegateConstants
    {
      internal const string NAME = "Delegate";
      internal const string DECLARING_TYPE = "Method.DeclaringType.AssemblyQualifiedName";
      internal const string METHOD_NAME = "Method.Name";
      internal const string PARAMETERS = "Method.GetParameters";
      internal const string PARAMETER_TYPE = "ParameterType.AssemblyQualifiedName";
    }

    #endregion

    #region To XML

    public static string StaticDelegateToXml<T>(T @delegate) where T : Delegate
    {
      return StaticDelegateToXml(@delegate, DefaultXmlWriterSettings);
    }

    public static string StaticDelegateToXml<T>(T @delegate, XmlWriterSettings xmlWriterSettings) where T : Delegate
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        StaticDelegateToXml(@delegate, stringWriter, xmlWriterSettings);
        return stringWriter.ToString();
      }
    }

    public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter) where T : Delegate
    {
      StaticDelegateToXml(@delegate, textWriter, DefaultXmlWriterSettings);
    }

    public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter, XmlWriterSettings xmlWriterSettings) where T : Delegate
    {
      using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
      {
        MethodInfo methodInfo = @delegate.Method;
        if (!methodInfo.IsStatic)
        {
          throw new InvalidOperationException("delegates assigned to non-static methods are not supported");
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

    public static T StaticDelegateFromXml<T>(string @string) where T : Delegate
    {
      using (StringReader stringReader = new StringReader(@string))
      {
        return StaticDelegateFromXml<T>(stringReader);
      }
    }

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
      throw new InvalidOperationException("delegates assigned to non-static methods are not supported");
    }

    #endregion

    #endregion
  }
}
