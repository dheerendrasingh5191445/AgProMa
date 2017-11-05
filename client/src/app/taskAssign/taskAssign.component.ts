import { Component } from '@angular/core';
import { TaskAssignService } from "../shared/services/task-assign.service";
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { TeamMaster } from "../shared/model/teamMaster";
import { ActivatedRoute } from "@angular/router";
import { Members } from "../shared/model/members";
import { HubConnection } from '@aspnet/signalr-client';
import swal from 'sweetalert2';

@Component({
    selector: 'multi',
    templateUrl: './taskAssign.component.html',
    styleUrls: ['./taskAssign.component.css']
})
export class TaskAssignComponent {
    //available task
    AvailTask:TaskBackLog[]; 
    AvailTeam:TeamMaster[]; 
    TeamMemberList:Members[];
    sprintId:number;
    projectId:number;
    userId:number;
    myId:number;
    name:string;
    data:TaskBackLog;
    connection:HubConnection;

    constructor(private task: TaskAssignService,private route:ActivatedRoute) { } //inject TaskAssignservices
    
    ngOnInit(){
        this.route.params.subscribe(param =>this.projectId = +param['id']);
        var session = sessionStorage.getItem("id");
        this.userId = parseInt(session);

        //this is call to connect the hub
        this.connectToHub();
    }



    //this is to set connection from hub
    connectToHub(){
        // for connecting with hub 
        this.connection = new HubConnection("http://localhost:52258/taskbacklog");
        // when this component reload ,it will call this method
        // registering event handlers
        this.connection.on("getAllTaskDetail",data =>{ this.AvailTask = data; });//this will return list of teams
        this.connection.on("getTeamList",data =>{ this.AvailTeam = data;  });//this will return list of available member
        this.connection.on("getTeamMember",data => { this.TeamMemberList = data;})
        //sweet alert when task happens following 3
        this.connection.on("whenUpdated",data => { swal('Member Added', '', 'success') }); 
        this.connection.on("whenAdded",data => { swal('Team Added', '', 'success') });
        this.connection.on("whenDeleted",data => { swal('Member Removed', '', 'success') });   
        this.connection.start().then(() => { 
        this.connection.invoke("SetConnectionId",this.userId);
        this.connection.invoke("GetAllTaskDetail",this.sprintId);//get the list of the tasks in a particular project
        this.connection.invoke("GetTeamList",this.projectId);
        });
      }



    //get the list of the team members according to their teams in a particular project
    getTeamMember(){
        this.connection.invoke("GetTeamMember",this.myId);
    }


    //after adding the member to the list the list must be updated with the member name
    teamListupdate($event,id:number){  
       let teamMember:any  = $event.dragData;
       this.connection.invoke("AssignTask",id,teamMember)
                      .then(data => {  })
       this.task.assignTask(teamMember,id) //assign task to particular members
                .then(data => {this.task.getTaskList(2)
                                        .then(data =>{ this.AvailTask = data.json();})});//return updated list in json
    }
    //method to bring member name from member id
    getName(task:TaskBackLog):string{
        return this.TeamMemberList.filter(t=>t["MemberId"]==task.PersonId)[0]["MemberName"];//filter members from member id and bring member name
     }
    
}