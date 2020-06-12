using System.Collections.Generic;

namespace DokuWikiTranslator.Application.Generation.Abstractions
{
    public abstract class HtmlSyntaxTreeNode : IHtmlSyntaxTreeNode
    {
        public IEnumerable<ISyntaxTreeNode<string>> Children { get; }

        public HtmlSyntaxTreeNode(IReadOnlyCollection<IHtmlSyntaxTreeNode> childNodes)
        {
            Children = childNodes;
        }

        public abstract string Generate();
    }
}
