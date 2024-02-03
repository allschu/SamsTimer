using SamsTimer.Services;
using SamsTimer.Shared.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

        public Member NewMember { get; set; }
        public Subscription NewSubscription { get; set; }

        public ICommand NextSignUpPageCommand { get; }
        public ICommand SignUpCommand { get; }

        public SignUpViewModel(IAuthService authService)
        {
            NewMember = new();
            NewSubscription = new Subscription();

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
            await _authService.SignUp(NewMember.Email, NewMember.Password);

            //return user to homescreen
            await Shell.Current.GoToAsync("");
        }
    }
}