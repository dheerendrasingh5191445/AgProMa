//import all the dependencies and packages 
import { Injectable } from '@angular/core'
import { Http, Headers, Response,RequestOptions } from '@angular/http';
import { Login } from '../model/login';
import { Observable } from "rxjs";
import { ProjectMember } from "../model/projectMember";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { IdPassword } from '../model/idpassword';
import { authentication } from "../model/Authentication";

@Injectable()
export class LoginService {

  constructor(private http: Http) { }
  url = 'http://192.168.252.131:8030/api/Login';                 //url for login 
  memberUrl='http://localhost:52258/api/ProjectMember';     //url for project members 
  invite_url='http://localhost:52258/api/InviteMembers/';
  checkurl='http://192.168.252.131:8030/api/Login/Check';
  private headers = new Headers({ 'Content-Type': 'application/json' });
 
 //get all the details of user
 getAll(){
   return this.http
   .get(this.url)
   .map((response)=>response.json());
 }
  //get the userid on basis of email
  get(email:string){
    return this.http.get(this.url+"/"+email)
                    .map(data => data);
  }

 //check details of a particular user by emailid and password
  check(data:IdPassword){
   return this.http
              .post(this.checkurl,data,{headers:this.headers})
              .map((response)=>response.json())
              .toPromise()
              .catch(this.handleError);

 }


 getToken(auth : authentication)
 {
   return this.http.post("http://localhost:57318/api/TokenGeneration/createtoken",auth,{headers:this.headers})
                    .toPromise();
 }

 //post the details of a new user 
  postLoginDetails(logindetails:Login){
    return this.http
                .post(this.url,logindetails,{headers:this.headers})
                .toPromise()
                .catch(this.handleError);
   
  }

  //post the details of member with team id
  postMemberDetails(memberdetails:ProjectMember){
    console.log(memberdetails);
    return this.http.post(this.memberUrl,memberdetails,{headers:this.headers}).toPromise().catch(this.handleError);
  }

  //handling the error
  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
   //this is to get the existing member in the project
  getUserData(projectId:number){
    return this.http.get(this.invite_url+projectId)
                    .map(Response=>Response);
  
  }

}

