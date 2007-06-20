using System;

namespace UriTemplate
{
    public class UriTemplate : AbstractTemplate
    {
        public static string Expand(string pattern, IResolver resolver)
        {
            UriTemplate template = new UriTemplate(resolver);
            return template.Expand(pattern);
        }

        public static Uri ExpandAsUri(string pattern, IResolver resolver)
        {
            UriTemplate template = new UriTemplate(resolver);
            return template.ExpandAsUri(pattern);
        }

        private UriTemplate(IResolver resolver)
            : base(resolver)
        {
        }

        public Uri ExpandAsUri(string pattern)
        {
            string ex = Expand(pattern);
            return new Uri(ex);
        }

        protected override void VerifyToken(string token)
        {
            foreach (char c in token.ToCharArray())
            {
                if (!IsTemplateChar(c))
                    throw new TemplateException(String.Format("Invalid character ({0:2x}), {0}", c));
            }
        }

        protected override void VerifyResult(string result)
        {
            try
            {
                Uri uri = new Uri(result);

                if (!uri.IsWellFormedOriginalString())
                    throw new TemplateException("Invalid expansion", new ArgumentException("malformed Uri", "result"));
            }
            catch (UriFormatException e)
            {
                throw new TemplateException("Invalid expansion", e);
            }
        }

        private bool IsTemplateChar(char c)
        {
            return (c >= 'A' && c <= 'Z') ||
                   (c >= 'a' && c <= 'z') ||
                   (c >= '0' && c <= '9') ||
                   c == '-' || c == '.' ||
                   c == '_' || c == '~';
        }
    }
}
