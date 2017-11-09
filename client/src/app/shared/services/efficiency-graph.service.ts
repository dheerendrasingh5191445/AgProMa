import { Injectable } from '@angular/core';
import { Http } from "@angular/http";

@Injectable()
export class EfficiencyGraphService {

  constructor(private http : Http) { }

  //local variable used for storing path which is used to hit API
  url = 'http://localhost:52258/api/EfficiencyForTask/'; 
  

  getEfficiencyDetail(userId : number){
    //This method will get the details for Efficiency
    return this.http.get(this.url+userId)
                    .map(response => response.json() );
              
  }

}
