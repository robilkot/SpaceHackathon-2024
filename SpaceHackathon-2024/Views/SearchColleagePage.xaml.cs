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

        private void ProfileButton_Clicked(object sender, EventArgs e)
        {
        }
        
        private void MessageButton_Clicked(object sender, EventArgs e)
        {
        }
    }
}