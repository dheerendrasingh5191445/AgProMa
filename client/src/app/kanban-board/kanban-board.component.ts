import { Component, OnInit } from '@angular/core';
import { KanbanService } from "../shared/services/kanban.service";
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'kanban',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  data : TaskBackLog[];
 
  taskBackLog : TaskBackLog ;
  SprintId : number = 2;
  constructor(private kanbanService : KanbanService, private route : ActivatedRoute) { }

  ngOnInit() {

    // this.route.params.subscribe((param) =>
    // this.taskBackLog.SprintId = +param['id']);

    this.kanbanService.getTaskDetail(this.SprintId).subscribe(data => {console.log(data);this.data = data});
  }
  compareStatus(status) {
    console.log("'''''''''''''"+status);
    if(status=='Unplanned') {
      return true;
    }
  }

}
