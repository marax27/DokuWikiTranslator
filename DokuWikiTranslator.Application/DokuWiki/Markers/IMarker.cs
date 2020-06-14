namespace DokuWikiTranslator.Application.DokuWiki.Markers
{
    public interface IHtmlMarker
    {
        string HtmlTag { get; }
    }

    public interface IDokuWikiMarker
    {
        string Start { get; }
        string End { get; }
    }

    public interface IMarker : IDokuWikiMarker, IHtmlMarker { }
}
