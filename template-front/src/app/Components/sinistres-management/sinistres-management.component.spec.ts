import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinistresManagementComponent } from './sinistres-management.component';

describe('SinistresManagementComponent', () => {
  let component: SinistresManagementComponent;
  let fixture: ComponentFixture<SinistresManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SinistresManagementComponent]
    });
    fixture = TestBed.createComponent(SinistresManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
