﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
                        result.Add(new RawTextNode(current.Value));
                        break;
                    case TokenType.Special:
                        result.Add(new RawTextNode("TODO!"));
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
            var innerNodes = HandleInner(innerTokens, marker);
            return new BasicMarkerNode(sourceCode, innerNodes, marker);
        }

        private ReadOnlyCollection<IDokuWikiTreeNode> HandleInner(IEnumerable<Token> tokens, IMarker marker)
        {
            var start = marker.Start;
            var result = new List<IDokuWikiTreeNode>();

            if (new[] {"%%", "<nowiki>"}.Contains(start))
            {
                var text = string.Join("", tokens.Select(token => token.Value));
                result.Add(new RawTextNode(text));
            }
            else
            {
                result.AddRange(Parse(tokens));
            }

            return result.AsReadOnly();
        }

        private string GetSourceCode(IEnumerable<Token> tokens)
            => string.Join("", tokens.Select(token => token.Value));

        private IStream<Token> CreateStream(IEnumerable<Token> tokens)
            => new TokenStream(tokens);
    }
}