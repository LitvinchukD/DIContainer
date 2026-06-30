namespace DContainer.Core
{
    public interface IBindScopeType
    {
        IBindConfiguration AsSingleton();
        IBindConfiguration AsTransient();
    }
}
