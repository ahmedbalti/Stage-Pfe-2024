import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Ticket, TicketDTO, TicketFilterDTO, TicketResponseDTO, TicketStatistics, TicketStatusUpdateDTO, TicketStatut, TicketTitle } from 'src/app/Models/ticket.model';
import { TicketManagementService } from 'src/app/Services/ticket-management.service';
import { Chart, ChartConfiguration, ChartData, ChartOptions, Point, BubbleDataPoint } from 'chart.js/auto';






@Component({
  selector: 'app-ticket-management',
  templateUrl: './ticket-management.component.html',
  styleUrls: ['./ticket-management.component.css']
})
export class TicketManagementComponent implements OnInit {
  tickets: Ticket[] = [];
  ticketForm: FormGroup;
  responseForm: FormGroup;
  statuses = Object.values(TicketStatut);
  statusForm: FormGroup; // Form for updating ticket status
  isStatusFormVisible = false; // Status form visibility

  isFormVisible = false;
  isResponseFormVisible = false;
  currentTicketId: string | null = null;
  currentResponseTicketId: string | null = null;
  selectedTitle: TicketTitle | null = null;
  selectedStatus: TicketStatut | null = null;
  filteredTickets: Ticket[] = [];
  ticketTitles: TicketTitle[] = Object.values(TicketTitle);
  ticketResponses: TicketResponseDTO[] = [];
  isResponseListVisible = false;

  ticketStatistics: TicketStatistics | null = null;
  resolutionStatusChart: Chart<"doughnut", (number | [number, number] | Point | BubbleDataPoint)[], unknown> | null = null;
  priorityChart: Chart<"bar", (number | [number, number] | Point | BubbleDataPoint)[], unknown> | null = null;

  constructor(private fb: FormBuilder, private ticketService: TicketManagementService) {
    this.ticketForm = this.fb.group({
      statut: ['']
    });

    this.responseForm = this.fb.group({
      response: ['']
    });

    this.statusForm = this.fb.group({
      statut: [''] // Control for ticket status
    });
  

  }

  ngOnInit(): void {
    this.loadTickets();
    this.getTicketStatistics();

  }
  getTicketStatistics(): void {
    this.ticketService.getTicketStatistics().subscribe(statistics => {
      this.ticketStatistics = statistics;
      this.createCharts();
      
    });
  }

  createCharts(): void {
    if (this.ticketStatistics) {
      // Resolution status chart
      this.resolutionStatusChart = new Chart('resolutionStatusChart', {
        type: 'doughnut',
        data: {
          labels: ['Resolved', 'Unresolved'],
          datasets: [
            {
              label: 'Ticket Resolution Status',
              data: [this.ticketStatistics.resolvedTickets, this.ticketStatistics.unresolvedTickets],
              backgroundColor: ['#36A2EB', '#FF6384']
            }
          ]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          animation: {
            animateRotate: true,
            animateScale: true
          }
        }
      });
  
      // Priority chart
      this.priorityChart = new Chart('priorityChart', {
        type: 'bar',
        data: {
          labels: ['Low', 'Medium', 'High'],
          datasets: [
            {
              label: 'Ticket Priority',
              data: [this.ticketStatistics.lowPriorityTickets, this.ticketStatistics.mediumPriorityTickets, this.ticketStatistics.highPriorityTickets],
              backgroundColor: ['#36A2EB', '#FF6384', '#FFCE56']
            }
          ]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          scales: {
            y: {
              beginAtZero: true,
              ticks: {
                precision: 0
              }
            }
          }
        }
      });
    }
  }

  loadTickets(): void {
    this.ticketService.getAllTickets().subscribe(tickets => {
      this.tickets = tickets;
      this.filteredTickets = this.tickets;
    });
  }

  filterTickets(): void {
    const filter: TicketFilterDTO = {
      Title: this.selectedTitle || undefined,
      Status: this.selectedStatus || undefined
    };
    this.ticketService.filterTickets(filter).subscribe(tickets => {
      this.filteredTickets = tickets;
    });
  }

  openResponseForm(ticketId: string): void {
    this.currentResponseTicketId = ticketId;
    this.responseForm.reset();
    this.isResponseFormVisible = true;
  }

  closeResponseForm(): void {
    this.isResponseFormVisible = false;
  }

  onSubmitResponse(): void {
    if (this.responseForm.valid && this.currentResponseTicketId) {
      const responseDTO: TicketResponseDTO = this.responseForm.value;
      responseDTO.ticketId = this.currentResponseTicketId;
      this.ticketService.respondToTicket(this.currentResponseTicketId, responseDTO).subscribe(() => {
        this.loadTickets();
        this.closeResponseForm();
      });
    }
  }

  openStatusForm(ticket: Ticket): void {
    this.currentTicketId = ticket.idTicket;
    this.statusForm.patchValue({ statut: ticket.statut });
    this.isStatusFormVisible = true;
  }

  closeStatusForm(): void {
    this.isStatusFormVisible = false;
  }

  onSubmitStatus(): void {
    if (this.statusForm.valid && this.currentTicketId) {
      const statusUpdateDTO: TicketStatusUpdateDTO = this.statusForm.value;
      this.ticketService.updateTicketStatus(this.currentTicketId, statusUpdateDTO).subscribe(() => {
        this.loadTickets();
        this.closeStatusForm();
      });
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