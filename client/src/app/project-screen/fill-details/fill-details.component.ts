import { Component, OnInit } from '@angular/core';
import { ProjectMaster } from '../../shared/model/ProjectMaster';
import { ProjectScreenService } from '../project-screen.service';
import { Router,ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fill-details',
  templateUrl: './fill-details.component.html',
  styleUrls: ['./fill-details.component.css']
})
export class FillDetailsComponent implements OnInit {
  updateId:number;
  condition:string;
  token:boolean;
  projectname:string;
  projectdescription:string;
  technologies:string;
  constructor(private projectscrservice:ProjectScreenService,private route:ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.route.params.subscribe((param) => console.log (param.id));

  }

  //this method is used to add new project
  AddProject()
  {
    if(this.projectdescription != null && this.projectname !=null && this.technologies != null)
    {
    let projectitem = new ProjectMaster(this.projectname,this.updateId,this.projectdescription,this.technologies);
    this.projectscrservice.addNewProject(projectitem)
                          .then(data => this.router.navigate(["app-dashboard"]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }

  //method is calling the service method to update project
  updateProject()
  {
    if(this.projectdescription != null && this.projectname !=null && this.technologies != null)
    {
    let projectitem = new ProjectMaster(this.projectname,this.updateId,this.projectdescription,this.technologies);
    this.projectscrservice.updateProject(this.updateId,projectitem)
                          .then(data => this.router.navigate(["app-dashboard"]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }
}
