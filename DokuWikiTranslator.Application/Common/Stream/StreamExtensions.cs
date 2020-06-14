namespace DokuWikiTranslator.Application.Common.Stream
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
