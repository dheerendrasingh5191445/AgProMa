import { Component, OnInit } from '@angular/core';
import { BacklogService } from '../shared/services/backlog.service';
import { ProductBacklog } from '../shared/model/productBacklog';
import swal from 'sweetalert2';
import { EpicService } from "../shared/services/epic.service";
import { Epic } from "../shared/model/epic";
import { HubConnection } from '@aspnet/signalr-client';
import { ActivatedRoute } from '@angular/router';
import { ConfigFile } from '../shared/config';

@Component({
  selector: 'app-epic',
  templateUrl: './epic.component.html',
  styleUrls: ['./epic.component.css']
})
export class EpicComponent implements OnInit {

  projectId: number;
  connection: HubConnection;
  data: Array<any> //all epics will store in this variable
  userId:number;

  model = new Epic(null, ''); //model for adding new epic
  constructor(private epicService: EpicService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe((param) =>
    this.projectId = +param['id']);
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    this.connection = new HubConnection(ConfigFile.EpicUrls.connection);//for connecting with hub // when this component reload ,it will call this method
    //registering event handlers
    this.connection.on("getBacklog",data =>{console.log("backlog called"); this.data = data }); //for gettting all epics based on project id
    this.connection.on("whenDeleted",data => { swal('Epic deleted', '', 'success') });  //sweet alerts
    this.connection.on("whenUpdated",data => { swal('Epic updated', '', 'success') }); 
    this.connection.on("whenAdded",data => { swal('Epic Added', '', 'success') });   
    this.connection.start().then(() => { 
    this.connection.invoke("SetConnectId",this.userId);
    this.connection.invoke("Get",this.projectId);
    });
  }

  addBacklog(story:any, comment:any)//for adding new  epic
  {
    if (story == "") {
      swal('Please fill user story','','error');
    } 
    else {
      this.model.description = story;
      this.model.projectId = this.projectId;
      this.connection.invoke("Post",this.model);
      this.connection.invoke("Get",this.projectId);
    }
  }

  updateBacklog(content:any, item){ //for updating  particular epic 
    if (content == "") {
      swal('Please fill user story', '', 'error');
    }
    else{
      item.description = content;
      this.connection.invoke("put",item.epicId,item);
    } 
  }

  deleteBacklog(item:any){       //for deleting the epic 
    this.connection.invoke("Delete",item.epicId);
    this.connection.invoke("Get",this.projectId);
  }
}