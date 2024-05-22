using CommunityToolkit.Mvvm.ComponentModel;
using SpaceHackathon_2024.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class StoreViewModel : ObservableObject
    {
        private ApplicationContext _appContext;
        public StoreViewModel(ApplicationContext appContext)
        {
            _appContext = appContext;

            //LoadMoreCommand = new Command(async () => await LoadMoreNewsAsync());
        }
    }
}
