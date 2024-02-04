using CommunityToolkit.Maui.Alerts;
using Microsoft.Extensions.Logging;
using SamsTimer.Services;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly ILogger<SignInViewModel> _logger;
        private readonly IAuthService _authService;

        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }

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

        public SignInViewModel(IAuthService authService, ILogger<SignInViewModel> logger)
        {
            _logger = logger;
            _authService = authService;

            SignInCommand = new Command(async () => await SignInUser());
            SignUpCommand = new Command(async () => await SignUpUser());
        }

        private async Task SignUpUser()
        {
            await Shell.Current.GoToAsync("register");
        }

        private async Task SignInUser()
        {
            _logger.LogInformation("try to sign in user");

            try
            {
                IsBusy = true;

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    _logger.LogWarning("No username or password are entered");

                    IsBusy = false;
                    //If there are no username and or password, then don't bother the server
                    await Toast.Make("Voer een gebruikersnaam en wachtwoord in", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    return;
                }

                _logger.LogInformation("Try to sign in user with authservice");
                //Sign in
                await _authService.SignIn(Username, Password);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                IsBusy = false;

                await Toast.Make("Verkeerde gebruikersnaam of wachtwoord", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
        }
    }
}