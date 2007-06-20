using System;
using System.Collections.Generic;
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
