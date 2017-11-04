import { Component, OnInit } from '@angular/core';
import { SprintService } from '../shared/services/sprint.service';
import { Sprint } from '../shared/model/sprint';
import { BacklogService } from '../shared/services/backlog.service';
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { ActivatedRoute } from "@angular/router";


@Component({
  selector: 'app-sprint',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.css']
})
export class SprintComponent implements OnInit {
  //variable declaration
  userId: number;
  projectId: number;
  sprints: Array<any>;
  backlogs: any[];
  unAssignedBacklogs: Array<any>;
  connection: HubConnection;
  newdata: any;


  newsprint = new Sprint(null, '', '', null, null, null);


  constructor(private sprintService: SprintService, private backlogService: BacklogService, private route: ActivatedRoute) { }

  ngOnInit() {
    //getting member Id from session
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);

    //get all the sprints according to the project id.
    this.connectBacklogHub();
  }
  //this is for connection to hub socket
  connectBacklogHub() {
    this.connection = new HubConnection("http://localhost:52258/sprint");
    this.connection.on("getSprints",data => { console.log(data);this.sprints = data });
    this.connection.on("getBacklogs",data => { console.log(data); this.backlogs = data });
    this.connection.on("postSprints",data => { console.log(data); this.sprints.push(data); });//get only unassigned user story.
    this.connection.start().then(() => {
      this.connection.invoke("SetConnectionId", 1); //member Id
      this.connection.invoke("GetSprints", this.projectId);
      this.connection.invoke("GetAllBacklogs", this.projectId);
    });
  }

  //compare whether story exist in sprint or not
  compareStory(sprintId, inSprintNo) {
    if (sprintId == inSprintNo) {
      return true;          //sprint are available for that particular sprint.
    }
    else {
      return false;         //sprint are not available for particular sprint.
    }
  }


  updateStoryInSprint($event, sprintNo: number) {
    //assign task to particular members
    console.log("successs");
    let storyData: any = $event.dragData;
    console.log($event.dragData);
    this.connection.invoke("UpdateStoryInSprint",storyData,sprintNo, this.projectId);
  }


  //it will add sprint in the sprint container
  onSaveSprint() {
    this.newsprint.status = false;
    this.newsprint.projectId = this.projectId;
    this.connection.invoke("AddSprint", this.newsprint);
  }
}