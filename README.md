## Project Description
The UriTemplate library is a simple set of code to encapsulate the building and parsing of URIs from replacement tokens. Using this couple of classes will make it easy to build RESTful URIs.

## News
[Release 1.0](https://github.com/IDisposable/URITemplate/tree/Releases/v1.1/Framework_2.0)  on 10/26/2007 to include a fix patterns with Regex meta-characters and ability to build compiled UriPatterns.

## What the heck is this all about?
UriTemplates are a simple way to codify the build of URI strings that follow web-optimization and RESTful principles. In short, you want URIs that are "hackable" and well formed. UriTemplates allow regular constructing and parsing through named **tokens** that specify where logical substitutions should be performed.

The idea of a UriTemplate was probably best described by Joe Gregorio in this blog posting [http://bitworking.org/news/URI_Templates](http://bitworking.org/news/URI_Templates) and it simply talks about using URIs patterns like `http://example.org/{item}/{userid}` into strings like `http://example.org/users/1234` and back, given a dictionary of token/value pairs where {{item = "users'}} and `userid = "1234"`. The ability to generate well-formed URIs from the dictionary, or parse them out for incoming requests lets you easily build RESTful interfaces.

The desire to implement this came from the upcoming UriTemplate in .Net (currently somewhat released in the BizTalk Services SDK).
This specific implementation of the UriTemplate **does not** have all the cool features of the Microsoft version; rather it follows from a java class written by James Snell in a posting here [http://www.snellspace.com/wp/?p=467](http://www.snellspace.com/wp/?p=467). I felt his class was just perfect in the level of functionality and simplicity. This code is essentially a C# port to .Net from his java code.

I expect that in a few years, .Net developers will be broadly using the System.UriTemplate currently contained in the BizTalk SDK's Microsoft.ServiceModel.Web.dll. For those of us developers in the real world not able to deploy all these cool new _pre-beta_ bits in production, this set of code will let you develop in either the 1.1 or 2.0 framework and establish the process now.
## How to use
Here's the simplest possible example; a console mode application that shows how to play with {{UriPattern}} to extract values for the tokens and how to use {{UriTemplate}} to generate a new URI from those token values:

```C#
using System;
using System.Collections;
using System.Collections.Generic;

using UriTemplate;

namespace TestJig
{
    public class Program
    {
        static void Main(string[]() args)
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
```
## Other blog entries
Steve Maine's  _UriTemplate 101_ talks about the System.UriTemplate class upcoming in future .Net framework releases [http://hyperthink.net/blog/2007/05/15/UriTemplate+101.aspx](http://hyperthink.net/blog/2007/05/15/UriTemplate+101.aspx)
Steve Maine's  _UriTemplate 101_ talks about the System.UriTemplate class' Match method. [http://hyperthink.net/blog/2007/05/16/UriTemplateMatch.aspx](http://hyperthink.net/blog/2007/05/16/UriTemplateMatch.aspx)
Mark Nottingham's _URI Templating_ talks about the specification of URI templating in a broader (not just Microsoft's .Net) scope [http://www.mnot.net/blog/2006/10/04/uri_templating](http://www.mnot.net/blog/2006/10/04/uri_templating)
