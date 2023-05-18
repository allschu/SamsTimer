using System.Diagnostics;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand ResetCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private bool _stopIsVisible;

        public bool StopIsVisible
        {
            get => _stopIsVisible;
            set => SetProperty(ref _stopIsVisible, value);
        }

        private bool _playIsVisible;

        public bool PlayIsVisible
        {
            get => _playIsVisible;
            set => SetProperty(ref _playIsVisible, value);
        }

        private string _stopwatchtext;

        public string StopWatchText
        {
            get => _stopwatchtext;
            set => SetProperty(ref _stopwatchtext, value);
        }

        private readonly Stopwatch _stopwatch;
        private readonly IDispatcherTimer _timer;

        public MainPageViewModel()
        {
            StopWatchText = "0:00:00:00";

            _playIsVisible = true;
            _stopIsVisible = false;

            _stopwatch = new Stopwatch();

            _timer = Dispatcher.GetForCurrentThread().CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16);
            _timer.Tick += _timer_Tick;

            StartCommand = new Command(() => Start());
            StopCommand = new Command(() => Stop());
            ResetCommand = new Command(() => Reset());
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //Update view here
                StopWatchText = _stopwatch.Elapsed.ToString(@"h\:mm\:ss\.ff");
            });
        }

        private void Reset()
        {
            _timer.Start();
            _stopwatch.Reset();

            PlayIsVisible = true;
            StopIsVisible = false;
        }

        private void Stop()
        {
            _timer.Start();
            _stopwatch.Stop();

            PlayIsVisible = true;
            StopIsVisible = false;
        }

        private void Start()
        {
            PlayIsVisible = false;
            StopIsVisible = true;

            _timer.Start();
            _stopwatch.Start();
        }
    }
}