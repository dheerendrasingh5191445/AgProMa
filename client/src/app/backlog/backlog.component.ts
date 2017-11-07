import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection } from "@aspnet/signalr-client";
import swal from 'sweetalert2';
import { ProductBacklog } from '../shared/model/productBacklog';
import { ConfigFile } from './../shared/config';
@Component({
  selector: 'app-backlog',
  templateUrl: './backlog.component.html',
  styleUrls: ['./backlog.component.css']
})
export class BacklogComponent implements OnInit {
  projectId: number;
  stories: Array<ProductBacklog>;
  userId: number;
  length:number;
  model: ProductBacklog = new ProductBacklog(null, '', '', null, null);  //model for adding new user story
  connection: HubConnection;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    //get the id for the user.
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);

    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);

    //register and invoke connection
    this.connectBacklogHub();
  }

  connectBacklogHub() {
    this.connection = new HubConnection('http://localhost:52258/backlog');
    //register to get Backlogs from the backend.
    this.connection.on("getbacklog", backlogs => {
      this.stories = backlogs;
      this.length=this.stories.length;
      this.stories.sort(function (a, b) {
        return a.priority - b.priority;
      });
    });

    //get the added backlog return data with socket
    this.connection.on("postBacklog", data => {
      console.log(data);
      this.stories.push(data);
    });

    //get the updated backlog
    this.connection.on("updateBacklog", backlog => {
    });

    //get the deleted backlog items
    this.connection.on("deleteBacklog", storyId => {
    });

    //establish the socket connection
    this.connection.start().then(() => {

      //set connection id and update status and get notified for changes
      this.connection.invoke("setConnectionId", this.userId); //member ID need to be passed

      //invoke the get backlog method
      this.connection.invoke("GetBacklog", this.projectId);
    });
  }

  //Add a new user story
  addBacklog(story: any, comment: any, priority) {
    if (story == "") {
      swal('Please fill user story', '', 'error');
    } else {
       this.model.storyName = story;
        this.model.comments = comment
        this.model.projectId = this.projectId;
        this.model.status = false;
        this.model.priority = priority;
        //invoke backend post method
        this.connection.invoke("PostBacklog", this.model)
                      .then(data=>{swal('User story deleted', '', 'success');
      
                      });
        this.connection.invoke("GetBacklog", this.projectId)
                      .then(data=>{this.stories.sort(function (a, b) {
                        return a.priority - b.priority;
                        });
                      });
      }
  }

  //for updating a particular user story
  updateBacklog(content: any, comment: any, priority: any, item) {
    if (content == "") {
      swal('Please fill user story', '', 'error');
    }
    else {
      item.storyName = content;
      item.comments = comment;
      item.priority = priority;
      this.connection.invoke("UpdateBacklog", item.storyId, item)
                    .then(data=>{swal('User story updated', '', 'success')});
      this.connection.invoke("GetBacklog", this.projectId)
                    .then(data=>{this.stories.sort(function (a, b) {
                      return a.priority - b.priority;
                      });
                    });
    }
  }

  //delete a backlog
  deleteBacklog(item: any) {
    this.connection.invoke("DeleteBacklog", item.storyId, this.projectId)
                    .then(data=>{ swal('User story deleted', '', 'success')});
    this.connection.invoke("GetBacklog", this.projectId)
                    .then(data=>{ this.stories.sort(function (a, b) {
                       return a.priority - b.priority;
                    });});
  }
}