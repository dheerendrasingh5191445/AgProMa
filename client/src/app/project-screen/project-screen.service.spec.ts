import { TestBed, inject } from '@angular/core/testing';

import { ProjectScreenService } from './project-screen.service';

describe('ProjectScreenService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProjectScreenService]
    });
  });

  it('should be created', inject([ProjectScreenService], (service: ProjectScreenService) => {
    expect(service).toBeTruthy();
  }));
});
