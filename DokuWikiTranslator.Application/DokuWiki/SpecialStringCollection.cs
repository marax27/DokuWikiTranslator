using DokuWikiTranslator.Application.DokuWiki.SpecialStrings;

namespace DokuWikiTranslator.Application.DokuWiki
{
    public static class SpecialStringCollection
    {
        public static readonly SpecialString[] SpecialStrings =
        {
            new SpecialString("->", "&rarr;"),
            new SpecialString("<-", "&larr;"),
            new SpecialString("<->", "&harr;"),
            new SpecialString("=>", "&rArr;"),
            new SpecialString("<=", "&lArr;"),
            new SpecialString("<=>", "&hArr;"),
            new SpecialString("<<", "&laquo;"),
            new SpecialString(">>", "&raquo;"),
            new SpecialString("(c)", "&copy;"),
            new SpecialString("(r)", "&reg;"),
            new SpecialString("(tm)", "&trade;"),
            new SpecialString("\\\\ ", "<br/>"), 
            new SpecialString("\\\\\n", "<br/>"),
        };
    }
}
