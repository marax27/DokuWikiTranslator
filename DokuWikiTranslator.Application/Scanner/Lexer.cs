using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DokuWikiTranslator.Application.Common;
using DokuWikiTranslator.Application.Common.Stream;
using DokuWikiTranslator.Application.DokuWiki;
using DokuWikiTranslator.Application.Scanner.Helpers;

namespace DokuWikiTranslator.Application.Scanner
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string sourceCode);
    }

    public class Lexer : ILexer
    {
        private string _buffer = "";

        public IEnumerable<Token> Lex(string sourceCode)
        {
            ICharacterStream stream = new CharacterStream(sourceCode);
            var result = new List<Token>();
            _buffer = "";

            while (stream.HasNext())
            {
                var current = stream.Next();

                ReadOnlyCollection<Token> tokens = TryFindUrl(stream);

                if (!tokens.Any())
                {
                    tokens = TryFindMarker(stream);
                    if (!tokens.Any())
                    {
                        tokens = TryFindSpecial(stream);
                        if (!tokens.Any())
                        {
                            tokens = TryNewLine(stream);
                            if (!tokens.Any())
                                _buffer += current;
                        }
                    }
                }
                result.AddRange(tokens);
            }

            result.AddRange(PopBuffer());
            return result.Select(token => { Console.Write($"[{token.Value}] ");
                return token;
            });
        }

        private ReadOnlyCollection<Token> TryFindUrl(ICharacterStream stream)
        {
            var result = new List<Token>();
            var textEnd = stream.Remaining.IndexOfAny(" \t\r\n".ToArray());
            var substring = stream.Remaining[..textEnd].ToString();

            if (substring.IsValidUrl())
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.Url, substring));
                stream.Skip(substring.Length - 1);
            }

            return result.AsReadOnly();
        }

        private ReadOnlyCollection<Token> TryFindMarker(ICharacterStream stream)
        {
            var result = new List<Token>();

            string? foundString = null;
            var matches = MarkerCollection.LanguageMarkers
                .Where(marker => stream.Remaining.StartsWith(marker.Start)).ToList();
            if (matches.Any())
                foundString = matches.Single().Start;
            else
            {
                matches = MarkerCollection.LanguageMarkers
                    .Where(marker => stream.Remaining.StartsWith(marker.End)).ToList();
                if (matches.Any())
                    foundString = matches.Single().End;
            }

            if (foundString != null)
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.Marker, foundString));
                stream.Skip(foundString.Length - 1);
            }
            return result.AsReadOnly();
        }

        private ReadOnlyCollection<Token> TryFindSpecial(ICharacterStream stream)
        { 
            var result = new List<Token>();
            var matchingSpecial = SpecialStringCollection.SpecialStrings
                .Where(s => stream.Remaining.StartsWith(s.Source))
                .LongestOrNull(ms => ms.Source);
            if (matchingSpecial != null)
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.Special, matchingSpecial.Source));
                stream.Skip(matchingSpecial.Source.Length - 1);
            }
            return result.AsReadOnly();
        }

        private ReadOnlyCollection<Token> TryNewLine(ICharacterStream stream)
        {
            var result = new List<Token>();
            string[] newLineIndicators = {"\n", "\r\n"};
            var matchingIndicator = newLineIndicators.SingleOrDefault(s => stream.Remaining.StartsWith(s));
            if (matchingIndicator != null)
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.NewLine, matchingIndicator));
                stream.Skip(matchingIndicator.Length - 1);
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
