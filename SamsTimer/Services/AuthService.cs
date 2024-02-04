using Firebase.Auth;
using SamsTimer.Shared;

namespace SamsTimer.Services
{
    public class AuthService : IAuthService
    {
        public readonly FirebaseAuthClient _authClient;

        public AuthService(FirebaseAuthClient firebaseAuthClient)
        {
            _authClient = firebaseAuthClient;
        }

        public async Task SignUp(string email, string password)
        {
            var userCredentials = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);

            if (userCredentials == null)
            {
                //todo something has gone wrong
                return;
            }

            //Set the username and email in the preferences
            Preferences.Set(AppConstants.PreferencesUsername, email);
            Preferences.Set(AppConstants.PreferencesPassword, password);
        }

        public async Task SignIn(string username, string password)
        {
            var userCredentials = await _authClient.SignInWithEmailAndPasswordAsync(username, password);
        }
    }
}