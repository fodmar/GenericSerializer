using GenericSerializer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericSerializerTests
{
    class DictionaryDataSourceByKey : IDataSourceByKey
    {
        private readonly Dictionary<string, object> dict;

        public DictionaryDataSourceByKey(Dictionary<string, object> dict)
        {
            this.dict = new Dictionary<string, object>(dict, StringComparer.OrdinalIgnoreCase);
        }

        public (bool, object) TryGetValueCaseInsensitive(string key)
        {
            object obj;
            bool exists = this.dict.TryGetValue(key, out obj);
            return (exists, obj);
        }
    }
}
