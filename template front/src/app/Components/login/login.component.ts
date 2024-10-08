import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginModel } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData: LoginModel = {
    Username: '',
    Password: ''
  };

  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        // After successful login, navigate to a different page, e.g., dashboard
        this.router.navigate(['/home']); 
      },
      error: (err) => {
        this.errorMessage = 'Invalid login credentials';
      }
    });
  }
}
