using System;

namespace DokuWikiTranslator.Application.Common
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string str)
        {
            var result = Uri.TryCreate(str, UriKind.Absolute, out var uriResult);
            return result && (uriResult?.Scheme == Uri.UriSchemeHttp || uriResult?.Scheme == Uri.UriSchemeHttps);
        }
    }
}
