//import { Injectable } from '@angular/core';
//import { HttpClient } from '@angular/common/http';
//import { HubConnection, HubConnectionState } from '@microsoft/signalr';

//@Injectable({
//  providedIn: 'root'
//})
//export class SignalrService {
//  private hubConnection: HubConnection;

//  constructor(private http: HttpClient) { }

//  public startConnection(): Promise<void> {
//    this.hubConnection = new HubConnectionBuilder()
//      .withUrl('http://localhost:5000/myHub')
//      .build();

//    return this.hubConnection.start();
//  }

//  public async sendMessage(message: string): Promise<void> {
//    if (this.hubConnection.connectionState === HubConnectionState.Connected) {
//      await this.hubConnection.invoke('SendMessage', message);
//    }
//  }

//  public onReceiveMessage(callback: (message: string) => void): Subscription {
//    return this.hubConnection.on('ReceiveMessage', callback);
//  }
//}
