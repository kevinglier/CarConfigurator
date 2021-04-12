import { Component, Inject } from '@angular/core';
import { CarModelService } from '../../services/car-model.service';
import { CarModel } from '../../models/CarModel';

@Component({
  selector: 'app-car-models-component',
  templateUrl: './car-models.component.html'
})
export class CarModelsComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }

  public carModels: CarModel[];

  constructor(
    private _carModelService: CarModelService
  ) {
    _carModelService.getModelList().subscribe(result => {
      this.carModels = result;
    }, error => console.error(error));
  }
}
