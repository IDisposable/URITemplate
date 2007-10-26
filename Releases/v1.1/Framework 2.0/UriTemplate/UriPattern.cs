using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UriTemplate
{
    public class UriPattern
    {
        private static Regex pattern = new Regex(@"\{[^{}]+\}", RegexOptions.Compiled);
        private static Regex regexMetaPattern = new Regex(@"[\\\[\]\(\)\&\^\$\?\#\+\*\|\>\<]", RegexOptions.Compiled);
        private static string regexMetaCharactersReplacements = @"\$0";
        private static string tokenReplacement = "([^/?#]*)?";

        private Regex p;
        private string[] tokens;

        protected UriPattern(string template)
            : this(template, RegexOptions.None)
        {
        }

        protected UriPattern(string template, bool compiled)
            : this(template, compiled ? RegexOptions.Compiled : RegexOptions.None)
        {
        }

        protected UriPattern(string template, RegexOptions options)
        {
            tokens = GetTokens(template);
            string finalPattern = BuildRegex(template);
            p = new Regex(finalPattern, options);
        }

        public IDictionary<string, string> Parse(string instance)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

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
            template = regexMetaPattern.Replace(template, regexMetaCharactersReplacements) + ".*";
            return pattern.Replace(template, tokenReplacement);
        }

        private string[] GetTokens(string template)
        {
            List<string> tokens = new List<string>();

            foreach (Match match in pattern.Matches(template))
            {
                string token = match.Value;
                token = token.Substring(1, token.Length - 2); // chop off the leading and trailing curly brace

                if (!tokens.Contains(token))
                    tokens.Add(token);
            }

            return tokens.ToArray();
        }

        public static UriPattern Create(string template)
        {
            return new UriPattern(template);
        }
        
        public static UriPattern Create(string template, bool compiled)
        {
            return new UriPattern(template, compiled);
        }

        public static UriPattern Create(string template, RegexOptions options)
        {
            return new UriPattern(template, options);
        }
    }
}
