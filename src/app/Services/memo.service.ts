import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Memo, MemoCreationRequete} from '../Models/memo';
import { Observable } from 'rxjs';
import { CompteService } from './compte.service';

@Injectable({
    providedIn: 'root'
})

export class MemoService {
    private urlBase = 'https://memos-APP.azurewebsites.net/api/Memo';


    constructor(private http: HttpClient, private compteService: CompteService) { }


    obtenirMemos(): Observable<Memo[]> {
        const nomUtilisateur = this.compteService.obtenirNomUtilisateur();
        return this.http.get<Memo[]>(`${this.urlBase}/ObtenirMemosParUtilisateur?nomUtilisateur=${nomUtilisateur}`);
    }

    creerMemo(memo: MemoCreationRequete): Observable<any> {
        const nomUtilisateur = this.compteService.obtenirNomUtilisateur();
        const memoAvecUtilisateur = {
            ...memo,
            nomUtilisateur: nomUtilisateur
        };

        return this.http.post(this.urlBase + '/CreerMemo', memoAvecUtilisateur, { responseType: 'text' });
    }

    supprimerMemo(idMemo: number): Observable<any> {
        return this.http.delete(`${this.urlBase}/SupprimerMemo/${idMemo}`, { responseType: 'text' });
    }

}


