using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

#pragma warning disable IDE0060 // Suppress Compiler Messages (they are intentional for testing)

namespace Towel_Testing
{
    [TestClass]
    public class Extensions_Testing
    {
        public bool loaded = false;
        public void LoadXmlDocumentation()
        {
            if (!loaded)
            {
                TowelSystemExtensions.LoadXmlDocumentation(File.ReadAllText(@"Towel_Testing.xml"));
                loaded = true;
            }
        }

        [TestMethod]
        public void GetDocumentation_Method()
        {
            LoadXmlDocumentation();

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
                //"Test I",
                "Test J",
                "Test K",
                "Test L",
                "Test M",
                "Test N",
                "Test O",
                "Test P",
                "Test Q",
                "Test R",
            };

            StringBuilder stringBuilder = new StringBuilder();
            foreach (MethodInfo methodInfo in typeof(XmlDocumentationMethod).GetMethods())
            {
                stringBuilder.AppendLine(methodInfo.GetDocumentation());
            }
            foreach (MethodInfo methodInfo in typeof(XmlDocumentationMethod.NestedType).GetMethods())
            {
                stringBuilder.AppendLine(methodInfo.GetDocumentation());
            }
            foreach (MethodInfo methodInfo in typeof(XmlDocumentationMethod.NestedGenericType<int>).GetMethods())
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
    }

    #region Method XML Testing

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

        ///// <summary>Test I</summary>
        ///// <param name="a">a</param>
        //public void DocumentedMethodIn(in object a) { }

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
    }

    #endregion
}

#pragma warning restore IDE0060
