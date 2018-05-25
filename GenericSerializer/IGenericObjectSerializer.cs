namespace GenericSerializer
{
    public interface IGenericObjectSerializer
    {
        IDataSourceByKey Serialize<T>(T obj);
        T Deserialize<T>(IDataSourceByKey dataSourceByKey);
    }
}
