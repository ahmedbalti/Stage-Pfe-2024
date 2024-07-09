import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-login-otp',
  templateUrl: './login-otp.component.html',
  styleUrls: ['./login-otp.component.css']
})
export class LoginOtpComponent {
  loginForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      otp: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, otp } = this.loginForm.value;
      this.authService.loginWithOTP(otp, username).subscribe(
        response => {
          console.log('Login successful', response);
          if (response.isSuccess) {
            // Stockez le jeton et redirigez l'utilisateur
            localStorage.setItem('accessToken', response.response.accessToken.token);
            localStorage.setItem('refreshToken', response.response.refreshToken.token);
            this.router.navigate(['/dashboard']); // Exemple de redirection
          } else {
            this.errorMessage = response.message;
          }
        },
        error => {
          console.error('Login failed', error);
          if (error.status === 404) {
            this.errorMessage = 'Endpoint not found. Please check your backend URL.';
          } else {
            this.errorMessage = error.message;
          }
        }
      );
    }
  }
}
