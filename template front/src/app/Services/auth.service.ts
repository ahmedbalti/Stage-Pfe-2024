import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode'; // Import corrigé

export interface LoginResponse {
  token: string;
  expiration: string;
  role?: string;
}

export interface LoginModel {
  Username: string;
  Password: string;
}

export interface UserProfile {
  userName: string;
  email: string;
  phoneNumber: string;
  address: string;
  profileImage?: string;  // Ajouter l'image de profil
  roles: string[];
}

export interface UpdateProfileModel {
  userName: string;
  email: string;
  phoneNumber: string;
  address: string;
}
export interface UserProfile {
  userName: string;
  email: string;
  phoneNumber: string;
  address: string;
  roles: string[];
}

export interface UpdateProfileModel {
  userName: string;
  email: string;
  phoneNumber: string;
  address: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7021/api/Authentication'; 
  private tokenKey = 'accessToken';
  private userRoleKey = 'userRole';

   // Ajouter un BehaviorSubject pour gérer le changement d'image
   public profileImageUrlSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
   public profileImageUrl$ = this.profileImageUrlSubject.asObservable(); // Observable pour suivre les changements d'image


  private currentUserSubject: BehaviorSubject<LoginResponse | null>;
  public currentUser: Observable<LoginResponse | null>;

  constructor(private http: HttpClient) {
    const storedUser = JSON.parse(localStorage.getItem('currentUser')!);
    this.currentUserSubject = new BehaviorSubject<LoginResponse | null>(storedUser);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(credentials: LoginModel): Observable<LoginResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials, { headers }).pipe(
      map(user => {
        console.log('Login response:', user);

        // Stocker le token
        localStorage.setItem('currentUser', JSON.stringify(user));
        localStorage.setItem(this.tokenKey, user.token);

        // Extraire le rôle du token JWT
        const decodedToken: any = this.decodeToken(user.token);
        const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        
        console.log('Extracted role from token:', userRole);

        // Stocker le rôle extrait
        localStorage.setItem(this.userRoleKey, userRole);
        console.log('Stored role:', userRole);

        // Mise à jour de l'utilisateur actuel
        this.currentUserSubject.next({...user, role: userRole});

            // Charger le profil de l'utilisateur pour obtenir l'image
            this.getProfile().subscribe(profile => {
              const profileImageUrl = profile.profileImage ? `https://localhost:7021${profile.profileImage}` : 'assets/img/default-avatar.png';
              this.profileImageUrlSubject.next(profileImageUrl); // Met à jour l'image de profil
            });
        return {...user, role: userRole};
      }),
      catchError(this.handleError)
    );
  }

  getToken(): string {
    return localStorage.getItem(this.tokenKey) || '';
  }

  getUserRole(): string {
    const role = localStorage.getItem(this.userRoleKey) || '';
    console.log('Retrieved role:', role);
    return role;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('currentUser');
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userRoleKey);
    this.currentUserSubject.next(null); // Mise à jour de l'état après déconnexion
  }

  // Fonction pour décoder le JWT
  public decodeToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (Error) {
      return null;
    }
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }

 /*getProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.apiUrl}/profile`);
  }*/


  // Quand on charge le profil, on met à jour l'image de profil dans le BehaviorSubject
  getProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.apiUrl}/profile`).pipe(
      map((profile) => {
        const imageUrl = profile.profileImage ? `https://localhost:7021${profile.profileImage}` : 'assets/img/default-avatar.png';
        this.profileImageUrlSubject.next(imageUrl); // Met à jour l'URL de l'image
        return profile;
      })
    );
  }

  updateProfile(profile: UpdateProfileModel, imageFile?: File): Observable<any> {
    const formData = new FormData();
    formData.append('userName', profile.userName);
    formData.append('email', profile.email);
    formData.append('phoneNumber', profile.phoneNumber);
    formData.append('address', profile.address);
  
    if (imageFile) {
      formData.append('profileImage', imageFile);  // Ajouter l'image si elle est présente
    }
  
    return this.http.put(`${this.apiUrl}/profile`, formData);  // Pas besoin de spécifier le 'Content-Type'
  }
  

 /* uploadProfileImage(imageFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('profileImage', imageFile);
  
    return this.http.put(`${this.apiUrl}/profile/upload-image`, formData); // Ne définissez pas 'Content-Type'
  }*/

    uploadProfileImage(imageFile: File): Observable<any> {
      const formData = new FormData();
      formData.append('profileImage', imageFile);
  
      return this.http.put(`${this.apiUrl}/profile/upload-image`, formData).pipe(
        map((response: any) => {
          const newImageUrl = `https://localhost:7021${response.imagePath}`;
          this.profileImageUrlSubject.next(newImageUrl); // Met à jour l'URL de l'image après téléchargement
          return response;
        })
      );
    }

  getProfileImage(): Observable<string> {
    return this.getProfile().pipe(
      map(profile => profile.profileImage || '')  // Récupère juste le chemin de l'image
    );
  }
  
  

}



