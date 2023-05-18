namespace SamsTimer.Models
{
    public class Exercise
    {
        public int Id { get; }
        public int Order { get; }
        public int Reps { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }
        public int Break { get; }

        public Exercise(int order, int reps, int hours, int minutes, int seconds, int @break)
        {
            Order = order;
            Reps = reps;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Break = @break;
        }
    }
}