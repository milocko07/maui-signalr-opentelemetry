﻿using MauiAppClient.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;

namespace MauiAppClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();

        // Include signalR connections.
        CounterHubService.SetConnection(
            new HubConnectionBuilder()
            .WithUrl("https://localhost:7007/counterHub")
            .Build());

        StockHubService.SetConnection(
           new HubConnectionBuilder()
           .WithUrl("https://localhost:7007/stockHub")
           .Build());

        // Include OpenTelemetry meter provider.
        var meterProvider = Sdk
            .CreateMeterProviderBuilder()
            .AddMeter("counterHubMeter")
            .AddPrometheusHttpListener(options => options.UriPrefixes = new string[] { "http://localhost:9464/" })
            .Build();

        builder.Services.AddSingleton(meterProvider); 

        // Include more OpenTelemetry provider if necessary.

        return builder.Build();
    }
}
