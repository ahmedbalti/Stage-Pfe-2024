import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Contract } from 'src/app/Models/contract.model';
import { ContractService } from 'src/app/Services/contract.service';

@Component({
  selector: 'app-contracts-management',
  templateUrl: './contracts-management.component.html',
  styleUrls: ['./contracts-management.component.css']
})
export class ContractsManagementComponent implements OnInit {
  contracts: Contract[] = [];
  contractForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  currentContractId: number | null = null;
  clients: any[] = [];  // Store clients


  constructor(private fb: FormBuilder, private contractService: ContractService) {
    this.contractForm = this.fb.group({
      policyNumber: ['', [Validators.required, Validators.pattern(/^POL\d{5}$/)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      isActive: [true],
      clientId: ['', Validators.required]  // Add clientId field

    });

  }

  ngOnInit(): void {
    this.loadContracts();
    this.loadClients();  // Load clients when component initializes

  }

  loadContracts(): void {
    this.contractService.getContracts1().subscribe(
      (contracts) => {
        this.contracts = contracts;
      },
      (error) => {
        console.error('Failed to load contracts', error);
      }
    );
  }

  loadClients(): void {
    this.contractService.getClients().subscribe(
      (clients) => {
        this.clients = clients;
      },
      (error) => {
        console.error('Failed to load clients', error);
      }
    );
  }


  renewContract(contractId: number): void {
    this.contractService.renewContract({ contractId }).subscribe(
      () => {
        console.log('Contract renewed successfully');
        // Recharger les contrats pour refléter les changements
        this.loadContracts();
      },
      (error) => {
        console.error('Failed to renew contract', error);
      }
    );
  }
  
  

  openCreateForm(): void {
    this.isEditMode = false;
    this.contractForm.reset();
    this.currentContractId = null;
    this.isFormVisible = true;
  }

  openEditForm(contract: Contract): void {
    this.isEditMode = true;
    this.contractForm.patchValue(contract);
    this.currentContractId = contract.id;
    this.isFormVisible = true;
  }

  closeForm(): void {
    this.isFormVisible = false;
  }

  onSubmit(): void {
    if (this.contractForm.valid) {
      const contractData = this.contractForm.value;
      if (this.isEditMode && this.currentContractId !== null) {
        this.contractService.updateContract(this.currentContractId, contractData).subscribe(() => {
          console.log('Contract updated successfully');
          this.loadContracts(); // Reload contracts after update
          this.closeForm(); // Close the form
        }, (error) => {
          console.error('Failed to update contract', error);
        });
      } else {
        this.contractService.addContract(contractData).subscribe(() => {
          console.log('Contract added successfully');
          this.loadContracts(); // Reload contracts after addition
          this.closeForm(); // Close the form
        }, (error) => {
          console.error('Failed to add contract', error);
        });
      }
    }
  }

  downloadPdf(): void {
    this.contractService.downloadContractsPdf().subscribe(
      (response) => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'AllContracts.pdf';
        a.click();
        window.URL.revokeObjectURL(url);
      },
      (error) => {
        console.error('Failed to download PDF', error);
      }
    );
  }
}
