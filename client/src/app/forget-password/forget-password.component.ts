import { Component, OnInit } from '@angular/core';

import swal from 'sweetalert2';
import { ForgetServiceService } from "../shared/services/forget-service.service";


@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {

  forgetData : any;
  email:string;
  constructor( private forget : ForgetServiceService ) { }

  ngOnInit() {
 
  }

  generateEmail()
  {
      this.forget.emailto(this.email)
               .then(data => 
                {
                  if(data.json() == true)
                { 
                  swal('E-mail Sent!','Please check your email and verify yourself','success')
                } 
                else{
                  swal('E-mail Not Register!','Sorry! Email is not Register with us','error')
                }
                });
               
  }
}
