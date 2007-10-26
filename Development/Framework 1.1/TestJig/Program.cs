using System;
using System.Collections;
using System.Collections.Specialized;

using UriTemplate;

namespace TestJig
{
    public class Program
    {
        static void Main(string[] args)
        {
            UriPattern p = UriPattern.Create("/{user}/{year}/{month}");
            NameValueCollection matches = p.Parse("/marc/2007/06?no#touch");
            foreach (string key in matches)
            {
                System.Console.WriteLine(key.ToString() + " = " + matches.Get(key).ToString());
            }
            IResolver resolver = new DictionaryResolver(matches);
            string url =  UriTemplate.UriTemplate.Expand("http://localhost/paystub/{year}/{month}/{user}", resolver);
            System.Console.WriteLine(url);

            // test case from Darrel Miller (work item 7938)
            p = UriPattern.Create("/LiveContacts/Contacts/Contact({ContactId})");
            matches = p.Parse("/LiveContacts/Contacts/Contact(1001)");
            System.Console.WriteLine("expect 1001: " + matches.Get("ContactId").ToString());
        }
    }
}
