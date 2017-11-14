import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  error : any;
  constructor(private route : ActivatedRoute) { }

  ngOnInit() {
<<<<<<< HEAD

=======
    console.log("In error page");
>>>>>>> e4493559c8f851a7b38e3b2ef19fcf678d7822b0
    this.route.params.subscribe(params=>this.error = params.id),{}

  }

}
