using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UriTemplate
{
    public abstract class AbstractTemplate : ITemplate
    {
        private static Regex pattern = new Regex(@"\{[^{}]+\}", RegexOptions.Compiled);
        private static string TOKEN_START = "{";
        private static string TOKEN_STOP = "}";

        private IResolver resolver;

        protected AbstractTemplate(IResolver resolver)
        {
            this.resolver = resolver;
        }

        public IResolver Resolver
        {
            get { return resolver; }
        }

        public string Expand(string pattern)
        {
            string[] tokens = GetTokens(pattern);
            foreach (string token in tokens)
            {
                string value = Resolve(token);
                pattern = Replace(pattern, token, value);
            }
            VerifyResult(pattern);
            return pattern;
        }

        protected string Replace(string pattern, string token, string value)
        {
            string quotedToken = TOKEN_START + token + TOKEN_STOP;
            return pattern.Replace(quotedToken, value);
        }

        protected string Resolve(string token)
        {
            string val = (resolver != null) ? resolver.Resolve(token) : String.Empty;
            return (val != null) ? System.Web.HttpUtility.UrlEncode(val, Encoding.UTF8) : String.Empty;
        }

        protected string[] GetTokens(string template)
        {
            List<string> tokens = new List<string>();
            foreach (Match match in pattern.Matches(template))
            {
                string token = match.Value;
                token = token.Substring(1, token.Length - 2); // chop off the leading and trailing curly brace
                VerifyToken(token);

                if (!tokens.Contains(token))
                    tokens.Add(token);
            }

            return tokens.ToArray();
        }

        protected abstract void VerifyToken(string token);
        protected abstract void VerifyResult(string result);
    }
}
