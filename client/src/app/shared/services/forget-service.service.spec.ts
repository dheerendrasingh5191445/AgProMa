import { TestBed, inject } from '@angular/core/testing';

import { ForgetServiceService } from './forget-service.service';

describe('ForgetServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ForgetServiceService]
    });
  });

  it('should be created', inject([ForgetServiceService], (service: ForgetServiceService) => {
    expect(service).toBeTruthy();
  }));
});
