export interface Compte {
    id: number;
    nomUtilisateur: string;
    motDePasse: string;
    dateDerniereConnexion: Date;
}

export interface CompteConnexionRequete {
    nomUtilisateur: string;
    motDePasse: string;
}

export interface CompteCreationRequete {
    nomUtilisateur: string;
    motDePasse: string;
}

export interface CompteConnexionReponse {
    jeton: string;
    nomUtilisateur: string;
}