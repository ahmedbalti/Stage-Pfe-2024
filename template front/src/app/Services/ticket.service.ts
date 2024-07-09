import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'; // Ajustez le chemin si nécessaire
import { Ticket, TicketDTO } from '../Models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = `https://localhost:7021/api/Ticket`;

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('accessToken');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  getTicketById(id: string): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }


  createTicket(ticketDTO: TicketDTO): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, ticketDTO, { headers: this.getHeaders() });
  }

  updateTicket(id: string, ticketDTO: TicketDTO): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/${id}`, ticketDTO, { headers: this.getHeaders() });
  }
 
  getTicketsByTitle(title: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/filterByTitle/${title}`, { headers: this.getHeaders() });
  }

}
