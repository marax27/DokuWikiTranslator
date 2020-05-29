using System.Collections.Generic;

namespace DokuWikiTranslator.Application.Generation.Abstractions
{
    public interface ISyntaxTreeNode
    {
        string Generate();
        IEnumerable<ISyntaxTreeNode> Children { get; }
    }
}
