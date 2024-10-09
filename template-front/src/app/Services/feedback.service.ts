import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private apiUrl = 'https://localhost:7021/api/feedback'; // URL de l'API backend

  constructor(private http: HttpClient) {}

  // Méthode pour ajouter un feedback
  addFeedback(feedback: { rating: number, comment: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}`, feedback);
  }

  // Méthode pour récupérer la liste des commentaires
  getAllComments(): Observable<any> {
    return this.http.get(`${this.apiUrl}/comments`);
  }

  // Méthode pour obtenir la moyenne des ratings
  getAverageRating(): Observable<any> {
    return this.http.get(`${this.apiUrl}/average-rating`);
  }

  // Méthode pour obtenir la distribution des ratings
  getRatingDistribution(): Observable<any> {
    return this.http.get(`${this.apiUrl}/rating-distribution`);
  }
}
