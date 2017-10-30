import { Component, OnInit,Input } from '@angular/core';
import { TaskBackLog } from "../../shared/model/TaskBacklog";
import { TaskAssignService } from "../../shared/services/task-assign.service";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
 @Input() Data:TaskBackLog;  //data for the person assigned to a particular task
  Name:string; 
  constructor(private service:TaskAssignService) { } //inject TaskAssignService

  ngOnInit() { 
    if(this.Data.PersonId != 0)
    {this.service.getName(this.Data["personId"]) //get the name of the member assigned by the paersonId
                .subscribe(data => this.Name = data.json());}
  }

}
