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
          console.log(response);
          if (response.isSuccess) {
            // Redirection ou action à effectuer en cas de succès
            // Par exemple, stocker le jeton dans le stockage local
          } else {
            this.errorMessage = response.message;
          }
        },
        error => {
          this.errorMessage = "Une erreur s'est produite. Veuillez réessayer.";
          console.error('Erreur:', error);
        }
      );
  }
}
