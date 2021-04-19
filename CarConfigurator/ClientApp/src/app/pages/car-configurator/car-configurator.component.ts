import { Component, OnDestroy, OnInit, AfterViewInit, Inject } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { ConfiguratorService } from '../../services/configurator.service';
import { CarConfiguration } from '../../models/CarConfiguration';
import { UrlHelper } from '../../helpers/UrlHelper';
import { CarOrderDetails } from '../../models/CarOrderDetails';
import { CarModelOrderService } from '../../services/car-model-order.service';

@Component({
    selector: 'app-car-configurator-component',
    templateUrl: './car-configurator.component.html'
})
export class CarConfiguratorComponent implements OnDestroy, OnInit, AfterViewInit {

    priceSummary = null;
    price = 0;

    configuration: CarConfiguration;

    constructor(
        @Inject('BASE_URL') private _baseUrl,
        private _configuratorService: ConfiguratorService,
        private _carModelOrderService: CarModelOrderService,
        private _activatedRoute: ActivatedRoute,
        private _router: Router) {
    }

    ngOnInit() {

        // Abrufen aller benötigten Daten für den Konfigurator
        this._activatedRoute.paramMap.subscribe((params: ParamMap) => {
            let name = params.get('model');
            name = UrlHelper.urlPartToName(name);

            const code = params.get('code');

            // Abruf genereller Konfigurator-Struktur
            this._configuratorService.getConfigurationForModel(name, code).subscribe(
                result => {
                    // Bei Fehler zurück zur Startseite
                    if (!result)
                        this._router.navigate(['/']);

                    // Wenn ein Code angegeben wurde, redirecten wir auf URL ohne Code-Angabe
                    this._router.navigate(['/configure/', UrlHelper.nameToUrlPart(name)]);

                    this.configuration = result;

                    this.updateSummary();
                },
                error => {
                    // Bei Fehlern zurück zur Startseite
                    console.error(error);
                    this._router.navigate(['/']);
                }
            );

            this.price = 20000;
        });
    }

    ngAfterViewInit(): void {}

    ngOnDestroy() {}

    updateSummary() {
        if (!this.configuration || !this.configuration.model.ean || !this.configuration.selectedOptionProducts)
            return;

        this._configuratorService
            .getPriceSummaryForSelectedCarOptionProducts(this.configuration.model.ean,
                this.configuration.selectedOptionProducts)
            .subscribe(result => {
                this.priceSummary = result;
            });
    }

    configurationChanged(selectedOptionProducts) {
        this.configuration.selectedOptionProducts = selectedOptionProducts;

        this.updateSummary();
    }

    orderCar() {

        let orderDetails = new CarOrderDetails(this.configuration.model.ean, this.configuration.selectedOptionProducts, this.priceSummary.code);

        this._carModelOrderService.sendOrder(orderDetails).subscribe(result => {
            this._router.navigate(['/order-receipt']);
        });
    }

    shareLink() {
        navigator.clipboard.writeText(this._baseUrl + 'configure/' + UrlHelper.nameToUrlPart(this.configuration.model.name) + '/' + this.priceSummary.code);
    }
}
