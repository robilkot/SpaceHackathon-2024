namespace Backend.Models;

public class ChatGroup
{
    public int Id { get; set; }
    
    public List<User> Users { get; set; }
    public string Name { get; set; }
    public List<ChatMessage> Messages { get; set; }
}