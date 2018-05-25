namespace GenericSerializer
{
    public interface IDataSourceByKey
    {
        object this[string key] { get; }
    }
}
