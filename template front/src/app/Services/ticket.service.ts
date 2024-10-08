import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket, TicketDTO, TicketResponseDTO } from '../Models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = `https://localhost:7021/api/Ticket`;

  constructor(private http: HttpClient) {}

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.apiUrl);
  }

  getTicketById(id: string): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`);
  }

  createTicket(ticketDTO: TicketDTO): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, ticketDTO);
  }

  updateTicket(id: string, ticketDTO: TicketDTO): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/${id}`, ticketDTO);
  }
  
  getTicketsByTitle(title: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/filterByTitle/${title}`);
  }

  getTicketResponses(ticketId: string): Observable<TicketResponseDTO[]> {
    return this.http.get<TicketResponseDTO[]>(`${this.apiUrl}/${ticketId}/responses`);
  }
}
