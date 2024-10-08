// forgot-password.component.ts
/*import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
})
export class ForgotPasswordComponent {
  forgotPasswordForm: UntypedFormGroup;
  message: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: UntypedFormBuilder, private authService: AuthService) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSubmit(): void {
    this.message = null;
    this.errorMessage = null;
    const email = this.forgotPasswordForm.get('email')?.value;
    this.authService.forgotPassword(email).subscribe({
      next: (response) => {
        this.message = response.message;
      },
      error: (error) => {
        this.errorMessage = error.error.message || 'An error occurred';
      },
    });
  }
}
*/

/*import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
})
export class ForgotPasswordComponent {
  forgotPasswordForm: FormGroup;
  message: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private route: ActivatedRoute
  ) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });

    // Récupération de l'email et du token depuis l'URL
    this.route.queryParams.subscribe((params) => {
      if (params['email'] && params['token']) {
        this.forgotPasswordForm.get('email')?.setValue(params['email']);
      }
    });
  }

  onSubmit(): void {
    this.message = null;
    this.errorMessage = null;
    const email = this.forgotPasswordForm.get('email')?.value;
    this.authService.forgotPassword(email).subscribe({
      next: (response) => {
        this.message = response.message;
      },
      error: (error) => {
        this.errorMessage = error.error.message || 'An error occurred';
      },
    });
  }
}*/
