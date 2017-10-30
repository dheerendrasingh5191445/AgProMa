import { Component, OnInit } from '@angular/core';
import { SprintService } from '../shared/services/sprint.service';
import { Sprint } from '../shared/model/sprint';
import { BacklogService } from '../shared/services/backlog.service';


@Component({
  selector: 'app-sprint',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.css']
})
export class SprintComponent implements OnInit {

  projectId: number=1;
  sprints: Array<any>;
  backlogs: any[];

  newsprint = new Sprint( null ,'','',null,null,null);
  listBoxers: Array<string> = ['Sugar Ray Robinson', 'Muhammad Ali', 'George Foreman', 'Joe Frazier', 'Jake LaMotta', 'Joe Louis', 'Jack Dempsey', 'Rocky Marciano', 'Mike Tyson', 'Oscar De La Hoya'];
  listTeamOne: Array<string> = ['George Foreman', 'Joe Frazier', 'Jake LaMotta'];
  listTeamTwo: Array<string> = ['Sugar Ray Robinson', 'Muhammad Ali',];

  constructor(private sprintService: SprintService, private backlogService: BacklogService) {  }

  ngOnInit() {
    //get all the sprints according to the project id.
    this.sprintService.getSprints(this.projectId)
      .subscribe(sprints =>{ 
        this.sprints=sprints;
      });

    //get all the project backlogs(user story) specific to projectid
    this.backlogService.getBacklogs(this.projectId)
      .subscribe(backlogs=>this.backlogs);
  }

  //it will add sprint in the sprint container
  onSaveSprint() {
    this.newsprint.status=false;
    this.newsprint.projectId=this.projectId;
    this.sprintService.onSave(this.newsprint);
  }
}
