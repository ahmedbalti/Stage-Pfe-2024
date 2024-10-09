// opportunity.component.ts

import { Component, OnInit } from '@angular/core';
import { Opportunity } from 'src/app/Models/opportunity.model';
import { OpportunityService } from 'src/app/Services/opportunity.service';


@Component({
  selector: 'app-opportunity',
  templateUrl: './opportunity.component.html',
  styleUrls: ['./opportunity.component.css']
})
export class OpportunityComponent implements OnInit {

  opportunities: Opportunity[] = [];

  constructor(private opportunityService: OpportunityService) { }

  ngOnInit(): void {
    this.loadOpportunities();
  }

  loadOpportunities() {
    this.opportunityService.getOpportunities().subscribe(
      opportunities => {
        this.opportunities = opportunities;
      },
      error => {
        console.log('Error fetching opportunities:', error);
      }
    );
  }

  approveOpportunity(id: number) {
    this.opportunityService.approveOpportunity(id).subscribe(
      () => {
        console.log('Opportunity approved successfully');
        // Refresh the list of opportunities after approval
        this.loadOpportunities();
      },
      error => {
        console.error('Error approving opportunity:', error);
      }
    );
  }
}
