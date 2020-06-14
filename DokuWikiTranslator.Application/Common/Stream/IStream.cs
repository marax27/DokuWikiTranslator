using System;

namespace DokuWikiTranslator.Application.Common.Stream
{
    public interface IStream<T>
    {
        T Next();
        bool HasNext();
        ReadOnlySpan<T> Remaining { get; }
    }
}
