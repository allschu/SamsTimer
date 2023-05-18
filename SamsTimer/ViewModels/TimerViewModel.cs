using SamsTimer.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class TimerViewModel : ViewModelBase, IQueryAttributable
    {
        private IDispatcherTimer _timer;
        private string _counterDownTimer;

        private double _step;

        public string CounterDownTimer
        {
            get => _counterDownTimer;
            set => SetProperty(ref _counterDownTimer, value);
        }

        private string _exerciseNr;

        public string ExerciseNr
        {
            get => _exerciseNr;
            set => SetProperty(ref _exerciseNr, value);
        }

        private int _totalReps;

        public int TotalReps
        {
            get => _totalReps;
            set => SetProperty(ref _totalReps, value);
        }

        private int _currentRep;

        public int CurrentRep
        {
            get => _currentRep;
            set => SetProperty(ref _currentRep, value);
        }

        private double _workProgress;

        public double WorkProgress
        {
            get => _workProgress;
            set => SetProperty(ref _workProgress, value);
        }

        private Exercise _exercise;

        public Exercise Exercise
        {
            get => _exercise;
            set
            {
                _exercise = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan ExerciseTimespan { get; set; }

        public ICommand ToSettingsCommand { get; }
        public ICommand StartCountDownCommand { get; }

        public TimerViewModel()
        {
            ToSettingsCommand = new Command(async () => await NavigateToSettings());
            StartCountDownCommand = new Command(async () => await StartCountDown());
        }

        private async Task NavigateToSettings()
        {
            await Shell.Current.GoToAsync("timersettings");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Exercise = query["exercise"] as Exercise;

            if (Exercise?.Reps == 0)
            {
                CounterDownTimer = string.Format("{0:D2}:{1:D2}:{2:D2}", Exercise.Hours, Exercise.Minutes, Exercise.Seconds);
            }
            else if (Exercise != null)
            {
                TotalReps = Exercise.Reps;
                CurrentRep = 1;
            }
        }

        private async Task StartCountDown()
        {
            WorkProgress = 0;
            ExerciseTimespan = new TimeSpan(Exercise.Hours, Exercise.Minutes, Exercise.Seconds);

            var secsHours = 3600 * Exercise.Hours;
            var secsminuten = 60 * Exercise.Minutes;
            _step = Math.Round(100 / (double)(secsHours + secsminuten + Exercise.Seconds), 2);

            _timer = Dispatcher.GetForCurrentThread().CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WorkProgress += _step;
                ExerciseTimespan = ExerciseTimespan.Subtract(new TimeSpan(0, 0, 1));
                if (ExerciseTimespan.Hours > 0)
                {
                    CounterDownTimer = string.Format("{0:D2}:{1:D2}:{2:D2}", ExerciseTimespan.Hours, ExerciseTimespan.Minutes, ExerciseTimespan.Seconds);
                }
                else if (ExerciseTimespan.Minutes > 0 && ExerciseTimespan.Hours == 0)
                {
                    CounterDownTimer = string.Format("{0:D2}:{1:D2}", ExerciseTimespan.Minutes, ExerciseTimespan.Seconds);
                }
                else if (ExerciseTimespan.Seconds >= 0 && ExerciseTimespan.Minutes == 0)
                {
                    CounterDownTimer = string.Format("{0:D2}", ExerciseTimespan.Seconds);
                }
            });

            if (ExerciseTimespan == new TimeSpan(0, 0, 0) && CurrentRep == TotalReps)
            {
                //workout is done
                _timer.Stop();
                CounterDownTimer = "Goed gedaan!";
            }
            else
            {
                //break mode
            }
        }
    }
}