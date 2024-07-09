import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';

import { AppComponent } from './app.component';
import { Signup1Component } from './signup1/signup.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

import { HomeModule } from './home/home.module';
import { Login1Component } from './login1/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { LoginOtpComponent } from './Components/login-otp/login-otp.component';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { AuthService } from './Services/auth.service';
import { AuthInterceptor } from './Interceptors/auth.interceptor';
import { ContractsComponent } from './Components/contracts/contracts.component';
import { SetTokenComponent } from './Components/set-token/set-token.component';
import { SinistresComponent } from './Components/sinistres/sinistres.component';
import { DevisComponent } from './Components/devis/devis.component';
import { OpportunityComponent } from './Components/opportunity/opportunity.component';
import { TicketComponent } from './Components/ticket/ticket.component';

@NgModule({
  declarations: [
    AppComponent,
    Signup1Component,
    LandingComponent,
    ProfileComponent,
    NavbarComponent,
    FooterComponent,
    Login1Component,
    SignupComponent,
    LoginComponent,
    LoginOtpComponent,
    DashboardComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ContractsComponent,
    SetTokenComponent,
    SinistresComponent,
    DevisComponent,
    OpportunityComponent,
    TicketComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    HomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
  ],
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
