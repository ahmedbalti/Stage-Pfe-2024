import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { Signup1Component } from './signup1/signup.component';
import { LandingComponent } from './landing/landing.component';
import { Login1Component } from './login1/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { LoginOtpComponent } from './Components/login-otp/login-otp.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { ContractsComponent } from './Components/contracts/contracts.component';
import { SetTokenComponent } from './Components/set-token/set-token.component';
import { SinistresComponent } from './Components/sinistres/sinistres.component';
import { DevisComponent } from './Components/devis/devis.component';
import { OpportunityComponent } from './Components/opportunity/opportunity.component';
import { TicketComponent } from './Components/ticket/ticket.component';


const routes: Routes =[
    { path: 'home',             component: HomeComponent },
    { path: 'user-profile',     component: ProfileComponent },
    { path: 'register',           component: Signup1Component },
    { path: 'assurances',          component: LandingComponent },
    { path: 'login1',          component: Login1Component },
    
  {path: 'signup', component: SignupComponent},
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'loginOtp', component: LoginOtpComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'setToken', component: SetTokenComponent },
  { path: 'sinistres', component: SinistresComponent },
  { path: 'devis', component: DevisComponent },
  { path: 'opportunity', component: OpportunityComponent },
  { path: 'ticket', component: TicketComponent },



  { path: 'contracts', component: ContractsComponent },

    { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
      useHash: true
    })
  ],
  exports: [RouterModule
  ],
})
export class AppRoutingModule { }
