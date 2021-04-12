import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CarModelsComponent } from "./pages/car-models/car-models.component";
import { CarConfiguratorComponent } from "./pages/car-configurator/car-configurator.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CarModelsComponent,
    CarConfiguratorComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      //{ path: '', component: HomeComponent, pathMatch: 'full' },
      { path: '', redirectTo: 'models', pathMatch: 'full' },
      { path: 'models', component: CarModelsComponent },
      { path: 'configure/:model', component: CarConfiguratorComponent },
      { path: 'fetch-data', component: FetchDataComponent }
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
