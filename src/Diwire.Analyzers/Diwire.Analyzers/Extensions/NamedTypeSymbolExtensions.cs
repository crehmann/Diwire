using Microsoft.CodeAnalysis;
using System.Linq;

namespace Diwire.Analyzers.Extensions
{
    public static class NamedTypeSymbolExtensions
    {
        public static bool HasBaseType(this INamedTypeSymbol symbol)
            => symbol.BaseType != null;

        public static bool Implements(this INamedTypeSymbol symbol, string fullName)
            => symbol.ContainingType.AllInterfaces.Any(x => x.GetFullName() == fullName);
    }
}
