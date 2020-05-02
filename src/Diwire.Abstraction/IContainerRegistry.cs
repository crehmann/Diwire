using System;

namespace Diwire.Abstraction
{
    public interface IContainerRegistry
    {
        bool Contains<T>();

        IContainerRegistry RegisterTransient<T>(Func<IContainerProvider, T> factory);

        IContainerRegistry RegisterSingleton<T>(Func<IContainerProvider, T> factory);

        bool Remove<T>();
    }
}
