export interface Devis {
  idDevis: number;
  typeAssurance: string;
  montant: number;
  numeroImmatriculation?: string;
  nombreDeChevaux?: number;
  ageVoiture?: number;
  carburant?: string;
  numeroSecuriteSociale?: string;
  age?: number;
  sexe?: string;
  fumeur?: boolean;
  adresse?: string;
  surface?: number;
  nombreDePieces?: number;
  beneficiaire?: string;
  duree?: number;
  capital?: number;
}

export interface DevisAuto extends Devis {
  numeroImmatriculation: string;
  nombreDeChevaux: number;
  ageVoiture: number;
  carburant: string;
}

export interface DevisSante extends Devis {
  numeroSecuriteSociale: string;
  age: number;
  sexe: string;
  fumeur: boolean;
}

export interface DevisHabitation extends Devis {
  adresse: string;
  surface: number;
  nombreDePieces: number;
}

export interface DevisVie extends Devis {
  beneficiaire: string;
  duree: number;
  capital: number;
}
