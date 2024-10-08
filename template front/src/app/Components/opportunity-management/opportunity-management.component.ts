import { Component, OnInit } from '@angular/core';
import { Opportunity } from 'src/app/Models/opportunity.model';
import { OpportunityService } from 'src/app/Services/opportunity.service';

@Component({
  selector: 'app-opportunity-management',
  templateUrl: './opportunity-management.component.html',
  styleUrls: ['./opportunity-management.component.css']
})
export class OpportunityManagementComponent implements OnInit {
  opportunities: Opportunity[] = [];

  constructor(private opportunityService: OpportunityService) {}

  ngOnInit(): void {
    this.loadOpportunities();
  }

  loadOpportunities() {
    this.opportunityService.getOpportunities1().subscribe(
      opportunities => {
        this.opportunities = opportunities;
      },
      error => {
        console.log('Error fetching opportunities:', error);
      }
    );
  }
}
