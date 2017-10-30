import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { ReleasePlanService } from "../shared/services/release-plan.service";

@Component({
  selector: 'app-release-plan',
  templateUrl: './release-plan.component.html',
  styleUrls: ['./release-plan.component.css']
})
export class ReleasePlanComponent implements OnInit {

  listTeamOne: Array<string> = [];

  constructor(private router:Router, private releasePlanService: ReleasePlanService) { }

  release:any;
  data:any;

  ngOnInit() {
    //this shows the existing release
    this.releasePlanService.getAllRelease().subscribe((release) => {
      this.release =release;
      console.log("this is from ts file ",this.data);
    })

    //this shows the list of all sprints
    this.releasePlanService.getAllSprints().subscribe((data) => {
      this.data = data;
      console.log(data);
    })
  }

  //this method is to go back on previous page
  navigateNewRelease(){
    this.router.navigateByUrl('/app-dashboard/newreleasedetail/1')
  }

}
