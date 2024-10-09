import { Component, OnInit } from '@angular/core';
import { Contract } from 'src/app/Models/contract.model';
import { ContractService } from 'src/app/Services/contract.service';

@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.css']
})
export class ContractsComponent implements OnInit {
  contracts: Contract[] = [];

  constructor(private contractService: ContractService) {}

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts(): void {
    this.contractService.getContracts().subscribe(
      (contracts) => {
        this.contracts = contracts;
      },
      (error) => {
        console.error('Failed to load contracts', error);
      }
    );
  }

  renewContract(contractId: number): void {
    const request = { contractId };
    this.contractService.renewContract(request).subscribe(
      (response) => {
        console.log('Contract renewed successfully', response);
        this.loadContracts(); // Reload contracts after renewal
      },
      (error) => {
        console.error('Failed to renew contract', error);
      }
    );
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
