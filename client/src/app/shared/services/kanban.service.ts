import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

@Injectable()
export class KanbanService {

  constructor(private http : Http) { }

  url = 'http://localhost:52258/api/TaskBacklog/GetAllTaskDetail/'; 

  getTaskDetail(sprintID : number)
  {
    return this.http
    .get(this.url+ sprintID)
    .map((response)=>response.json());
  }

}
