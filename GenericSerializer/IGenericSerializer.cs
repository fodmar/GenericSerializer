namespace GenericSerializer
{
    public interface IGenericSerializer
    {
        IDataSourceByKey Serialize<T>(T obj);
        T Deserialize<T>(IDataSourceByKey dataSourceByKey);
    }
}
