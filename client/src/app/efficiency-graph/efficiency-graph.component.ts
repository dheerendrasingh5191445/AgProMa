import { Component, OnInit } from '@angular/core';
import { EfficiencyGraphService } from "../shared/services/efficiency-graph.service";
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'efficiency',
  templateUrl: './efficiency-graph.component.html',
  styleUrls: ['./efficiency-graph.component.css']
})
export class EfficiencyGraphComponent implements OnInit {

  //local variable used in component
  
  data : any;
  taskId : number =20;
  efficient : number  ;

  //Initializing variable for doughnut chaty
  doughnutChartLabels:string[]=['Efficient', 'Remaining']; //setting names on graph
  doughnutChartData:number[]=[this.data, this.efficient ]; //setting the data to graph
  doughnutChartType:string="doughnut"; //defining type of chart 

  constructor(private efficiencyGraphService : EfficiencyGraphService, private route : ActivatedRoute) {
    
   }

  ngOnInit() {

    // getting task id from route 
    // this.route.params.subscribe((param) =>
    // this.taskId = +param['id']);

    //this will get the data from 
    this.efficiencyGraphService.getEfficiencyDetail(this.taskId)
                               .subscribe(data => {this.data = data; console.log("init",this.data);

                               //logic for douhgnut chart
                               this.efficient=100 -this.data;
                               this.doughnutChartData = [  this.data, this.efficient];
                              });                              
  }


  // event on click to graph
   public chartClicked(e:any):void {
  }

 // event on hovering on chart
 public chartHovered(e:any):void {
 }

}

