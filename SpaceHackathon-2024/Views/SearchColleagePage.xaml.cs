using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views
{
    public partial class SearchColleagePage : ContentPage
    {
        private readonly SearchColleageViewModel _viewModel;
        
        public SearchColleagePage(SearchColleageViewModel viewModel)
        {
            BindingContext = _viewModel = viewModel;
            InitializeComponent();
        }

        private async void SearchButton_Clicked_Async(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            
            if (!string.IsNullOrEmpty(name))
            {
                await _viewModel.SearchColleageAsync(name);
            }
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ProfilePage)}", animate:true);
        }
        
        private async void MessageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ChatPage)}", animate:true);
        }
    }
}