import { TestBed } from '@angular/core/testing';

import { CarModelService } from './car-model.service';

describe('CarModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CarModelService = TestBed.get(CarModelService);
    expect(service).toBeTruthy();
  });
});
