import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { ReleasePlanService } from "../../shared/services/release-plan.service";
import { ReleasePlan } from "../../shared/model/release-plan";
import swal from 'sweetalert2';

@Component({
  selector: 'app-new-release-filling-details',
  templateUrl: './new-release-filling-details.component.html',
  styleUrls: ['./new-release-filling-details.component.css']
})
export class NewReleaseFillingDetailsComponent implements OnInit {
  relName: string = "";
  description: string = "";
  startDate: string = "";
  releaseDate: string = "";

  constructor(private router: Router, private releasePlanService: ReleasePlanService) { }

  release: ReleasePlan = new ReleasePlan();
  data: any;
  ngOnInit() {
  }

  //this method goes back to previous page
  backOnPrevious() {
    this.router.navigateByUrl('/app-dashboard/app-release')
  }

  //this method adds a new release
  addingNewRelease() {
    console.log("ramram", this.release.releaseDate > this.release.startDate);
    if ((this.release.releaseName == undefined)
      || (this.release.description == undefined)
      || (this.release.startDate == undefined)
      || (this.release.releaseDate == undefined)) {
      swal('PLEASE FILL ALL DETAILS', '', 'error')
    }
    else {
       swal('ADDED','','success')
      var date1 = this.release.releaseDate.split("-")
      console.log(date1[2]);
      var date2 = this.release.startDate.split("-")
      console.log(date1, date2);
      if (date2[0] <= date1[0])//year
      {
        if (date2[1] <= date1[1]) // month
        {
          console.log(date2[1]<=date1[1])
          if (date2[2] <= date1[2])
           {
            this.release.projectId = 1;
            this.releasePlanService.addingNewRelease(this.release)
            this.router.navigateByUrl('/app-dashboard/app-release')
          }
          else {
            swal('enter valid date', '', 'error') //alert for a year
          }
        }
        else {
          swal('enter valid months', '', 'error') //alert for a month
        }
      }
      else {
        swal('enter valid year', '', 'error')  //alert for a date
      } 
    } 
  }
}
