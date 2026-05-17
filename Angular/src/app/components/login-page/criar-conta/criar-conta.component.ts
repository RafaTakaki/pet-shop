import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';  // Importe o FormsModule aqui
import { ApiService } from '../../../services/api.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";


@Component({
  selector: 'app-criar-conta',
  standalone: true,  // Defina o componente como standalone
  imports: [
      CommonModule, 
      FormsModule, 
      MatFormFieldModule, 
      MatInputModule, 
      MatButtonModule,
      ImagemPadraoComponent, 
    ],   // Importe o FormsModule aqui
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.css']
})
export class CriarContaComponent {
  email: string = '';
  senha: string = '';
  nome: string = '';
  mensagem: any = '';  // Ajuste o tipo para string para garantir a consistência

  constructor(private apiService: ApiService) { }

  onSubmit() {
    // Verifica se os campos necessários foram preenchidos
    if (!this.email || !this.senha || !this.nome) {
      this.mensagem = 'Por favor, preencha todos os campos!';
      return;
    }

    // Chama o serviço para cadastro
    this.apiService.cadastro(this.email, this.senha, this.nome).subscribe(
      response => {
        console.log('Resposta do servidor:', response);
        this.mensagem = response; 
      },
      error => {
        console.error('Erro ao enviar dados', error);
        this.mensagem = 'Erro ao criar conta. Tente novamente.';
      }
    );
  }
}
