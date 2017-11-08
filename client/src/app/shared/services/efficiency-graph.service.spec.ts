import { TestBed, inject } from '@angular/core/testing';

import { EfficiencyGraphService } from './efficiency-graph.service';

describe('EfficiencyGraphService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EfficiencyGraphService]
    });
  });

  it('should be created', inject([EfficiencyGraphService], (service: EfficiencyGraphService) => {
    expect(service).toBeTruthy();
  }));
});
