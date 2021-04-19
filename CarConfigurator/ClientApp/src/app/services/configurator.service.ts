import { Injectable, Inject } from '@angular/core';
import { CarModelService } from './car-model.service';
import { Observable, forkJoin } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { CarConfiguration } from '../models/CarConfiguration';
import { CarModel } from '../models/CarModel';
import { CarModelOption } from '../models/CarModelOption';
import { CarConfiguratorSummary } from '../models/CarConfiguratorSummary';
import { CarModelOptionProduct } from '../models/CarModelOptionProduct';
import { RestService } from './rest.service';

@Injectable({
  providedIn: 'root'
})
export class ConfiguratorService {


  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _carModelService: CarModelService,
    private _restService: RestService
  ) {
  }

  public getConfigurationForModel(ean: string): Observable<CarConfiguration> {

    return this._carModelService.getModelByName(ean).pipe(switchMap(
      (model: CarModel) => {

        const storage = this.readFromLocalStorage();

        // If code is given (in the LocalStorage) lets try to get the selection back then.
        let observables = <any>{ modelOptions: this._carModelService.getModelOptions(model) };
        if (storage && storage.saves[ean] && storage.saves[ean].code) {
          observables.savedConfiguration = this.getSavedConfiguration(storage.saves[ean].code);
        }

        return forkJoin(observables).pipe(
          map((result: any) => {

            console.log('RESULT', result);

            let carConfiguration = new CarConfiguration(model,
              result.modelOptions,
              result.savedConfiguration ? result.savedConfiguration : null);

            console.log('carConfiguration', carConfiguration);

            return carConfiguration;
          })
        );
      }));
  }

  getPriceSummaryForSelectedCarOptionProducts(
      carModelEAN: string,
      carModelOptionProduct: { [optionId: number]: CarModelOptionProduct }):
    Observable<CarConfiguratorSummary> {

    // read the code from localStorage if available so that changes will be updated in the db for this configuration
    // if there is no code given, the rest endpoint will set one that will be used in the bottom of this function.
    const carConfiguratorSave = this.readFromLocalStorage();
    const code = carConfiguratorSave && carConfiguratorSave.saves && carConfiguratorSave.saves[carModelEAN]
      ? carConfiguratorSave.saves[carModelEAN].code
      : null;


    const payload = {
      code: code,
      carModelEAN: carModelEAN,
      optionProducts: carModelOptionProduct
    };

    return this._restService.post('carconfigurator/summary', payload).pipe(map((summary: CarConfiguratorSummary) => {

      console.log('resultof carconfigurator/summary', summary);

      if (!summary)
        return null;

      // Save the server validated code in the localstorage
      this.saveConfigurationToLocalStorage(carModelEAN, summary.code);

      return summary;
    }));
  }

  getSavedConfiguration(code): Observable<CarModelOption[]> {

    return this._restService.get('carconfigurator/savedconfiguration/{code}', { code: code }).pipe(map(result => {

      console.log('resultof carconfigurator/savedconfiguration/', result);

      return <any>result;
    }));
  }

  private static localStorageKey = 'carConfigurator';

  private saveConfigurationToLocalStorage(carModelEAN, code) {

    let storageItem = this.readFromLocalStorage();
    if (!storageItem)
      storageItem = <ILocalStorageItemCarConfigurator>{ saves: {} };

    storageItem.saves[carModelEAN] = {
      carModelEAN: carModelEAN,
      code: code
    }

    const lsString = JSON.stringify(storageItem);
    localStorage.setItem(ConfiguratorService.localStorageKey, lsString);
  }

  private readFromLocalStorage(): ILocalStorageItemCarConfigurator {
    const lsString = localStorage.getItem(ConfiguratorService.localStorageKey);
    return <ILocalStorageItemCarConfigurator>JSON.parse(lsString);
  }
}
