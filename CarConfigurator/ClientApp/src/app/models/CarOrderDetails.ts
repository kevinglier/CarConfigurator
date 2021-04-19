import { CarModelOptionProduct } from './CarModelOptionProduct';

export class CarOrderDetails {

    orderNo: string;

    constructor(
        public carModelEAN: string,
        public carOptionProducts: { [groupId: number]: CarModelOptionProduct },
        public code: string) {
    }
}
