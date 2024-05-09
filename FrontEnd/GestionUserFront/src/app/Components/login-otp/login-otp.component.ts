import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login-otp',
  templateUrl: './login-otp.component.html',
  styleUrls: ['./login-otp.component.css']
})
export class LoginOtpComponent {

  code: string = '';
  userName: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService) { }

  onSubmit() {
    this.authService.loginWithOTP(this.code, this.userName)
      .subscribe(
        response => {
          // Gérer la réponse du backend
          console.log(response);
        },
        error => {
          this.errorMessage = error.error.message;
          console.error('Erreur:', error);
        }
      );
  }
}
