using System;
using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.Scanner.Helpers;

namespace DokuWikiTranslator.Application.Scanner
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string sourceCode);
    }

    public class Lexer : ILexer
    {
        private readonly string[] _markers =
        {
            "//", "**", "__", "[[", "]]", "{{", "}}"
        };

        public IEnumerable<Token> Lex(string sourceCode)
        {
            var stream = new CharacterStream(sourceCode);
            var result = new List<Token>();
            var buffer = "";

            while (stream.HasNext())
            {
                var current = stream.Next();

                var matchingMarker = _markers.SingleOrDefault(marker => stream.Remaining.StartsWith(marker));
                if (matchingMarker == null)
                {
                    buffer += current;
                }
                else
                {
                    buffer = PopBuffer(result, buffer);
                    result.Add(new Token(TokenType.Marker, matchingMarker));
                    stream.Skip(matchingMarker.Length - 1);
                }
            }

            PopBuffer(result, buffer);
            return result;
        }

        private string PopBuffer(ICollection<Token> tokens, string buffer)
        {
            if (!string.IsNullOrEmpty(buffer))
            {
                tokens.Add(new Token(TokenType.Text, buffer));
                buffer = "";
            }
            return buffer;
        }
    }
}
