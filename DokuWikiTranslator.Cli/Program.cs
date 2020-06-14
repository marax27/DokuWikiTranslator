using System;
using System.IO;
using DokuWikiTranslator.Application;
using DokuWikiTranslator.Application.Exceptions;

namespace DokuWikiTranslator.Cli
{
    class Program
    {
        private const string SampleSourceCode = "sample text**//very <del>artistic</del>{// not-so much**...\n\r\n\nSomething=>Else, **<<xyz>>**. Now, %%__this__ text **shan't** be formatted.%%  \r\n end of text";

        static void Main(string[] args)
        {
            var sourceCode = args.Length < 2 ? SampleSourceCode : LoadSourceFile(args[1]);
            var translator = new Translator();

            try
            {
                var outputCode = translator.Translate(sourceCode);
                Console.WriteLine(outputCode);
            }
            catch (TranslationException exc)
            {
                Console.WriteLine($"Error: {exc.Message}");
            }
        }

        private static string LoadSourceFile(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
