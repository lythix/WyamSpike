using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
// using Microsoft.CodeAnalysis.MSBuild;    //Some problem in this line? Read on.

namespace Opinion
{
    public class Opinion
    {
        public async Task<List<string>> GetProjectClasses(string directoryPath)
        {
            var path = Path.GetDirectoryName(directoryPath);
            var sln = Path.Combine(path, "Wyam.sln");

            // var workspace = Microsoft.CodeAnalysis.Workspaces.Desktop.MSBuildWorkspace.Create(); // TODO: Fix this line, it threw an exception for me, might be ok with beta 
            //var solution = await workspace.OpenSolutionAsync(sln);


            //Project project = solution.Projects.First(x => x.Name == "Wyam");
            //var compilation = await project.GetCompilationAsync();

            //foreach (var @class in compilation.GlobalNamespace.GetNamespaceMembers().SelectMany(x => x.GetMembers()))
            //{
            //    Console.WriteLine(@class.Name);
            //    Console.WriteLine(@class.ContainingNamespace.Name);
            //}

            //var classVisitor = new ClassVirtualizationVisitor();

            //foreach (var syntaxTree in compilation.SyntaxTrees)
            //{
            //    var tree = syntaxTree.GetRoot();
            //    ClassDeclarationSyntax node = (ClassDeclarationSyntax) tree.ChildNodes().FirstOrDefault(); // TODO: Fix this line, how to get ClassDeclarationSytax from syntax tree
            //    classVisitor.VisitClassDeclaration(node);
            //}

            //var classes = classVisitor.Classes; // list of classes in your solution
            var classes = new List<string>();
            return classes;
        }

        public string ParseSyntaxTree()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
                @"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}");

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var result = root.GetText();
            return result.ToString();
        }
    }
}
