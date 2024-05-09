import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { LoginOtpComponent } from './Components/login-otp/login-otp.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';


const routes: Routes = [

  {path: 'signup', component: SignupComponent},
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },

  { path: 'loginOtp', component: LoginOtpComponent },

  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
