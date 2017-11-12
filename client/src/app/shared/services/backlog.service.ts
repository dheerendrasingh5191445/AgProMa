import { Injectable } from '@angular/core';
import { HttpModule, Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from "rxjs/Observable";

@Injectable()
export class BacklogService {
  getStoryUrl: string = "http://localhost:52258/api/backlog"; //url for getting dataa
  postStoryUrl: string = "http://localhost:52258/api/backlog";//url for posting user story 
  deleteStoryUrl: string = "http://localhost:52258/api/backlog";//url for deleting user story
  updateStoryUrl: string = "http://localhost:52258/api/backlog";//url updating new user story
  product_url= 'http://localhost:52258/api/backlog/';

  constructor(private http: Http) { }
  private headers = new Headers({ 'Content-Type': 'application/json' });

  get(id)//for getting all user story based on product id 
  {
    return this.http.get(this.getStoryUrl + '/' + id)
                    .map((res: Response) => res.json())
  }
  add(item) // for adding new user story
  {
    console.log(item)
    return this.http.post(this.postStoryUrl, item, { headers: this.headers });

  }
  update(id: any, item: any)//for updating a particular user story based on storyid
  {
    {
      return this.http.put(this.updateStoryUrl + "/" + id, item, { headers: this.headers }).catch((error: any) => {

        return Observable.throw(error);
      })
    }
  }
  delete(id: any)////for deleting  a particular user story based on storyid
  {
    return this.http.delete(this.deleteStoryUrl + "/" + id);
  }

  //it will get stories specific to sprint id
  getUserStoryByProjectId(projectId) {
    return this.http.get(this.product_url + projectId)
      .map(Response => Response.json());
  }

  //get only unassigned user stories.
  getBacklogs(projectId) {
    return this.http.get(this.product_url + 'GetUnassignedStory/' + projectId)
      .map(Response => Response.json());
  }

}
