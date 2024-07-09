import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sinistre, SinistreDto } from '../Models/sinistre.model';

@Injectable({
  providedIn: 'root'
})
export class SinistreService {
  private apiUrl = 'https://localhost:7021/api/Sinistres';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getSinistres(): Observable<Sinistre[]> {
    return this.http.get<Sinistre[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  getSinistreById(id: number): Observable<Sinistre> {
    return this.http.get<Sinistre>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  createSinistre(sinistreDto: SinistreDto): Observable<Sinistre> {
    return this.http.post<Sinistre>(this.apiUrl, sinistreDto, { headers: this.getHeaders() });
  }
  
  updateSinistre(id: number, sinistreDto: SinistreDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, sinistreDto, { headers: this.getHeaders() });
  }
}
