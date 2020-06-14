using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.Generation.Abstractions;

namespace DokuWikiTranslator.Application.DokuWiki.Abstractions
{
    public interface IDokuWikiTreeNode : ISyntaxTreeNode<IHtmlSyntaxTreeNode>
    {
        string SourceCode { get; }
    }

    public abstract class DokuWikiTreeNode : IDokuWikiTreeNode
    {
        public abstract IHtmlSyntaxTreeNode Generate();
        public IEnumerable<ISyntaxTreeNode<IHtmlSyntaxTreeNode>> Children { get; }
        public string SourceCode { get; }

        protected DokuWikiTreeNode(string sourceCode, IReadOnlyCollection<IDokuWikiTreeNode> childNodes)
        {
            Children = childNodes;
            SourceCode = sourceCode;
        }

        protected IEnumerable<IHtmlSyntaxTreeNode> ProcessChildrenRecursively()
            => Children.Select(node => node.Generate());
    }
}
