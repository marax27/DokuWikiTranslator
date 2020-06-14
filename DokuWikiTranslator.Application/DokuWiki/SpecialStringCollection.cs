using DokuWikiTranslator.Application.DokuWiki.SpecialStrings;

namespace DokuWikiTranslator.Application.DokuWiki
{
    public static class SpecialStringCollection
    {
        public static readonly SpecialString[] SpecialStrings =
        {
            new SpecialString("->", "→"),
            new SpecialString("<-", "←"),
            new SpecialString("<->", "↔"),
            new SpecialString("=>", "⇒"),
            new SpecialString("<=", "⇐"),
            new SpecialString("<=>", "⇔"),
            new SpecialString("<<", "«"),
            new SpecialString(">>", "»"),
            new SpecialString("(c)", "©"),
            new SpecialString("(r)", "®"),
            new SpecialString("(tm)", "™"),
        };
    }
}
