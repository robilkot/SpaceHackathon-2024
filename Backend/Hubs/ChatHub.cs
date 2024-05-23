using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

public class ChatHub : Hub
{
    public async Task SendPrivateMessage(string message)
    {
        await this.Clients.All.SendAsync("ReceiveMessage", message);
        Console.WriteLine(message);
    }
    
    public async Task JoinGroup(string username, string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.All.SendAsync("Notify", $"{username} вошел в чат в группу {groupName}");
    }

    public async Task SendGroupMessage(string groupName, string user, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        Console.WriteLine(message);
    }
}