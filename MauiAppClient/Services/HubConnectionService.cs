using Microsoft.AspNetCore.SignalR.Client;

namespace MauiAppClient.Services;

internal class HubConnectionService
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static HubConnection _stockConnection;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    internal static void SetStockConnection(HubConnection connection)
    {
        _stockConnection = connection;
    }

    internal static async Task StartStockConnectionAsync()
    {
        // should apply Polly retries.
        await _stockConnection.StartAsync();
    }

    internal static void EnableStockMessages<T>(string methodName, Action<T> handler)
    {
        _stockConnection.On<T>(methodName, handler);
    }

    internal static async Task SendStockAsync<T>(string methodName, T message)
    {
        await _stockConnection.SendAsync(methodName, message);
    }
}
