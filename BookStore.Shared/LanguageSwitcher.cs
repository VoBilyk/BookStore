namespace BookStore.Shared
{
    using System.Collections.Generic;
    using System.Globalization;

    using BookStore.Shared.Resources;

    /// <summary>
    /// Language localization switcher
    /// </summary>
    public static class LanguageSwitcher
    {
        private static readonly Dictionary<string, string> _cultures = new Dictionary<string, string>
        {
            { "en", "en-US" },
            { "ua", "uk-UA" }
        };

        /// <summary>
        /// Gets or sets current culture
        /// </summary>
        public static CultureInfo Culture
        {
            get => Resource.Culture;
            set => Resource.Culture = value;
        }

        /// <summary>
        /// Switch localization to another
        /// </summary>
        public static void Switch()
        {
            if (Culture.Name == _cultures["en"])
            {
                Culture = new CultureInfo(_cultures["ua"]);
            }
            else
            {
                Culture = new CultureInfo(_cultures["en"]);
            }
        }
    }
}
