using MauiAppClient.Services;

namespace MauiAppClient;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await CounterHubService.StartAsync();

        CounterHubService.EnableReceivingMessages<int>("ReceiveCounter", (message) =>
        {
            // Render new message on the UI on the main thread
            Dispatcher.Dispatch(() =>
            {
                CounterBtn.Text = $"Clicked {message} times";

                SemanticScreenReader.Announce(CounterBtn.Text);
            });
        });
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        await CounterHubService.SendMessageAsync<int>("SendCounter", count);

        // Avoiding this to simulate sending and rendering in real time.
        //if (count == 1)
        //    CounterBtn.Text = $"Clicked {count} time";
        //else
        //    CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
