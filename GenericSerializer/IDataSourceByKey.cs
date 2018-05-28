namespace GenericSerializer
{
    public interface IDataSourceByKey
    {
        (bool, object) TryGetValueCaseInsensitive(string key);
    }
}
