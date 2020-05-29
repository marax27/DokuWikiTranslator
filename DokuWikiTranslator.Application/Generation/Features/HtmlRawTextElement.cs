using System.Collections.Generic;
using DokuWikiTranslator.Application.Generation.Abstractions;

namespace DokuWikiTranslator.Application.Generation.Features
{
    public class HtmlRawTextElement : ISyntaxTreeNode
    {
        public string Text { get; }

        public HtmlRawTextElement(string text)
            => Text = text;

        public string Generate()
            => Text;

        public IEnumerable<ISyntaxTreeNode> Children
            => new List<ISyntaxTreeNode>();
    }
}
