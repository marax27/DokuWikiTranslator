using System;
using System.IO;
using DokuWikiTranslator.Application;
using DokuWikiTranslator.Application.Exceptions;

namespace DokuWikiTranslator.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Filename not provided.");
                return;
            }

            var sourceCode = LoadSourceFile(args[1]);
            if (sourceCode == null)
                return;

            var translator = new Translator();
            try
            {
                var outputCode = translator.Translate(sourceCode);
                Console.WriteLine(outputCode);
            }
            catch (TranslationException exc)
            {
                Console.WriteLine($"Error: {exc.Message}\n{exc.InnerException}");
            }
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
