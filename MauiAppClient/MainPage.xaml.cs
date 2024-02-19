using MauiAppClient.Services;

namespace MauiAppClient;

public partial class MainPage : ContentPage
{
    int count = 0;

    //HubConnection _connection;

    public MainPage()
    {
        InitializeComponent();

        //_connection = HubConnectionService.EnableStockConnection();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        //await _connection.StartAsync();
        await HubConnectionService.StartStockConnectionAsync();

        HubConnectionService.EnableStockMessages<string>("ReceiveMessage", (message) =>
        {
            // Update UI on the main thread
            Dispatcher.Dispatch(() =>
            {
                CounterBtn.Text = $"Clicked {message} time";

            });
        });
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        await HubConnectionService.SendStockAsync<string>("SendMessage", count.ToString());

        //if (count == 1)
        //    CounterBtn.Text = $"Clicked {count} time";
        //else
        //    CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
