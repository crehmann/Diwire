using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Diwire.Generation.Roslyn.Extensions
{
    public static class SymbolExtensions
    {
        public static IEnumerable<AttributeData> GetAttributes<T>(this ISymbol symbol)
            => symbol.GetAttributes()
            .Where(x => string.Join(".", x.AttributeClass.ContainingNamespace.ToString(), x.AttributeClass.Name) == typeof(T).FullName);

        public static string GetFullName(this ISymbol symbol)
            => string.Join(".", symbol.ContainingNamespace, symbol.Name);
    }
}
