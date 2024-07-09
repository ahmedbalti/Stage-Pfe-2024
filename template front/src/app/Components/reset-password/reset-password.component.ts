// src/app/reset-password/reset-password.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ResetPassword } from 'src/app/Models/ResetPassword';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: UntypedFormGroup;
  isSuccess = false;
  isError = false;
  message = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: UntypedFormBuilder,
    private authService: AuthService
  ) {
    this.resetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
      token: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    const token = this.route.snapshot.queryParamMap.get('token');
    const email = this.route.snapshot.queryParamMap.get('email');

    if (token && email) {
      this.authService.getResetPasswordModel(token, email).subscribe(
        (response: any) => {
          this.resetPasswordForm.patchValue({
            email: response.model.email,
            token: response.model.token
          });
        },
        (error) => {
          this.isError = true;
          this.message = 'Invalid token or email';
        }
      );
    }
  }

  onSubmit(): void {
    if (this.resetPasswordForm.invalid) {
      this.message = 'Invalid form data';
      this.isError = true;
      return;
    }

    const model: ResetPassword = this.resetPasswordForm.value;
    if (model.password !== model.confirmPassword) {
      this.message = "Passwords don't match";
      this.isError = true;
      return;
    }

    this.authService.resetPassword(model).subscribe(
      (response: any) => {
        this.isSuccess = true;
        this.isError = false;
        this.message = response.message || 'Password has been reset successfully.';
        // Redirect after a delay
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      },
      (error) => {
        this.isSuccess = false;
        this.isError = true;
        this.message = error.error.message || 'Error resetting password';
      }
    );
  }
}
