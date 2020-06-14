using System;

namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public class TagMarker : IMarker
    {
        public TagMarker(string dokuWikiMarkerName, string htmlMarkerName)
        {
            if (string.IsNullOrEmpty(dokuWikiMarkerName))
                throw new ArgumentException(nameof(dokuWikiMarkerName));

            Start = $"<{dokuWikiMarkerName}>";
            End = $"</{dokuWikiMarkerName}>";
            HtmlTag = htmlMarkerName;
        }

        public string Start { get; }
        public string End { get; }
        public string HtmlTag { get; }

        public override string ToString()
            => $"<TagMarker({Start}, {End}) -> {HtmlTag}>";
    }
}
