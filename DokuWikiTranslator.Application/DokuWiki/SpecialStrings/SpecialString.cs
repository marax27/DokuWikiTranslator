namespace DokuWikiTranslator.Application.DokuWiki.SpecialStrings
{
    public class SpecialString
    {
        public string Source { get; }
        public string Output { get; }

        public SpecialString(string source, string output)
        {
            Source = source;
            Output = output;
        }
    }
}
