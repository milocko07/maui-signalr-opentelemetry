using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer;

public class StockHub : Hub
{
    public async Task UpdateStockPrice(string symbol, double price)
    {
        // Format the message according to your needs (e.g., JSON)
        var message = JsonSerializer.Serialize(new { Symbol = symbol, Price = price });

        // Broadcast the update to all connected clients
        await Clients.All.SendAsync("PriceUpdate", message.ToString());
    }
}
