//module declaration
import { BrowserModule } from '@angular/platform-browser';
import { SocialLoginModule } from "angular4-social-login";
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule }from './app-routing.module';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { Http, HttpModule } from '@angular/http';
import {ShowHidePasswordModule} from 'ngx-show-hide-password';

//component declaration
import { SignupComponent } from './signup/signup.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectScreenComponent } from './project-screen/project-screen.component';
import { ProjectDetailComponent } from './project-screen/project-detail/project-detail.component';
import { FillDetailsComponent } from './project-screen/fill-details/fill-details.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { RegisterUserWithNewPasswordComponent } from './register-user-with-new-password/register-user-with-new-password.component';
import { InvitePeopleComponent } from './invite-people/invite-people.component';
import { TeammemberComponent } from './teams/teammember/teammember.component';

//service declaration
import { LoginService } from "./shared/services/login.service";
import { InvitePeopleService } from "./shared/services/invite-people.service";
import { ForgetServiceService } from "./shared/services/forget-service.service";
import { RegisterUserWithNewPasswordService } from "./shared/services/register-user-with-new-password.service";
import { AuthService } from "angular4-social-login";
import { AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import { ProjectScreenService } from './project-screen/project-screen.service';
import { TeamsService } from './shared/services/teams.service';


//
let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("861775280808-se6g8tlmjg61rqc97mbi29cdrobfj4ed.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("1942249056044697")
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    RegisterComponent,
    DashboardComponent,
    ProjectScreenComponent,
    ProjectDetailComponent,
    FillDetailsComponent,
    ForgetPasswordComponent,
    RegisterUserWithNewPasswordComponent,
    InvitePeopleComponent,
    TeammemberComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    NgxPaginationModule,
    HttpModule,
    ShowHidePasswordModule.forRoot()
  ],
  providers: [
    ProjectScreenService,
    AuthService,
    ForgetServiceService,
    InvitePeopleService,
    RegisterUserWithNewPasswordService,
    LoginService,
    TeamsService,
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
