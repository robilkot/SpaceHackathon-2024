using System.Collections.ObjectModel;
using SpaceHackathon_2024.Models.Dtos;
using SpaceHackathon_2024.ViewModels;

namespace SpaceHackathon_2024.Views
{
    public partial class SearchColleagePage : ContentPage
    {
        private readonly SearchColleageViewModel _viewModel;
        
        public ObservableCollection<UserDto> SearchResults { get; set; }
        
        public SearchColleagePage(SearchColleageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            SearchResults = new ObservableCollection<UserDto>();
        }
        
        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            List<UserDto> results = await _viewModel.SearchColleage(NameEntry.Text);
            
            SearchResults.Clear();
            
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }
        }
    }
}