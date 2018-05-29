using GenericSerializer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericSerializerTests
{
    class DictionaryDataSourceByKey : IDataSourceByKey
    {
        private readonly Dictionary<string, object> dict;
        private readonly HashSet<string> visitedKeys;

        public DictionaryDataSourceByKey(Dictionary<string, object> dict)
        {
            this.dict = new Dictionary<string, object>(dict, StringComparer.OrdinalIgnoreCase);
            this.visitedKeys = new HashSet<string>();
        }

        public (bool, object) TryGetValueCaseInsensitive(string key)
        {
            if (!this.visitedKeys.Add(key))
            {
                throw new Exception($"Key {key} is visited more than one");
            }

            object obj;
            bool exists = this.dict.TryGetValue(key, out obj);
            return (exists, obj);
        }
    }
}
