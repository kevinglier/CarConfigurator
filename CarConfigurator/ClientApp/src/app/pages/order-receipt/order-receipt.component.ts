import { Component, OnDestroy, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { CarOrderDetails } from '../../models/CarOrderDetails';
import { CarModelOrderService } from '../../services/car-model-order.service';

@Component({
    selector: 'app-order-receipt-component',
    templateUrl: './order-receipt.component.html'
})
export class OrderReceiptComponent implements OnDestroy, OnInit, AfterViewInit {

    order: CarOrderDetails;

    constructor(
        private _carModelOrderService: CarModelOrderService,
        private _router: Router) {
    }

    ngOnInit() {
        this.order = this._carModelOrderService.latestOrder;

        if (!this.order)
            this._router.navigate(['/']);
    }

    ngAfterViewInit(): void {}

    ngOnDestroy() {}
}
