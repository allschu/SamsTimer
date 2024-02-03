using SamsTimer.ViewModels;

namespace SamsTimer;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        BindingContext = mainPageViewModel;
    }
}