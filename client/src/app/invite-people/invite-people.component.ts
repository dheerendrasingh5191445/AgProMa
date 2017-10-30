import { Component, OnInit } from '@angular/core';
import { InvitePeopleService } from "../shared/services/invite-people.service";
import swal from 'sweetalert2';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-invite-people',
  templateUrl: './invite-people.component.html',
  styleUrls: ['./invite-people.component.css']
})
export class InvitePeopleComponent implements OnInit {
  private model={
    projectId:0,
    email:''
  };
  constructor(private invitePeople : InvitePeopleService, private route : ActivatedRoute) { }

  ngOnInit() {
     this.route.params.subscribe((param) => 
              this.model.projectId = +param['id']);
  }

  inviteMember()
  {
    this.invitePeople.emailto(this.model)
               .then(data =>  swal('E-mail Sent!','Please check your email and verify yourself','success'));
               console.log(this.model);
  }

}
