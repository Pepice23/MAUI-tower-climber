using MAUI_tower_climber.ViewModel;

namespace MAUI_tower_climber;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

