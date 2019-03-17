namespace Diwire.Abstraction
{
    public interface IContainerProvider
    {
        T Resolve<T>();
    }
}
