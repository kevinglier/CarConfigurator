import { Injectable, Inject } from '@angular/core';
import { CarModelService } from './car-model.service';
import { Observable, forkJoin } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { CarConfiguration } from '../models/CarConfiguration';
import { CarModel } from '../models/CarModel';
import { CarModelOption } from '../models/CarModelOption';

@Injectable({
  providedIn: 'root'
})
export class ConfiguratorService {

  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _carModelService: CarModelService
  ) {
  }

  public getConfiguratorForCode(code: string) {
    // CarModelUserConfigurationService
  }

  public getNewConfigurationForModel(ean: string): Observable<CarConfiguration> {

    return this._carModelService.getModelByName(ean).pipe(switchMap(
      (model: CarModel) => {
        return this._carModelService.getModelOptions(model).pipe(
          map((modelOptions: CarModelOption[]) => {
            console.log(model, modelOptions);
            return new CarConfiguration(model, modelOptions);
          })
        );
      }));


//return forkJoin(
//  this._carModelService.getModelByName(ean),
//  this._carModelService.getModelOptions()
//  // ANOTHER_OBSERVABLE(), forkJoin on works for observables that complete
//).pipe(
//  map(([carModel, /*anotherObservable*/]) => {
//    // forkJoin returns an array of values, here we map those values to an object
//    return new CarConfiguration(carModel);
//  })
//);
  }
}
