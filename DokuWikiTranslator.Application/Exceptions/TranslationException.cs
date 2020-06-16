using System;

namespace DokuWikiTranslator.Application.Exceptions
{
    public class TranslationException : Exception
    {
        public int? LineCount { get; }

        public TranslationException(string message, int? lineCount)
            : base(message)
        {
            LineCount = lineCount;
        }

        public TranslationException(string message, int? lineCount, Exception inner)
            : base(message, inner)
        {
            LineCount = lineCount;
        }
    }
}
