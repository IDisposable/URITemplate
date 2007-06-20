using System.Collections.Generic;

namespace UriTemplate
{
    public class DictionaryResolver : Dictionary<string, string>, IResolver
    {
        public DictionaryResolver()
        {
        }

        public DictionaryResolver(IDictionary<string, string> variables)
            : base(variables)
        {
        }

        public string Get(string key)
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
