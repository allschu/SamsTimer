using SamsTimer.ViewModels;

namespace SamsTimer;

public partial class TimerPage : ContentPage
{
    public TimerPage(TimerViewModel timerViewModel)
    {
        InitializeComponent();

        BindingContext = timerViewModel;
    }
}