using SamsTimer.ViewModels;

namespace SamsTimer;

public partial class TimerSettingsPage : ContentPage
{
    public TimerSettingsPage(TimerSettingsViewModel timerSettingsViewModel)
    {
        InitializeComponent();

        BindingContext = timerSettingsViewModel;
    }
}