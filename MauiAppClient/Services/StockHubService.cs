using Microsoft.AspNetCore.SignalR.Client;

namespace MauiAppClient.Services;

internal class StockHubService
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static HubConnection _stockHubConnection;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    internal static void SetConnection(HubConnection connection)
    {
        _stockHubConnection = connection;
    }

    internal static async Task StartAsync()
    {
        // should apply Polly retries.
        await _stockHubConnection.StartAsync();
    }

    internal static void EnableReceivingMessages<T>(string methodName, Action<T> handler)
    {
        _stockHubConnection.On<T>(methodName, handler);
    }

    internal static async Task SendMessageAsync<T>(string methodName, T message)
    {
        await _stockHubConnection.SendAsync(methodName, message);
    }
}
