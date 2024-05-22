using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceHackathon_2024.Models
{
    [PrimaryKey("Id")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double KPI { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surame { get; set; } = string.Empty;
        public string AvatarURL { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string BranchOffice { get; set; } = string.Empty;
    }
}
