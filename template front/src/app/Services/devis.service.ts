import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Devis, DevisAuto, DevisHabitation, DevisSante, DevisVie } from '../Models/devis.model';

@Injectable({
  providedIn: 'root'
})
export class DevisService {
  private apiUrl = 'https://localhost:7021/api/Devis'; // Remplacez par l'URL de votre API

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getDevis(): Observable<Devis[]> {
    return this.http.get<Devis[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  getDevisById(id: number): Observable<Devis> {
    return this.http.get<Devis>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  createDevisAuto(devisAuto: DevisAuto): Observable<DevisAuto> {
    return this.http.post<DevisAuto>(`${this.apiUrl}/auto`, devisAuto, { headers: this.getHeaders() });
  }

  createDevisSante(devisSante: DevisSante): Observable<DevisSante> {
    return this.http.post<DevisSante>(`${this.apiUrl}/sante`, devisSante, { headers: this.getHeaders() });
  }

  createDevisHabitation(devisHabitation: DevisHabitation): Observable<DevisHabitation> {
    return this.http.post<DevisHabitation>(`${this.apiUrl}/habitation`, devisHabitation, { headers: this.getHeaders() });
  }

  createDevisVie(devisVie: DevisVie): Observable<DevisVie> {
    return this.http.post<DevisVie>(`${this.apiUrl}/vie`, devisVie, { headers: this.getHeaders() });
  }
}
