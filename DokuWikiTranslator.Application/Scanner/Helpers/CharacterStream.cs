using System;
using DokuWikiTranslator.Application.Common.Stream;

namespace DokuWikiTranslator.Application.Scanner.Helpers
{
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
            throw new StreamEndException($"Failed to read character no. {_index + 1}: no more characters to read.");
        }

        public bool HasNext()
            => _index + 1 < _data.Length;
    }
}
