using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using MetroLog.MicrosoftExtensions;
using MetroLog.Operators;
using Microsoft.Extensions.Logging;
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
            .AddLogging()
            .AddFirebaseAuth()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("EmblemaOne-Regular.ttf", "Emblema");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<IAuthService, AuthService>();
        mauiAppBuilder.Services.AddTransient<ILogService, LogService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<TimerViewModel>();
        mauiAppBuilder.Services.AddSingleton<TimerSettingsViewModel>();
        mauiAppBuilder.Services.AddSingleton<SignInViewModel>();
        mauiAppBuilder.Services.AddSingleton<SignUpViewModel>();

        mauiAppBuilder.Services.AddSingleton<SupportViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<TimerPage>();
        mauiAppBuilder.Services.AddSingleton<SignUpPage>();
        mauiAppBuilder.Services.AddSingleton<SignInPage>();
        mauiAppBuilder.Services.AddSingleton<TimerSettingsPage>();

        mauiAppBuilder.Services.AddSingleton<SupportPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder AddLogging(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Logging
          .AddTraceLogger(
              options =>
              {
                  options.MinLevel = LogLevel.Trace;
                  options.MaxLevel = LogLevel.Critical;
              }) // Will write to the Debug Output
          .AddConsoleLogger(
              options =>
              {
                  options.MinLevel = LogLevel.Information;
                  options.MaxLevel = LogLevel.Critical;
              }) // Will write to the Console Output (logcat for android)
          .AddStreamingFileLogger(
              options =>
              {
                  options.RetainDays = 2;
                  options.FolderPath = Path.Combine(
                      FileSystem.CacheDirectory,
                      "Logs");
              }); // Will write to files

        mauiAppBuilder.Services.AddSingleton(LogOperatorRetriever.Instance);

        return mauiAppBuilder;
    }

    public static MauiAppBuilder AddFirebaseAuth(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton(services => new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "",
            AuthDomain = "lidmaatschapbeheer.firebaseapp.com",
            Providers =
                [
                    new EmailProvider()
                ]
                //UserRepository = services.GetRequiredService<IUserRepository>()
        }));

        return mauiAppBuilder;
    }
}