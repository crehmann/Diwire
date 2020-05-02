using System;

namespace Diwire.Abstraction
{
    public interface IContainerProvider
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}
