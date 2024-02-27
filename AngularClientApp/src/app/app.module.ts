import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter.component';

@NgModule({
  declarations: [
    //AppComponent
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  //bootstrap: [AppComponent]
  bootstrap: [AppComponent]
})
export class AppModule { }
