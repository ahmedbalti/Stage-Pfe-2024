import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResetPassword } from '../Models/ResetPassword';

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
  private apiUrl = 'https://localhost:7021/api/Authentication'; 
  private currentUserSubject: BehaviorSubject<LoginResponse | null>;
  public currentUser: Observable<LoginResponse | null>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<LoginResponse | null>(JSON.parse(localStorage.getItem('currentUser')!));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  
  
  setTokenManually(token: string) {
    localStorage.setItem('accessToken', token);
    const user = { accessToken: token, expiration: '', token: '' };
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }
  
  loginWithOTP(code: string, userName: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/authentication/login-2FA`, { code, userName });
  }

  login(credentials: LoginModel): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials, { headers }).pipe(
      map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      })
    );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  refreshToken(): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/Refresh-Token`, {});
  }

  forgotPassword(email: string): Observable<any> {
    const params = new HttpParams().set('email', email);
    return this.http.post(`${this.apiUrl}/forgot-password`, null, { params });
  }

  getResetPasswordModel(token: string, email: string): Observable<any> {
    const params = new HttpParams().set('token', token).set('email', email);
    return this.http.get(`${this.apiUrl}/reset-password`, { params });
  }

  resetPassword(model: ResetPassword): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiUrl}/reset-password`, model, { headers });
  }

}
