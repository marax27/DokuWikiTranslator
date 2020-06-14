using System;

namespace DokuWikiTranslator.Application.Exceptions
{
    public class TranslationException : Exception
    {
        public TranslationException(string message)
            : base(message) { }
    }
}
