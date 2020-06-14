using System.Linq;
using DokuWikiTranslator.Application.Parsing;
using DokuWikiTranslator.Application.Scanner;

namespace DokuWikiTranslator.Application
{
    public class Translator
    {
        public string Translate(string inputCode)
        {
            ILexer lexer = new Lexer();
            var tokens = lexer.Lex(inputCode);

            IParser parser = new Parser();
            var dokuWikiNodes = parser.Parse(tokens);

            var htmlNodes = dokuWikiNodes.Select(node => node.Generate());
            var outputCode = string.Join("", htmlNodes.Select(node => node.Generate()));

            return outputCode;
        }
    }
}
