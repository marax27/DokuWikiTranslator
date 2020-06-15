using System;
using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.Parsing;
using DokuWikiTranslator.Application.Scanner;

namespace DokuWikiTranslator.Application
{
    public class Translator
    {
        public string Translate(string inputCode)
        {
            ILexer lexer = new Lexer();
            var tokens = lexer.Lex(inputCode).ToList();
            _onTokensScanned?.Invoke(tokens);

            IParser parser = new Parser();
            var dokuWikiNodes = parser.Parse(tokens);

            var htmlNodes = dokuWikiNodes.Select(node => node.Generate());
            var outputCode = string.Join("", htmlNodes.Select(node => node.Generate()));

            return outputCode;
        }

        public void OnTokensScanned(Action<IEnumerable<Token>> action)
        {
            _onTokensScanned = action;
        }

        private Action<IEnumerable<Token>>? _onTokensScanned = null;
    }
}
