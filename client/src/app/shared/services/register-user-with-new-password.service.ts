import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions,Response } from '@angular/http';

import 'rxjs/add/operator/map';
import { Master } from "./../model/master";


@Injectable()
export class RegisterUserWithNewPasswordService {

  constructor(private http:Http) { }

  getUserDetails(id : string)
  {
    return this.http.get("http://localhost:52258/api/Master/"+id)
                    .map(Response =>Response);
  }

  updatePassword(userDetail : Master):Promise<any>
  {
    return this.http.put('http://localhost:52258/api/Master/'+ userDetail.id,userDetail,{headers: new Headers({'Content-Type':'application/json'})})
                    .toPromise();
  }

}
