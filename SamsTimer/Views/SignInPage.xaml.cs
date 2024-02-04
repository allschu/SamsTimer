using SamsTimer.ViewModels;

namespace SamsTimer.Views;

public partial class SignInPage : ContentPage
{
    public SignInPage(SignInViewModel signInViewModel)
    {
        InitializeComponent();

        BindingContext = signInViewModel;
    }
}