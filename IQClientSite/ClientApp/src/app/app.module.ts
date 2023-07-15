import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RawComponent } from './raw/raw.component';
import { DetailComponent } from './detail/detail.component';
import { ConsumptionHistoryComponent } from './consumption-history/consumption-history.component';
import { DbDetailComponent } from './db-detail/db-detail.component';
import { MetersDetailComponent } from './meters-detail/meters-detail.component';
import { InvertersDetailComponent } from './inverters-detail/inverters-detail.component';
import { ConsumptionDetailComponent } from './consumption-detail/consumption-detail.component';
import { StatusDetailComponent } from './status-detail/status-detail.component';
import { MeterReadingsDetailComponent } from './meter-readings-detail/meter-readings-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RawComponent,
    DetailComponent,
    ConsumptionHistoryComponent,
    DbDetailComponent,
    MetersDetailComponent,
    InvertersDetailComponent,
    ConsumptionDetailComponent,
    StatusDetailComponent,
    MeterReadingsDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      // any server api routes get specified in proxy.config
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'raw', component: RawComponent },
      { path: 'detail', component: DetailComponent },
      { path: 'detail/:action', component: DetailComponent },
      { path: 'consumptions', component: ConsumptionHistoryComponent },
      { path: 'dbdetail/:id', component: DbDetailComponent },
      { path: 'dbdetail', component: DbDetailComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
