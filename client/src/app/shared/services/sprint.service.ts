import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class SprintService {
  response: any;
  private headers = new Headers({ 'Content-Type': 'application/json' });
  sprint_url = 'http://localhost:52258/api/sprint/';

  constructor(private http: Http) { }

  //it will get all the sprints from the database.
  getSprints(projectId) {
    return this.http.get(this.sprint_url + projectId)
      .map((response) => response.json());
  }
  //update the sprint in database
  updateSprint(storyData,Id){
    return this.http.put(this.sprint_url+Id,storyData, { headers: this.headers });
  }
  //it will add new sprint.
  onSave(sprint: any) {
    return this.http.post(this.sprint_url, sprint, { headers: this.headers });
  }
}
