namespace GenericSerializer
{
    public interface IDataSourceByKey
    {
        (bool, object) TryGetValue(string key);
    }
}
