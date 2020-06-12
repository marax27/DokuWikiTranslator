using System.Collections.Generic;

namespace DokuWikiTranslator.Application.Generation.Abstractions
{
    public interface ISyntaxTreeNode<out T>
    {
        T Generate();
        IEnumerable<ISyntaxTreeNode<T>> Children { get; }
    }
}
