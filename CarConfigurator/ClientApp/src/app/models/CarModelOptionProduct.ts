export class CarModelOptionProduct {
  private _name: string;
  private _description: string;

  public get name(): string {
    return this._name;
  }
  public get description(): string {
    return this._description;
  }

  constructor(productId: string, name: string, description: string, optionValues: CarModelOptionProduct[]) {
    this._name = name;
    this._description = description;
  }
}
