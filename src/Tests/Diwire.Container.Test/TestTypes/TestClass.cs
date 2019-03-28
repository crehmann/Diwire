using System;

namespace Diwire.Container.Test.TestTypes
{
    internal class TestClass
    {
        public TestClass(IDependency1 dependency1, IDependency2 dependency2)
        {
            Dependency1 = dependency1 ?? throw new ArgumentNullException(nameof(dependency1));
            Dependency2 = dependency2 ?? throw new ArgumentNullException(nameof(dependency2));
        }

        public IDependency1 Dependency1 { get; }
        public IDependency2 Dependency2 { get; }
    }
}
