namespace BookStore.Shared.Extensions
{
    using System;

    /// <summary>
    /// Extension for system type String
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Checking if string contains another string while ignoring case when compare
        /// </summary>
        /// <param name="source">String in which need to find</param>
        /// <param name="toCheck">String which need find in another string</param>
        /// <returns>If string toCheck contains in source string</returns>
        public static bool ContainsIgnoringCase(this string source, string toCheck)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
