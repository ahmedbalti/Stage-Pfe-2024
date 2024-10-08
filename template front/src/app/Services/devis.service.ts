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

  

  getDevis(): Observable<Devis[]> {
    return this.http.get<Devis[]>(this.apiUrl);
  }

  getDevisById(id: number): Observable<Devis> {
    return this.http.get<Devis>(`${this.apiUrl}/${id}`);
  }

  createDevisAuto(devisAuto: DevisAuto): Observable<DevisAuto> {
    return this.http.post<DevisAuto>(`${this.apiUrl}/auto`, devisAuto);
  }

  createDevisSante(devisSante: DevisSante): Observable<DevisSante> {
    return this.http.post<DevisSante>(`${this.apiUrl}/sante`, devisSante);
  }

  createDevisHabitation(devisHabitation: DevisHabitation): Observable<DevisHabitation> {
    return this.http.post<DevisHabitation>(`${this.apiUrl}/habitation`, devisHabitation);
  }

  createDevisVie(devisVie: DevisVie): Observable<DevisVie> {
    return this.http.post<DevisVie>(`${this.apiUrl}/vie`, devisVie);
  }
}
