import { Injectable } from '@angular/core';
import {HttpModule,Http,Response,Headers} from '@angular/http';
import 'rxjs/add/operator/map'; 
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from "rxjs/Observable";

@Injectable()
export class TaskService {
getTaskUrl:string="http://localhost:52258/api/task";
postTaskUrl:string="http://localhost:52258/api/task";
deleteTaskUrl:string="http://localhost:52258/api/task";
updateTaskUrl:string="http://localhost:52258/api/task";
 constructor(private http:Http) { }
 private headers=new Headers({'Content-Type':'application/json'});
 

 
 // this method will get the task backlog 
 get(){
   return this.http.get(this.getTaskUrl).map((res:Response)=>res.json());
 }
 // this method will add task to task backlog
 add(item){
    console.log(item)
      return this.http.post(this.postTaskUrl,item,{headers:this.headers});
   
   }
// this method will update task to task backlog
   update(id:any,item:any){
    {
        return this.http.put(this.updateTaskUrl+"/"+id,item ,{headers:this.headers}).catch((error:any)=>{
      
          return Observable.throw(error);
        })
      }
 
 }
//  this method will delete task from task backlog
 delete(id:any){
 
  return this.http.delete(this.deleteTaskUrl+"/"+id);
 
        }
 
 }