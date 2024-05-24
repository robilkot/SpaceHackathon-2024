using Microsoft.EntityFrameworkCore;
using SpaceHackathon_2024.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceHackathon_2024.Models
{
    [PrimaryKey("Id")]
    public class ScheduleDay
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public DayTypes Type { get; set; }

        public DateTime ShiftBegin { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string Description {  get; set; } = string.Empty;
    }
}
