import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'efficiency',
  templateUrl: './efficiency-graph.component.html',
  styleUrls: ['./efficiency-graph.component.css']
})
export class EfficiencyGraphComponent implements OnInit {

  total : number = 100;
  notEfficient : number = 20;
  efficient : number = 100 - this.notEfficient;

  
  constructor() { }

  ngOnInit() {
  }

   // Doughnut
 public doughnutChartLabels:string[] = ['Work Done', 'Remaining'];
 public doughnutChartData:number[] = [ this.efficient, this.notEfficient];
 public doughnutChartType:string = 'doughnut';

 // events
 public chartClicked(e:any):void {
   console.log(e);
 }

 public chartHovered(e:any):void {
   console.log(e);
 }

}

