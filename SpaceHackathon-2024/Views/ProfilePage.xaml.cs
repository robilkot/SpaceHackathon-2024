using SpaceHackathon_2024.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHackathon_2024.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel viewModel;
    public ProfilePage(ProfileViewModel profileViewModel)
    {
        InitializeComponent();

        BindingContext = viewModel = profileViewModel;
    }
}