import { Component, OnInit } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { LoginService } from '../shared/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-leader',
  templateUrl: './dashboard-leader.component.html',
  styleUrls: ['./dashboard-leader.component.scss']
})
export class DashboardLeaderComponent implements OnInit {
  pushRightClass: string = 'push-right';
  session: string;
  isvalid:string;
  userId:number;

  constructor(private authService: AuthService,private loginservice: LoginService,private router:Router) { }
  ngOnInit() {
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
  }
  toggleSidebar() {
    const dom: any = document.querySelector('body');
    dom.classList.toggle(this.pushRightClass);
  }

  onLoggedout():void{
   // this.authService.signOut();
    this.loginservice.logOut(this.userId)
                     .then(data => {this.router.navigate(["app-signup"]);})
  }
}
