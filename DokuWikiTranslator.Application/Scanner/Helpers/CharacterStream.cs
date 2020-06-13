using System;

namespace DokuWikiTranslator.Application.Scanner.Helpers
{
    public class EndOfDataException : Exception
    {
        public EndOfDataException(string message)
            : base(message) { }
    }

    public interface IStream<T>
    {
        T Next();
        bool HasNext();
        ReadOnlySpan<T> Remaining { get; }
    }

    public class CharacterStream : IStream<char>
    {
        private readonly string _data;
        private int _index;

        public CharacterStream(string data)
        {
            _data = data;
            _index = -1;
        }

        public ReadOnlySpan<char> Remaining
            => _data.Substring(_index).AsSpan();
        
        public char Next()
        {
            if (HasNext())
                return _data[++_index];
            throw new EndOfDataException($"Failed to read character no. {_index + 1}: no more characters to read.");
        }

        public bool HasNext()
            => _index + 1 < _data.Length;
    }
}
