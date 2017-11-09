//import all dependency
import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Checklist } from '../model/checklist';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs'

@Injectable()
export class ChecklistService {
  //intialize class with Http service
  constructor(private http: Http) { }
  //localhost address
  getTaskUrl:string="http://localhost:52258/api/Checklist/GetTaskDetail/";
  checkListUrl = 'http://localhost:52258/api/Checklist/';
  efficiencyUrl = 'http://localhost:52258/api/Efficiency/';
  private headers = new Headers({ 'Content-Type': 'application/json' });
  //method used to add checklist
  addCheckList(checklist: Checklist) {
    console.log('i am in post', checklist);
    return this.http.post(this.checkListUrl, checklist, { headers: this.headers });

  }

  
//get the task object for particular Id
getById(id){
  return this.http.get(this.getTaskUrl+id).map((res:Response)=>res.json());
 }

  //method used to get checklist by task id
  getCheckList(id) {
    return this.http.get(this.checkListUrl + id)
      .map(Response => Response.json());
  }

  //method used to update status of checklist
  completionStatus(id, checklist) {
    return this.http
      .put(this.efficiencyUrl + id, checklist, { headers: this.headers })
      .map((Response) => Response)
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }
  //method used to delete checklist
  deleteChecklists(checklistId) {
    return this.http
      .delete(this.checkListUrl + checklistId, { headers: this.headers })
      .map((Response) => Response)
      .catch((error: any) => {
        return Observable.throw(error);
      });

  }

}
