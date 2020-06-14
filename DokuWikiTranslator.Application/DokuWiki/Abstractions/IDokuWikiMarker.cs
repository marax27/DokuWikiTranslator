namespace DokuWikiTranslator.Application.DokuWiki.Abstractions
{
    public interface IDokuWikiMarker
    {
        string Identifier { get; }
        string HtmlTag { get; }
    }

    public class DokuWikiMarker : IDokuWikiMarker
    {
        public DokuWikiMarker(string identifier, string htmlTag)
        {
            Identifier = identifier;
            HtmlTag = htmlTag;
        }

        public string Identifier { get; }
        public string HtmlTag { get; }


    }
}