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
  taskbacklog : TaskBackLog[] =[];
  taskBackLog : TaskBackLog ;
  projectId : number = 1;
  isDataAvailable = false;
  data:any;
  constructor(private kanbanService : KanbanService, private route : ActivatedRoute) { }

  ngOnInit() {

    // getting sprint id from route 
    this.route.params.subscribe((param) =>{this.projectId = +param['id']});

    //Getting the detail of task backlog
    this.kanbanService.getTaskDetail(this.projectId)
                      .subscribe(data => {this.data = data; this.isDataAvailable = true;});

  }
}
