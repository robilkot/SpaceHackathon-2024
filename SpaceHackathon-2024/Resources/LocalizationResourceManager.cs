using SpaceHackathon_2024.Helpers;
using System.ComponentModel;
using System.Globalization;

namespace SpaceHackathon_2024.Resources
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        public event PropertyChangedEventHandler? PropertyChanged;

        private LocalizationResourceManager()
        {
            var cultureName = Preferences.Get(PrefKeys.AppCulture, null);

            if (cultureName != null)
            {
                SetCulture(new CultureInfo(cultureName));
            }
        }

        public string this[string key] => ResourcesView.ResourceManager.GetString(key, ResourcesView.Culture);

        public void SetCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            ResourcesView.Culture = language;

            Invalidate();

            Preferences.Set(PrefKeys.AppCulture, language.TwoLetterISOLanguageName);
        }

        /// <summary>
        /// Triggers all localization bindings in the app to update their values.
        /// </summary>
        private void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
