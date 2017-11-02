import { Component, OnInit } from '@angular/core';
import { BacklogService } from '../shared/services/backlog.service';
import { ProductBacklog } from '../shared/model/productBacklog';
import swal from 'sweetalert2';
import { EpicService } from "../shared/services/epic.service";
import { Epic } from "../shared/model/epic";
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-epic',
  templateUrl: './epic.component.html',
  styleUrls: ['./epic.component.css']
})
export class EpicComponent implements OnInit {

  projectId: number = 1;
  connection: HubConnection;
  data: Array<any>
  userId:number;

  model = new Epic(null, ''); //model for adding new epic
  constructor(private epicService: EpicService) { }

  ngOnInit() {
    this.getBacklog(); // when this component reload ,it will call this method
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    this.connection = new HubConnection("http://localhost:52258/epichub");//for connecting with hub
    this.connection.on("addproject",data  =>{});  //registering event handlers
    this.connection.start().then(() => {
    console.log("donebabyvjdhvkjfdvhkfdvd");
    this.connection.invoke("SetConnectId",this.userId);
    });
  }

addBacklog(story:any, comment:any)//for adding new  epic
{
  if (story == "") {
    swal('Please fill user story','','error');
  } else {
    this.model.description = story;
    this.model.projectId = 1;
    console.log("the value of model", this.model);
    this.epicService.add(this.model)
      .subscribe((f) => {
        swal('Epic Added', '', 'success');
        this.getBacklog();
      });
  }
}
updateBacklog(content:any, item) //for updating  particular epic 
{
  if (content == "") {
    swal('Please fill user story', '', 'error');
  }
  item.description = content;

  console.log("item value is ", item.storyId);
  this.epicService.update(item.epicId, item).subscribe(
    f => { swal('Epic updated', '', 'success'); }
  );
}
getBacklog()//for gettting all epics based on project id 
{

  this.epicService.get(this.projectId).subscribe(data => {
    this.data = data;
    console.log("the vaue of data", data);
  });
}
deleteBacklog(item:any){
  this.epicService.delete(item.epicId).subscribe((f) => {
    swal('Epic deleted', '', 'success');
    this.getBacklog();
  });
}
  
  }