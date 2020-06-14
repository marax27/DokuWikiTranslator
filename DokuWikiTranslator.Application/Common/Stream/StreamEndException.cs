using System;

namespace DokuWikiTranslator.Application.Common.Stream
{
    public class StreamEndException : Exception
    {
        public StreamEndException(string message)
            : base(message) { }
    }
}
