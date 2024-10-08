import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Ticket, TicketDTO, TicketResponseDTO } from 'src/app/Models/ticket.model';
import { TicketService } from 'src/app/Services/ticket.service';


@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  tickets: Ticket[] = [];
  ticketForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  currentTicketId: string | null = null;
  ticketTitles = ['DemandeAide', 'DemandeInformation', 'Reclamation'];
  ticketPriorities = ['Faible', 'Moyenne', 'Haute'];
  filteredTickets: Ticket[] = [];
  selectedTitle: string = '';
  ticketResponses: TicketResponseDTO[] = [];
  isResponseListVisible = false;
  responseForm: FormGroup;


  constructor(private fb: FormBuilder, private ticketService: TicketService) {
    this.ticketForm = this.fb.group({
      titre: [''],
      description: [''],
      priority: ['']
    });

    this.responseForm = this.fb.group({
      response: ['']
    });
  }

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getTickets().subscribe(tickets => {
      this.tickets = tickets;
      this.filteredTickets = this.tickets;

    });
  }

  filterTickets(): void {
    if (this.selectedTitle) {
      this.ticketService.getTicketsByTitle(this.selectedTitle).subscribe(tickets => {
        this.filteredTickets = tickets;
      });
    } else {
      // Si aucun titre sélectionné, afficher tous les tickets
      this.filteredTickets = this.tickets;
    }
  }
  openCreateForm(): void {
    this.isEditMode = false;
    this.ticketForm.reset();
    this.currentTicketId = null;
    this.isFormVisible = true;
  }

  openEditForm(ticket: Ticket): void {
    this.isEditMode = true;
    this.ticketForm.patchValue(ticket);
    this.currentTicketId = ticket.idTicket;
    this.isFormVisible = true;
  }

  closeForm(): void {
    this.isFormVisible = false;
  }

  onSubmit(): void {
    if (this.ticketForm.valid) {
      const ticketDTO: TicketDTO = this.ticketForm.value;
      if (this.isEditMode && this.currentTicketId) {
        this.ticketService.updateTicket(this.currentTicketId, ticketDTO).subscribe(() => {
          this.loadTickets();
          this.closeForm();
        });
      } else {
        this.ticketService.createTicket(ticketDTO).subscribe(() => {
          this.loadTickets();
          this.closeForm();
        });
      }
    }
  }

  loadTicketResponses(ticketId: string): void {
    this.ticketService.getTicketResponses(ticketId).subscribe(responses => {
      this.ticketResponses = responses;
      this.isResponseListVisible = true;
    });
  }

  closeResponseView(): void {
    this.isResponseListVisible = false;
  }
}

  

