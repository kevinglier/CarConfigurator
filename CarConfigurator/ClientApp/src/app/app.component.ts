import { Component } from '@angular/core';

import { CarModelService } from './services/car-model.service';
import { ConfiguratorService } from './services/configurator.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [
    CarModelService,
    ConfiguratorService
  ]
})
export class AppComponent {
  title = 'ANT';
}
