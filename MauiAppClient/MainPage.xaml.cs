using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Text.Json;
using MauiAppClient.Services;

namespace MauiAppClient;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Stock> Stocks { get { return stocks; } }
    private readonly ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();

    private readonly Meter _meter = new Meter("counterHubMeter");
    private readonly Counter<int> _hubTelemetryCounter;
    private readonly Counter<int> _hubTelemetryMicrosoftCounter;
    private readonly Counter<int> _hubTelemetryGoogleCounter;
    private readonly Counter<int> _hubTelemetryAppleCounter;

    public MainPage()
    {
        InitializeComponent();
        _hubTelemetryCounter = _meter.CreateCounter<int>("maui-hub-counter-1", "hub", "A count of stocks");
        _hubTelemetryMicrosoftCounter = _meter.CreateCounter<int>("maui-hub-counter-microsoft-1", "hub", "A count of microsoft stocks");
        _hubTelemetryGoogleCounter = _meter.CreateCounter<int>("maui-hub-counter-google-1", "hub", "A count of google stocks");
        _hubTelemetryAppleCounter = _meter.CreateCounter<int>("maui-hub-counter-apple-1", "hub", "A count of apple stocks");
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        LoadInitialStocks();
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
                CounterMessage.Text = $"Counted {counter} times";
                SemanticScreenReader.Announce(CounterMessage.Text);

                // Here goes the timeseries telemetry
                _hubTelemetryCounter.Add(1);
                //_hubTelemetryCounter.Add(counter);
            });
        });

        await StockHubService.StartAsync();

        StockHubService.EnableReceivingMessages<string>("PriceUpdate", (stockModel) =>
        {
            var receivedStock = JsonSerializer.Deserialize<Stock>(stockModel);

            var foundStock = stocks.FirstOrDefault(s => s.Symbol == receivedStock?.Symbol);

            if (foundStock != null)
            {
                foundStock.Price = receivedStock.Price;

                switch (receivedStock.Symbol)
                {
                    case "MSFT":
                        _hubTelemetryMicrosoftCounter.Add(1);
                        break;
                    case "GOOG":
                        _hubTelemetryGoogleCounter.Add(1);
                        break;
                    case "AAPL":
                        _hubTelemetryAppleCounter.Add(1);
                        break;
                }
            }
        });
    }


    private void LoadInitialStocks()
    {
        stocks.Add(new Stock { Symbol = "MSFT", Name = "Microsoft", Icon = "icon.png", Price = 0 });
        stocks.Add(new Stock { Symbol = "GOOG", Name = "Google", Icon = "icon.png", Price = 0 });
        stocks.Add(new Stock { Symbol = "AAPL", Name = "Apple", Icon = "icon.png", Price = 0 });

        stocksCollectionView.ItemsSource = stocks;
    }
}
