using System;
using System.Collections;
using System.Text;

namespace UriTemplate
{
    public interface ITemplate
    {
        IResolver Resolver
        {
            get;
        }

        string Expand(string pattern);
    }
}
