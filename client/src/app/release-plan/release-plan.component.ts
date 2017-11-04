import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ReleasePlanService } from "../shared/services/release-plan.service";
import { HubConnection } from "@aspnet/signalr-client/dist/src";
@Component({
  selector: 'app-release-plan',
  templateUrl: './release-plan.component.html',
  styleUrls: ['./release-plan.component.css']
})
export class ReleasePlanComponent implements OnInit {
  listTeamOne: Array<string> = [];
  constructor(private router:Router, private releasePlanService: ReleasePlanService,private route:ActivatedRoute) { }
  projectId:any;
  release:any;
  data:any;
  connection:HubConnection;
  connectReleasePlanHub() {
    this.connection = new HubConnection("http://localhost:52258/releaseplan");
    this.connection.on("getreleaseplans", data => { this.release = data });
    this.connection.on("getsprints", sprint =>this.data=sprint);
    this.connection.start().then(() => {
      this.connection.invoke("SetConnectionId",3);
      this.connection.invoke("GetReleasePlans",this.projectId)
      .then(()=>this.connection.invoke("GetAllSprints",this.projectId));
    
    });
  }

  ngOnInit() {
    this.route.params.subscribe((param) => 
    this.projectId = +param['id']);               //getting project id from route
    
this.connectReleasePlanHub();
  }
  //this method is to go back on previous page
  navigateNewRelease(){
    this.router.navigateByUrl('/app-dashboard/newreleasedetail/1')
  }
}