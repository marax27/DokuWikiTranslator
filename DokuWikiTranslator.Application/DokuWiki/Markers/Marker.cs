namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public interface IHtmlMarker
    {
        string HtmlTag { get; }
    }

    public readonly struct Marker : IHtmlMarker
    {
        public string Start { get; }
        public string End { get; }
        public string HtmlTag { get; }

        public Marker(string start, string end, string htmlTag)
        {
            Start = start;
            End = end;
            HtmlTag = htmlTag;
        }
    }
}
