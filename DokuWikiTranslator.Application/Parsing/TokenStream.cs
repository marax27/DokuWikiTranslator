using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.Common.Stream;
using DokuWikiTranslator.Application.Scanner;

namespace DokuWikiTranslator.Application.Parsing
{
    public class TokenStream : IStream<Token>
    {
        private readonly IList<Token> _data;
        private int _index;

        public TokenStream(IEnumerable<Token> data)
        {
            _data = data.ToList();
            _index = -1;
        }

        public Token Next()
        {
            if (HasNext())
                return _data[++_index];
            throw new StreamEndException($"Failed to read token no. {_index + 1}: no more tokens to read.");
        }

        public bool HasNext()
            => _index + 1 < _data.Count;
    }
}
