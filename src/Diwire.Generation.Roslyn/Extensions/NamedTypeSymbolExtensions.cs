using Microsoft.CodeAnalysis;
using System;

namespace Diwire.Generation.Roslyn.Extensions
{
    public static class NamedTypeSymbolExtensions
    {
        public static bool HasBaseType(this INamedTypeSymbol symbol)
            => symbol.BaseType != null;

        public static bool HasBaseType(this INamedTypeSymbol symbol, Type ofType)
            => symbol.HasBaseType()
            && symbol.BaseType.ToString() == ofType.FullName;
    }
}
