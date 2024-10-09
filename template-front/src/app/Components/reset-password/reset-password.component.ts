// reset-password.component.ts
/*import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';
import { ResetPassword } from 'src/app/Models/ResetPassword';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: UntypedFormGroup;
  token: string = '';
  email: string = '';
  message: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: UntypedFormBuilder,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.resetPasswordForm = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
      this.email = params['email'];
    });
  }

  onSubmit(): void {
    if (this.resetPasswordForm.valid) {
      const password = this.resetPasswordForm.get('password')?.value;
      const confirmPassword = this.resetPasswordForm.get('confirmPassword')?.value;

      if (password !== confirmPassword) {
        this.errorMessage = "Passwords do not match!";
        return;
      }

      const resetPasswordData: ResetPassword = {
        token: this.token,
        email: this.email,
        password: password,
        confirmPassword: confirmPassword // Ajouter confirmPassword ici
      };

      this.authService.resetPassword(resetPasswordData).subscribe({
        next: (response) => {
          this.message = response.message;
          this.router.navigate(['/login']); // Redirige vers la page de login après la réinitialisation
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'An error occurred';
        }
      });
    }
  }
}
*/