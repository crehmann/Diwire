using Diwire.Generation.Attributes;
using Diwire.Generation.Roslyn.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diwire.Generation.Roslyn
{
    public class ModuleInfo
    {
        public ModuleInfo(INamedTypeSymbol moduleSymbol)
        {
            Module = moduleSymbol ?? throw new ArgumentNullException(nameof(moduleSymbol));
            Registrations = Module.GetAttributes<RegisterTypeAttribute>()
                .Select(x => new RegistrationInfo(x)).ToArray();
        }

        public INamedTypeSymbol Module { get; }

        public IEnumerable<RegistrationInfo> Registrations { get; }
    }
}
