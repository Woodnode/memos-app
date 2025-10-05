import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Memo } from '../Models/memo';
import { MemoCarteComponent } from '../memo-carte/memo-carte.component';

@Component({
  selector: 'app-memo-liste',
  standalone: true,
  imports: [CommonModule, MemoCarteComponent],
  templateUrl: './memo-liste.component.html',
  styleUrl: './memo-liste.component.css'
})
export class MemoListeComponent {
  @Input() memos: Memo[] = [];
  @Input() memoASupprimer: number | null = null;
  
  @Output() supprimerMemo = new EventEmitter<number>();
  @Output() confirmerSuppression = new EventEmitter<void>();
  @Output() annulerSuppression = new EventEmitter<void>();

  onSupprimerMemo(idMemo: number): void {
    this.supprimerMemo.emit(idMemo);
  }

  onConfirmerSuppression(): void {
    this.confirmerSuppression.emit();
  }

  onAnnulerSuppression(): void {
    this.annulerSuppression.emit();
  }

  formaterDate(date: Date): string {
    return new Date(date).toLocaleDateString('fr-FR', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }
}
