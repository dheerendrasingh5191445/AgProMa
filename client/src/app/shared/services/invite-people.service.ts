import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { Router } from "@angular/router";

@Injectable()
export class InvitePeopleService {

  invite_url='http://localhost:52258/api/InviteMembers'; //used for calling Invite People controller in API
  
  //local variable used in service
  errorMsg : any;

  constructor(private http:Http, private router : Router) { }


 getAll(){
   //To get the details of all user
   return this.http.get(this.invite_url).map(response=>response.json());
 }

  emailto(projectdetails:any) : Promise<any>
  {
    
    //this method will email to that user for invitation
    let headers=new Headers({ 'Content-Type': 'application/json' });
    let options=new RequestOptions({headers:headers});
    return this.http.post(this.invite_url,projectdetails,options)
    .toPromise()
    .catch(
      error=>{
        this.errorMsg = error; this.router.navigate(['/app-error/'])
      }
    );
  }
 
  
}
