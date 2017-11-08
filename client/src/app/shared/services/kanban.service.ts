import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

@Injectable()
export class KanbanService {

  constructor(private http : Http) { }
  token= sessionStorage.getItem("token");
  headers = new Headers({'Content-Type':'application/json','Authorization':'Bearer '+this.token});
  options = new RequestOptions({ headers: this.headers});
  //local variable used for storing path which is used to hit API
  url = 'http://localhost:52258/api/TaskBacklog/GetAllTaskDetail/'; 


  getTaskDetail(sprintID : number)
  {
    //This method will get the details for kanban
    return this.http
               .get(this.url+ sprintID,this.options)
               .map((response)=>response.json());
  }

}
