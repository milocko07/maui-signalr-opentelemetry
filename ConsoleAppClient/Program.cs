// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Client to update stocks in real time!");

var _counterHubConnection = new HubConnectionBuilder()
          .WithUrl("https://localhost:7007/counterHub")
          .Build();

await _counterHubConnection.StartAsync();

var _stockHubConnection = new HubConnectionBuilder()
          .WithUrl("https://localhost:7007/stockHub")
          .Build();

await _stockHubConnection.StartAsync();

int counter = 1;
Random random = new Random();
var stockSymbols = new List<string> { "MSFT", "GOOG", "AAPL" };

while (true)
{
    // Generate random order.
    var shuffledSymbols = stockSymbols
                            .OrderBy(x => random.Next())
                            .ToList();

    foreach (var stockSymbol in shuffledSymbols)
    {
        double randomValue = random.NextDouble();
        double scaledValue = randomValue * (10 - (-1)) + (-1);

        await _stockHubConnection.SendAsync("UpdateStockPrice", stockSymbol, scaledValue);

        int randomDelay = random.Next(5000);
        await Task.Delay(randomDelay);
    }

    await _counterHubConnection.SendAsync("SendCounter", counter);
    Console.WriteLine($"Updated stocks {counter} times.");
    counter++;
}
