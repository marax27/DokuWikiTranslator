namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public readonly struct AsymmetricMarker : IMarker
    {
        public string Start { get; }
        public string End { get; }
        public string HtmlTag { get; }

        public AsymmetricMarker(string start, string end, string htmlTag)
        {
            Start = start;
            End = end;
            HtmlTag = htmlTag;
        }
    }
}
