import { Component, OnInit,Input,Output, EventEmitter } from '@angular/core';
import { ProjectMaster } from './../../shared/model/ProjectMaster';
import { ProjectScreenService } from "./../project-screen.service";
import { Router} from  '@angular/router';
import { TitleCasePipe } from '@angular/common';


@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {
 @Input() Data:ProjectMaster[];
 @Output() onDelete = new EventEmitter<number>();
 projectname:string;
 projectdescription:string;
 technologies:string;
  constructor(private projectservice:ProjectScreenService,private router:Router) { }

  ngOnInit() {
  }
//method is calling the service method to delete project
  delete(id:number):void{
    this.projectservice.deleteProject(id)
                       .then(data => {this.onDelete.emit(id)});
  }
}
