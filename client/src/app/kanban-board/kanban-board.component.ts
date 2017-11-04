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

  //local variable used in backend
  data : TaskBackLog[];
  taskBackLog : TaskBackLog ;
  SprintId : number = 2;

  constructor(private kanbanService : KanbanService, private route : ActivatedRoute) { }

  ngOnInit() {

    // getting sprint id from route 
    // this.route.params.subscribe((param) =>
    // this.taskBackLog.SprintId = +param['id']);

    //Getting the detail of task backlog
    this.kanbanService.getTaskDetail(this.SprintId).subscribe(data => {this.data = data});
  }

}
