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
  