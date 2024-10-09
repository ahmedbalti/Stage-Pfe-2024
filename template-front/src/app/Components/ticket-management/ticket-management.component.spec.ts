import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketManagementComponent } from './ticket-management.component';

describe('TicketManagementComponent', () => {
  let component: TicketManagementComponent;
  let fixture: ComponentFixture<TicketManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TicketManagementComponent]
    });
    fixture = TestBed.createComponent(TicketManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
