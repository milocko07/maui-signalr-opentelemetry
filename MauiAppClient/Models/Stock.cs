using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiAppClient;

public class Stock : ObservableObject
{
    private double price;
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public double Price
    {
        get => price;
        set => SetProperty(ref price, value);
    }
}
