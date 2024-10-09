import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';

import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

import { HomeModule } from './home/home.module';
import { Login1Component } from './login1/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
//import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { AuthService } from './Services/auth.service';
import { ContractsComponent } from './Components/contracts/contracts.component';
import { SinistresComponent } from './Components/sinistres/sinistres.component';
import { DevisComponent } from './Components/devis/devis.component';
import { OpportunityComponent } from './Components/opportunity/opportunity.component';
import { TicketComponent } from './Components/ticket/ticket.component';
import { TicketManagementComponent } from './Components/ticket-management/ticket-management.component';
import { OpportunityManagementComponent } from './Components/opportunity-management/opportunity-management.component';
import { ContractsManagementComponent } from './Components/contracts-management/contracts-management.component';
import { SinistresManagementComponent } from './Components/sinistres-management/sinistres-management.component';
import { Navbar1Component } from './shared/navbar1/navbar1.component';
import { Navbar2Component } from './shared/navbar2/navbar2.component';
//import { SharedModule } from './shared.module';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './Interceptors/auth-interceptor.service';
import { FeedbackComponent } from './Components/feedback/feedback.component';
import { NgxStarRatingModule } from 'ngx-star-rating';
import { FeedbackListComponent } from './Components/feedback-list/feedback-list.component';


@NgModule({
  declarations: [ 
    AppComponent,
    LandingComponent,
    ProfileComponent,
    NavbarComponent,
    Navbar1Component,
    Navbar2Component,
    FooterComponent,
    Login1Component,
    SignupComponent,
    LoginComponent,
    DashboardComponent,
    //ForgotPasswordComponent,
    ContractsComponent,
    SinistresComponent,
    DevisComponent,
    OpportunityComponent,
    TicketComponent,
    TicketManagementComponent,
    OpportunityManagementComponent,
    ContractsManagementComponent,
    SinistresManagementComponent,
    FeedbackComponent,
    FeedbackListComponent
    //HomeComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
   // HomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    NgxStarRatingModule
        //SharedModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],  // Ajoutez cette ligne si nécessaire

  providers: [
    AuthService, 
    
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
