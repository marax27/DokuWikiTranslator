using System.Collections.Generic;

namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public static class MarkerCollection
    {
        public static IEnumerable<Marker> GetAllMarkers()
            => new List<Marker>
            {
                new Marker("//", "//", "i"),
                new Marker("**", "**", "b"),
                new Marker("__", "__", "u"),
                new Marker("[[", "]]", "a"),
                new Marker("{{", "}}", "img"),
                new Marker("%%", "%%", "pre"),
                new Marker("''", "''", "tt")
            };
    }
}
