import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import { Login }from './../shared/model/login';
import swal from 'sweetalert2';
import{LoginService}from '../shared/services/login.service'
import { ProjectMember } from "../shared/model/projectMember";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: SocialUser;
  details:any[]=[];
  userdetails:any;
  index:Login;
  private model = {
    FirstName:'',
    LastName:'',  
    Organization:'',  
   Department:'', 
    Email:'',   
   Password:''
    };
    ConfirmPassword:string;
    projectmember:ProjectMember={
      ProjectId:null,
    MemberId:null,
    ActAs:null
    }
  
  
    constructor(private authService: AuthService,private router:Router,private loginservice:LoginService,private route:ActivatedRoute) { }
  
    ngOnInit() {
      this.authService.authState.subscribe((user) => {
        this.user = user;
       if(this.user != null){ this.router.navigate(["app-dashboard"])}
      });
      this.loginservice.getAll().subscribe(details=>{
        this.details=details;
      })
      this.route.params.subscribe((param) => 
      this.projectmember.ProjectId = +param['id']);
      console.log("helllooo",this.projectmember.ProjectId);
    }
  
    signInWithGoogle(): void {
      this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)
                      .then(data =>{ console.log(data)
                        this.authService.authState.subscribe((user) => {
                          this.user = user;
                          if(this.user != null){ this.router.navigate(["app-dashboard"])}
                        })});
    }
  
    signInWithFB(): void {
      this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(data =>{ console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user;
          if(this.user != null){ this.router.navigate(["app-dashboard"])}
        })});
    }
  
    signOut(): void {
      this.authService.signOut();
    }
    
    CreateAccount(){

      
      this.index=this.details.find((m)=>m.email==this.model.Email);
     
      
      if(this.model.Department==''||this.model.Email==''||this.model.FirstName==''||this.model.LastName==''||this.model.Organization==''||this.model.Password==''){
        swal('','Enter the Required fields','error');
      }
     else if (!this.model.Email.includes("@"&&".")){
      swal('','Enter valid email-address','error');  
    }
    else if(this.model.Password != this.ConfirmPassword){
      swal('','confirm password does not match the password','error')
    }
   
     
     else if(this.model.Password==this.ConfirmPassword){
       if(this.index==undefined ){
         if((this.projectmember.ProjectId != null))
          {
            this.projectmember.ActAs=1;
      this.loginservice.postLoginDetails(this.model).then(data=>this.loginservice.get(this.model.Email).then(detail=>{
        this.userdetails=detail;
        this.projectmember.MemberId=this.userdetails.id;
        console.log('gsgsgsgs',this.userdetails);
      })).then(project=>this.loginservice.postMemberDetails(this.projectmember)).then(
      msg=> swal('','Your account has been created','success'));

      this.router.navigateByUrl('/app-signup');
          }
          else{
            this.loginservice.postLoginDetails(this.model);
            swal('','Your account has been created','success');
            this.router.navigateByUrl('/app-signup');
          }
       }
       else{
         swal('','Email Already Exists','error')
       } 
    }
     
    
    }

}
