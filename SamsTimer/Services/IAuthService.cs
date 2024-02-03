namespace SamsTimer.Services
{
    public interface IAuthService
    {
        Task SignUp(string email, string password);

        Task SignIn(string username, string password);
    }
}