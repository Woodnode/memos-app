import { Routes } from '@angular/router';
import { connexionGuard } from './gardes/connexion.guard';
import { ConnectionComponent } from './connection/connection.component';
import { InscriptionComponent } from './inscription/inscription.component';
import { AccueilComponent } from './accueil/accueil.component';
import { MemoNouveauComponent } from './memo-nouveau/memo-nouveau.component';

export const routes: Routes = [
    { path: '', component: ConnectionComponent },                  
    { path: 'nouveauCompte', component: InscriptionComponent }, 
    { path: 'home', component: AccueilComponent, canActivate: [connexionGuard] },
    { path: 'memo/nouveau', component: MemoNouveauComponent, canActivate: [connexionGuard] },
    { path: '**', component: AccueilComponent }
  ];