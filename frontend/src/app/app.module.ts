import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ClipboardModule } from 'ngx-clipboard';
import { OrderModule } from 'ngx-order-pipe';

import { AppRoutingModule } from './app-routing.module';

import * as apiClient from '../api/apiclient';
import { API_BASE_URL } from '../api/apiclient';

import { AppComponent } from './app.component';
import { LunchComponent } from './lunch/lunch.component';
import { HomeComponent } from './home/home.component';
import { NewLunchComponent } from './new-lunch/new-lunch.component';
import { AttendeeComponent } from './lunch/attendee/attendee.component';
import { LunchHeroComponent } from './lunch/lunch-hero/lunch-hero.component';
import { LunchDetailComponent } from './lunch/lunch-detail/lunch-detail.component';


@NgModule({
  declarations: [
    AppComponent,
    LunchComponent,
    HomeComponent,
    NewLunchComponent,
    AttendeeComponent,
    LunchHeroComponent,
    LunchDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    AppRoutingModule,
    ClipboardModule,
    OrderModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: 'https://localhost:62417/' },
    apiClient.LunchesClient,
    apiClient.AttendeesClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
