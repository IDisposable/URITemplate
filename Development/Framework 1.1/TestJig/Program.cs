using System;
using System.Collections;
using System.Collections.Generic;

using UriTemplate;

namespace TestJig
{
    public class Program
    {
        static void Main(string[] args)
        {
            UriPattern p = UriPattern.Create("/{user}/{year}/{month}");
            IDictionary<string, string> values = p.Parse("/marc/2007/06?no#touch");
            foreach (KeyValuePair<string, string> item in values)
            {
                System.Console.WriteLine(item.Key.ToString() + " = " + item.Value.ToString());
            }
            IResolver resolver = new DictionaryResolver(values);
            string url =  UriTemplate.UriTemplate.Expand("http://localhost/paystub/{year}/{month}/{user}", resolver);
            System.Console.WriteLine(url);
        }
    }
}
