import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, NavigationEnd } from '@angular/router';
import { ConfiguratorService } from '../../services/configurator.service';
import { CarConfiguration } from '../../models/CarConfiguration';
import { CarModelOptionProduct } from '../../models/CarModelOptionProduct';
import { CarModelOption } from '../../models/CarModelOption';

@Component({
  selector: 'app-car-configurator-component',
  templateUrl: './car-configurator.component.html'
})
export class CarConfiguratorComponent {

  public price = 0;

  public configuration: CarConfiguration;

  constructor(
   private _configuratorService: ConfiguratorService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router) {
  }

  ngOnInit() {

    this._activatedRoute.paramMap.subscribe((params: ParamMap) => {
      const ean = params.get('model');

      this._configuratorService.getNewConfigurationForModel(ean).subscribe(
        result => {
          if (!result)
            this._router.navigate(['/']);

          this.configuration = result;
          console.log('configuration', this.configuration);
        },
        error => { 
          console.error(error);
          this._router.navigate(['/']);
        }
      );

      this.price = 20000;
    });
  }
  
  getProductsForOption(optionId: number): CarModelOptionProduct[] {
    return this.configuration.availableOptions.find(x => x.id === optionId).products;
  }

  setSelectedOptionProduct($event: CarModelOptionProduct, option: CarModelOption): void {
    
    console.log('option: ', option, 'event: ', $event);

    this.configuration.selectedOptionProducts[option.id] = $event;
  }
}
