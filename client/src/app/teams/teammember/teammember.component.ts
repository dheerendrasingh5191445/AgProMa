import { Component, OnInit,Input } from '@angular/core';
import{ Members } from '../../shared/model/members';
import{ TeamMaster } from '../../shared/model/teamMaster';
import { TeamsService } from "../../shared/services/teams.service";
import swal from 'sweetalert2';


@Component({
  selector: 'app-teammember',
  templateUrl: './teammember.component.html',
  styleUrls: ['./teammember.component.css']
})
export class TeammemberComponent implements OnInit {
@Input() Data:number;
TeamList:Members[];
  constructor(private teamService:TeamsService) { }

  //this will invoke at component loading time and will get the list of teams
  ngOnInit() {
    this.teamService.getTeamList(this.Data)
                    .then(data => {this.TeamList = data.json();})
    }
    //this is to delete the member from a team
   delete(id:number)
   {
     if(id){
     console.log(id);
     this.teamService.deleteMember(id)
                     .then(data =>  {swal('Member deleted successfully','','success');  {this.teamService.getTeamList(this.Data)
                                                    .then(data => {this.TeamList = data.json();})
                                                  }});
   }
   
  }
}
