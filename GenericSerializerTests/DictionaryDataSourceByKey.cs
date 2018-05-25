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
            this.dict = dict;
        }

        public object this[string key] => this.dict[key];
    }
}
