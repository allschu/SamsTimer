namespace SamsTimer.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int BreakTime { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}