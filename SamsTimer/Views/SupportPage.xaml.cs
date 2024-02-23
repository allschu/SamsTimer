using SamsTimer.ViewModels;

namespace SamsTimer.Views;

public partial class SupportPage : ContentPage
{
    public SupportPage(SupportViewModel supportViewModel)
    {
        InitializeComponent();

        BindingContext = supportViewModel;
    }
}