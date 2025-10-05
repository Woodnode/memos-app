import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CompteConnexionRequete, CompteConnexionReponse, CompteCreationRequete } from '../Models/compte';
import { Observable, map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CompteService {
    private urlBase = 'http://localhost:5266/api/Compte';
    private jeton: string = '';

    constructor(private http: HttpClient) {}

    seConnecter(infoConnexion: CompteConnexionRequete): Observable<CompteConnexionReponse> {
        return this.http.post<CompteConnexionReponse>(this.urlBase + '/Connexion', infoConnexion).pipe(
            map(reponse => {
                this.jeton = reponse.jeton;
                return reponse;
            })
        );
    }

    sInscrire(compte: CompteCreationRequete): Observable<any> {
        return this.http.post(this.urlBase + '/AjouterCompte', compte, { responseType: 'text' });
    }

    seDeconnecter(): void {
        this.jeton = '';
    }

    estConnecte(): boolean {
        return this.jeton !== '';
    }

    obtenirNomUtilisateur(): string | null {
        return this.jeton || null;
    }
}