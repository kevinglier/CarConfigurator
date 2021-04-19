import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { CarModelOption } from '../../models/CarModelOption';
import { CarModelOptionProduct } from '../../models/CarModelOptionProduct';

@Component({
    selector: 'car-model-options',
    templateUrl: './car-model-options.component.html'
})
export class CarModelOptionsComponent implements OnInit {

    private _value: { [optionId: number]: CarModelOptionProduct };
    initialized = false;

    @Input('availableOptions')
    availableOptions: CarModelOption[];

    @Output()
    valueChange = new EventEmitter<{ [optionId: number]: CarModelOptionProduct }>();

    @Input('value')
    set value(value: { [optionId: number]: CarModelOptionProduct }) {

        let newValue: { [optionId: number]: CarModelOptionProduct } = {};

        for (let optionId in value) {

            const option = this.availableOptions.find(x => x.id === +optionId);
            const product = value[optionId];

            // set correct reference to objects in availableOptions
            newValue[optionId] = option.products.find(prod => prod.ean === product.ean);
        }

        this._value = newValue;
        //this.valueChange.emit(this._value);
    }

    get value(): { [optionId: number]: CarModelOptionProduct } {
        return this._value;
    }

    constructor() {

    }

    ngOnInit(): void {

        console.log(this.value, this.availableOptions);
        this.initialized = true;
    }

    getProductsForOption(optionId: number): CarModelOptionProduct[] {
        return this.availableOptions.find(x => x.id === optionId).products;
    }

    getPriceForSelectedOption(optionId) {
        if (this.value[optionId]) {
            return this.value[optionId].price;
        }
        return null;
    }

    getPriceChangeForOptionProduct(optionId, optionProduct) {

        if (!optionProduct)
            return 0;

        if (this.value[optionId]) {
            return optionProduct.price - this.value[optionId].price;
        }

        return optionProduct.price;
    }


    setSelectedOptionProduct($event: CarModelOptionProduct, option: CarModelOption): void {
        this.value[option.id] = $event;

        this.valueChange.emit(this.value);
    }
}
