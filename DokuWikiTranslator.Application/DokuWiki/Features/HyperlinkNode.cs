using System;
using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class HyperlinkNode : DokuWikiTreeNode
    {
        public HyperlinkNode(string url, string? description, string sourceCode)
            : base(sourceCode, new []{ new RawTextNode(description ?? url) })
        {
            Url = url;
            Description = description;
        }

        public string? Description { get; }
        public string Url { get; }

        public override IHtmlSyntaxTreeNode Generate()
        {
            var attributes = new[] {new HtmlAttribute("href", Url)};
            return new HtmlElement("a", attributes, ProcessChildrenRecursively().ToList().AsReadOnly());
        }
    }
}
