import { Component, OnInit } from '@angular/core';
import { AuthService, LoginModel } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sections',
  templateUrl: './sections.component.html',
  styleUrls: ['./sections.component.css']
})
export class SectionsComponent implements OnInit {
  focus;
  focus1;
  loginData: LoginModel = {
    Username: '',
    Password: ''
  };
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  onSubmit() {
    // Validation: Check if both Username and Password are not empty
    if (!this.loginData.Username || !this.loginData.Password) {
      this.errorMessage = 'Both username and password are required.';
      this.successMessage = '';
      return;
    }

    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        // Show success message
        this.successMessage = 'Login successful!';
        this.errorMessage = ''; // Clear any previous error messages

        // Navigate to home or another page after successful login
        this.router.navigate(['/home']);

        // Clear input fields after 5 seconds
        setTimeout(() => {
          this.loginData.Username = '';
          this.loginData.Password = '';
          this.successMessage = ''; // Clear the success message after 5 seconds
        }, 3000);
      },
      error: (err) => {
        // Show error message
        this.errorMessage = 'Invalid login credentials';
        this.successMessage = ''; // Clear any previous success messages
      }
    });
  }
}
