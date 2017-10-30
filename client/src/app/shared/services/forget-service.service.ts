import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

@Injectable()
export class ForgetServiceService {

  constructor(private http:Http) { }

  emailto(emailId : string) : Promise<any>
  {
    let headers=new Headers({ 'Content-Type': 'application/json' });
    let options=new RequestOptions({headers:headers});
    return this.http.post("http://localhost:52258/api/forgetpassword?email="+emailId,null,options)
    .toPromise();
  }
}
