namespace DContainer.Core
{
    public interface IBindConfiguration
    {
        IBindConfiguration WithArgument(System.Type type, object value, short position);
        IBindConfiguration WithArguments(params object[] values);
        IBindConfiguration WithIdentifier(object identifier);
        void Commit();
    }
}
