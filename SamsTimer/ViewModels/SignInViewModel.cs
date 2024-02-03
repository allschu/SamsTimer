using SamsTimer.Services;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
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

        public SignInViewModel(AuthService authService)
        {
            _authService = authService;

            SignInCommand = new Command(async () => await SignInUser());
        }

        private async Task SignInUser()
        {
            await _authService.SignIn(Username, Password);
        }
    }
}