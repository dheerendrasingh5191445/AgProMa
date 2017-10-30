import { Component, OnInit } from '@angular/core';
import { ProjectMaster } from './../shared/model/ProjectMaster';
import { ProjectScreenService } from "./project-screen.service";
import { Router} from  '@angular/router';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-project-screen',
  templateUrl: './project-screen.component.html',
  styleUrls: ['./project-screen.component.css']
})
export class ProjectScreenComponent implements OnInit {
  //variable that are used to manipulate the value in project
  projects:any=Array<ProjectMaster>();
  connection:HubConnection;
  userId:number;
  constructor(private projectscrservice:ProjectScreenService,private router:Router) { }

  ngOnInit() {
    //this is to fetch the list of project of particular employee
      var session = sessionStorage.getItem("id");
      console.log(session);
      this.userId = parseInt(session);
      this.projectscrservice.getAllProjectOfEmployee(this.userId)
                            .subscribe(data =>{this.projects = data.json();console.log(this.projects)});
  }
  //this is splice the list
  onDelete(Id:number)
  {
    var ele = this.projects.find(f => f.Id == Id);
    const index = this.projects.indexOf(ele);
    this.projects.splice(index, 1);
  }
  // example()
  // {
  //   this.connection =new HubConnection("apiconnectionurl/promaster");//for connecting with hub
  //   //registering event handlers
  //   this.connection.on("addproject",  => console.log("getting data sent by the server"))
  // }
  // this.connection.start().then(() => this.connection.invoke("servermethod",parameter));
}
