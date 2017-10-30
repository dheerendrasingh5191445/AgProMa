import { Injectable } from '@angular/core';
import { Http,Headers,Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { TeamMaster } from "../../model/teamMaster";
import { Members } from "../../model/members";

@Injectable()
export class TeamsService{
   constructor(private http:Http){}
   private headers=new Headers({'Content-Type':'application/json'});
   /**/
   addTeam(team:TeamMaster):Promise<any>{    
     return this.http.post("http://localhost:52258/api/team",team,{headers: this.headers})
                     .toPromise()
                     .then(response => response);
   }

   getTeams(projectId:number):Promise<Response>{
     return this.http.get("http://localhost:52258/api/team/"+projectId)
                     .toPromise()
                     .then(data => data);
   }

   updateTeammembers(myMember:Members):Promise<any>{
    console.log(myMember);
    return this.http.post("http://localhost:52258/api/team/UpdateteamMember",myMember,{headers: this.headers})
                    .toPromise()
                    .then();
   }

   /*this will return request details from web api*/
   deleteMember(memberId:number):Promise<any>{
    return this.http.delete("http://localhost:52258/api/team/"+memberId,{headers: this.headers})
                    .toPromise()
                    .then(response =>response);
  }

   getAvailableList(projectId:number):Promise<Response>{
     return this.http.get("http://localhost:52258/api/team/GetAvailableMember/"+projectId)
                     .toPromise()
                     .then(data => data);
  }

  getTeamList(projectId:number){
    return this.http.get("http://localhost:52258/api/team/GetTeamMember/"+projectId)
                    .toPromise()
                    .then(data => data);
  }
   
}