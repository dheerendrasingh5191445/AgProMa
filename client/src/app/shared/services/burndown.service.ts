import { Injectable } from '@angular/core';
import { Http, Headers, Response} from '@angular/http';
import 'rxjs/add/operator/map'

@Injectable()
export class BurndownService {

  constructor(private http:Http) { }
  
  private headers = new Headers({ 'Content-Type': 'application/json' });
  burndownUrl='http://localhost:52258/api/Burndown/GetTasks/';

  getTaskTimes(userId:number) {
    return this.http.get(this.burndownUrl+userId)
                      .map(Response=>Response.json());
  }

}
