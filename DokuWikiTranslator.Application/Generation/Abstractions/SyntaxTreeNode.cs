using System.Collections.Generic;

namespace DokuWikiTranslator.Application.Generation.Abstractions
{
    public abstract class SyntaxTreeNode : ISyntaxTreeNode
    {
        public IEnumerable<ISyntaxTreeNode> Children { get; }

        public SyntaxTreeNode(IReadOnlyCollection<ISyntaxTreeNode> childNodes)
        {
            Children = childNodes;
        }

        public abstract string Generate();
    }
}
