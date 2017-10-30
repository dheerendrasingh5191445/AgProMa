import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewReleaseFillingDetailsComponent } from './new-release-filling-details.component';

describe('NewReleaseFillingDetailsComponent', () => {
  let component: NewReleaseFillingDetailsComponent;
  let fixture: ComponentFixture<NewReleaseFillingDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewReleaseFillingDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewReleaseFillingDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
