import { Component, OnInit } from '@angular/core';
import{ TeamsService} from '../shared/services/teams.service';
import{ TeamMaster} from '../shared/model/teamMaster';
import { ActivatedRoute } from "@angular/router";
import { Members } from "../shared/model/members";
import swal from 'sweetalert2';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
   projectId:number = 12;
   teams:TeamMaster[];
   teamList:Members[];
   myTeamList:Members[];
   TeamList;
   val:string="";
  constructor(private teamService:TeamsService,private route:ActivatedRoute) { 
    
  }
  ngOnInit() {
      // this.route.params.subscribe(param =>this.projectId = +param['id']);
      this.teamService.getTeams(this.projectId)
                      .then(data => {console.log("team is ",data.json()),this.teams = data.json();
                                     this.teamService.getAvailableList(this.projectId)
                                                     .then(data => {console.log("data is ",data.json()),this.teamList = data.json();})})
  }

  //this will add new  team 
    addTeam(name:string){
      if(name==""){
        swal('Enter valid task name','','warning');
      }
      if(name){
        let mobject:TeamMaster = new TeamMaster(this.projectId,name);
        this.teamService.addTeam(mobject)
                        .then(data => {
                        this.teamService.getTeams(this.projectId)
                        .then(data => {this.teams = data.json();})
                        });
      }
    }

  //this will remove a particular team member
  removeMember(){
    this.teamService.getAvailableList(this.projectId)
                    .then(data => {console.log("data json",data.json()),this.teamList = data.json();}) 
                  
  }

 //this will add member to a particular team
  teamListupdate($event: any,teamId:number) {
    console.log("success");
    if(teamId){
    let teamMember:any  = $event.dragData;
    let Id:number=teamMember.memberId;
    let mobject:Members = new Members(teamId,Id);
    this.teamService.updateTeammembers(mobject)
                    .then(data =>{ swal('Member added successfully','','success');this.teamService.getTeams(this.projectId)
                                                                                                  .then(data => {this.teams = data.json();})
                                 }
                    );
    }
  }

  //this is for deleting a member from a team
  delete(id: number) {
    if (id) {
      console.log(id);
      this.teamService.deleteMember(id)
        .then(projectId => {
          swal('Member deleted successfully', '', 'success'); {
            this.teamService.getTeamList(this.projectId)
            .then(project => { this.TeamList = project.json(); })
          }
        });
    }
  }
}
