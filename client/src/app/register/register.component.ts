//import all the dependencies

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
  private model = {             //Binding with html 
    FirstName:'',
    LastName:'',  
    Organization:'',  
   Department:'', 
    Email:'',   
   Password:''
    };
    ConfirmPassword:string;
    projectmember:ProjectMember={               //object type of Projectmember
      ProjectId:null,
    MemberId:null,
    ActAs:null
    }
  
  //inject sevices on which this component depends
    constructor(private authService: AuthService,private router:Router,private loginservice:LoginService,private route:ActivatedRoute) { }
  
    ngOnInit() {
      this.authService.authState.subscribe((user) => {                
        this.user = user;
       if(this.user != null){ this.router.navigate(["app-dashboard"])}
      });
      this.loginservice.getAll().subscribe(details=>{         //calling getall function of loginservice 
        this.details=details;                                 //and return details of all users
      })
      this.route.params.subscribe((param) => 
      this.projectmember.ProjectId = +param['id']);               //getting project id from route
      console.log("helllooo",this.projectmember.ProjectId);
    }
  
    signInWithGoogle(): void {
      this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)   //calling signIn function of authoservice
                      .then(data =>{ console.log(data)
                        this.authService.authState.subscribe((user) => {
                          this.user = user;
                          if(this.user != null){ this.router.navigate(["app-dashboard"])}
                        })});
    }
  
    signInWithFB(): void {
      this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)    //calling signIn function from authservice
      .then(data =>{ console.log(data)
        this.authService.authState.subscribe((user) => {
          this.user = user;
          if(this.user != null){ this.router.navigate(["app-dashboard"])}
        })});
    }
  
    signOut(): void {
      this.authService.signOut();
    }
    
    CreateAccount(){                        //registering the user

      
      this.index=this.details.find((m)=>m.email==this.model.Email);  //find the details of a particular user 
     
      
      if(this.model.Department==''||this.model.Email==''||this.model.FirstName==''||this.model.LastName==''||this.model.Organization==''||this.model.Password==''){
        swal('','Enter the Required fields','error');             //if any entry is empty then show the alert 
      }
     else if (!this.model.Email.includes("@"&&".")){
      swal('','Enter valid email-address','error');               //if emailid does not contains @ and . then show the alert
    }
    else if(this.model.Password != this.ConfirmPassword){
      swal('','confirm password does not match the password','error')    //if confirm and password is not equal then show password does not match
    }
   
     
     else if(this.model.Password==this.ConfirmPassword){
       if(this.index==undefined ){
         if((this.projectmember.ProjectId != null))
          {
            this.projectmember.ActAs=1;
      this.loginservice.postLoginDetails(this.model).then(data=>this.loginservice.get(this.model.Email).then(detail=>{  //posting the details of user 
        this.userdetails=detail;                                                                                        //then calling the get method to find the user unique id  
        this.projectmember.MemberId=this.userdetails.id;                                                                //then posting the user id and team id
        console.log('gsgsgsgs',this.userdetails);
      })).then(project=>this.loginservice.postMemberDetails(this.projectmember)).then(
      msg=> swal('','Your account has been created','success'));

      this.router.navigateByUrl('/app-signup');     //navigate to the signup page
          }
          else{
            this.loginservice.postLoginDetails(this.model);       //calling post method to register the details       
            swal('','Your account has been created','success');
             this.router.navigateByUrl('/app-signup');      //navigate to the signup page
          }
       }
       else{
         swal('','Email Already Exists','error')      //if enter id matches with the existing id in database 
       } 
    }
     
    
    }

}
