import { Component, OnInit } from '@angular/core';
import{ TeamsService} from '../shared/services/teams.service';
import{ TeamMaster} from './../shared/model/teamMaster';
import { ActivatedRoute } from "@angular/router";
import { Members } from "./../shared/model/members";

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
   ProjectId:number;
   Teams:TeamMaster[];
   TeamList:Members[];
   MyTeamList:Members[];
  constructor(private teamService:TeamsService,private route:ActivatedRoute) { 
    
  }
  ngOnInit() {
      this.route.params.subscribe(param =>this.ProjectId = +param['id']);
      this.teamService.getTeams(this.ProjectId)
                      .then(data => {this.Teams = data.json();
                                     this.teamService.getAvailableList(this.ProjectId)
                                                     .then(data => {console.log(data.json()),this.TeamList = data.json();})})
  }

  //this will add new  team 
    addTeam(name:string){
     let mobject:TeamMaster = new TeamMaster(this.ProjectId,name);
     this.teamService.addTeam(mobject)
                      .then(data => {
                        this.teamService.getTeams(this.ProjectId)
                        .then(data => {this.Teams = data.json();})
                      });
  }

  //this will remove a particular team member
  removeMember(){
    this.teamService.getAvailableList(this.ProjectId)
                    .then(data => {console.log(data.json()),this.TeamList = data.json();})  
  }

 //this will add member to a particular team
  teamListupdate($event: any,teamId:number) {
    console.log("success");
    let teamMember:any  = $event.dragData;
    let Id:number=teamMember.memberId;
    let mobject:Members = new Members(teamId,Id);
    this.teamService.updateTeammembers(mobject)
                    .then(data =>{this.teamService.getTeams(this.ProjectId)
                                                  .then(data => {this.Teams = data.json();})});
}
}
