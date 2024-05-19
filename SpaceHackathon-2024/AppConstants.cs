using SpaceHackathon_2024.Models;
using System.Collections.Immutable;

namespace SpaceHackathon_2024
{
    public static class AppConstants
    {
        public static readonly ImmutableArray<AppLanguage> SupportedLanguages =
        [
            new AppLanguage
                {
                    Name = "Russian",
                    CultureName = "ru",
                    FlagName = "RU",
                },
        ];

        public static string GatewayUrl
        {
            get
            {
                return IsProduction
                    ? "https://example.com"
                    : "https://example.com";
            }
        }
        public static string IdsUrl
        {
            get
            {
                return IsProduction
                    ? "https://example.com"
                    : "https://example.com";
            }
        }

        private static bool? isProduction;

        public static bool IsProduction
        {
            get
            {
                isProduction ??= GetIsProduction();

                return isProduction.GetValueOrDefault();
            }
        }

        private static bool GetIsProduction()
        {
#if DEBUG
            return false;
#else
            var version = VersionTracking.CurrentVersion;
            var lastDigit = version.Last();
            var versionInt = Convert.ToInt32(lastDigit);

            var result = versionInt % 2 == 0;

            return result;
#endif
        }
    }
}