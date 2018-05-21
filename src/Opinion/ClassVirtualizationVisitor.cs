using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Wyam.Tests
{
    public class ClassVirtualizationVisitor : CSharpSyntaxRewriter
    {
        public List<string> Classes { get; } = new List<String>();

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            node = (ClassDeclarationSyntax)base.VisitClassDeclaration(node);

            string className = node.Identifier.ValueText;
            Classes.Add(className); // save your visited classes

            return node;
        }
    }
}
