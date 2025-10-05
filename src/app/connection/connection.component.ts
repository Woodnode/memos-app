import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CompteService } from '../Services/compte.service';
import { CompteConnexionRequete } from '../Models/compte';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-connection',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './connection.component.html',
  styleUrl: './connection.component.css'
})
export class ConnectionComponent {
  infoConnexion: CompteConnexionRequete = {
    nomUtilisateur: '',
    motDePasse: ''
  };

  constructor(
    private compteService: CompteService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  seConnecter(): void {
    this.compteService.seConnecter(this.infoConnexion).subscribe({
        next: (reponse) => {
            this.toastr.success('Connexion réussie');
            this.router.navigate(['/home']);
        },
        error: (error) => {
            this.toastr.error(error.error || 'Une erreur est survenue lors de la connexion');
        }
    });
}

  allerVersInscription(): void {
    this.router.navigate(['/nouveauCompte']);
  }

}