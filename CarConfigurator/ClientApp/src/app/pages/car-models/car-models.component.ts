import { Component, OnInit } from '@angular/core';
import { CarModelService } from '../../services/car-model.service';
import { CarModel } from '../../models/CarModel';
import { UrlHelper } from '../../helpers/UrlHelper';

@Component({
    selector: 'app-car-models-component',
    templateUrl: './car-models.component.html'
})
export class CarModelsComponent implements OnInit {

    carModels: CarModel[];

    constructor(
        private _carModelService: CarModelService
    ) {
    }

    ngOnInit(): void {
        this._carModelService.getModelList().subscribe(result => {
                this.carModels = result;
            },
            error => console.error(error));
    }

    getUrlPartNameOf(name) {
        return UrlHelper.nameToUrlPart(name);
    }
}
