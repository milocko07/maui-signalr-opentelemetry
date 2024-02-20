// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Client to update in real time!");

var _counterHubConnection = new HubConnectionBuilder()
          .WithUrl("https://localhost:7007/counterHub")
          .Build();

await _counterHubConnection.StartAsync();


//while (true)
//{
//    await _counterHubConnection.SendAsync("SendCounter", counter);
//    Console.ReadKey();
//    counter++;
//}

var _stockHubConnection = new HubConnectionBuilder()
          .WithUrl("https://localhost:7007/stockHub")
          .Build();

await _stockHubConnection.StartAsync();

int counter = 1;
Random random = new Random();
while (true)
{
    await _stockHubConnection.SendAsync("UpdateStockPrice", "MSFT", random.Next(-1, 10));
    Task.Delay(random.Next(0, 1001)).Wait();
    await _stockHubConnection.SendAsync("UpdateStockPrice", "GOOG", random.Next(-1, 4));
    Task.Delay(random.Next(0, 1001)).Wait();
    await _stockHubConnection.SendAsync("UpdateStockPrice", "AAPL", random.Next(-1, 7));
    Task.Delay(random.Next(0, 1001)).Wait();
    await _counterHubConnection.SendAsync("SendCounter", counter++);
}
