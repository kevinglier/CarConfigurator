export class CarModel {
  private _name: string;
  private _description: string;

  public get name(): string {
    return this._name;
  }
  public get description(): string {
    return this._description;
  }

  constructor(name: string, description: string) {
    this._name = name;
    this._description = description;
  }
}
