using SamsTimer.ViewModels;

namespace SamsTimer.Views;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(SignUpViewModel signUpViewModel)
    {
        InitializeComponent();

        BindingContext = signUpViewModel;
    }
}