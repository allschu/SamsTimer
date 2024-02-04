using SamsTimer.Services;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        public ICommand SignInCommand { get; }

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public SignInViewModel(IAuthService authService)
        {
            _authService = authService;

            SignInCommand = new Command(async () => await SignInUser());
        }

        private async Task SignInUser()
        {
            try
            {
                IsBusy = true;

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    IsBusy = false;
                    return;
                }

                await _authService.SignIn(Username, Password);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                //Todo: Add some exception logging, handling

                IsBusy = false;

                Application.Current.MainPage.DisplayAlert("Fout", "Er is iets fout gegaan. Probeer het later nog eens.", "Ok");
            }
        }
    }
}