import { Component, OnDestroy, OnInit, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { ConfiguratorService } from '../../services/configurator.service';
import { CarConfiguration } from '../../models/CarConfiguration';

@Component({
    selector: 'app-car-configurator-component',
    templateUrl: './car-configurator.component.html'
})
export class CarConfiguratorComponent implements OnDestroy, OnInit, AfterViewInit {

    public priceSummary = null;
    public price = 0;

    public configuration: CarConfiguration;

    constructor(
        private _configuratorService: ConfiguratorService,
        private _activatedRoute: ActivatedRoute,
        private _router: Router) {
    }

    ngOnInit() {

        // Abrufen aller benötigten Daten für den Konfigurator
        this._activatedRoute.paramMap.subscribe((params: ParamMap) => {
            const ean = params.get('model');

            // Abruf genereller Konfigurator-Struktur
            this._configuratorService.getConfigurationForModel(ean).subscribe(
                result => {
                    // Bei Fehler zurück zur Startseite
                    if (!result)
                        this._router.navigate(['/']);

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
                console.log('priceSummary', result);
                this.priceSummary = result;
            });
    }

    configurationChanged(selectedOptionProducts) {
        this.configuration.selectedOptionProducts = selectedOptionProducts;

        this.updateSummary();
    }

    orderCar() {
        alert('Bestellung ausführen');
    }
}
