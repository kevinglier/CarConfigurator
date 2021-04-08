import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<CarModel[]>(baseUrl + 'carmodel/list').subscribe(result => {
      this.carModels = result;
    }, error => console.error(error));
  }
}

interface CarModel {
  name: string;
  description: string;
}
