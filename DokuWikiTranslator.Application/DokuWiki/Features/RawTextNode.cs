using System;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;
using System.Collections.Generic;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class RawTextNode : IDokuWikiTreeNode
    {
        public RawTextNode(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public string SourceCode => Text;

        public IEnumerable<ISyntaxTreeNode<IHtmlSyntaxTreeNode>> Children
            => Array.Empty<ISyntaxTreeNode<IHtmlSyntaxTreeNode>>();

        public IHtmlSyntaxTreeNode Generate()
        {
            return new HtmlRawTextElement(Text);
        }
    }
}
