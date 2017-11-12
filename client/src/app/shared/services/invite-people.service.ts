import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { ConfigFile } from "../config";

@Injectable()
export class InvitePeopleService {
  
  constructor(private http:Http) { }


 getAll(){
   //To get the details of all user
   return this.http.get(ConfigFile.InvitePeopleServiceUrl.invite_url).map(response=>response.json());
 }

  emailto(projectdetails:any) : Promise<any>
  {
    
    //this method will email to that user for invitation
    let headers=new Headers({ 'Content-Type': 'application/json' });
    let options=new RequestOptions({headers:headers});
    return this.http.post(ConfigFile.InvitePeopleServiceUrl.invite_url,projectdetails,options)
    .toPromise().catch(this.handleError);
  }
  
  //Used for error handling
    private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
  
}
