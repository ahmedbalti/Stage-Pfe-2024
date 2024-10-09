/*import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login-otp',
  templateUrl: './login-otp.component.html',
  styleUrls: ['./login-otp.component.css']
})
export class LoginOtpComponent {
  otp: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    const username = localStorage.getItem('username');
    if (!username) {
      this.errorMessage = 'No username found. Please login first.';
      return;
    }

    this.authService.verifyOtp({ Username: username, OTP: this.otp }).subscribe({
      next: (response) => {
        alert('Login link has been sent to your email.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.errorMessage = 'Invalid OTP';
      }
    });
  }
}*/