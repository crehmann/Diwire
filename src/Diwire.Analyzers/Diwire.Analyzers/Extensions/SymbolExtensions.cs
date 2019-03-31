using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Diwire.Analyzers.Extensions
{
    public static class SymbolExtensions
    {
        public static IEnumerable<AttributeData> GetAttributes(this ISymbol symbol, string fullName)
            => symbol.GetAttributes()
            .Where(x => string.Join(".", x.AttributeClass.ContainingNamespace.ToString(), x.AttributeClass.Name) == fullName);

        public static string GetFullName(this ISymbol symbol)
            => string.Join(".", symbol.ContainingNamespace, symbol.Name);
    }
}
