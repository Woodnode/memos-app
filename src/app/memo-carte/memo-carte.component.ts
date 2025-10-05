import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Memo } from '../Models/memo';

@Component({
  selector: 'app-memo-carte',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './memo-carte.component.html',
  styleUrl: './memo-carte.component.css'
})
export class MemoCarteComponent {
  @Input() memo!: Memo;
  @Output() supprimer = new EventEmitter<number>();

  onSupprimer(): void {
    this.supprimer.emit(this.memo.id);
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
