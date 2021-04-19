import { CarModelOptionProduct } from './CarModelOptionProduct';

export class CarConfiguratorSummary {

    priceBase: number;
    priceTotal: number;
    priceOptions: number;
    selectedOptionProducts: CarModelOptionProduct[];
    code: string;
    selectedModelEAN: string;
}
