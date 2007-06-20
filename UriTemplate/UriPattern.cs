using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace UriTemplate
{
    public class UriPattern
    {
        private static Regex pattern = new Regex(@"\{[^{}]+\}", RegexOptions.Compiled);
        private static string regexMetaCharacters = @"[\\\[\]\(\)\&\^\$\?\#\+\*\|\>\<]";
        private static string regexMetaCharactersReplacements = @"\\$0";
        private static string tokenReplacement = "([^/?#]*)?";

        private Regex p;
        private string[] tokens;

        protected UriPattern(string template)
        {
            tokens = GetTokens(template);
            string finalPattern = BuildRegex(template);
            p = new Regex(finalPattern); // not compiled by default...
        }

        public NameValueCollection Parse(string instance)
        {
            NameValueCollection dict = new NameValueCollection();

            foreach (Match match in p.Matches(instance))
            {
                int tokenIndex = 0;
                foreach (Group group in match.Groups)
                {
                    if (tokenIndex > 0 && group.Success)
                    {
                        dict.Add(tokens[tokenIndex - 1], group.Value);
                    }

                    tokenIndex++;
                }
            }

            return dict; // make readonly?
        }

        private string BuildRegex(string template)
        {
            template = template.Replace(regexMetaCharacters, regexMetaCharactersReplacements) + ".*";
            return pattern.Replace(template, tokenReplacement);
        }

        private string[] GetTokens(string template)
        {
            ArrayList tokens = new ArrayList();

            foreach (Match match in pattern.Matches(template))
            {
                string token = match.Value;
                token = token.Substring(1, token.Length - 2); // chop off the leading and trailing curly brace

                if (!tokens.Contains(token))
                    tokens.Add(token);
            }

            return (string[])tokens.ToArray(typeof(string));
        }

        public static UriPattern Create(string template)
        {
            return new UriPattern(template);
        }
    }
}
