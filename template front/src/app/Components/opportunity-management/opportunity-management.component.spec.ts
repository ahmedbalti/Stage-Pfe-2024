import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpportunityManagementComponent } from './opportunity-management.component';

describe('OpportunityManagementComponent', () => {
  let component: OpportunityManagementComponent;
  let fixture: ComponentFixture<OpportunityManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpportunityManagementComponent]
    });
    fixture = TestBed.createComponent(OpportunityManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
