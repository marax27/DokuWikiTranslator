using System;
using System.Collections.Generic;
using System.IO;
using DokuWikiTranslator.Application;
using DokuWikiTranslator.Application.Exceptions;
using DokuWikiTranslator.Application.Scanner;

namespace DokuWikiTranslator.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (args.Length < 2)
            {
                Console.Error.WriteLine("Error: Filename not provided.");
                return;
            }

            var sourceCode = LoadSourceFile(args[1]);
            if (sourceCode == null)
                return;
            sourceCode = PreprocessSource(sourceCode);

            var translator = new Translator();
            translator.OnTokensScanned(DisplayAllTokens);

            try
            {
                var outputCode = translator.Translate(sourceCode);
                Console.WriteLine(outputCode);
            }
            catch (TranslationException exc)
            {
                Console.Error.WriteLine($"Translation error:\n{exc.Message}\n\nDetails:\n{exc.InnerException}");
            }
        }

        private static void DisplayAllTokens(IEnumerable<Token> tokens)
        {
            foreach(var t in tokens)
                Console.Error.Write(t.Type.ToString()[0] + "(" + t.Value.Replace("\n", @"\n") + ") ");
        }

        private static string PreprocessSource(string sourceCode)
        {
            return sourceCode.Replace("\r\n", "\n");
        }

        private static string? LoadSourceFile(string filename)
        {
            try
            {
                return File.ReadAllText(filename);
            }
            catch (IOException exc)
            {
                Console.WriteLine($"Error: {exc.Message}");
                return null;
            }
        }
    }
}
