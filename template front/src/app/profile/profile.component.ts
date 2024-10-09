import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService, UpdateProfileModel, UserProfile } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  userProfile: UserProfile | null = null;
  selectedFile: File | null = null;  // Pour gérer l'image sélectionnée
  imageUrl: string | ArrayBuffer | null = null;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.profileForm = this.fb.group({
      userName: [''],
      email: [''],
      phoneNumber: [''],
      address: ['']
    });
  }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.authService.getProfile().subscribe({
      next: (profile) => {
        this.userProfile = profile;
        // Assurez-vous que l'URL de l'image est complète
        this.imageUrl = profile.profileImage ? `https://localhost:7021${profile.profileImage}` : 'assets/img/default-avatar.png';
        this.profileForm.patchValue({
          userName: profile.userName,
          email: profile.email,
          phoneNumber: profile.phoneNumber,
          address: profile.address
        });
      },
      error: () => {
        this.errorMessage = 'Error loading profile';
      }
    });
  }
  

  onFileSelected(event: any): void {
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e) => this.imageUrl = e.target?.result;
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      const updateProfile: UpdateProfileModel = this.profileForm.value;
      this.authService.updateProfile(updateProfile).subscribe({
        next: () => {
          this.successMessage = 'Profile updated successfully';
          // Rechargez le profil pour obtenir les données mises à jour
          this.loadProfile();
        },
        error: () => {
          this.errorMessage = 'Error updating profile';
        }
      });
    }
  }
  

  onUpload(): void {
    if (this.selectedFile) {
      this.authService.uploadProfileImage(this.selectedFile).subscribe({
        next: (response) => {
          this.successMessage = 'Image uploaded successfully';
          // Rechargez le profil pour obtenir l'URL mise à jour de l'image
          this.loadProfile();
        //  this.selectedFile = null; // Réinitialisez le fichier sélectionné
        },
        error: () => {
          this.errorMessage = 'Error uploading image';
        }
      });
    }
  }
  
}
/*
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService, UpdateProfileModel, UserProfile } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  userProfile: UserProfile | null = null;
  selectedFile: File | null = null;  // Pour gérer l'image sélectionnée
  imageUrl: string | ArrayBuffer | null = null;
  successMessage: string = '';
  errorMessage: string = '';
  isEditModalVisible: boolean = false;  // Pour gérer l'affichage de la modale

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.profileForm = this.fb.group({
      userName: [''],
      email: [''],
      phoneNumber: [''],
      address: ['']
    });
  }

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.authService.getProfile().subscribe({
      next: (profile) => {
        this.userProfile = profile;
        this.imageUrl = profile.profileImage;
        this.profileForm.patchValue({
          userName: profile.userName,
          email: profile.email,
          phoneNumber: profile.phoneNumber,
          address: profile.address
        });
      },
      error: () => {
        this.errorMessage = 'Error loading profile';
      }
    });
  }

  openEditProfile(): void {
    this.isEditModalVisible = true;
  }

  closeEditProfile(): void {
    this.isEditModalVisible = false;
  }

  onFileSelected(event: any): void {
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e) => this.imageUrl = e.target?.result;
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      const updateProfile: UpdateProfileModel = this.profileForm.value;
      this.authService.updateProfile(updateProfile, this.selectedFile).subscribe({
        next: () => {
          this.successMessage = 'Profile updated successfully';
          this.closeEditProfile();
          this.loadProfile();  // Recharger les données du profil
        },
        error: () => {
          this.errorMessage = 'Error updating profile';
        }
      });
    }
  }
}
*/