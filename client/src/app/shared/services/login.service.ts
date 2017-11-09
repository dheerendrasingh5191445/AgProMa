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
import { Credential } from "../model/credential";

@Injectable()
export class LoginService {

  constructor(private http: Http) { }
  url = 'http://localhost:52258/api/Login';                 //url for login 
  memberUrl='http://localhost:52258/api/ProjectMember';     //url for project members 
  invite_url='http://localhost:52258/api/InviteMembers/';
  checkurl='http://localhost:52258/api/Login/Check';
  updateUrl='http://localhost:52258/api/Login/Details/';
  updatePasswordUrl='http://localhost:52258/api/Login/UpdatePassword/';
//  private headers = new Headers({ 'Content-Type': 'application/json' });
 // checkurl='http://localhost:52258/api/Login/Check';
  logouturl="http://localhost:52258/api/Login/SetLogOut/";
 

  token = sessionStorage.getItem("token");
  headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.token });
  options = new RequestOptions({ headers: this.headers });
 
 //get all the details of user
 getAll(){
   return this.http
   .get(this.url)
   .map((response)=>response.json());
 }
//this function is to logout from the system
 logOut(userId:number){
  return this.http.post(this.logouturl+userId,"",this.options)
                  .map((response)=>response.json())
                  .toPromise()
                  .catch(this.handleError);
 }
  //get the userid on basis of email
  get(email:string){
    return this.http.get(this.url+"/"+email)
                    .map(data => data);
  }

 //check details of a particular user by emailid and password
  check(userData:IdPassword){
   return this.http
              .post(this.checkurl,userData,this.options)
              .map((response)=>response.json())
              .toPromise()
              .catch(this.handleError);

 }

  //get userdata by id for view profile
  getById(id:any){
    return this.http.get(this.updateUrl+id,this.options)
                    .map(data=>data.json());
  }

  //this method is used to get the token
  getToken(auth:Credential)
  {
    let headers = new Headers({ 'Content-Type': 'application/json'});
    let options = new RequestOptions({ headers: this.headers });
    return this.http.post("http://localhost:59382/api/TokenGeneration/createtoken",auth,options)
                      .toPromise();
  }

  //post the details of a new user 
    postLoginDetails(logindetails:Login){
      return this.http
                  .post(this.url,logindetails,this.options)
                  .toPromise()
                  .catch(this.handleError);
    
    }

    //post the details of member with team id
    postMemberDetails(memberdetails:ProjectMember){
      console.log(memberdetails);
      return this.http.post(this.memberUrl,memberdetails,this.options).toPromise().catch(this.handleError);
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
    //update details of a user
    updatePassword(id:number,user:any) {
      this.http.put(this.updatePasswordUrl+id,user,{headers:this.headers}).subscribe();
    }
  }
