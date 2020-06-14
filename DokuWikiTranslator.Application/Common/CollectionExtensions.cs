using System;
using System.Collections.Generic;
using System.Linq;

namespace DokuWikiTranslator.Application.Common
{
    public static class CollectionExtensions
    {
        public static T? LongestOrNull<T>(this IEnumerable<T> collection, Func<T, string> predicate)
            where T : class
        {
            T? result = null;
            foreach (var element in collection)
            {
                if (result == null)
                    result = element;
                else
                    result = predicate(result).Length < predicate(element).Length ? element : result;
            }
            return result;
        }
    }
}
