using Microsoft.CodeAnalysis;
using System.Linq;

namespace Diwire.Generation.Roslyn
{
    public class RegistrationInfo
    {
        public RegistrationInfo(AttributeData registerTypeAttribute)
        {
            FromType = (INamedTypeSymbol)registerTypeAttribute.ConstructorArguments[0].Value;
            Constructor = ((INamedTypeSymbol)registerTypeAttribute.ConstructorArguments[1].Value).Constructors.Single();
            Lifetime = (Lifetime)((int)registerTypeAttribute.ConstructorArguments[2].Value);
        }

        public INamedTypeSymbol FromType { get; }

        public Lifetime Lifetime { get; }

        public IMethodSymbol Constructor { get; }
    }
}
