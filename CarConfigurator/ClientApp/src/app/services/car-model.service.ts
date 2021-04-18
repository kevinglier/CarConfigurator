import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarModel } from '../models/CarModel';
import { CarModelOption } from '../models/CarModelOption';
import { RestService } from './rest.service';

@Injectable({
  providedIn: 'root'
})
export class CarModelService {
  
  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _restService: RestService
  ) {
  }

  public getModelByName(ean: string): Observable<CarModel> {
    return this._restService.get<CarModel>('carmodel/{ean}', { ean: ean });
  }

  public getModelList(): Observable<CarModel[]> {
    return this._restService.get<CarModel[]>('carmodel/list');
  }

  public getModelOptions(carModel: CarModel): Observable<CarModelOption[]> {

    return this._restService.get<CarModelOption[]>('carmodel/{ean}/option/list', { ean: carModel.ean });
  }
}
