import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '../../Services/feedback.service';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css']
})
export class FeedbackListComponent implements OnInit {
  comments: any[] = [];
  averageRating: number = 0; // Init avec une valeur par défaut
  ratingDistribution: any[] = [];
  totalFeedbacks: number = 0;

  constructor(private feedbackService: FeedbackService) {}

  ngOnInit() {
    this.getComments();
    this.getAverageRating();
    this.getRatingDistribution();
  }

  getAverageRating() {
    this.feedbackService.getAverageRating().subscribe((response: any) => {
      if (response && response.averageRating !== undefined) {
        this.averageRating = response.averageRating || 0;
        console.log('Note Moyenne:', this.averageRating); // Vérifier les données ici
      } else {
        console.log('Erreur dans la réception de la note moyenne', response);
      }
    });
  }
  

  // Récupère et trie les commentaires par nombre d'étoiles (du plus élevé au plus bas)
  getComments() {
    this.feedbackService.getAllComments().subscribe((response: any) => {
      this.comments = response
        .map((comment: any) => {
          return {
            ...comment,
            profileImageUrl: comment.profileImage ? `https://localhost:7021${comment.profileImage}` : 'assets/img/default-avatar.png'
          };
        })
        .sort((a: any, b: any) => new Date(b.dateCreated).getTime() - new Date(a.dateCreated).getTime()); // Tri par date, du plus récent au plus ancien
      });
  }
  
  // Récupère la distribution des notes et calcule le total des feedbacks
  getRatingDistribution() {
    this.feedbackService.getRatingDistribution().subscribe((response: any) => {
      this.ratingDistribution = response.sort((a: any, b: any) => b.rating - a.rating);
      this.totalFeedbacks = this.ratingDistribution.reduce((sum, item) => sum + item.count, 0); // Calcul du total
    });
  }
}
