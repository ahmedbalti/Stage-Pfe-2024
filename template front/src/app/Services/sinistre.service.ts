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

  

  getSinistres(): Observable<Sinistre[]> {
    return this.http.get<Sinistre[]>(this.apiUrl);
  }

  getSinistreById(id: number): Observable<Sinistre> {
    return this.http.get<Sinistre>(`${this.apiUrl}/${id}`);
  }

  createSinistre(sinistreDto: SinistreDto): Observable<Sinistre> {
    return this.http.post<Sinistre>(this.apiUrl, sinistreDto);
  }
  
  updateSinistre(id: number, sinistreDto: SinistreDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, sinistreDto);
  }

  getSinistres1(): Observable<Sinistre[]> {
    return this.http.get<Sinistre[]>(`${this.apiUrl}/all`);
  }

}
