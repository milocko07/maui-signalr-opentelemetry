using Microsoft.AspNetCore.SignalR;

namespace SignalRServer;

public class CounterHub : Hub
{
    public async Task SendCounter(int count)
    {
        // Validate message if needed
        await Clients.All.SendAsync("ReceiveCounter", count);
    }
}
