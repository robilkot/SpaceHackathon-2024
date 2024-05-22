using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

public class TicketHub : Hub
{
    public async Task Send(string username, string message)
    {
        await this.Clients.All.SendAsync("Receive", username, message);
    }
}