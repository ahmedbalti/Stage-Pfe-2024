import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResetPassword } from '../Models/ResetPassword';


export interface LoginResponse {
  token: string;
  expiration: string;
  accessToken: string;

}

export interface LoginResponse1 {
  isSuccess: boolean;
  message: string;
  response?: any; // Vous pouvez ajouter une interface pour représenter la réponse de succès si nécessaire
}

interface ResetPasswordModel {
  email: string;
  password: string;
  confirmPassword: string;
  token: string;
}

export interface LoginModel {
  Username: string;
  Password: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44366/api/Authentication'; 
  private baseUrl = 'https://localhost:44366/api/Authentication'; 

  private loginUrl = `${this.apiUrl}/login`;
  private login2FAUrl = `${this.apiUrl}/login2FA`;
  private refreshTokenUrl = `${this.apiUrl}/RefreshToken`;
  private forgotPasswordUrl = `${this.baseUrl}/forgot-password`;
  private resetPasswordUrl = `${this.apiUrl}/resetPassword`;
  constructor(private http: HttpClient) {}

  login(credentials: LoginModel): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<LoginResponse>(this.loginUrl, credentials, { headers });
  }

 
  loginWithOTP(code: string, userName: string): Observable<LoginResponse1> {
    const body = { code, userName };
    return this.http.post<LoginResponse1>('https://localhost:44366/api/Authentication/login2FA?code=ahmed&userName=509746',body);
  }

  refreshToken(): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.refreshTokenUrl, {});
  }
  
  forgotPassword(email: string): Observable<any> {
    const params = new HttpParams().set('email', email);
    return this.http.post(`${this.baseUrl}/forgot-password`, null, { params });
  }

  /*forgotPassword(email: string): Observable<any> {
    const params = new HttpParams().set('email', email);
    return this.http.post(this.forgotPasswordUrl, null, { params });
  }*/

  getResetPasswordModel(token: string, email: string): Observable<any> {
    const params = new HttpParams().set('token', token).set('email', email);
    return this.http.get(`${this.apiUrl}/reset-password`, { params });
  }

  resetPassword(model: ResetPassword): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.resetPasswordUrl, model, { headers });
  }
}
