import { TestBed, inject } from '@angular/core/testing';

import { RegisterUserWithNewPasswordService } from './register-user-with-new-password.service';

describe('RegisterUserWithNewPasswordService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegisterUserWithNewPasswordService]
    });
  });

  it('should be created', inject([RegisterUserWithNewPasswordService], (service: RegisterUserWithNewPasswordService) => {
    expect(service).toBeTruthy();
  }));
});
