import { Component } from '@angular/core';
import { FeedbackService } from '../../Services/feedback.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent {
  rating: number = 0;
  comment: string = '';

  constructor(private feedbackService: FeedbackService) {}

  onSubmit(feedbackForm: NgForm) {
    const feedback = {
      rating: this.rating,
      comment: this.comment
    };

    this.feedbackService.addFeedback(feedback).subscribe(response => {
      alert('Feedback ajouté avec succès !');
      feedbackForm.reset();
    }, error => {
      alert('Erreur lors de l\'ajout du feedback');
    });
  }
}
