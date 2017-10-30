import { Component, OnInit } from '@angular/core';
import { routerTransition } from './../router.animations';
import { AuthService } from 'angular4-social-login';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  animations: [routerTransition()]
})
export class DashboardComponent implements OnInit {
  pushRightClass: string = 'push-right';
  session: string;
  constructor(private authService: AuthService) { }
  ngOnInit() {
  }
  toggleSidebar() {
    const dom: any = document.querySelector('body');
    dom.classList.toggle(this.pushRightClass);
  }
  
  signOut(): void {
    this.authService.signOut();
  }

}