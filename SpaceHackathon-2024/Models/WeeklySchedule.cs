using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceHackathon_2024.Models
{
    [PrimaryKey("Id")]
    public class WeeklySchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateOnly StartDay { get; set; }
        public DateOnly EndDay { get; set; }
        public List<ScheduleDay> Days { get; set; }
    }
}
