using Microsoft.Extensions.Logging;
using SamsTimer.Services;
using SamsTimer.Shared.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<SignUpViewModel> _logger;

        private Member _newMember;

        public Member NewMember
        {
            get => _newMember;
            set
            {
                _newMember = value;
                OnPropertyChanged();
            }
        }

        public Subscription NewSubscription { get; set; }

        public ICommand NextSignUpPageCommand { get; }
        public ICommand SignUpCommand { get; }

        public SignUpViewModel(IAuthService authService, ILogger<SignUpViewModel> logger)
        {
            NewMember = new();
            NewSubscription = new();

            _authService = authService;
            _logger = logger;

            NextSignUpPageCommand = new Command(async () => await NextSignUpPage());
            SignUpCommand = new Command(async () => await SignUpUser());

            _logger.LogInformation("SignUpViewModel created");
        }

        /// <summary>
        /// Optional additional questions
        /// </summary>
        /// <returns></returns>
        private async Task NextSignUpPage()
        {
            //Create place for additional questions?
            await Shell.Current.GoToAsync("register2");
        }

        /// <summary>
        /// Sign up the user
        /// </summary>
        /// <returns></returns>
        private async Task SignUpUser()
        {
            _logger.LogInformation("Try to register user with authservice");

            try
            {
                await _authService.SignUp(NewMember.Email, NewMember.Password);

                //to user profile
                await Shell.Current.GoToAsync("");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");

                Application.Current.MainPage.DisplayAlert("Foutmelding", "Er is iets fout gegaan tijdens het registreren. Probeer het later nog eens", "Ok");
            }
        }
    }
}