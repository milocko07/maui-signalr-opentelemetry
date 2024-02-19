using System.Diagnostics.Metrics;
using MauiAppClient.Services;
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

        CounterHubService.SetConnection(
            new HubConnectionBuilder()
            .WithUrl("https://localhost:7007/counterHub")
            .Build());

        //using var meter = new Meter("counterHubMeter");

        var meterProvider = Sdk
            .CreateMeterProviderBuilder()
            .AddMeter("counterHubMeter")
            .AddPrometheusHttpListener(options => options.UriPrefixes = new string[] { "http://localhost:9465/" })
            .Build();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton(meterProvider);

        return builder.Build();
    }
}
