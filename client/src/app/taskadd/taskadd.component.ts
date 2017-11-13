
import { Component, OnInit } from '@angular/core';
import { Task } from '../shared/model/task';
import swal from 'sweetalert2';
import { HubConnection } from '@aspnet/signalr-client';
import { ActivatedRoute } from '@angular/router';
import { ConfigFile } from './../shared/config';

@Component({
  selector: 'app-taskadd',
  templateUrl: './taskadd.component.html',
  styleUrls: ['./taskadd.component.css']
})
export class TaskAddComponent implements OnInit {
  //variable declaration
  sprintId:number;
  sub: string = "";
  data: any[];
  connection: HubConnection;
  userId:number=1;

  constructor(private route:ActivatedRoute) {

  }
  //this will get the task backlog list
  ngOnInit() {
    this.route.params.subscribe((param) =>{ this.sprintId = +param['id'];});
    
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);

    this.connectToHub();
  }

  //this is to make connection with the hub
  connectToHub(){
    
    this.connection = new HubConnection(ConfigFile.TaskAddUrls.connection);//for connecting with hub // when this component reload ,it will call this method
    //registering event handlers
    this.connection.on("getTasks",data =>{console.log("backlog called"); this.data = data });//this will return task backlogs
    this.connection.on("whenUpdated",data => { swal('Epic updated', '', 'success') }); //sweet alert when task happens
    this.connection.on("whenAdded",data => { swal('Epic Added', '', 'success') });   
    this.connection.start().then(() => { 
    this.connection.invoke("SetConnectionId",this.userId);
    this.connection.invoke("GetTaskBacklogs",this.sprintId);
    });
  }

  //this will add new task to the backlog
  addBacklog(taskName: string, comment: any, startDate: any, endDate: any) {
    //this will give alert if nothing is entered as task
    if ((taskName == "")) {
      swal('Task Cannot Be Empty ', '', 'warning')
    }
    //this will work if task name is entered and  add new task to backlog
    if (taskName) {
      let model = new Task(1,this.sprintId,taskName,this.userId,startDate,endDate);
      this.connection.invoke("PostTask",model,this.sprintId);
      this.connection.invoke("GetTaskBacklogs",this.sprintId);
    }
  }
  //this will uddate task backlog values
  updateBacklog(content: any, item: any, startDate: any, endDate: any) {
    //this will give alert if no task is entered for updation(empty value)
    if (content == "") {
      swal('Enter Some Task', '', 'warning')
    }
    else {
      item.taskName = content;
      item.startDate = startDate;
      item.endDate = endDate;
      //this will give alert if task is successfully upadated
      this.connection.invoke("UpdateTask",this.sprintId,item);
    }
  }
}