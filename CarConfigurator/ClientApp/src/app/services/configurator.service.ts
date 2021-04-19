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

    public getConfigurationForModel(name: string, code: string=undefined): Observable<CarConfiguration> {

        return this._carModelService.getModelByName(name).pipe(switchMap(
            (model: CarModel) => {

                const observables = <any>{ modelOptions: this._carModelService.getModelOptions(model) };

                // If sharing-code is given (in the LocalStorage) lets try to get the selection back then.
                if (code) {
                    observables.savedConfiguration = this.getSavedConfiguration(code);
                } else {
                    const storage = this.readFromLocalStorage();
                    if (storage && storage.saves[model.ean] && storage.saves[model.ean].code) {
                        observables.savedConfiguration = this.getSavedConfiguration(storage.saves[model.ean].code);
                    }
                }

                return forkJoin(observables).pipe(
                    map((result: any) => {
                        const carConfiguration = new CarConfiguration(model,
                            result.modelOptions,
                            result.savedConfiguration ? result.savedConfiguration : null);

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

        return this._restService.post('carconfigurator/summary', payload).pipe(map(
            (summary: CarConfiguratorSummary) => {

                if (!summary)
                    return null;

                // Save the server validated code in the localstorage
                this.saveConfigurationToLocalStorage(carModelEAN, summary.code);

                return summary;
            }));
    }

    getSavedConfiguration(code): Observable<CarModelOption[]> {

        return this._restService.get('carconfigurator/savedconfiguration/{code}', { code: code }).pipe(map(result => {

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
