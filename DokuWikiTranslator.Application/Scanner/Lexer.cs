using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            "//", "**", "__", "[[", "]]", "{{", "}}", "%%", "''",
        };

        private readonly string[] _specialStrings =
        {
            "->", "<-", "<->", "=>", "<=", "<=>", "<<", ">>", "(c)", "(r)", "(tm)"
        };

        private string _buffer = "";

        public IEnumerable<Token> Lex(string sourceCode)
        {
            var stream = new CharacterStream(sourceCode);
            var result = new List<Token>();
            _buffer = "";

            while (stream.HasNext())
            {
                var current = stream.Next();

                ReadOnlyCollection<Token> tokens = TryFindMarker(stream);
                if (!tokens.Any())
                {
                    tokens = TryFindSpecial(stream);
                    if (!tokens.Any())
                        _buffer += current;
                }
                result.AddRange(tokens);
            }

            result.AddRange(PopBuffer());
            return result;
        }

        private ReadOnlyCollection<Token> TryFindMarker(CharacterStream stream)
        {
            var result = new List<Token>();
            var matchingMarker = _markers.SingleOrDefault(marker => stream.Remaining.StartsWith(marker));
            if (matchingMarker != null)
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.Marker, matchingMarker));
                stream.Skip(matchingMarker.Length - 1);
            }
            return result.AsReadOnly();
        }

        private ReadOnlyCollection<Token> TryFindSpecial(CharacterStream stream)
        {
            var result = new List<Token>();
            var matchingSpecial = _specialStrings.SingleOrDefault(s => stream.Remaining.StartsWith(s));
            if (matchingSpecial != null)
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.Special, matchingSpecial));
                stream.Skip(matchingSpecial.Length - 1);
            }
            return result.AsReadOnly();
        }

        private IEnumerable<Token> PopBuffer()
        {
            var result = new List<Token>();
            if (!string.IsNullOrEmpty(_buffer))
            {
                result.Add(new Token(TokenType.Text, _buffer));
                _buffer = "";
            }
            return result;
        }
    }
}
