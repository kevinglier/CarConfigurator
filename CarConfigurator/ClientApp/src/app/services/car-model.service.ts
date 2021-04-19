import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarModel } from '../models/CarModel';
import { CarModelOption } from '../models/CarModelOption';
import { RestService } from './rest.service';

@Injectable({
    providedIn: 'root'
})
export class CarModelService {

    constructor(
        private _restService: RestService
    ) {
    }

    getModelByName(name: string): Observable<CarModel> {
        return this._restService.get<CarModel>('carmodel/{name}', { name: name });
    }

    getModelList(): Observable<CarModel[]> {
        return this._restService.get<CarModel[]>('carmodel/list');
    }

    getModelOptions(carModel: CarModel): Observable<CarModelOption[]> {

        return this._restService.get<CarModelOption[]>('carmodel/{name}/option/list', { name: carModel.name });
    }
}
