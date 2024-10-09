// opportunity.model.ts

export interface Opportunity {
    id: number;
    description: string;
    montant: number;
    dateCreation: Date;
    assuranceType: string; // Auto, Habitation, Santé, Vie
    primeAnnuelle: number; // Prime annuelle de l'assurance
    dureeContrat: number; // Durée du contrat en mois
    couverture: string; // Détails de la couverture d'assurance
    devisId: number;
    userId: string;
    isApproved: boolean;
  }
  