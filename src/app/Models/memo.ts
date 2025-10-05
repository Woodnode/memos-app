export interface Memo {
    id: number;
    titre: string;
    description: string;
    dateCreation: Date;
    nomUtilisateur: string;
}

export interface MemoCreationRequete {
    titre: string;
    description: string;
    nomUtilisateur: string;
}
