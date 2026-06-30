namespace DContainer.Core
{
    public interface IBindContract
    {
        IBindImplementation Bind<TContract>() where TContract : class;
        IBindImplementation Bind(System.Type contractType);
    }
}
