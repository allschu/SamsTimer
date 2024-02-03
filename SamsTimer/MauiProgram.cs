using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using SamsTimer.Services;
using SamsTimer.ViewModels;
using SamsTimer.Views;
using Syncfusion.Maui.Core.Hosting;

namespace SamsTimer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("EmblemaOne-Regular.ttf", "Emblema");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(services => new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "",
            AuthDomain = "lidmaatschapbeheer.firebaseapp.com",
            Providers =
                [
                    new EmailProvider()
                ],
            UserRepository = services.GetRequiredService<IUserRepository>()
        }));

        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<IAuthService, AuthService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<TimerViewModel>();
        mauiAppBuilder.Services.AddSingleton<TimerSettingsViewModel>();
        mauiAppBuilder.Services.AddSingleton<SignInViewModel>();
        mauiAppBuilder.Services.AddSingleton<SignUpViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<TimerPage>();
        mauiAppBuilder.Services.AddSingleton<SignUpPage>();
        mauiAppBuilder.Services.AddSingleton<SignInPage>();
        mauiAppBuilder.Services.AddSingleton<TimerSettingsPage>();

        return mauiAppBuilder;
    }
}