// imports here
import { Component, OnInit, trigger, state, animate, transition, style } from '@angular/core';
import { NgStyle } from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { Checklist } from './../shared/model/checklist'
import { ChecklistService } from './../shared/services/checklist.service'
import { TaskService } from './../shared/services/task.service'
// import ends
@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.css']
})
export class ChecklistComponent implements OnInit {
  // variable declarations
  StatusStyle;
  totalCount: number = 0;
  check: any;
  task: any;
  id: any = 85;
  checklistStatus: number;
  statusInPer;
  countChecklist: number = 0;
  details: Checklist[];
  model: Checklist = {
    taskId: null,
    checklistName: '',
    status: false
  };
  msg: string;
  constructor(private checkListService: ChecklistService, private taskService: TaskService, private route: ActivatedRoute) { }
  // for taking data from backend
  ngOnInit() {
    this.route.params.subscribe((param) => this.model.taskId = +param['id']);
    this.taskService.getById(this.id).subscribe((tasks => {
      this.task = tasks;
      console.log("helllllllllllllloooooooooooooooooooooooooooo", this.task)
    }));

    this.checkListService.getCheckList(this.id)
      .subscribe(data => {
        this.details = data;
        console.log(this.details);
        this.details.forEach(p => { this.totalCount++ });
        this.details.forEach(p => { if (p["status"] == true) { this.countChecklist++ } });
        this.checklistStatus = (((this.countChecklist) / (this.totalCount)) * 100);
        this.statusInPer = (this.checklistStatus + '%')
        this.StatusStyle = { 'width': this.statusInPer };
      });


  }
  // for adding checklist of user
  addCheckList() {
    this.model.taskId = 85;
    console.log(this.model);
    
    this.checkListService.addCheckList(this.model)
      .subscribe(result => {
        this.totalCount++;
        this.ngOnInit();
      },
      error => {
        this.msg = "Something Went Wrong, Please Try Again Later";
      })
      

  }
  // task completed logic
  markCompleted(status: any, item) {
    if (status == false) {
      item.status = true;
      this.countChecklist++;
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Successfully Completed"
        })
    }
    // task incomplete logic
    else {
      item.status = false;
      this.countChecklist--;
      this.check = status;
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Incomplete"
        })

    }
    this.checklistStatus = (((this.countChecklist) / (this.totalCount)) * 100);
    this.statusInPer = (this.checklistStatus + '%')
    this.StatusStyle = { 'width': this.statusInPer };
    
  }
  //delete checklist
  deleteChecklist(item) {
    this.checkListService.deleteChecklists(item.checklistId)
      .subscribe(result => {
        this.totalCount--;
        if(item.status==true){
          this.countChecklist--;
        }
        this.ngOnInit();
        this.msg = "deleted";
      });
      
  }
}
