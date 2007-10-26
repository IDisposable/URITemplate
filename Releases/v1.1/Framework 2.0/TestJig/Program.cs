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

            // test case from Darrel Miller (work item 7938)
            p = UriPattern.Create("/LiveContacts/Contacts/Contact({ContactId})", true);
            values = p.Parse("/LiveContacts/Contacts/Contact(1001)");
            System.Console.WriteLine("expect 1001: " + (values["ContactId"] ?? ""));
        }
    }
}
