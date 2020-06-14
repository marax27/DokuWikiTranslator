namespace DokuWikiTranslator.Application.DokuWiki.Markers
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
            new AsymmetricMarker("%%", "%%", "pre"),
            new AsymmetricMarker("''", "''", "tt"),
            new TagMarker("nowiki", "pre"),
            new TagMarker("sup", "sup"), 
            new TagMarker("sub", "sub"), 
            new TagMarker("del", "strike"),
        };
    }
}
