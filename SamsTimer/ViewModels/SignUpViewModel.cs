using SamsTimer.Shared.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        public Member NewMember { get; set; }
        public Subscription NewSubscription { get; set; }

        public ICommand NextSignUpPageCommand { get; }

        public SignUpViewModel()
        {
            NewMember = new();
            NewSubscription = new Subscription();

            NextSignUpPageCommand = new Command(async () => await NextSignUpPage());
        }

        private async Task NextSignUpPage()
        {
            await Shell.Current.GoToAsync("register2");
        }
    }
}