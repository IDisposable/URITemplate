using System;
using System.Collections.Specialized;
using System.Text;

namespace UriTemplate
{
    public class DictionaryResolver : NameValueCollection, IResolver
    {
        public DictionaryResolver()
        {
        }

        public DictionaryResolver(NameValueCollection variables)
            : base(variables)
        {
        }

        public new string Get(string key)
        {
            return base[key];
        }

        public void Put(string key, string value)
        {
            base[key] = value;
        }

        public string Resolve(string key)
        {
            return base[key];
        }
    }
}
