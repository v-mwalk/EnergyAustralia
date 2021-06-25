import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FestivalDataService } from './core/data-services/festival-data.service';
import { FestivalsComponent } from './festivals/festivals.component';
import { FestivalsService } from './festivals/festivals.service';

@NgModule({
  declarations: [
    AppComponent,
    FestivalsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    FestivalDataService,
    FestivalsService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
