import { Injectable } from '@angular/core'
import { Http, Headers, Response,RequestOptions } from '@angular/http';
import { Login } from '../model/login';
import { Observable } from "rxjs";
import { ProjectMember } from "../model/projectMember";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class LoginService {

  constructor(private http: Http) { }
  url = 'http://localhost:52258/api/Login';
  memberUrl='http://localhost:52258/api/ProjectMember';
 
  private headers = new Headers({ 'Content-Type': 'application/json' });
 // options=new RequestOptions({headers:this.headers});
 
 getAll(){
   return this.http
   .get(this.url)
   .map((response)=>response.json());
 }
 get(email:string){
   return this.http
   .get(this.url + '/' + email)
   .map((response)=>response.json())
   .toPromise()
   .catch(this.handleError);

 }
  postLoginDetails(logindetails:Login){
    return this.http
    .post(this.url,logindetails,{headers:this.headers}).toPromise().catch(this.handleError);
   
  }
  postMemberDetails(memberdetails:ProjectMember){
    console.log(memberdetails);
    return this.http.post(this.memberUrl,memberdetails,{headers:this.headers}).toPromise().catch(this.handleError);
  }
  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }


}

