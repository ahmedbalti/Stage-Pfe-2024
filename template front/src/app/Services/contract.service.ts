import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contract, RenewalRequest } from '../Models/contract.model';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  private apiUrl = `https://localhost:7021/api/Contracts`;

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  renewContract(request: RenewalRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/renew`, request, { headers: this.getHeaders() });
  }

  downloadContractsPdf(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/pdf/all`, { headers: this.getHeaders(), responseType: 'blob' });
  }
}
