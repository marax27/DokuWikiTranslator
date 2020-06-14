using System;

namespace DokuWikiTranslator.Application.Common.Stream
{
    public interface IStream<out T>
    {
        T Next();
        bool HasNext();
    }
}
