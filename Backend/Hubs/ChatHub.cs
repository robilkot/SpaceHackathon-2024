using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Hubs;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;

    public ChatHub(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task SendPrivateMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
        Console.WriteLine($"{user}: {message}");
    }
        
    public async Task JoinGroup(string username, string groupName)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Name == groupName);
        if (group == null)
        {
            group = new ChatGroup { Name = groupName };
            _context.ChatGroups.Add(group);
            await _context.SaveChangesAsync();
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("Notify", $"{username} вошел в чат в группу {groupName}");
    }

    public async Task SendGroupMessage(string groupName, string senderUsername, string message)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Name == groupName);
        if (group != null)
        {
            var chatMessage = new ChatMessage
            {
                SenderUsername = senderUsername,
                Content = message,
                Timestamp = DateTime.UtcNow,
                IsPrivate = false,
                ChatGroup = group
            };
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", senderUsername, message);
        }
    }
    
    public async Task<IEnumerable<ChatMessage>> GetGroupMessageHistory(string groupName)
    {
        var group = await _context.ChatGroups.FirstOrDefaultAsync(g => g.Name == groupName);
        if (group != null)
        {
            return group.Messages.OrderBy(m => m.Timestamp);
        }
        return Enumerable.Empty<ChatMessage>();
    }
}