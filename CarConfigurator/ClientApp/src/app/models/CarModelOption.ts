import { CarModelOptionProduct } from './CarModelOptionProduct';

export class CarModelOption {
    id: number;
    name: string;
    description: string;
    products: CarModelOptionProduct[];
}
