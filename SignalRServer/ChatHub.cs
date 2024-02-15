namespace SignalRServer;

using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        // Validate message if needed
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
