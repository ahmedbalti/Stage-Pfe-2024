import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Devis, DevisAuto, DevisHabitation, DevisSante, DevisVie } from 'src/app/Models/devis.model';
import { DevisService } from 'src/app/Services/devis.service';
import { Router } from '@angular/router'; // Ensure Router is imported


@Component({
  selector: 'app-devis',
  templateUrl: './devis.component.html',
  styleUrls: ['./devis.component.css']
})
export class DevisComponent implements OnInit {
  devisList: Devis[] = [];
  selectedType: string = '';
  devisAuto: DevisAuto = {} as DevisAuto;
  devisSante: DevisSante = {} as DevisSante;
  devisHabitation: DevisHabitation = {} as DevisHabitation;
  devisVie: DevisVie = {} as DevisVie;
  selectedDevis: Devis | null = null;
  isFormVisible: boolean = false;
  isViewVisible: boolean = false;
  devisForm: FormGroup;
  errorMessage: string = '';
  isOpportunityConfirmVisible: boolean = false;


  constructor(private devisService: DevisService, private fb: FormBuilder, private router: Router) {
    this.devisForm = this.fb.group({
      numeroImmatriculation: ['', [Validators.required, Validators.pattern(/^\d{3}[A-Z]{2}\d{4}$/)]],
      nombreDeChevaux: ['', [Validators.required, Validators.min(4), Validators.max(30)]],
      ageVoiture: ['', [Validators.required, Validators.min(0), Validators.max(30)]],
      carburant: ['', Validators.required],
      numeroSecuriteSociale: ['', [Validators.required, Validators.pattern(/^\d{8}$/)]],
      age: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
      sexe: ['', Validators.required],
      fumeur: ['', Validators.required],
      adresse: ['', [Validators.required, Validators.pattern(/^\d+\s?,\s?[A-Za-z\s]+,\s?[A-Za-z\s]+$/)]],
      surface: ['', [Validators.required, Validators.min(1), Validators.max(10000)]],
      nombreDePieces: ['', Validators.required],
      beneficiaire: ['', Validators.required],
      duree: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      capital: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadDevis();
  }

  loadDevis() {
    this.devisService.getDevis().subscribe(data => {
      this.devisList = data;
    });
  }

  onTypeChange(event: any) {
    this.selectedType = event.target.value;
    this.setupFormControls();
    this.openCreateForm();
  }

  setupFormControls() {
    // Reset all controls
    this.devisForm.reset();
    // Remove all controls
    Object.keys(this.devisForm.controls).forEach(key => {
      this.devisForm.removeControl(key);
    });

    // Add controls based on selected type
    if (this.selectedType === 'Auto') {
      this.devisForm.addControl('numeroImmatriculation', this.fb.control('', [Validators.required, Validators.pattern(/^\d{3}[A-Z]{2}\d{4}$/)]));
      this.devisForm.addControl('nombreDeChevaux', this.fb.control('', [Validators.required, Validators.min(4), Validators.max(30)]));
      this.devisForm.addControl('ageVoiture', this.fb.control('', [Validators.required, Validators.min(0), Validators.max(30)]));
      this.devisForm.addControl('carburant', this.fb.control('', Validators.required));
    } else if (this.selectedType === 'Sante') {
      this.devisForm.addControl('numeroSecuriteSociale', this.fb.control('', [Validators.required, Validators.pattern(/^\d{8}$/)]));
      this.devisForm.addControl('age', this.fb.control('', [Validators.required, Validators.min(0), Validators.max(100)]));
      this.devisForm.addControl('sexe', this.fb.control('', Validators.required));
      this.devisForm.addControl('fumeur', this.fb.control('', Validators.required));
    } else if (this.selectedType === 'Habitation') {
      this.devisForm.addControl('adresse', this.fb.control('', [Validators.required, Validators.pattern(/^\d+\s?,\s?[A-Za-z\s]+,\s?[A-Za-z\s]+$/)]));
      this.devisForm.addControl('surface', this.fb.control('', [Validators.required, Validators.min(1), Validators.max(10000)]));
      this.devisForm.addControl('nombreDePieces', this.fb.control('', Validators.required));
    } else if (this.selectedType === 'Vie') {
      this.devisForm.addControl('beneficiaire', this.fb.control('', Validators.required));
      this.devisForm.addControl('duree', this.fb.control('', [Validators.required, Validators.min(1), Validators.max(10)]));
      this.devisForm.addControl('capital', this.fb.control('', Validators.required));
    }
  }

  openCreateForm() {
    this.isFormVisible = true;
  }

  closeForm() {
    this.isFormVisible = false;
    this.devisForm.reset();
    this.errorMessage = '';
  }

  createDevisAuto() {
    this.devisService.createDevisAuto(this.devisForm.value).subscribe(() => {
      this.loadDevis();
      this.closeForm();
      this.showOpportunityConfirm();

    }, error => {
      this.errorMessage = error.error.message || 'Erreur lors de la création du devis.';
    });
  }

  createDevisSante() {
    this.devisService.createDevisSante(this.devisForm.value).subscribe(() => {
      this.loadDevis();
      this.closeForm();
      this.showOpportunityConfirm();

    }, error => {
      this.errorMessage = error.error.message || 'Erreur lors de la création du devis.';
    });
  }

  createDevisHabitation() {
    this.devisService.createDevisHabitation(this.devisForm.value).subscribe(() => {
      this.loadDevis();
      this.closeForm();
      this.showOpportunityConfirm();

    }, error => {
      this.errorMessage = error.error.message || 'Erreur lors de la création du devis.';
    });
  }

  createDevisVie() {
    this.devisService.createDevisVie(this.devisForm.value).subscribe(() => {
      this.loadDevis();
      this.closeForm();
      this.showOpportunityConfirm();

    }, error => {
      this.errorMessage = error.error.message || 'Erreur lors de la création du devis.';
    });
  }

  viewDevis(id: number) {
    this.devisService.getDevisById(id).subscribe(devis => {
      this.selectedDevis = devis;
      this.isViewVisible = true;
    });
  }

  closeView() {
    this.isViewVisible = false;
    this.selectedDevis = null;
  }

  showOpportunityConfirm() {
    this.isOpportunityConfirmVisible = true;
  }
  
  closeModal(): void {
    this.isOpportunityConfirmVisible = false;
  }
  
  navigateToOpportunity(): void {
    this.router.navigate(['/opportunity']);
  }
  

  onSubmit() {
    if (this.devisForm.invalid) {
      return;
    }

    if (this.selectedType === 'Auto') {
      this.createDevisAuto();
    } else if (this.selectedType === 'Sante') {
      this.createDevisSante();
    } else if (this.selectedType === 'Habitation') {
      this.createDevisHabitation();
    } else if (this.selectedType === 'Vie') {
      this.createDevisVie();
    }
  }

  getTypeAssurance(type: number): string {
    switch (type) {
      case 0:
        return 'Auto';
      case 1:
        return 'Santé';
      case 2:
        return 'Habitation';
      case 3:
        return 'Vie';
      default:
        return 'Inconnu';
    }
  }
}