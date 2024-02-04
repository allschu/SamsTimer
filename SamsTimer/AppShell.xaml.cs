using SamsTimer.Views;

namespace SamsTimer;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routes.Add("timersettings", typeof(TimerSettingsPage));
        Routes.Add("timer", typeof(TimerPage));
        Routes.Add("signIn", typeof(SignInPage));
        Routes.Add("register", typeof(SignUpPage));
        Routes.Add("register2", typeof(SignUpPage2)); //Can be used for extra questions

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}