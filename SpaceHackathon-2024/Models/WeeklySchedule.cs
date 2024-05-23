namespace SpaceHackathon_2024.Models
{
    public class WeeklySchedule
    {
        public DateOnly StartDay { get; set; }
        public DateOnly EndDay { get; set; }
        public List<ScheduleDay> Days { get; set; }
    }
}
