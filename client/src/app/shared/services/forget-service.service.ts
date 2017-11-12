import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { ConfigFile } from "../config";

@Injectable()
export class ForgetServiceService {

  constructor(private http:Http) { }

  emailto(emailId : string) : Promise<any>
  {
  	//this method will get a string(email) and call api with that parameter
    let headers=new Headers({ 'Content-Type': 'application/json' });
    let options=new RequestOptions({headers:headers});
    return this.http.post(ConfigFile.ForgetServiceUrl.forgeturl+emailId,null,options)
    .toPromise();
  }
}
