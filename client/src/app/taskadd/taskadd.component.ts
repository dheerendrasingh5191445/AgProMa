
  import { Component, OnInit } from '@angular/core';
  import { TaskService } from '../shared/services/task.service';
  import { Task} from '../shared/model/task';
  import swal from 'sweetalert2';
  
  
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskAddComponent implements OnInit {
  sub: string ="";
  


  data:Array<any>

model =new Task(null,null,'',null,null,null);
  constructor(private taskService:TaskService)
   { }

   //this will get the task backlog list
  ngOnInit() {
    this.getBacklog();
  }

//this will add new task to the backlog
 addBacklog(taskName:string,comment:any,startDate:any,endDate:any){
  
  //this will give alert if nothing is entered as task
  if((taskName=="")){
    swal('Task Cannot Be Empty ','','warning')
  }
  //this will work if task name is entered and  add new task to backlog
  if(taskName){
  this.model.sprintId=2;
   this.model.personId=1;
   this.model.startDate=startDate;
   this.model.endDate=endDate;
   this.model.taskName=taskName;
   this.model.status=1;
  this.taskService.add(this.model)
  .subscribe((f)=>{
   this.getBacklog();
  });
}
}
 //this will uddate task backlog values
 updateBacklog(content:any,item:any,startDate:any,endDate:any){
//this will give alert if no task is entered for updation(empty value)
	if(content == ""){
    swal('Enter Some Task','','warning')
	}
	else{
  item.taskName=content;

  item.startDate=startDate;
  item.endDate=endDate;
//this will give alert if task is successfully upadated
  this.taskService.update(item.taskId,item).subscribe(f=>{	swal('Task updated Successfully','','success');}); 
} 
}
//this will return task backlog
  getBacklog(){
   this.taskService.get().subscribe(data => {
   this.data = data;

  });
}
//this will delete task from backlog
deleteBacklog(item:any){
  if((this.sub == "")){
    swal('Task Deleted Successfully','','error')
  }
  this.taskService.delete(item.taskId).subscribe((f)=>{
    this.getBacklog();
   });
 }
 
}