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
        
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.SearchColleage(NameEntry.Text);
        }
    }
}