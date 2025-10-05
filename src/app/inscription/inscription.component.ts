import { Component} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CompteService } from '../Services/compte.service';
import { CompteCreationRequete } from '../Models/compte';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-inscription',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './inscription.component.html',
  styleUrl: './inscription.component.css'
})
export class InscriptionComponent {
  
  compte: CompteCreationRequete = {
    nomUtilisateur: '',
    motDePasse: ''
  };

  constructor(private compteService: CompteService, private toastr: ToastrService,private router: Router) {}

  sInscrire(): void {
    this.compteService.sInscrire(this.compte).subscribe({
        next: (message) => {
            this.toastr.success(message + '. Connectez-vous pour accéder à vos mémos');
            this.annuler();
        },
        error: (error) => {
          this.toastr.error(error.error || 'Une erreur est survenue lors de la création du compte');
      }
    });
}

  annuler(): void {
    this.router.navigate(['/']);
  }

}