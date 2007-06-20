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
