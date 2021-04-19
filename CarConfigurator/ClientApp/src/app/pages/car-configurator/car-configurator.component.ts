import { Component, Inject, ViewChild, OnDestroy, OnInit, AfterViewInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Subscription, Subject } from 'rxjs';

import { ConfiguratorService } from '../../services/configurator.service';
import { CarConfiguration } from '../../models/CarConfiguration';
import { CarModelOptionProduct } from '../../models/CarModelOptionProduct';
import { CarModelOption } from '../../models/CarModelOption';

@Component({
  selector: 'app-car-configurator-component',
  templateUrl: './car-configurator.component.html'
})
export class CarConfiguratorComponent implements OnDestroy, OnInit, AfterViewInit {

  private _initialized = false;

  private _formChangesSubscription: Subscription;

  private _configurationForm: NgForm;

  
  @ViewChild('configurationForm', { static: false })
  set configurationForm(form: NgForm) {
    if (form && !this._formChangesSubscription) {

      form.resetForm();
      this._configurationForm = form;

      
      // Subscribe to form changes
      this._formChangesSubscription = this._configurationForm.valueChanges.subscribe(form => {

        if (!this._initialized)
          return;

        // If there was no user change yet, we don't need to handle this event
        //if (!this._configurationForm.touched)
        //  return;

        // If the component is not fully initialized, don't do anything
        if (!this.configuration && !this.configuration.selectedOptionProducts)
          return;

        this.formChanged(this._configurationForm);
      });
    }
  }

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

  ngAfterViewInit(): void {
    this._initialized = true;
  }

  ngOnDestroy() {
    if (this._formChangesSubscription)
      this._formChangesSubscription.unsubscribe();
  }

  formChanged(result) {
    console.log('!!');
    return;
    
    console.log('formChanged 1');
    // If the component is not fully initialized, don't do anything
    if (!this.configuration && !this.configuration.selectedOptionProducts)
      return;

    console.log('formChanged 2');
    
    this._configuratorService
      .getPriceSummaryForSelectedCarOptionProducts(this.configuration.model.ean, this.configuration.selectedOptionProducts)
      .subscribe(result => {
        this.priceSummary = result;
      });
  }

  getProductsForOption(optionId: number): CarModelOptionProduct[] {
    return this.configuration.availableOptions.find(x => x.id === optionId).products;
  }

  updateSummary() {
    if (!this.configuration || !this.configuration.model.ean || !this.configuration.selectedOptionProducts)
      return;

    this._configuratorService
      .getPriceSummaryForSelectedCarOptionProducts(this.configuration.model.ean, this.configuration.selectedOptionProducts)
      .subscribe(result => {
        console.log('priceSummary', result);
        this.priceSummary = result;
      }); 
  }

  setSelectedOptionProduct($event: CarModelOptionProduct, option: CarModelOption): void {
    this.configuration.selectedOptionProducts[option.id] = $event;

    this.updateSummary();
  }

  getPriceForSelectedOption(optionId) {
    if (this.configuration.selectedOptionProducts[optionId]) {
      return this.configuration.selectedOptionProducts[optionId].price;
    }
    return null;
  }

  getPriceChangeForOptionProduct(optionId, optionProduct) {

    if (!optionProduct)
      return 0;

    if (this.configuration.selectedOptionProducts[optionId]) {
      return optionProduct.price - this.configuration.selectedOptionProducts[optionId].price;
    }

    return optionProduct.price;
  }
}
