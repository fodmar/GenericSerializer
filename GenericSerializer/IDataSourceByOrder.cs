namespace GenericSerializer
{
    public interface IDataSourceByOrder
    {
        string this[int index] { get; }
    }
}
