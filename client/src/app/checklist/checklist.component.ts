// imports here
import { Component, OnInit } from '@angular/core';
import { ChecklistService } from './../shared/services/checklist.service'
import { Checklist } from './../shared/model/checklist'
// import ends
@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.css']
})
export class ChecklistComponent implements OnInit {
  // variable declarations
  newTodo: string;
  check: any;
  details: Checklist[];
  model: Checklist = {
    taskId: 21,
    checklistName: '',
    status: false
  };
  msg: string;
  constructor(private checkListService: ChecklistService) { }
// for taking data from backend
  ngOnInit() {
    this.checkListService.getCheckList()
      .subscribe(data => {
        this.details = data;
        console.log(this.details);
      });

  }
  // for adding checklist of user
  addCheckList() {
    this.checkListService.addCheckList(this.model)
      .subscribe(result => {
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
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Successfully Completed"
        })
    }
    // task incomplete logic
    else {
      item.status = false;
      this.check = status;
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Incomplete"
        })
    }
  }
  deleteChecklist(item){
    this.checkListService.deleteChecklists(item.checklistId)
    .subscribe(result=>{
      this.msg="deleted"
    })
  }
}
