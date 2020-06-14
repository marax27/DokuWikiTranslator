
namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public class TagMarker : IMarker
    {
        public TagMarker(string dokuWikiMarkerName, string htmlMarkerName)
        {
            Start = $"<{dokuWikiMarkerName}>";
            End = $"</{dokuWikiMarkerName}>";
            HtmlTag = htmlMarkerName;
        }

        public string Start { get; }
        public string End { get; }
        public string HtmlTag { get; }
    }
}
