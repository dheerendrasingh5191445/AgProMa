import { Component, OnInit,Input } from '@angular/core';
import{ Members } from './../../shared/model/members';
import{ TeamMaster } from './../../shared/model/teamMaster';
import { TeamsService } from "../../shared/services/teams.service";

@Component({
  selector: 'app-teammember',
  templateUrl: './teammember.component.html',
  styleUrls: ['./teammember.component.css']
})
export class TeammemberComponent implements OnInit {
@Input() Data:number;
TeamList:Members[];
  constructor(private teamService:TeamsService) { }

  ngOnInit() {
    this.teamService.getTeamList(this.Data)
                    .then(data => {this.TeamList = data.json();})
    }
    //this is to delete the member from a team
   delete(id:number)
   {
     console.log(id);
     this.teamService.deleteMember(id)
                     .then(data => {this.teamService.getTeamList(this.Data)
                                                    .then(data => {this.TeamList = data.json();})
                                                  });
   }
}
