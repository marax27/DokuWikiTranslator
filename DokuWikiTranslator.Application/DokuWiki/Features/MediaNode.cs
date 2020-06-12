using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class MediaNode : IDokuWikiTreeNode
    {
        public MediaNode(string mediaPath, string sourceCode)
        {
            MediaPath = mediaPath;
            SourceCode = sourceCode;
        }

        public string MediaPath { get; }
        public string SourceCode { get; }

        public IHtmlSyntaxTreeNode Generate()
        {
            var attributes = new[]
            {
                new HtmlAttribute("src", MediaPath),
            };
            return new HtmlElement("img", attributes, Array.Empty<IHtmlSyntaxTreeNode>());
        }

        public IEnumerable<ISyntaxTreeNode<IHtmlSyntaxTreeNode>> Children
            => Array.Empty<ISyntaxTreeNode<IHtmlSyntaxTreeNode>>();
    }
}
