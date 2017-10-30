import { TestBed, inject } from '@angular/core/testing';

import { InvitePeopleService } from './invite-people.service';

describe('InvitePeopleService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InvitePeopleService]
    });
  });

  it('should be created', inject([InvitePeopleService], (service: InvitePeopleService) => {
    expect(service).toBeTruthy();
  }));
});
