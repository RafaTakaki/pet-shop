export interface CadastroPet {
    id: string;
    idUsuario: string;
    nomePet: string;
    tipoPet: string;
    idadePet: number;
    sexo?: string; 
    raca: string;
    imagem?: string;
  }