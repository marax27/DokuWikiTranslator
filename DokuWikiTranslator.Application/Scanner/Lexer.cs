using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using DokuWikiTranslator.Application.Common;
using DokuWikiTranslator.Application.Common.Stream;
using DokuWikiTranslator.Application.DokuWiki;
using DokuWikiTranslator.Application.Exceptions;
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
            var lineCounter = 1;
            _buffer = "";

            try
            {
                while (stream.HasNext())
                {
                    var current = stream.Next();

                    ReadOnlyCollection<Token> tokens = TryFindMarker(stream);
                    if (!tokens.Any())
                    {
                        tokens = TryNewLine(stream);
                        if (tokens.Any(token => token.Type == TokenType.NewLine))
                            ++lineCounter;

                        if (!tokens.Any())
                        {
                            tokens = TryFindSpecial(stream);
                            if (!tokens.Any())
                            {
                                _buffer += current;
                            }
                        }
                    }

                    result.AddRange(tokens);
                }
            }
            catch (Exception exc)
            {
                throw new TranslationException($"Lexer error at line {lineCounter}: {exc.Message}", exc);
            }

            result.AddRange(PopBuffer());
            return result;
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

            // Find start-of-line marker.
            string[] patterns = { @">+", @"==+", @"[^\*](\*)[^\*]", @"[^-](-)[^->]", "----+" };
            var remaining = stream.Remaining.ToString();
            var index = remaining.IndexOf(ch => !char.IsWhiteSpace(ch));
            if (index != -1)
            {
                var bestMatch = patterns
                    .Select(pattern => Regex.Match(remaining, pattern))
                    .Where(match => match.Success)
                    .Select(match => match.Groups[^1])
                    .Where(group => group.Index == index)
                    .LongestOrNull(group => group.Value)
                    ?.Value;

                if (bestMatch != null)
                {
                    result.AddRange(PopBuffer());
                    if (index > 1)
                        result.Add(new Token(TokenType.Text, remaining[1..index]));
                    result.Add(new Token(TokenType.Special, bestMatch));
                    stream.Skip(index + bestMatch.Length - 1);
                }
            }

            if (!result.Any())
            {
                // Find a special character.
                var matchingSpecial = SpecialStringCollection.SpecialStrings
                    .Where(s => stream.Remaining.StartsWith(s.Source))
                    .LongestOrNull(ms => ms.Source);
                if (matchingSpecial != null)
                {
                    result.AddRange(PopBuffer());
                    result.Add(new Token(TokenType.Special, matchingSpecial.Source));
                    stream.Skip(matchingSpecial.Source.Length - 1);
                }
            }
            return result.AsReadOnly();
        }

        private ReadOnlyCollection<Token> TryNewLine(ICharacterStream stream)
        {
            // Find newline.
            var result = new List<Token>();
            if (stream.Remaining.StartsWith("\n"))
            {
                result.AddRange(PopBuffer());
                result.Add(new Token(TokenType.NewLine, "\n"));
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
