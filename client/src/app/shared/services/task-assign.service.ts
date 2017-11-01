import { Injectable } from '@angular/core';
import { Http , Response , Headers }  from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { Members } from "./../model/members";

@Injectable()
export class TaskAssignService {

  constructor(private http : Http) { } //inject http 
 //get the list of tasks accordingly their particular sprint
  getTaskList(id:number):Promise<any>{
    return this.http.get("http://localhost:52257/api/TaskBacklog/GetTask/"+id)
                    .toPromise()
                    .then(Response => Response);//return the response

  }
//get the list of teams in the particular project by its projectId
  getTeamList(id:number):Promise<any>{
    return this.http.get("http://localhost:52257/api/TaskBacklog/GetByTeamId/"+id)
                    .toPromise()
                    .then(Response => Response);

  }
//get the list of members in a particular team for assigning task
  getTeamMemberList(id:number):Promise<any>{
    return this.http.get("http://localhost:52257/api/TaskBacklog/GetTeamMember/"+id)
                    .toPromise()
                    .then(Response => Response);

  }
//update methos that updates with the values of the members assigned to a particular task
  assignTask(object:Members,id:number):Promise<any>{
    return this.http.put("http://localhost:52257/api/TaskBacklog/"+id,object,{headers: new Headers({ 'Content-Type': 'application/json'})})
                    .toPromise();
  }
//get the name of the members that are assigned to a particular task
  getName(id:number){
       return this.http.get("http://localhost:52257/api/TaskBacklog/GetMemberName/"+id)
                   .map(Response => Response);
  }
}
