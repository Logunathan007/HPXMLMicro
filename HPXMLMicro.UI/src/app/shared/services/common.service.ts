import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { envVariable } from '../../environents/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private http:HttpClient) { }

  //for Address
  sendAddress(req:any){
    return this.http.post(envVariable.API_URL+'Address',req);
  }

  //for About
  sendAbout(req:any){
    return this.http.post(envVariable.API_URL+'About',req);
  }
}
