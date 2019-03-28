using Diwire.Abstraction;
using System;
using System.Collections.Generic;

namespace Diwire.Container
{
    public class DiwireContainer : IContainerProvider, IContainerRegistry
    {
        private readonly IDictionary<Type, Func<object>> _registry;

        public DiwireContainer()
        {
            _registry = new Dictionary<Type, Func<object>>();
        }

        public bool Contains<T>() => _registry.ContainsKey(typeof(T));

        public IContainerRegistry RegisterSingelton<T>(Func<IContainerProvider, T> factory)
        {
            var typeOfT = typeof(T);
            if (_registry.ContainsKey(typeOfT))
            {
                throw new InvalidOperationException($"The type of {typeOfT} is already registered.");
            }

            var lazyInstance = new Lazy<T>(() => factory(this));
            _registry.Add(typeOfT, () => lazyInstance.Value);
            return this;
        }

        public IContainerRegistry RegisterTransient<T>(Func<IContainerProvider, T> factory)
        {
            var typeOfT = typeof(T);
            if (_registry.ContainsKey(typeOfT))
            {
                throw new InvalidOperationException($"The type of {typeOfT} is already registered.");
            }

            _registry.Add(typeof(T), () => factory(this));
            return this;
        }

        public T Resolve<T>()
        {
            if (_registry.TryGetValue(typeof(T), out Func<object> factory))
            {
                return (T)factory();
            }
            else
            {
                throw new InvalidOperationException($"The type '{typeof(T)}' is not registered in the container.");
            }
        }

        public bool Remove<T>() => _registry.Remove(typeof(T));
    }
}
