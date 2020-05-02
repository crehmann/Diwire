using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TestHelper;

namespace Diwire.Analyzers.Test
{
    [TestClass]
    public class DiwireAnalyzersUnitTests : CodeFixVerifier
    {

        [TestMethod]
        public void EmptySource_NoDiagnosticsExpected()
        {
            // arrange
            var test = @"";

            // act & assert
            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void Static_Field()
        {
            var before = @" using Diwire.Abstraction;
                            using Diwire.Abstraction.Attributes;
                            using System;

                            namespace Diwire.Analyzers.Test
                            {
                                class Class
                                {
                                    private static string Test = String.Empty;
                                }

                                [RegisterType(typeof(Class))]
                                class Module : IModule
                                {

                                }
                            }";

            var expected = @" using Diwire.Abstraction;
                            using Diwire.Abstraction.Attributes;
                            using System;

                            namespace Diwire.Analyzers.Test
                            {
                                class Class
                                {
                                    private static string Test = String.Empty;
                                }

                                [RegisterType(typeof(Class))]
                                class Module : IModule
                                {
                                    public void RegisterTypes(IContainerRegistry containerRegistry)
                                    {
                                        throw new NotImplementedException();
                                    }
                                }
                            }";


            VerifyCSharpFix(before, expected);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new RegisterTypeCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new RegisterTypeAnalyzer();
        }
    }
}
