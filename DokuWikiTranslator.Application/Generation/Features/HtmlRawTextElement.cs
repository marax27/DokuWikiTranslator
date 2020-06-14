using System.Collections.Generic;
using DokuWikiTranslator.Application.Generation.Abstractions;

namespace DokuWikiTranslator.Application.Generation.Features
{
    public class HtmlRawTextElement : IHtmlSyntaxTreeNode
    {
        public string Text { get; }

        public HtmlRawTextElement(string text)
            => Text = text;

        public string Generate()
            => Text;

        public IEnumerable<ISyntaxTreeNode<string>> Children
            => new List<IHtmlSyntaxTreeNode>();
    }
}
