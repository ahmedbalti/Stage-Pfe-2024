import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Sinistre, SinistreDto } from 'src/app/Models/sinistre.model';
import { SinistreService } from 'src/app/Services/sinistre.service';


@Component({
  selector: 'app-sinistres',
  templateUrl: './sinistres.component.html',
  styleUrls: ['./sinistres.component.css']
})
export class SinistresComponent implements OnInit {
  sinistres: Sinistre[] = [];
  sinistreForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  currentSinistreId: number | null = null;
  sinistreTypes = ['Incendie', 'Inondation', 'Vol'];
  filteredSinistres: Sinistre[] = [];
  selectedType: string = '';

  statuts = [
    { value: 0, label: 'Ouverte' },
    { value: 1, label: 'EnCours' },
    { value: 2, label: 'Fermee' }
  ];

  constructor(private sinistreService: SinistreService, private fb: FormBuilder) {
    this.sinistreForm = this.fb.group({
      numeroDossier: ['', [Validators.required, Validators.pattern(/^DS\d{4}$/)], this.numeroDossierValidator.bind(this)],
      description: ['', Validators.required],
      montantEstime: [0, [Validators.required, Validators.min(100), Validators.max(50000)]],
      montantPaye: [0, [Validators.required, Validators.min(100), Validators.max(50000)]]
    });
  }

  ngOnInit(): void {
    this.loadSinistres();
  }

  loadSinistres(): void {
    this.sinistreService.getSinistres().subscribe(data => {
      this.sinistres = data;
      this.filteredSinistres = this.sinistres;
    });
  }

 

  openCreateForm(): void {
    this.isEditMode = false;
    this.sinistreForm.reset({ statut: 0 });
    this.currentSinistreId = null;
    this.isFormVisible = true;
  }

  openEditForm(sinistre: Sinistre): void {
    this.isEditMode = true;
    this.currentSinistreId = sinistre.id;
    this.sinistreForm.patchValue(sinistre);
    this.isFormVisible = true;
  }

  closeForm(): void {
    this.isFormVisible = false;
  }

  onSubmit(): void {
    if (this.sinistreForm.invalid) {
      return;
    }

    const sinistreData: SinistreDto = {
      ...this.sinistreForm.value,
      dateDeclaration: new Date(), // ou utilisez la date réelle si elle existe
    };

    if (this.isEditMode && this.currentSinistreId !== null) {
      this.sinistreService.updateSinistre(this.currentSinistreId, sinistreData).subscribe(() => {
        this.loadSinistres();
        this.closeForm();
      });
    } else {
      this.sinistreService.createSinistre(sinistreData).subscribe(() => {
        this.loadSinistres();
        this.closeForm();
      });
    }
  }

  numeroDossierValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise(resolve => {
      if (this.sinistres.find(s => s.numeroDossier === control.value)) {
        resolve({ numeroDossierTaken: true });
      } else {
        resolve(null);
      }
    });
  }

  

  getStatutLabel(statut: number): string {
    const statutItem = this.statuts.find(s => s.value === statut);
    return statutItem ? statutItem.label : '';
  }
}