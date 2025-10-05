import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompteService } from '../Services/compte.service';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})

export class NavComponent {
  title = 'MemosWeb';

  constructor(private compteService: CompteService, private router: Router) {}

  estConnecte(): boolean {
    return this.compteService.estConnecte();
  }

  seDeconnecter(): void {
    this.compteService.seDeconnecter();
    this.router.navigate(['/']);
  }
}