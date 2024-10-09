import { Component, OnInit } from '@angular/core';
import { Sinistre } from 'src/app/Models/sinistre.model';
import { SinistreService } from 'src/app/Services/sinistre.service';


@Component({
  selector: 'app-sinistres-management',
  templateUrl: './sinistres-management.component.html',
  styleUrls: ['./sinistres-management.component.css']
})
export class SinistresManagementComponent implements OnInit {
  sinistres: Sinistre[] = [];

  constructor(private sinistreService: SinistreService) {}

  ngOnInit(): void {
    this.getSinistres1();
  }

  getSinistres1(): void {
    this.sinistreService.getSinistres1().subscribe(
      (data: Sinistre[]) => {
        this.sinistres = data;
      },
      (error) => {
        console.error('Error fetching sinistres', error);
      }
    );
  }
}
