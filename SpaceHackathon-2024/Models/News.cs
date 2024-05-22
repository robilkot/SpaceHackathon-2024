namespace SpaceHackathon_2024.Models
{
    public class News(string title, string description, string imageURL)
    {
        public string Title { get; private set; } = title;

        public string Description { get; private set; } = description;

        public string ImageURL { get; private set; } = imageURL;
    }
}
