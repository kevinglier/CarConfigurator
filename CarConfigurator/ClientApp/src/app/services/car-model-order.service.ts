import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestService } from './rest.service';
import { map } from 'rxjs/operators';
import { CarOrderDetails } from '../models/CarOrderDetails';

@Injectable({
    providedIn: 'root'
})
export class CarModelOrderService {

    constructor(
        private _restService: RestService
    ) {
    }

    latestOrder: CarOrderDetails;

    sendOrder(orderDetails: CarOrderDetails): Observable<CarOrderDetails> {
        return this._restService.post<CarOrderDetails>('carorder/send', orderDetails).pipe(
            map((order: CarOrderDetails) => {

                this.latestOrder = order;
                return order;
            })
        );
    }
}
