namespace GenericSerializer
{
    public interface IDataSourceByKey
    {
        string this[string key] { get; }
    }
}
