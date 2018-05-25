namespace GenericSerializer
{
    public interface IDataSourceByOrder
    {
        object this[int index] { get; }
    }
}
