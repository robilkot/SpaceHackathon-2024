namespace SpaceHackathon_2024.Models;

public class Message
{
    public string Author { get; set; }
    
    public string Text { get; set; }
    
    public bool IsUserMessage { get; set; }
}