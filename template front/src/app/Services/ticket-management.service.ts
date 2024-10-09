import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket, TicketDTO, TicketResponseDTO, TicketFilterDTO, TicketStatusUpdateDTO, TicketStatistics } from '../Models/ticket.model';


@Injectable({
  providedIn: 'root'
})
export class TicketManagementService {
  private apiUrl = `https://localhost:7021/api/Ticket`;

  constructor(private http: HttpClient) {}

  
  getAllTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/all`);
  }

  filterTickets(filter: TicketFilterDTO): Observable<Ticket[]> {
    const params: any = {};
    if (filter.Title) {
      params.Title = filter.Title;
    }
    if (filter.Status) {
      params.Status = filter.Status;
    }
    
    // Passer les paramètres de filtrage dans la requête
    return this.http.get<Ticket[]>(`${this.apiUrl}/filter`, { params });
  }
  

  respondToTicket(id: string, responseDTO: TicketResponseDTO): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/${id}/respond`, responseDTO);
  }

 
  
  getTicketResponses(ticketId: string): Observable<TicketResponseDTO[]> {
    return this.http.get<TicketResponseDTO[]>(`${this.apiUrl}/${ticketId}/responses`);
  }
  
  updateTicketStatus(ticketId: string, statusUpdateDTO: TicketStatusUpdateDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${ticketId}/status`, statusUpdateDTO);
  }

  getTicketStatistics(): Observable<TicketStatistics> {
    return this.http.get<TicketStatistics>(`${this.apiUrl}/statistics`);
  }

}
