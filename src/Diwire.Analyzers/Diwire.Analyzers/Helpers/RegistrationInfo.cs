using Microsoft.CodeAnalysis;
using System.Linq;

namespace Diwire.Analyzers.Helpers
{
    public class RegistrationInfo
    {
        public RegistrationInfo(AttributeData registerTypeAttribute)
        {
            var constructorIndex = registerTypeAttribute.ConstructorArguments.Length == 3
                ? 1
                : 0;
            var lifetimeIndex = constructorIndex + 1;
            FromType = (INamedTypeSymbol)registerTypeAttribute.ConstructorArguments[0].Value;
            Constructor = ((INamedTypeSymbol)registerTypeAttribute.ConstructorArguments[constructorIndex].Value).Constructors
                .Where(x => x.DeclaredAccessibility == Accessibility.Public || x.DeclaredAccessibility == Accessibility.Internal)
                .Single();
            Lifetime = ((int)registerTypeAttribute.ConstructorArguments[lifetimeIndex].Value);
        }

        public INamedTypeSymbol FromType { get; }

        public int Lifetime { get; }

        public IMethodSymbol Constructor { get; }
    }
}
