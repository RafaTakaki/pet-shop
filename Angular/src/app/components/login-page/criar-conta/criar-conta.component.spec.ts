import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';  // Importe o FormsModule aqui
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-criar-conta',
  standalone: true,  // Defina o componente como standalone
  imports: [FormsModule],  // Importe o FormsModule aqui
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.css']
})
export class CriarContaComponent {
  email: string = '';
  senha: string = '';
  nome: string = '';
  mensagem: string = '';  // Ajuste o tipo para string para garantir a consistência

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
        // Aqui a resposta será uma string simples ou um objeto, depende do que o servidor retorna
        console.log('Resposta do servidor:', response);
        // Supondo que a resposta seja uma string, mas caso seja um objeto, podemos converter de acordo com a API
        this.mensagem = typeof response === 'string' ? response : 'Conta criada com sucesso!'; // Exibe uma mensagem padrão se não for uma string
      },
      error => {
        console.error('Erro ao enviar dados', error);
        this.mensagem = 'Erro ao criar conta. Tente novamente.';
      }
    );
  }
}
