import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { ReleasePlan } from "../model/release-plan";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

@Injectable()
export class ReleasePlanService {

  constructor(private http:Http) { }

  options=new RequestOptions({headers: new Headers({ 'Content-Type': 'application/json'})});

  //this method adds a new release
  addingNewRelease(release: ReleasePlan) {
    return this.http.post("http://localhost:52258/api/ReleasePlan", release, this.options)
                    .toPromise().catch(this.handleError);       
  }

  //this method shows the list of all release
  getAllRelease() {
    return this.http.get("http://localhost:52258/api/ReleasePlan/GetRelease/"+1)
                    .map(response => response.json());
  }

  //this method shows the list of all sprints
  getAllSprints() {
    return this.http.get("http://localhost:52258/api/ReleasePlan/GetSprint/"+1)
                    .map(response => response.json());
  }

  //this method is for error handling
  private handleError(error:any):Promise<any>{
    console.log("An error occured",error);
    return Promise.reject(error.message || error);
  }

}
