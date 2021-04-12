import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarModel } from '../models/CarModel';

@Injectable({
  providedIn: 'root'
})
export class CarModelService {


  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _httpClient: HttpClient
  ) {
  }

  public getModelByName(modelName: string): Observable<CarModel> {
    return this._httpClient.get<CarModel>(`${this._baseUrl}carmodel/getbyname/${modelName}`);
  }

  public getModelList(): Observable<CarModel[]> {
    return this._httpClient.get<CarModel[]>(this._baseUrl + 'carmodel/list');
  }
}
