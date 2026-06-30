
namespace DContainer.Core
{
    public interface IDependencyResolver
    {
        object Resolve(System.Type contractType, object identifier);
    }
}
