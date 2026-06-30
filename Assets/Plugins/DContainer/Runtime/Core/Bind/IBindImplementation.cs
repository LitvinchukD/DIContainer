
namespace DContainer.Core
{
    public interface IBindImplementation
    {
        IBindScopeType To<TImplemenation>() where TImplemenation : class;
        IBindScopeType To(System.Type implementationType);
        IBindScopeType FromFactory<TImplemenation>(System.Func<IDependencyResolver, TImplemenation> factory) where TImplemenation : class;
        IBindScopeType ToInstance<TImplementation>(TImplementation instance) where TImplementation : class;
        IBindScopeType ToSlef();
    }
}
