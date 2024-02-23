﻿// See https://aka.ms/new-console-template for more information
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
while (true)
{
    double randomValue = random.NextDouble();
    double scaledValue = randomValue * (10 - (-1)) + (-1);
    await _stockHubConnection.SendAsync("UpdateStockPrice", "MSFT", scaledValue);
    Task.Delay(random.Next(0, 1001)).Wait();

    randomValue = random.NextDouble();
    scaledValue = randomValue * (10 - (-1)) + (-1);
    await _stockHubConnection.SendAsync("UpdateStockPrice", "GOOG", scaledValue);
    Task.Delay(random.Next(0, 1002)).Wait();
    
    randomValue = random.NextDouble();
    scaledValue = randomValue * (10 - (-1)) + (-1);
    await _stockHubConnection.SendAsync("UpdateStockPrice", "AAPL", scaledValue);
    Task.Delay(random.Next(0, 1000)).Wait();
    
    await _counterHubConnection.SendAsync("SendCounter", counter++);
    Console.WriteLine($"Updated stocks {counter} times.");
}
