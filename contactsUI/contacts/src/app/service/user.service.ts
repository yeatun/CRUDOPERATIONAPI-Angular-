import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = "https://localhost:7135/api/User/"
  
  constructor(private http: HttpClient) { }

  signUp(userObj:User){
    userObj.id =  '00000000-0000-0000-0000-000000000000';
    userObj.token= '';
    return this.http.post<any>(`${this.baseUrl}register`, userObj)
  }

  login(loginObj: any){
    return this.http.post<any>(`${this.baseUrl}authenticate`, loginObj)
  }

}
