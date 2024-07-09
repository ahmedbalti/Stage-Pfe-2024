export interface Contract {
    id: number;
    policyNumber: string;
    startDate: Date;
    endDate: Date;
    isActive: boolean;
    userId: string;
  }
  
  export interface RenewalRequest {
    contractId: number;
  }
  