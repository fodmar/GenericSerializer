using System.Collections.Generic;
using GenericSerializer;

public class DataSourceByKeyWraper : IDataSourceByKey
{
    private readonly IDataSourceByKey dataSourceByKey;
    private readonly Dictionary<string, (bool, object)> cache;

    public DataSourceByKeyWraper(IDataSourceByKey dataSourceByKey)
    {
        this.dataSourceByKey = dataSourceByKey;
        this.cache = new Dictionary<string, (bool, object)>();
    }

    public (bool, object) TryGetValueCaseInsensitive(string key)
    {
        if (this.cache.TryGetValue(key, out (bool, object) value))
        {
            return value;
        }

        (bool, object) val = this.dataSourceByKey.TryGetValueCaseInsensitive(key);
        this.cache.Add(key, val);
        return val;
    }
}