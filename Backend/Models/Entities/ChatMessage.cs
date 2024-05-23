namespace Backend.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public string SenderUsername { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsPrivate { get; set; }
    public int ChatGroupId { get; set; }
    public ChatGroup ChatGroup { get; set; }
}