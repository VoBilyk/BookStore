namespace BookStore.Shared.Extensions
{
    using System;

    public static class StringExtension
    {
        public static bool ContainsIgnoringCase(this string source, string toCheck)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
