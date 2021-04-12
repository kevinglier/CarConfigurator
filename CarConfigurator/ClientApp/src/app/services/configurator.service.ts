import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CarModelService } from './car-model.service';
import { Observable, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { CarConfiguration } from '../models/CarConfiguration';

@Injectable({
  providedIn: 'root'
})
export class ConfiguratorService {

  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _carModelService: CarModelService,
    private _http: HttpClient
  ) { }

  public getConfiguratorForCode(code: string) {
  }

  public getNewConfigurationForModel(modelName: string): Observable<CarConfiguration> {

    return forkJoin(
      this._carModelService.getModelByName(modelName)
      // ANOTHER_OBSERVABLE(), forkJoin on works for observables that complete
    ).pipe(
      map(([carModel, /*anotherObservable*/]) => {
        // forkJoin returns an array of values, here we map those values to an object
        return new CarConfiguration(carModel);
      })
    );
  }
}
