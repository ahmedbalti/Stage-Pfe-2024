import { Component } from '@angular/core';
import { SignupService } from '../../Services/signup.service';

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

  constructor(private signupService: SignupService){}

  onSubmit(){
    this.signupService.signup(this.userInput)
    .subscribe(
      (response: any) => {
        console.log('Signup successful',response);
      },
      (error) => {
        console.error('Signup failed', error);
      }
    );
  }

}
