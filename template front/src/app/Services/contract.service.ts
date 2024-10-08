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


  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.apiUrl);
  }

  getContracts1(): Observable<Contract[]> {
    return this.http.get<Contract[]>(`${this.apiUrl}/all`);
  }

  renewContract(request: RenewalRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/renew`, request);
  }

  

  addContract(contract: Partial<Contract>): Observable<Contract> {
    return this.http.post<Contract>(this.apiUrl, contract);
  }

  updateContract(id: number, contract: Partial<Contract>): Observable<string> {
    return this.http.put(`${this.apiUrl}/${id}`, contract, { responseType: 'text' });
  }

  downloadContractsPdf(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/pdf/all`, {responseType: 'blob' });
  }

  getClients(): Observable<any[]> {  // New method to fetch clients
    return this.http.get<any[]>(`${this.apiUrl}/clients`);
  }
}
