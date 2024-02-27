import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  title = 'AngularClientApp';

  private connection: HubConnection;
  public joined = false;
  public counter = 0;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7007/counterHub')
      .build();

    this.connection.on("ReceiveCounter", message => {
      this.counter = message;
    });
    //this.connection.on("NewMessage", message => this.newMessage(message));
    //this.connection.on("LeftUser", message => this.leftUser(message));
  }

  ngOnInit(): void {
    this.connection.start()
      .then(_ => {
        console.log('SignalR connection Started');
      }).catch(error => {
        return console.log(error);
      });
  }
}
