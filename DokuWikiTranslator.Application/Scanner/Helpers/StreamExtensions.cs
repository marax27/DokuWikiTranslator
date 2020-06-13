namespace DokuWikiTranslator.Application.Scanner.Helpers
{
    public static class StreamExtensions
    {
        public static void Skip<T>(this IStream<T> stream, int count)
        {
            while (count > 0)
            {
                stream.Next();
                --count;
            }
        }
    }
}
