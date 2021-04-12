import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, NavigationEnd } from '@angular/router';
import { ConfiguratorService } from '../../services/configurator.service';

@Component({
  selector: 'app-car-configurator-component',
  templateUrl: './car-configurator.component.html'
})
export class CarConfiguratorComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }

  public configuration;

  constructor(
   private _configuratorService: ConfiguratorService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router) {
  }

  ngOnInit() {

    this._activatedRoute.paramMap.subscribe((params: ParamMap) => {
      const modelName = params.get('model');

      this._configuratorService.getNewConfigurationForModel(modelName).subscribe(
        result => {
          if (!result)
            this._router.navigate(['/']);

          this.configuration = result;
        },
        error => { 
          console.error(error);
          this._router.navigate(['/']);
        }
      );
    });
  }
}
