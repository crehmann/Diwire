using System;

namespace Diwire.Container.Test.TestTypes
{
    internal class Dependency2 : IDependency2
    {
        public Dependency2(IDependency1 dependency)
        {
            Dependency1 = dependency ?? throw new ArgumentNullException(nameof(dependency));
        }

        public IDependency1 Dependency1 { get; }
    }
}
