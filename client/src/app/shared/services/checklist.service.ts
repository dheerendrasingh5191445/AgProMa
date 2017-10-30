import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import{Checklist} from '../model/checklist';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs'

@Injectable()
export class ChecklistService {

  constructor(private http: Http) { }
  
    private checkListUrl = 'http://localhost:52258/api/Checklist/';
    private headers = new Headers({ 'Content-Type': 'application/json' });
    addCheckList(checklist:Checklist) {
      console.log('i am in post',checklist);
          return this.http.post(this.checkListUrl, checklist, { headers: this.headers });
      
        }
        getCheckList() {
          console.log('i am in service using getCheckList');
          return this.http.get(this.checkListUrl,{ headers: this.headers })
                          .map(Response => Response.json());
        }
        completionStatus(id,checklist){
          console.log("hhhhhhhhhhhhhhhhhhhhhhhhh",id,checklist)
          return this.http
          .put(this.checkListUrl+'/'+id,checklist,{headers: this.headers})
          .map((Response)=>Response.json())
          .catch((error:any)=>{
            return Observable.throw(error);
          });
        }
        deleteChecklists(checklistId){
          return this.http
          .delete(this.checkListUrl+checklistId,{headers:this.headers})
          .map((Response)=>Response.json())
          .catch((error:any)=>{
        return Observable.throw(error);
      });
      
        }

}
