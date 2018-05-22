namespace GenericSerializer
{
    public class GenericSerializer : IGenericSerializer
    {
        public T Deserialize<T>(IDataSourceByKey dataSourceByKey)
        {
            throw new System.NotImplementedException();
        }

        public IDataSourceByKey Serialize<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
