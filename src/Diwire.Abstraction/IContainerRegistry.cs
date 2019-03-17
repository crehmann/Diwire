using System;

namespace Diwire.Abstraction
{
    public interface IContainerRegistry
    {
        IContainerRegistry RegisterTransient<T>(Func<IContainerProvider, T> factory);

        IContainerRegistry RegisterSingelton<T>(Func<IContainerProvider, T> factory);
    }
}
