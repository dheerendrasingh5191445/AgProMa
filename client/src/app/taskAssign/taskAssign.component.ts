import { Component } from '@angular/core';
import { TaskAssignService } from "../shared/services/task-assign.service";
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { TeamMaster } from "../shared/model/teamMaster";
import { ActivatedRoute } from "@angular/router";
import { Members } from "../shared/model/members";

@Component({
    selector: 'multi',
    templateUrl: './taskAssign.component.html',
    styleUrls: ['./taskAssign.component.css']
})
export class TaskAssignComponent {
    AvailTask:TaskBackLog[]; //available task
    AvailTeam:TeamMaster[]; 
    TeamMemberList:Members[]; 
    SprintId:number;
    myId:number;
    name:string;
    data:TaskBackLog;
    constructor(private task: TaskAssignService,private route:ActivatedRoute) { } //inject TaskAssignservices
    
    ngOnInit(){
        //get the list of the tasks in a particular project
        this.task.getTaskList(2)
                 .then(data =>{ this.AvailTask = data.json();
                                this.task.getTeamList(2)
                                         .then(data =>{this.AvailTeam = data.json()});});
    }
    //get the list of the team members according to their teams in a particular project
    getTeamMember(){
        this.task.getTeamMemberList(this.myId)
                 .then(data => {this.TeamMemberList = data.json();console.log(this.TeamMemberList)})
    }
    //after adding the member to the list the list must be updated with the member name
    teamListupdate($event,id:number){  
       let teamMember:any  = $event.dragData;
       this.task.assignTask(teamMember,id) //assign task to particular members
                .then(data => {this.task.getTaskList(2)
                                        .then(data =>{ this.AvailTask = data.json();})});//return updated list in json
    }
    //method to bring member name from member id
    getName(task:TaskBackLog):string{
        return this.TeamMemberList.filter(t=>t["MemberId"]==task.PersonId)[0]["MemberName"];//filter members from member id and bring member name
     }
    
}