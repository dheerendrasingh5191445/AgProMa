import { async, ComponentFixture, TestBed ,fakeAsync,tick} from '@angular/core/testing';
import { DebugElement ,NO_ERRORS_SCHEMA}    from '@angular/core';
import {By} from '@angular/platform-browser';
import { UserProfileComponent } from './user-profile.component';
import { LoginService } from '.././shared/services/login.service';
import { FormsModule } from '@angular/forms';
import { HttpModule,Http } from '@angular/http';
import {Router} from '@angular/router';
import { Observable } from 'rxjs'
import { RouterTestingModule } from '@angular/router/testing';

describe('UserProfileComponent', () => {
  let component: UserProfileComponent;
  let fixture: ComponentFixture<UserProfileComponent>;
  let spy: jasmine.Spy;
  let spyTransaction:jasmine.Spy;
  let de:DebugElement;
  let el:HTMLElement;
  let MOCKMASTER = {"department":"Bootcamp","email":"aabhaas9413@gmail.com","firstName":"aabhaas","lastName":"malhotra","password":"abc"}
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[FormsModule,HttpModule,RouterTestingModule],
      declarations: [UserProfileComponent],
      providers: [LoginService, {provide:Router}],
      schemas:[NO_ERRORS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserProfileComponent);
    component = fixture.componentInstance;
    var login=fixture.debugElement.injector.get(LoginService);
   
    spy=spyOn(login,'getById').and.returnValue(Observable.of(MOCKMASTER));
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
  it('Display title your info', () => {
    //query the id head of form
    let de = fixture.debugElement.query(By.css('#YourInfo'));
    let el = de.nativeElement;
    expect('  Your Info  ').toContain(el.textContent);
  });
  it('should show building dropdown after ngOnInit (fakeAsync)', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#firstName'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual("aabhaas");
  }));
  it('should show building dropdown after ngOnInit (fakeAsync)', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#lastName'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" malhotra");
  }));
  it('should show building dropdown after ngOnInit (fakeAsync)', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#email'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" aabhaas9413@gmail.com");
  }));
  it('should show building dropdown after ngOnInit (fakeAsync)', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#department'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" Bootcamp");
  }));
  it('should bind property toDate to the correct input', fakeAsync(() => {
    fixture.detectChanges();
    component.details.password="123";
    component.str="12345";
    component.newPassword="1234";
    component.confirmPassword="1234";
    spyOn(window,'alert')
    fixture.detectChanges();
    tick();  
    // component.model.toDate = '2017-09-09';
    // fixture.detectChanges();
     let element = fixture.debugElement.queryAll(By.css('#password_modal_save'));
    // element.dispatchEvent(new Event('input'));
     component.checkPassword();
     tick();
     fixture.detectChanges();
     expect(window.alert).toHaveBeenCalledWith("enter correct password");
    // ).toContain(component.model.toDate);

  }));

})