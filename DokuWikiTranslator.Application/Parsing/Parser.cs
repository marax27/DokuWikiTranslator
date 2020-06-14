using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using DokuWikiTranslator.Application.Common.Stream;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.DokuWiki.Features;
using DokuWikiTranslator.Application.DokuWiki.Markers;
using DokuWikiTranslator.Application.Scanner;

namespace DokuWikiTranslator.Application.Parsing
{
    public interface IParser
    {
        IEnumerable<IDokuWikiTreeNode> Parse(IEnumerable<Token> tokens);
    }

    public class Parser : IParser
    {
        public IEnumerable<IDokuWikiTreeNode> Parse(IEnumerable<Token> tokens)
        {
            var stream = CreateStream(tokens);
            var result = new List<IDokuWikiTreeNode>();

            while (stream.HasNext())
            {
                var current = stream.Next();
                switch (current.Type)
                {
                    case TokenType.Text:
                        result.Add(new RawTextNode(current.Value, current.Value));
                        break;
                    case TokenType.Special:
                        result.Add(new RawTextNode("TODO!", current.Value));
                        break;
                    case TokenType.Marker:
                        result.Add(ProcessMarker(current, stream));
                        break;
                    default:
                        continue;
                }
            }

            return result;
        }

        private IDokuWikiTreeNode ProcessMarker(Token startToken, IStream<Token> stream)
        {
            var allMarkers = MarkerCollection.LanguageMarkers;
            var marker = allMarkers.Single(marker => marker.Start == startToken.Value);

            var innerTokens = new List<Token>();
            var current = stream.Next();
            while (current.Value != marker.End)
            {
                innerTokens.Add(current);
                current = stream.Next();
            }

            var sourceCode = GetSourceCode(new[] {startToken}.Concat(innerTokens).Concat(new[] {current}));
            var innerNodes = Parse(innerTokens).ToList().AsReadOnly();
            return new BasicMarkerNode(sourceCode, innerNodes, marker);
        }

        private string GetSourceCode(IEnumerable<Token> tokens)
            => string.Join("", tokens.Select(token => token.Value));

        private IStream<Token> CreateStream(IEnumerable<Token> tokens)
            => new TokenStream(tokens);
    }
}
