import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SignupService {

  private apiUrl = 'https://localhost:44366/api/Authentication';
  constructor(private http:HttpClient) { }
  signup(formData: any){
    return this.http.post(this.apiUrl,formData)
  }
}
