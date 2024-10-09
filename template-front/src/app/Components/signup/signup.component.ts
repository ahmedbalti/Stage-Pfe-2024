import { Component } from '@angular/core';
import { SignupService } from 'src/app/Services/signup.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  userInput = {
    username: '',
    email: '',
    password:'',
    roles: [
      "Client"
    ]
  };

  constructor(private signupService: SignupService, private router: Router){}

  onSubmit(){
    this.signupService.signup(this.userInput)
    .subscribe(
      (response: any) => {
        console.log('Signup successful',response);
        this.router.navigate(['/login']);

      },
      (error) => {
        console.error('Signup failed', error);
      }
    );
  }

}
