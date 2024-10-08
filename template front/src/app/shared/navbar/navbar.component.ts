import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public isCollapsed = true;
  isAuthenticated = false;
  userRole: string | null = null;
  profileImageUrl: string | null = null;  // URL de l'image de profil

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit() {
    this.authService.currentUser.subscribe(user => {
      this.isAuthenticated = !!user; // Vérifie si un utilisateur est authentifié
      this.userRole = user ? user.role : null; // Récupère le rôle si l'utilisateur est authentifié

   //marra jeya hedhi entre commentaire
      if (this.isAuthenticated) {
        this.authService.getProfile().subscribe(profile => {
          // Ajoutez le préfixe URL si nécessaire pour accéder à l'image.
          this.profileImageUrl = profile.profileImage ? `https://localhost:7021${profile.profileImage}` : 'assets/img/default-avatar.png';
        });
      }
// hatta lhouni

        this.authService.profileImageUrlSubject.subscribe(imageUrl => {
          this.profileImageUrl = imageUrl;
        });
      
    });
  }

   /* if (this.isAuthenticated) {
      // Charge le profil de l'utilisateur
      this.authService.getProfile().subscribe(profile => {
        this.profileImageUrl = profile.profileImage || 'assets/img/default-avatar.png';  // Utilise une image par défaut si l'utilisateur n'a pas de photo
      });
    } else {
      this.profileImageUrl = null;  // Réinitialise si l'utilisateur n'est pas authentifié
    }
  });
}*/

  logout() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
