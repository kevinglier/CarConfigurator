import { CarModel } from './CarModel';

export class CarConfiguration {
  private _model: CarModel;

  public get model(): CarModel {
    return this._model;
  }

  constructor(model: CarModel) {
    this._model = model;
  }
}
