using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.Scanner.Helpers;

namespace DokuWikiTranslator.Application.Scanner
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string sourceCode);
    }

    public class Lexer : ILexer
    {
        public IEnumerable<Token> Lex(string sourceCode)
        {
            var stream = new CharacterStream(sourceCode);

            return Array.Empty<Token>();
        }
    }
}
