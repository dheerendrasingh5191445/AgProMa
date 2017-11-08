import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { Router } from '@angular/router';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import swal from 'sweetalert2';

import { LoginService } from '../shared/services/login.service'
import { IdPassword } from '../shared/model/idpassword';
import { Credential } from '../shared/model/credential';
import { authentication } from "../shared/model/Authentication";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  //local variable used in this component
  user: SocialUser;
  email: string = '';
  password: string = '';
  userCred: Credential;
  tokenData: any;

  constructor(private authService: AuthService, private router: Router, private loginservice: LoginService) { }

  ngOnInit() {
    //this method will run when the page is loaded
    //this method will check wheather the user is logged in with social account or not then the user can be moved to according screens
    this.authService.authState.subscribe((user) => {
      this.user = user; //checking wheather user variable has data in it or not
      if (this.user != null)
      { this.router.navigateByUrl('app-dashboard') } //if the user is logged in with social account then user can directly moved to dashboard screen
    });
  }

  signInWithGoogle(): void {
    //this method is used for social login(gmail)
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(data => {
        console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user; // data is assigned from gmail to local variable
          if (this.user != null)
          { this.router.navigateByUrl('app-dashboard') } //user will be redirected to dashboard screen
        })
      });
  }

  signInWithFB(): void {
    //this method is used for social login(facebook)
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(data => {
        console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user; // data is assigned from facebook to local variable
          if (this.user != null)
          { this.router.navigateByUrl('app-dashboard') } //user will be redirected to dashboard screen
        })
      });
  }

  signOut(): void {
    //this method is used to signout with AgProMa project
    this.authService.signOut();
  }


  login() {
    let auth: authentication = new authentication("", "");
    //this method is used to verify user's credentials
    if (this.email == '' || this.password == '') //if username or password is empty
    {
      swal('', "Enter the proper details", "error");
    }

    else {

      let model: IdPassword = new IdPassword(this.email, this.password);

      this.loginservice.check(model).then(data => {
        this.userCred = data;
        if (this.userCred["status"] == "success") // if user is register with AgProMa
        {
          //call method for token generation
          this.loginservice.getToken(this.userCred).then(data => {
            //debugger;
            this.tokenData = JSON.parse(data["_body"]).token;
            sessionStorage.setItem("id", this.userCred["userId"].toString());
            sessionStorage.setItem("token", this.tokenData);
        
          if (this.tokenData)
          { this.router.navigate(["/app-dashboard"]); } //if user's credentials are correct then user will br redirected to dashboard
        });
        }

        else if (this.userCred["status"] == "email") {
          swal('', "Enter Valid Id or Password", "error");
        }
        else {
          //if username or password is incorrect
          swal('', "you are not registered", "error");
        }
      });
    }

  }


}