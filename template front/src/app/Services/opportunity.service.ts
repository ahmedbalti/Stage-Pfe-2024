// opportunity.service.ts

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Opportunity } from '../Models/opportunity.model';

@Injectable({
  providedIn: 'root'
})
export class OpportunityService {

  private apiUrl = 'https://localhost:7021/api/Opportunities';

  constructor(private http: HttpClient) { }

 

  getOpportunities(): Observable<Opportunity[]> {
    return this.http.get<Opportunity[]>(this.apiUrl);
  }

  approveOpportunity(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/approve/${id}`, {});
  }

  getOpportunities1(): Observable<Opportunity[]> {
    return this.http.get<Opportunity[]>(`${this.apiUrl}/all`);
  }
}
