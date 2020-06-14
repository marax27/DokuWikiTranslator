using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class MediaNode : IDokuWikiTreeNode
    {
        public MediaNode(string mediaPath, string? description, string sourceCode)
        {
            MediaPath = mediaPath;
            Description = description;
            SourceCode = sourceCode;
        }

        public string MediaPath { get; }
        public string? Description { get; }
        public string SourceCode { get; }

        public IHtmlSyntaxTreeNode Generate()
        {
            var attributes = new List<HtmlAttribute> { new HtmlAttribute("src", MediaPath) };
            if (Description != null)
            {
                attributes.Add(new HtmlAttribute("title", Description));
                attributes.Add(new HtmlAttribute("alt", Description));
            }
            return new HtmlElement("img", attributes, Array.Empty<IHtmlSyntaxTreeNode>());
        }

        public IEnumerable<ISyntaxTreeNode<IHtmlSyntaxTreeNode>> Children
            => Array.Empty<ISyntaxTreeNode<IHtmlSyntaxTreeNode>>();
    }
}
