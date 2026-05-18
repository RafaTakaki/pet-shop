import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ApiService } from '../../../services/api.service'; // Certifique-se de que ApiService está corretamente importado
import { Router } from '@angular/router';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";

@Component({
  selector: 'app-cadastro-pet',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ImagemPadraoComponent
],
  templateUrl: './cadastro-pet.component.html',
  styleUrls: ['./cadastro-pet.component.css']
})
export class CadastroPetComponent {
  nomePet: string = '';
  tipoPet: string = '';
  idadePet: number | null = null;
  raca: string = '';
  sexo: string = '';
  mensagem: string = '';
  racas: string[] = []; // Lista de raças para ser preenchida dinamicamente
  isLoadingRacas: boolean = false;

  constructor(private apiService: ApiService, private router: Router) { }
  

  onSubmit() {
    if (!this.nomePet || !this.tipoPet || this.idadePet === null || !this.raca || !this.sexo) {
      this.mensagem = 'Por favor, preencha todos os campos!';
      return;
    }

    this.apiService.cadastroPet(this.nomePet, this.tipoPet, this.idadePet, this.raca, this.sexo)
      .subscribe({
        next: response => {
          console.log('Resposta do servidor:', response);
          this.mensagem = 'Pet cadastrado com sucesso!';
          
          // Resetar os campos após o cadastro bem-sucedido
          this.nomePet = '';
          this.tipoPet = '';
          this.idadePet = null;
          this.raca = '';
          this.sexo = '';
        },
        error: error => {
          console.error('Erro ao enviar dados:', error);
          this.mensagem = error.error?.message || 'Erro ao cadastrar pet. Tente novamente.';
        }
      });
  }

onTipoPetChange() {
  console.log('onTipoPetChange chamado com tipoPet:', this.tipoPet);
  this.raca = ''; // Limpa a seleção anterior
  this.racas = []; // Limpa a lista na tela antes de carregar a nova

  if (this.tipoPet) {
    this.isLoadingRacas = true;
    
    this.apiService.buscaRacas(this.tipoPet).subscribe({
      next: (dadosMapeados: string[]) => {
        // Como o serviço já tratou o objeto, 'dadosMapeados' já é o array direto de strings
        this.racas = dadosMapeados;
        this.isLoadingRacas = false;
        console.log('Raças prontas para o HTML:', this.racas);
      },
      error: (error) => {
        console.error('Erro ao carregar raças no componente:', error);
        this.racas = [];
        this.isLoadingRacas = false;
      }
    });
  } else {
    console.log('Nenhum tipo de pet selecionado');
    this.racas = [];
  }
}
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
