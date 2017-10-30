import { TestBed, inject } from '@angular/core/testing';

import { ReleasePlanService } from './release-plan.service';

describe('ReleasePlanService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReleasePlanService]
    });
  });

  it('should be created', inject([ReleasePlanService], (service: ReleasePlanService) => {
    expect(service).toBeTruthy();
  }));
});
