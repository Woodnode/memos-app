import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CompteService } from '../Services/compte.service';

export const connexionGuard: CanActivateFn = (route, state) => {
  const compteService = inject(CompteService);
  const toastr = inject(ToastrService);
  const routeur = inject(Router);

  if (!compteService.estConnecte()) {
    toastr.error('La page demandée n\'est pas accessible. Veuillez vous connecter');
    routeur.navigateByUrl('/');
    return false;
  }
  return true;
};