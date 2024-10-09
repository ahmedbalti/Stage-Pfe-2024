export interface Sinistre {
  id: number;
  numeroDossier: string;
  dateDeclaration: Date;
  description: string;
  statut: string; // Changer de number à string
  montantEstime: number;
  montantPaye: number;
  userId: string;
}

export interface SinistreDto {
  id?: number;
  numeroDossier: string;
  dateDeclaration: Date;
  description: string;
  statut: string; // Changer de number à string
  montantEstime: number;
  montantPaye: number;
}
