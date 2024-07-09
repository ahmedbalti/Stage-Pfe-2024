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

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getOpportunities(): Observable<Opportunity[]> {
    return this.http.get<Opportunity[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  approveOpportunity(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/approve/${id}`, {}, { headers: this.getHeaders() });
  }
}
