using System.Collections.Generic;
using GenericSerializer;

public class DataSourceByKeyWrapper : IDataSourceByKey
{
    private readonly IDataSourceByKey dataSourceByKey;
    private readonly Dictionary<string, (bool, object)> cache;
    private readonly string path;
    private readonly bool applyPath;

    public DataSourceByKeyWrapper(IDataSourceByKey dataSourceByKey, string path)
    {
        this.dataSourceByKey = dataSourceByKey;
        this.cache = new Dictionary<string, (bool, object)>();
        this.path = path;
        this.applyPath = !string.IsNullOrEmpty(path);

        if (this.applyPath && this.path.StartsWith("."))
        {
            this.path = this.path.Substring(1);
        }
    }

    public (bool, object) TryGetValue(string key)
    {
        if (this.applyPath)
        {
            key = $"{path}.{key}";
        }

        if (this.cache.TryGetValue(key, out (bool, object) value))
        {
            return value;
        }

        (bool, object) val = this.dataSourceByKey.TryGetValue(key);
        this.cache.Add(key, val);
        return val;
    }
}