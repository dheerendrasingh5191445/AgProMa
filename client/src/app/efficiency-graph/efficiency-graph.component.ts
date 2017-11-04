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
  total : number = 100;
  //notEfficient : number = 20;
  
  data : any;
  taskId : number =20;
  doughnutChartLabels:string[]=['Work Done', 'Remaining'];
  efficient : number  = 100 - this.data;
  doughnutChartData:number[]=[this.data, this.total ];
  doughnutChartType:string="doughnut";

  constructor(private efficiencyGraphService : EfficiencyGraphService, private route : ActivatedRoute) {
    
   }

  ngOnInit() {

    // getting task id from route 
    // this.route.params.subscribe((param) =>
    // this.taskId = +param['id']);

    //this will get the data from 
    this.efficiencyGraphService.getEfficiencyDetail(this.taskId)
                               .subscribe(data => {this.data = data; console.log("init",this.data);
                               this.efficient=this.data;
                               this.doughnutChartLabels = ['Work Done', 'Remaining'];
                               this.doughnutChartData = [ this.efficient, this.data];
                               this.doughnutChartType = 'doughnut'; 
                              });
        
                                                             
  }


 // Doughnut
//  public doughnutChartLabels:string[] = ['Work Done', 'Remaining'];
//  public doughnutChartData:number[] = [ this.efficient, this.data];
//  public doughnutChartType:string = 'doughnut';

 // events
 public chartClicked(e:any):void {
   console.log(e);
 }

 // events
 public chartHovered(e:any):void {
   console.log(e);
 }

}

