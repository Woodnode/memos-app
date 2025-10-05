import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MemoService } from '../Services/memo.service';
import { CompteService } from '../Services/compte.service';
import { MemoCreationRequete } from '../Models/memo';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-memo-nouveau',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './memo-nouveau.component.html',
  styleUrl: './memo-nouveau.component.css'
})
export class MemoNouveauComponent {
  nouveauMemo: MemoCreationRequete = {
    titre: '',
    description: '',
    nomUtilisateur: ''
  };

  constructor(
    private memoService: MemoService,
    private compteService: CompteService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ajouterMemo(): void {
    this.memoService.creerMemo(this.nouveauMemo).subscribe({
        next: (message) => {
            this.toastr.success('Mémo créé avec succès');
            this.router.navigate(['/home']);
        },
        error: (error) => {
            this.toastr.error('Erreur lors de la création du mémo');
        }
    });
}

  annuler(): void {
    this.router.navigate(['/home']);
  }
}
