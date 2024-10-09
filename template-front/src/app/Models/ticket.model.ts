export enum TicketPriority {
    Faible = 'Faible',
    Moyenne = 'Moyenne',
    Haute = 'Haute'
  }
  
  export enum TicketStatut {
    Nouveau = 'Nouveau',
    EnCours = 'EnCours',
    Resolu = 'Resolu'
  }
  
  export enum TicketTitle {
    DemandeAide = 'DemandeAide',
    DemandeInformation = 'DemandeInformation',
    Reclamation = 'Reclamation'
  }
  
  export interface Ticket {
    idTicket: string;
    titre: TicketTitle;
    description: string;
    createdOn: Date;
    resolutionDate?: Date;
    priority: TicketPriority;
    statut: TicketStatut;
    ownerId: string;
  }
  
  export interface TicketDTO {
    titre: TicketTitle;
    description: string;
    priority: TicketPriority;
    statut: TicketStatut;
    resolutionDate?: Date;
  }

  export interface TicketResponseDTO {
    response: string;
    ticketId: string;
  }
  
  export interface TicketFilterDTO {
    Title?: TicketTitle;
    Status?: TicketStatut;
  }
  
  export interface TicketStatusUpdateDTO {
    statut: TicketStatut;
  }

  export interface TicketStatistics {
    totalTickets: number;
    resolvedTickets: number;
    unresolvedTickets: number;
    lowPriorityTickets: number;
    mediumPriorityTickets: number;
    highPriorityTickets: number;
  }
  
  
 