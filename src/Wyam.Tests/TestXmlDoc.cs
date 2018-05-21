using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Wyam.Tests
{
    public class TestXmlDoc
    {

        [Fact]
        public async Task XmlDoc_CheckAllAssemblies_HaveExamples()
        {
            var helper = new Helper();
            var allClasses = await helper.GetProjectClasses();

            foreach (var projectClass in allClasses)
            {
                var codeText = new StreamReader(projectClass).ReadToEnd();
                var code = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

                // Parse the C# code...
                CSharpParseOptions parseOptions = new CSharpParseOptions()
                    .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                    .WithLanguageVersion(LanguageVersion.Latest); // ...enabling the latest language features

                // Compile the C# code...
                CSharpCompilationOptions compileOptions =
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary) // ...to a dll
                        .WithOptimizationLevel(OptimizationLevel.Release) // ...in Release configuration
                        .WithAllowUnsafe(enabled: true); // ...enabling unsafe code

                // Invoke the compiler...
                CSharpCompilation compilation =
                    CSharpCompilation.Create("TestInMemoryAssembly") // ..with some fake dll name
                        .WithOptions(compileOptions)
                        .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location)); // ...referencing the same mscorlib we're running on

                // Parse and compile the C# code into a *.dll and *.xml file in-memory
                var tree = CSharpSyntaxTree.ParseText(codeText, parseOptions);
                var newCompilation = compilation.AddSyntaxTrees(tree);

            }

        }
        
        /// <summary>
        /// Modified example from 
        /// https://blog.stephencleary.com/2018/05/roslyn-unit-testing.html
        /// Changed it to only return the Xml from the code which is compiled
        /// </summary>
        [Fact]
        public void Library_ParseXmlDoc_ContainsText()
        {
            var code = @"public class SampleClass {
              /// <summary>Text to find.</summary>
              public void SampleMethod() { } }";
            // var (assembly, xmldoc) = Compile(code);

            var xmldoc = Compile(code);

            var doc = xmldoc.Descendants("member").FirstOrDefault(); // .FirstOrDefault(x => x.Attribute("name")?.Value); //  == member.MemberXmldocIdentifier()).Element(elementName);
            string expectedValue = "<summary>Text to find.</summary>";
            Assert.Equal(expectedValue, string.Join("", doc.Nodes().Select(x => x.ToString(SaveOptions.DisableFormatting))));

            // var type = assembly.Modules.SelectMany(x => x.Types).Single(x => x.Name == "SampleClass");
            // var method = type.Methods.Single(x => x.Name == "SampleMethod");

            // Assert.Equal("M:SampleClass.SampleMethod", method.XmldocIdentifier());
            // AssertXmldoc("Text to find.", xmldoc, method);
        }

        //public static void AssertXmldoc(XDocument xmldoc, string expectedValue, IMemberDefinition member, string elementName = "summary")
        //{
        //    var doc = xmldoc.Descendants("member").FirstOrDefault(x => x.Attribute("name")?.Value == member.MemberXmldocIdentifier()).Element(elementName);
        //    Assert.Equal(expectedValue, string.Join("", doc.Nodes().Select(x => x.ToString(SaveOptions.DisableFormatting))));
        //}

        //public static (AssemblyDefinition Dll, XDocument Xml) Compile(string code)
        public static XDocument Compile(string code)
        {
            // Parse the C# code...
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                .WithLanguageVersion(LanguageVersion.Latest); // ...enabling the latest language features

            // Compile the C# code...
            CSharpCompilationOptions compileOptions =
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary) // ...to a dll
                    .WithOptimizationLevel(OptimizationLevel.Release) // ...in Release configuration
                    .WithAllowUnsafe(enabled: true); // ...enabling unsafe code

            // Invoke the compiler...
            CSharpCompilation compilation =
                CSharpCompilation.Create("TestInMemoryAssembly") // ..with some fake dll name
                    .WithOptions(compileOptions)
                    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location)); // ...referencing the same mscorlib we're running on

            // Parse and compile the C# code into a *.dll and *.xml file in-memory
            var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
            var newCompilation = compilation.AddSyntaxTrees(tree);

            var peStream = new MemoryStream();
            var xmlStream = new MemoryStream();
            var emitResult = newCompilation.Emit(peStream, xmlDocumentationStream: xmlStream);
            if (!emitResult.Success)
                throw new InvalidOperationException("Compilation failed: " + string.Join("\n", emitResult.Diagnostics));

            // Parse the *.dll (with Cecil) and the *.xml (with XDocument) // Cecil is mono so I'm not using that here. Only want the Xml from the assembly anyway
            peStream.Seek(0, SeekOrigin.Begin);
            xmlStream.Seek(0, SeekOrigin.Begin);
            return XDocument.Load(xmlStream);
            //return (AssemblyDefinition.ReadAssembly(peStream), XDocument.Load(xmlStream));
        }
    }
}
