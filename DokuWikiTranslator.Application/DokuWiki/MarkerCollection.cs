using DokuWikiTranslator.Application.DokuWiki.Markers;

namespace DokuWikiTranslator.Application.DokuWiki
{
    public static class MarkerCollection
    {
        public static readonly IMarker[] LanguageMarkers =
        {
            new AsymmetricMarker("//", "//", "i"),
            new AsymmetricMarker("**", "**", "b"),
            new AsymmetricMarker("__", "__", "u"),
            new AsymmetricMarker("[[", "]]", "a"),
            new AsymmetricMarker("{{", "}}", "img"),
            new AsymmetricMarker("%%", "%%", ""),
            new AsymmetricMarker("''", "''", "tt"),
            new TagMarker("nowiki", ""),
            new TagMarker("sup", "sup"), 
            new TagMarker("sub", "sub"), 
            new TagMarker("del", "strike"),
        };
    }
}
