using SpaceHackathon_2024.Interfaces;

namespace SpaceHackathon_2024.Models
{
    public class AppLanguage : ISelectable
    {
        public string Name { get; set; } = string.Empty;
        public string CultureName { get; set; } = string.Empty;
        public string FlagName { get; set; } = string.Empty;
        public ImageSource ImageSource => ImageSource.FromFile($"Flags/{FlagName}.png");
        public string Description => Name;
    }
}
