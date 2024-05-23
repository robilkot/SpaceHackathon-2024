using Android.Content;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace SpaceHackathon_2024.Platforms.Android
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context) { }

        protected override IShellTabLayoutAppearanceTracker CreateTabLayoutAppearanceTracker(ShellSection shellSection)
        {
            return new CustomTabLayoutAppearanceTracker(this);
        }
    }

    public class CustomTabLayoutAppearanceTracker : ShellTabLayoutAppearanceTracker
    {
        public CustomTabLayoutAppearanceTracker(IShellContext shellContext) : base(shellContext) { }

        public override void SetAppearance(TabLayout tabLayout, ShellAppearance appearance)
        {
            base.SetAppearance(tabLayout, appearance);

            var displayWidth = (int)DeviceDisplay.MainDisplayInfo.Width;

            for (int i = 0; i < tabLayout.TabCount; i++)
                tabLayout.GetTabAt(i).View.SetMinimumWidth(displayWidth / tabLayout.TabCount);
        }
    }
}
