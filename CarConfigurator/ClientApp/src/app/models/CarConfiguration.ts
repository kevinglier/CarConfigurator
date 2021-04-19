import { CarModel } from './CarModel';
import { CarModelOption } from './CarModelOption';
import { CarModelOptionProduct } from './CarModelOptionProduct';

export class CarConfiguration {
    private _selectedOptionProducts: { [optionId: number]: CarModelOptionProduct };
    private _availableOptions: CarModelOption[];
    private _model: CarModel;

    get model(): CarModel { return this._model; }

    get availableOptions(): CarModelOption[] { return this._availableOptions; }

    get selectedOptionProducts(): { [optionId: number]: CarModelOptionProduct } { return this._selectedOptionProducts; }

    set selectedOptionProducts(value: { [optionId: number]: CarModelOptionProduct }) {
        this._selectedOptionProducts = value;
    }

    constructor(
        model: CarModel,
        availableOptions: CarModelOption[],
        selectedOptionProducts: { [optionId: number]: CarModelOptionProduct } = null
    ) {
        this._availableOptions = availableOptions;
        this._model = model;

        selectedOptionProducts = selectedOptionProducts ? selectedOptionProducts : {};

        const ids = this._availableOptions.map(x => x.id);
        for (let id of ids) {

            const option = this._availableOptions.find(opt => opt.id === id);

            // Set currently unselected options to empty to reflect the number of available options.
            if (!selectedOptionProducts[id]) {
                selectedOptionProducts[id] = option.products.find(prod => prod.isDefault);
            }
        }

        this._selectedOptionProducts = selectedOptionProducts;
    }
}
