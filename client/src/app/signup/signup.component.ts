import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { Router } from '@angular/router';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import swal from 'sweetalert2';
import{ LoginService }from '../shared/services/login.service'
import { Master } from '../shared/model/master';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  user: SocialUser;
  email:string ='';
  password:string='';
  details:Master;
  
    constructor(private authService: AuthService, private router:Router,private loginservice:LoginService) { }
  
    ngOnInit() {
      this.authService.authState.subscribe((user) => {
        this.user = user;
        if(this.user != null)
          { this.router.navigateByUrl('app-dashboard')}
        
      });
    }
  
    signInWithGoogle(): void {
      this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(data =>{ console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user;
          if(this.user != null)
          { this.router.navigateByUrl('app-dashboard')}
        })});
    }
  
    signInWithFB(): void {
      this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(data =>{ console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user;
          if(this.user != null)
          { this.router.navigateByUrl('app-dashboard')}
        })});
    }
  
    signOut(): void {
      this.authService.signOut();
    }
    login(){
      if(this.email=='' || this.password=='')
        {
           swal('',"Enter the proper details","error");
        }
        else{
      this.loginservice.get(this.email).then(details=>{
                                                this.details=details;
        if(details==undefined)
          {
            swal('',"you are not registered","error"); 
          }
        else if((this.password == this.details.password)&&(this.email==this.details.email)){
          sessionStorage.setItem("id",this.details.id.toString());
          this.router.navigate(["app-dashboard"]);
        }
        else{
          swal('',"Enter Valid Id or Password","error");          
        }
      });
    }
  
    }


}
