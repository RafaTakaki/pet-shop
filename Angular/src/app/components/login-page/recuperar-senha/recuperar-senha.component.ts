import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ApiService } from '../../../services/api.service';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";

@Component({
  selector: 'app-recuperar-senha',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ImagemPadraoComponent
],  
  templateUrl: './recuperar-senha.component.html',
  styleUrls: ['./recuperar-senha.component.css']
})
export class RecuperarSenhaComponent {
  email: string = '';  // Variável para armazenar o e-mail
  mensagem: string = '';  // Variável para armazenar a mensagem de feedback

  constructor(private apiService: ApiService) {}

  recuperarSenha() {
    if (this.email) {
      this.apiService.sendEmail(this.email).subscribe(
        response => {
          // Aqui a resposta será uma string simples
          console.log('Resposta do servidor:', response);
          this.mensagem = response;  // Exibe a mensagem recebida diretamente
        },
        error => {
          console.error('Erro ao enviar email', error);
          this.mensagem = 'Erro ao enviar e-mail. Tente novamente.';
        }
      );
    } else {
      this.mensagem = 'Por favor, insira um e-mail válido.';
    }
  }
  
}
