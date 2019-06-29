using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

#pragma warning disable IDE0060 // Suppress Compiler Messages (they are intentional for testing)

namespace Towel_Testing
{
    [TestClass]
    public class Extensions_Testing
    {
        #region XML Documentation From Method

        [TestMethod]
        public void GetDocumentation_Method()
        {
            string[] tests = new string[]
            {
                "Test A",
                "Test B",
                "Test C",
                "Test D",
                "Test E",
                "Test F",
                "Test G",
                "Test H",
                "Test I",
                "Test J",
                "Test K",
                "Test L",
                "Test M",
                "Test N",
                "Test O",
                "Test P",
                "Test Q",
                "Test R",
                "Test S",
                "Test T",
                "Test U",
            };

            StringBuilder stringBuilder = new StringBuilder();
            foreach (MethodInfo methodInfo in
                typeof(XmlDocumentationMethod).GetMethods().Concat(
                typeof(XmlDocumentationMethod.NestedType).GetMethods()).Concat(
                typeof(XmlDocumentationMethod.NestedGenericType<int>).GetMethods()).Where(
                    x => x.DeclaringType.Assembly == typeof(XmlDocumentationMethod).Assembly))
            {
                stringBuilder.AppendLine(methodInfo.GetDocumentation());
            }
            string documentationFromReflection = stringBuilder.ToString();
            foreach (string test in tests)
            {
                if (!documentationFromReflection.Contains(test))
                {
                    Assert.Fail(test);
                }
            }
        }

        #endregion

        #region XML Documentation From Type

        [TestMethod]
        public void GetDocumentation_Type()
        {
            string[] tests = new string[]
            {
                "Test A",
                "Test B",
                "Test C",
                "Test D",
                "Test E",
                "Test F",
                "Test G",
                "Test H",
                "Test I",
                "Test J",
                "Test K",
                "Test L",
                "Test M",
                "Test N",
                "Test O",
                "Test P",
                "Test Q",
                "Test R",
                "Test S",
            };

            StringBuilder stringBuilder = new StringBuilder();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypesWithAttribute<XmlDocumentationFromTypeAttribute>())
            {
                stringBuilder.AppendLine(type.GetDocumentation());
            }
            string documentationFromReflection = stringBuilder.ToString();
            foreach (string test in tests)
            {
                if (!documentationFromReflection.Contains(test))
                {
                    Assert.Fail(test);
                }
            }
        }

