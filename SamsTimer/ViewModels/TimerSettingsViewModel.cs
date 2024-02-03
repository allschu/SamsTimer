using System.Collections.ObjectModel;
using SamsTimer.Models;
using System.Windows.Input;

namespace SamsTimer.ViewModels
{
    public class TimerSettingsViewModel : ViewModelBase
    {
        private ObservableCollection<Exercise> _exerciseList;

        public ObservableCollection<Exercise> ExerciseList
        {
            get => _exerciseList;
            set => SetProperty(ref _exerciseList, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand MinutesSliderDragCompleted { get; }
        public ICommand SecondsSliderDragCompleted { get; }

        private int _hours;

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        private int _minutes;

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        private int _seconds;

        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        private int _reps;

        public int Reps
        {
            get => _reps;
            set => SetProperty(ref _reps, value);
        }

        private int _break;

        public int Break
        {
            get => _break;
            set => SetProperty(ref _break, value);
        }

        public TimerSettingsViewModel()
        {
            if (ExerciseList == null)
            {
                ExerciseList = new ObservableCollection<Exercise>();
            }

            SaveCommand = new Command(async () => await Save());

            MinutesSliderDragCompleted = new Command(OnMinutesSliderDragCompleted);
            SecondsSliderDragCompleted = new Command(OnSecondsDragCompleted);
        }

        private async Task Save()
        {
            var exercise = new Exercise(ExerciseList.Count + 1, Reps, Hours, Minutes, Seconds, Break);

            ExerciseList.Add(exercise);

            var navigationParameter = new Dictionary<string, object>
                                                        {
                                                            { "exercise", exercise }
                                                        };

            //await Shell.Current.GoToAsync("timer", navigationParameter);
        }

        private void OnMinutesSliderDragCompleted(object obj)
        {
        }

        private void OnSecondsDragCompleted(object obj)
        {
        }
    }
}