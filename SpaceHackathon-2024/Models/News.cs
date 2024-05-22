using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceHackathon_2024.Models
{
    [PrimaryKey("Id")]
    public class News(string title, string publishDate, string description, string imageURL)
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; private set; } = title;

        public string PublishDate { get; private set; } = publishDate;

        public string Description { get; private set; } = description;

        public string ImageURL { get; private set; } = imageURL;
    }
}
