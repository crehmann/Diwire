using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Diwire.Analyzers.Extensions
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static bool IsOverridingMethod(this ClassDeclarationSyntax classDeclarationSyntax, string methodName)
            => classDeclarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .Where(x => x.Identifier.ToString() == methodName)
                .Any();
    }
}
