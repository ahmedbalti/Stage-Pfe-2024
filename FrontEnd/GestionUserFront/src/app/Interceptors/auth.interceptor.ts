// auth.interceptor.ts
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from '../Services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          // Vérifiez si le token est expiré et rafraîchissez-le automatiquement
          return this.handle401Error(request, next);
        }
        return throwError(error);
      })
    );
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Rafraîchissez le token automatiquement
    return this.authService.refreshToken().pipe(
      switchMap((response) => {
        // Réessayez la requête initiale avec le nouveau token
        const newRequest = request.clone({
          setHeaders: {
            Authorization: `Bearer ${response.accessToken}`
          }
        });
        return next.handle(newRequest);
      }),
      catchError((error) => {
        // Gérer l'erreur de rafraîchissement du token
        console.error('Erreur lors du rafraîchissement du token:', error);
        return throwError(error);
      })
    );
  }
}

