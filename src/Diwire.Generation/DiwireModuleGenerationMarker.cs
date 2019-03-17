using Diwire.Abstraction;

namespace Diwire.Generation
{
    public abstract class DiwireModuleGenerationMarker : IModule
    {
        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
