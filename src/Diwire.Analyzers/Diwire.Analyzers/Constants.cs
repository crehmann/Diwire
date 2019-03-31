namespace Diwire.Analyzers
{
    internal static class Constants
    {
        public const string RegisterTypeAttribute = "Diwire.Abstraction.Attributes.RegisterTypeAttribute";
        public const string RegisterTypeMethod = "RegisterTypes";
        public const string RegisterSingeltonMethod = "RegisterSingelton";
        public const string RegisterTransientMethod = "RegisterTransient";
        public const string ModuleInterface = "Diwire.Abstraction.IModule";
        public const string ContainerRegistryInterface = "Diwire.Abstraction.IContainerRegistry";
        public const int LifetimeSingelton = 0;
        public const int LifetimeTransient = 1;
    }
}
