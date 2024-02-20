using System.Diagnostics.Metrics;
using MauiAppClient.Services;

namespace MauiAppClient;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly Meter _meter = new Meter("counterHubMeter");
    private readonly Counter<int> _hubTelemetryCounter;

    public MainPage()
    {
        InitializeComponent();
        _hubTelemetryCounter = _meter.CreateCounter<int>("maui-hub-counter-3", "hub", "A count of things");
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await CounterHubService.StartAsync();

        CounterHubService.EnableReceivingMessages<int>("ReceiveCounter", (counter) =>
        {
            // Render new message on the UI on the main thread
            Dispatcher.Dispatch(() =>
            {
                CounterBtn.Text = $"Clicked {counter} times";
                SemanticScreenReader.Announce(CounterBtn.Text);

                // Here goes the timeseries telemetry
                _hubTelemetryCounter.Add(1);
                //_hubTelemetryCounter.Add(counter);
            });
        });
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        //await CounterHubService.SendMessageAsync<int>("SendCounter", count);

        // Avoiding this to simulate sending and rendering in real time.
        //if (count == 1)
        //    CounterBtn.Text = $"Clicked {count} time";
        //else
        //    CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
