import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginResponse {
  token: string;
  expiration: string;
  accessToken: string;

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
  private loginUrl = `${this.apiUrl}/login`;

  private refreshTokenUrl = `${this.apiUrl}/Refresh-Token`;


  constructor(private http: HttpClient) {}

  login(credentials: LoginModel): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<LoginResponse>(this.loginUrl, credentials, { headers });
  }

  loginWithOTP(code: string, userName: string) {
    return this.http.post<any>(`${this.apiUrl}/login-2FA?code=${code}&userName=${userName}`, {});
  }

  refreshToken(): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.refreshTokenUrl, {});
  }
 
}
