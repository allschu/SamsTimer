using Admin.Shared;

namespace SamsTimer.Services
{
    public class AuthService : IAuthService
    {
        
        public AuthService()
        {
            
        }

        public async Task SignUp(string email, string password)
        {
            //Sign up the user

            Preferences.Set(AppConstants.PreferencesUsername, email);
            Preferences.Set(AppConstants.PreferencesPassword, password);
        }

        public async Task SignIn(string username, string password)
        {
            //sign in the user
        }
    }
}