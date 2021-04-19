import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID /*, DEFAULT_CURRENCY_CODE*/ } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import localeDe from '@angular/common/locales/de';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu.component';
import { CarConfiguratorComponent } from './pages/car-configurator/car-configurator.component';
import { CarModelsComponent } from './pages/car-models/car-models.component';
import { CarModelOptionsComponent } from './components/car-configurator/car-model-options.component';
import { OrderReceiptComponent } from './pages/order-receipt/order-receipt.component';

registerLocaleData(localeDe, 'de');

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CarModelsComponent,
        CarConfiguratorComponent,
        CarModelOptionsComponent,
        OrderReceiptComponent
    ],
    providers: [
        {
            provide: LOCALE_ID,
            useValue: 'de'
        }
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'models', pathMatch: 'full' },
            { path: 'models', component: CarModelsComponent },
            { path: 'configure/:model', component: CarConfiguratorComponent },
            { path: 'configure/:model/:code', component: CarConfiguratorComponent },
            { path: 'order-receipt', component: OrderReceiptComponent }
        ])
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
