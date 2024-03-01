import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

interface Stock {
  Symbol: string;
  Name: string;
  Price: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  title = 'AngularClientApp';

  private counterConnection: HubConnection;
  private stockConnection: HubConnection;
  public counter = 0;

  public stocks: Stock[] = [
    { Symbol: 'MSFT', Name: 'Microsoft', Price: 0 },
    { Symbol: 'GOOG', Name: 'Google', Price: 0 },
    { Symbol: 'AAPL', Name: 'Apple', Price: 0 },
  ];

  constructor() {
    this.counterConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7007/counterHub')
      .build();

    this.counterConnection.on("ReceiveCounter", message => {
      this.counter = message;
    });

    this.stockConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7007/stockHub')
      .build();

    this.stockConnection.on("PriceUpdate", message => {
      let receivedStock: Stock = JSON.parse(message);

      if (receivedStock) {
        console.log(message);

        let foundStock = this.stocks.find(stock => stock.Symbol === receivedStock.Symbol);
        if (foundStock) {
          foundStock.Price = receivedStock.Price;
        }
      }
    });
  }

  ngOnInit(): void {
    this.counterConnection.start()
      .then(_ => {
        console.log('SignalR counter connection Started');
      }).catch(error => {
        return console.log(error);
      });

    this.stockConnection.start()
      .then(_ => {
        console.log('SignalR stock connection Started');
      }).catch(error => {
        return console.log(error);
      });
  }
}
