﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DokuWikiTranslator.Application.Common.Stream;
using DokuWikiTranslator.Application.DokuWiki;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.DokuWiki.Features;
using DokuWikiTranslator.Application.DokuWiki.Markers;
using DokuWikiTranslator.Application.Exceptions;
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
            var startOfLine = true;

            while (stream.HasNext())
            {
                var current = stream.Next();
                switch (current.Type)
                {
                    case TokenType.Text:
                        result.Add(new RawTextNode(current.Value));
                        startOfLine = string.IsNullOrWhiteSpace(current.Value);
                        break;
                    case TokenType.Special:
                        result.Add(ProcessSpecial(current, stream, startOfLine));
                        startOfLine = false;
                        break;
                    case TokenType.Marker:
                        result.Add(ProcessMarker(current, stream));
                        startOfLine = false;
                        break;
                    case TokenType.Url:
                        result.Add(new HyperlinkNode(current.Value, null, current.Value));
                        startOfLine = false;
                        break;
                    case TokenType.NewLine:
                        result.Add(new RawTextNode("\n"));
                        startOfLine = true;
                        break;
                }
            }

            return result;
        }

        private IDokuWikiTreeNode ProcessSpecial(Token current, IStream<Token> stream, bool startOfLine)
        {
            var marker = current.Value;
            
            Console.WriteLine($"Try: {startOfLine} | {marker}");
            if (startOfLine)
            {
                // Line start marker.
                if (marker.StartsWith("----"))
                {
                    return new BasicMarkerNode(marker,
                        new List<IDokuWikiTreeNode>().AsReadOnly(),
                        new TagMarker(marker, "hr"));
                }
                else if (marker.StartsWith("=="))
                {
                    var innerTokens = new List<Token>();
                    var cur = stream.Next();
                    while (cur.Value != marker)
                    {
                        innerTokens.Add(cur);
                        cur = stream.Next();
                    }

                    var children = Parse(innerTokens).ToList().AsReadOnly();
                    var sourceCode = GetSourceCode(new[] {current}.Concat(innerTokens).Concat(new[] {cur}));
                    return new BasicMarkerNode(sourceCode, children, new TagMarker(marker, "h1"));
                }
                else return new RawTextNode(marker);
            }
            else
            {
                var special = SpecialStringCollection.SpecialStrings
                    .SingleOrDefault(s => s.Source == marker);
                return new RawTextNode(special?.Output ?? marker);
            }
        }

        private IDokuWikiTreeNode ProcessMarker(Token startToken, IStream<Token> stream)
        {
            var allMarkers = MarkerCollection.LanguageMarkers;
            var foundMarker = allMarkers.Single(marker => marker.Start == startToken.Value);

            try
            {
                var innerTokens = new List<Token>();
                var current = stream.Next();
                while (current.Value != foundMarker.End)
                {
                    innerTokens.Add(current);
                    current = stream.Next();
                }

                return CreateMarkerNode(startToken, innerTokens, current, foundMarker);
            }
            catch (StreamEndException exc)
            {
                throw new TranslationException($"Failed while processing marker {foundMarker}", exc);
            }
        }

        private IDokuWikiTreeNode CreateMarkerNode(Token startToken, IList<Token> innerTokens, Token endToken, IMarker marker)
        {
            if (startToken.Type != TokenType.Marker)
                throw new ArgumentException($"{startToken} is not a marker.");

            var sourceCode = GetSourceCode(new[] { startToken }.Concat(innerTokens).Concat(new[] { endToken }));
            var innerText = string.Join("", innerTokens.Select(t => t.Value));

            switch (startToken.Value)
            {
                case "%%":
                case "<nowiki>":
                    return new RawTextNode(innerText);
                case "[[":
                    return HandleHyperlink(innerText, sourceCode);
                case "{{":
                    return HandleMedia(innerText, sourceCode);
                default:
                    return new BasicMarkerNode(sourceCode, ProcessInnerNodesRecursively(innerTokens), marker);
            }
        }

        private MediaNode HandleMedia(string innerText, string sourceCode)
        {
            var parts = innerText.Split('|', 2);
            var url = parts[0];
            var description = parts.Length > 1 ? parts[1] : null;
            return new MediaNode(url, description, sourceCode);
        }

        private HyperlinkNode HandleHyperlink(string innerText, string sourceCode)
        {
            var parts = innerText.Split('|', 2);
            var url = parts[0];
            var description = parts.Length > 1 ? parts[1] : url;
            return new HyperlinkNode(url, description, sourceCode);
        }

        private ReadOnlyCollection<IDokuWikiTreeNode> ProcessInnerNodesRecursively(IEnumerable<Token> tokens)
        {
            var result = Parse(tokens);
            return result.ToList().AsReadOnly();
        }

        private string GetSourceCode(IEnumerable<Token> tokens)
            => string.Join("", tokens.Select(token => token.Value));

        private IStream<Token> CreateStream(IEnumerable<Token> tokens)
            => new TokenStream(tokens);
    }
}
