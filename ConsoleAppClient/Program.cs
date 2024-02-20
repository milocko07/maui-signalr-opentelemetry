// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, World!");


var _connection = new HubConnectionBuilder()
          .WithUrl("https://localhost:7007/counterHub")
          .Build();

await _connection.StartAsync();

//_connection.On<string>("ReceiveMessage", (message) =>
//{
//    Console.WriteLine("Actual counter is: " + message);
//    Console.WriteLine("");
//    counter = int.Parse(message);
//    //CounterBtn.Text = "fsdfd";
//    // Update UI with the received message
//});

int counter = 1;

while (true)
{
    await _connection.SendAsync("SendCounter", counter);
    Console.ReadKey();
    counter++;
}
