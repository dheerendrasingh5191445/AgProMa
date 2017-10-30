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
  unAssignedBacklogs:Array<any>;

  newsprint = new Sprint( null ,'','',null,null,null);

  listBoxers: Array<string> = ['Sugar Ray Robinson', 'Muhammad Ali', 'George Foreman', 'Joe Frazier', 'Jake LaMotta', 'Joe Louis', 'Jack Dempsey', 'Rocky Marciano', 'Mike Tyson', 'Oscar De La Hoya'];
  listTeamOne: Array<string> = ['George Foreman', 'Joe Frazier', 'Jake LaMotta'];
  listTeamTwo: Array<string> = ['Sugar Ray Robinson', 'Muhammad Ali',];

  constructor(private sprintService: SprintService, private backlogService: BacklogService) {  }

  ngOnInit() {
    //get all the sprints according to the project id.
    this.getSprints();

    //get only unassigned user story.
    this.getUnassignedStory();

    //get all the project backlogs(user story) specific to projectid
    this.getAllBacklogs();
  }

  //get all the sprints according to the project id.
  getSprints(){
    this.sprintService.getSprints(this.projectId)
    .subscribe(sprints =>{this.sprints=sprints ,console.log(this.sprints)});
  }

  //get only unassigned user story.
  getUnassignedStory(){
    this.backlogService.getBacklogs(this.projectId)
    .subscribe(data=>{this.unAssignedBacklogs=data,console.log(this.unAssignedBacklogs)});
  }

  //get all user story specific to projectId
  getAllBacklogs() {
    this.backlogService.getUserStoryByProjectId(this.projectId)
    .subscribe(backlogs=>this.backlogs=backlogs)
  }

  compareStory(sprintId,inSprintNo){
    if(sprintId == inSprintNo) {
      return true;          //sprint are available for that particular sprint.
    }
    else {
      return false;         //sprint are not available for particular sprint.
    }
  }
  updateSprint($event,Id:number){
    console.log("successs");
    let storyData:any  = $event.dragData;
    console.log(storyData);
    this.sprintService.updateSprint(storyData,Id) //assign task to particular members
             .subscribe(data => {this.getAllBacklogs()});

  }
  //it will add sprint in the sprint container
  onSaveSprint() {
    this.newsprint.status=false;
    this.newsprint.projectId=this.projectId;
    this.sprintService.onSave(this.newsprint)
    .subscribe((call)=>this.getSprints());
  }
}