        #endregion
    }

    #region XML Documentation From Method Types

    public class XmlDocumentationMethod
    {
        /// <summary>Test A</summary>
        /// <returns>object</returns>
        public object DocumentedMethodReturns() { return null; }

        /// <summary>Test B</summary>
        public void DocumentedMethod() { }

        /// <summary>Test C</summary>
        /// <param name="a">a</param>
        public void DocumentedMethod(object a) { }

        /// <summary>Test D</summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        public void DocumentedMethod(object a, object b) { }

        /// <summary>Test E</summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="c">c</param>
        public void DocumentedMethod(object a, object b, object c) { }

        /// <summary>Test F</summary>
        /// <param name="a">a</param>
        public void DocumentedMethod(params object[] a) { }

        /// <summary>Test G</summary>
        /// <param name="a">a</param>
        public void DocumentedMethodOut(out object a) { a = null; }

        /// <summary>Test H</summary>
        /// <param name="a">a</param>
        public void DocumentedMethodRef(ref object a) { }

        /// <summary>Test I</summary>
        /// <param name="a">a</param>
        public void DocumentedMethodIn(in object a) { }

        /// <summary>Test J</summary>
        /// <typeparam name="A">A</typeparam>
        public void DocumentedMethod<A>() { }

        /// <summary>Test K</summary>
        /// <typeparam name="A">A</typeparam>
        /// <param name="a">a</param>
        public void DocumentedMethod<A>(A a) { }

        /// <summary>Test L</summary>
        /// <typeparam name="A">A</typeparam>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        public void DocumentedMethod<A>(A a, object b) { }

        /// <summary>Test M</summary>
        /// <typeparam name="A">A</typeparam>
        /// <typeparam name="B">B</typeparam>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        public void DocumentedMethod<A, B>(A a, B b) { }

        /// <summary>Test N</summary>
        /// <typeparam name="A">A</typeparam>
        /// <typeparam name="B">B</typeparam>
        /// <param name="b">b</param>
        /// <param name="a">a</param>
        public void DocumentedMethod<A, B>(B b, A a) { }

        /// <summary>Test U</summary>
        /// <param name="a">a</param>
        public unsafe void DocumentedMethod(int* a) { }

        public class NestedType
        {
            /// <summary>Test O</summary>
            public void DocumentedMethod() { }
        }

        public class NestedGenericType<A>
        {
            /// <summary>Test P</summary>
            public void DocumentedMethod() { }
        }

        /// <summary>Test Q</summary>
        /// <param name="a">a</param>
        public void DocumentedMethod(NestedType a) { }

        /// <summary>Test R</summary>
        /// <typeparam name="A">A</typeparam>
        /// <param name="a">a</param>
        public void DocumentedMethod<A>(NestedGenericType<A> a) { }

        /// <summary>Test S</summary>
        /// <typeparam name="A">A</typeparam>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="c">c</param>
        public void DocumentedMethod<A>(A a, List<A> b, A[] c) { }

        /// <summary>Test T</summary>
        /// <typeparam name="A">A</typeparam>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="c">c</param>
        /// <param name="d">d</param>
        public void DocumentedMethod<A>(A a, List<A> b, A[] c, A[,] d) { }
    }

    #endregion

    #region XML Documentation From Type Types

    public class XmlDocumentationFromTypeAttribute : Attribute { }

    /// <summary>Test A</summary>
    [XmlDocumentationFromType]
    public class XmlDocumentationFromTypeA { }

    /// <summary>Test B</summary>
    /// <typeparam name="A">A</typeparam>
    [XmlDocumentationFromType]
    public class XmlDocumentationFromTypeB<A> { }

    /// <summary>Test C</summary>
    /// <typeparam name="A">A</typeparam>
    /// <typeparam name="B">B</typeparam>
    [XmlDocumentationFromType]
    public class XmlDocumentationFromTypeC<A, B> { }

    /// <summary>Test D</summary>
    [XmlDocumentationFromType]
    public struct XmlDocumentationFromTypeD { }

    /// <summary>Test E</summary>
    /// <typeparam name="A">A</typeparam>
    [XmlDocumentationFromType]
    public struct XmlDocumentationFromTypeE<A> { }

    /// <summary>Test F</summary>
    /// <typeparam name="A">A</typeparam>
    /// <typeparam name="B">B</typeparam>
    [XmlDocumentationFromType]
    public struct XmlDocumentationFromTypeF<A, B> { }

    /// <summary>Test G</summary>
    [XmlDocumentationFromType]
    public ref struct XmlDocumentationFromTypeG { }

    /// <summary>Test H</summary>
    /// <typeparam name="A">A</typeparam>
    [XmlDocumentationFromType]
    public ref struct XmlDocumentationFromTypeH<A> { }

    /// <summary>Test I</summary>
    /// <typeparam name="A">A</typeparam>
    /// <typeparam name="B">B</typeparam>
    [XmlDocumentationFromType]
    public ref struct XmlDocumentationFromTypeI<A, B> { }

    /// <summary>Test J</summary>
    [XmlDocumentationFromType]
    public delegate void XmlDocumentationFromTypeJ();

    /// <summary>Test K</summary>
    /// <typeparam name="A">A</typeparam>
    [XmlDocumentationFromType]
    public delegate void XmlDocumentationFromTypeK<A>();

    /// <summary>Test L</summary>
    /// <typeparam name="A">A</typeparam>
    /// <typeparam name="B">B</typeparam>
    [XmlDocumentationFromType]
    public delegate void XmlDocumentationFromTypeL<A, B>();

    /// <summary>Test M</summary>
    [XmlDocumentationFromType]
    public enum XmlDocumentationFromTypeM { }

    public class XmlDocumentationFromTypeBase
    {
        /// <summary>Test N</summary>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeN { }

        /// <summary>Test O</summary>
        /// <typeparam name="A">A</typeparam>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeO<A> { }

        /// <summary>Test P</summary>
        /// <typeparam name="A">A</typeparam>
        /// <typeparam name="B">B</typeparam>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeP<A, B> { }
    }

    public class XmlDocumentationFromTypeBase<A>
    {
        /// <summary>Test Q</summary>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeQ { }

        /// <summary>Test R</summary>
        /// <typeparam name="B">B</typeparam>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeR<B> { }

        /// <summary>Test S</summary>
        /// <typeparam name="B">B</typeparam>
        /// <typeparam name="C">C</typeparam>
        [XmlDocumentationFromType]
        public class XmlDocumentationFromTypeS<B, C> { }
    }

    #endregion
}

#pragma warning restore IDE0060
