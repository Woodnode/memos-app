import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MemoService } from '../Services/memo.service';
import { CompteService } from '../Services/compte.service';
import { Memo } from '../Models/memo';
import { ToastrService } from 'ngx-toastr';
import { MemoListeComponent } from '../memo-liste/memo-liste.component';

@Component({
  selector: 'app-accueil',
  standalone: true,
  imports: [CommonModule, MemoListeComponent],
  templateUrl: './accueil.component.html',
  styleUrl: './accueil.component.css'
})
export class AccueilComponent implements OnInit {
  memos: Memo[] = [];
  nomUtilisateur: string = '';
  memoASupprimer: number | null = null;

  constructor(
    private memoService: MemoService,
    private compteService: CompteService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.nomUtilisateur = this.compteService.obtenirNomUtilisateur() || '';
    this.chargerMemos();
  }

  chargerMemos(): void {
    this.memoService.obtenirMemos().subscribe({
      next: (memos) => {
        this.memos = memos;
        if (memos.length === 0) {
          this.toastr.info('Aucun mémo trouvé');
        }
      },
      error: (error) => {
        this.toastr.error('Erreur lors du chargement des mémos');
      }
    });
  }

  allerVersNouveauMemo(): void {
    this.router.navigate(['/memo/nouveau']);
  }

  supprimerMemo(idMemo: number): void {
    this.memoASupprimer = idMemo;
  }

  confirmerSuppression(): void {
    if (this.memoASupprimer !== null) {
      this.memoService.supprimerMemo(this.memoASupprimer).subscribe({
        next: () => {
          this.toastr.success('Mémo supprimé avec succès');
          this.chargerMemos();
          this.memoASupprimer = null;
        },
        error: (error) => {
          this.toastr.error('Erreur lors de la suppression du mémo');
          this.memoASupprimer = null;
        }
      });
    }
  }

  annulerSuppression(): void {
    this.memoASupprimer = null;
    this.toastr.clear();
  }

}