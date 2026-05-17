import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../../services/api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";

@Component({
  selector: 'cadastro-agendamento',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ImagemPadraoComponent
],

  templateUrl: './cadastro-agendamento.component.html',
  styleUrl: './cadastro-agendamento.component.css'
})


export class CadastroAgendamentoComponent implements OnInit {
  nomePet: string = '';
  servico: string = '';
  data: string = '';
  observacao: string = '';
  mensagem: string | undefined;
  nomePets: string[] = [];
  dataMinima: string = '';
  isError: boolean = false;

  constructor(private apiService: ApiService, private router: Router) { }

  onSubmit() {
    if (!this.nomePet || !this.servico || !this.data) {
      this.mensagem = 'Por favor, preencha todos os campos!';
      return;
    }

    this.apiService.cadastroAgendamento(this.nomePet, this.servico, this.data, this.observacao)
      .subscribe({
        next: response => {
          console.log('Resposta do servidor:', response);
          this.mensagem = 'Agendamento realizado com sucesso!';

          // Resetar os campos após o cadastro bem-sucedido
          this.nomePet = '';
          this.servico = '';
          this.data = '';
          this.observacao = '';
        },
        error: err => {
          this.isError = true;
          console.error('Erro ao realizar agendamento:', err);
          this.mensagem = 'Ocorreu um erro, tente novamente.';
        }



      });

  }

  ngOnInit(): void {
    this.apiService.buscaNomePetsToken().subscribe((data) => {
      this.nomePets = data;
      console.log(this.nomePets);




      const hoje = new Date();
      const ano = hoje.getFullYear();
      const mes = String(hoje.getMonth() + 1).padStart(2, '0'); // Ajusta mês com dois dígitos
      const dia = String(hoje.getDate()).padStart(2, '0'); // Ajusta dia com dois dígitos

      this.dataMinima = `${ano}-${mes}-${dia}`; // Define a data mínima para o input



      
    },
      error => {
        console.log('Erro ao buscar pets', error);
      }
    );
  }
}
