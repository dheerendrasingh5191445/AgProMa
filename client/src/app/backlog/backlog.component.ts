import { Component, OnInit } from '@angular/core';
import { BacklogService } from '../shared/services/backlog.service';
import { ProductBacklog} from '../shared/model/productBacklog';
import swal from 'sweetalert2';

@Component({
  selector: 'app-backlog',
  templateUrl: './backlog.component.html',
  styleUrls: ['./backlog.component.css']
})
export class BacklogComponent implements OnInit {
projectId:number=1;
 data:Array<any>
 length:number;
 model =new ProductBacklog(null,'','',null,null);  //model for adding new user story
  constructor(private backlogService:BacklogService)
   { }

  ngOnInit() 
   {
    this.getBacklog();  //on reload of this component ,this method will call
  }
  addBacklog(story:any,comment:any,priority) //for adding new user story
  {
    if(story==""){
      swal('Please fill user story','','error');
    }else{
   this.model.storyName=story;
   this.model.comments=comment
   this.model.projectId=1;
   this.model.status=false;
   this.model.priority=priority;
   this.backlogService.add(this.model)
   .subscribe((f)=>{
    swal('User story added','','success');
    this.getBacklog();
   });}

  }
  updateBacklog(content:any,comment:any,priority:any,item) //for updating a particular user story
  {
    if(content==""){
      swal('Please fill user story','','error');
    }
    item.storyName=content;
    item.comments=comment;
    item.priority=priority;
this.backlogService.update(item.storyId,item).subscribe(f=>{swal('User story updated','','success')});
  }
  getBacklog(){
    
 this.backlogService.get(this.projectId).subscribe(data => {
  this.data = data;
  this.length=data.length;


  data.sort(function(a,b){
    return a.priority-b.priority;
  });

  ;
  });
}
deleteBacklog(item:any) // for deleting a particular user story
{
    this.backlogService.delete(item.storyId).subscribe((f)=>{
      swal('User story deleted','','success');
    this.getBacklog();
   });
}

}