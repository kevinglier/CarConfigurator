import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID/*, DEFAULT_CURRENCY_CODE*/ } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CarConfiguratorComponent } from './pages/car-configurator/car-configurator.component';
import { CarModelsComponent } from './pages/car-models/car-models.component';
import { CarModelOptionsComponent } from './components/car-configurator/car-model-options.component';

registerLocaleData(localeDe, 'de');

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CarModelsComponent,
    CarConfiguratorComponent,
    FetchDataComponent,
    CarModelOptionsComponent
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'de'
    },
    //{ provide: DEFAULT_CURRENCY_CODE, useValue: 'EUR' }
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
export class AppModule {
}
