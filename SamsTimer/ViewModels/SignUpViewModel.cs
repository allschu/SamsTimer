using SamsTimer.Services;
using SamsTimer.Shared.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

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

        public SignUpViewModel(IAuthService authService)
        {
            NewMember = new();
            NewSubscription = new();

            _authService = authService;

            NextSignUpPageCommand = new Command(async () => await NextSignUpPage());
            SignUpCommand = new Command(async () => await SignUpUser());
        }

        private async Task NextSignUpPage()
        {
            //Create place for additional questions?
            await Shell.Current.GoToAsync("register2");
        }

        private async Task SignUpUser()
        {
            try
            {
                await _authService.SignUp(NewMember.Email, NewMember.Password);

                //return user to homescreen
                await Shell.Current.GoToAsync("");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Foutmelding", "Er is iets fout gegaan tijdens het registreren. Probeer het later nog eens", "Ok");
            }
        }
    }
}