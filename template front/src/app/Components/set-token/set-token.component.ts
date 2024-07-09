import { Component } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-set-token',
  templateUrl: './set-token.component.html',
  styleUrls: ['./set-token.component.css']
})
export class SetTokenComponent {
  tokenForm: FormGroup;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.tokenForm = this.fb.group({
      token: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.tokenForm.valid) {
      const { token } = this.tokenForm.value;
      this.authService.setTokenManually(token);
      this.successMessage = 'Token set successfully!';
    }
  }
}
