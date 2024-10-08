import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { LandingComponent } from './landing/landing.component';
import { Login1Component } from './login1/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
//import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { ContractsComponent } from './Components/contracts/contracts.component';
import { SinistresComponent } from './Components/sinistres/sinistres.component';
import { DevisComponent } from './Components/devis/devis.component';
import { OpportunityComponent } from './Components/opportunity/opportunity.component';
import { TicketComponent } from './Components/ticket/ticket.component';
import { TicketManagementComponent } from './Components/ticket-management/ticket-management.component';
import { OpportunityManagementComponent } from './Components/opportunity-management/opportunity-management.component';
import { ContractsManagementComponent } from './Components/contracts-management/contracts-management.component';
import { SinistresManagementComponent } from './Components/sinistres-management/sinistres-management.component';
import { FeedbackComponent } from './Components/feedback/feedback.component';
import { FeedbackListComponent } from './Components/feedback-list/feedback-list.component';


const routes: Routes =[
    { path: 'home',             component: HomeComponent },
    { path: 'userProfile',     component: ProfileComponent },
    { path: 'assurances',          component: LandingComponent },
    { path: 'login1',          component: Login1Component },
    
  {path: 'signup', component: SignupComponent},
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  //{ path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'sinistres', component: SinistresComponent },
  { path: 'devis', component: DevisComponent },
  { path: 'opportunity', component: OpportunityComponent },
  { path: 'ticket', component: TicketComponent },
  { path: 'ticketManagement', component: TicketManagementComponent },
  { path: 'opportunityManagement', component: OpportunityManagementComponent },
  { path: 'sinistreManagement', component: SinistresManagementComponent },
  { path: 'feedback', component: FeedbackComponent },
  { path: 'feedbackList', component: FeedbackListComponent },





  { path: 'contracts', component: ContractsComponent },
  { path: 'contractsManagement', component: ContractsManagementComponent },

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
