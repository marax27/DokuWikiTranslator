namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public readonly struct Marker
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
