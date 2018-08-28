namespace BookStore.Shared
{
    using System.Collections.Generic;
    using System.Globalization;

    using BookStore.Shared.Resources;

    public static class LanguageSwitcher
    {
        private static readonly Dictionary<string, string> _cultures = new Dictionary<string, string>
        {
            { "en", "en-US" },
            { "ua", "uk-UA" }
        };

        public static CultureInfo Culture
        {
            get => Resource.Culture;
            set => Resource.Culture = value;
        }

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
