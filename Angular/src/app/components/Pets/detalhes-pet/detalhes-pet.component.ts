import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";

@Component({
  selector: 'app-detalhes-pet',
  standalone: true,
  imports: [CommonModule, FormsModule, ImagemPadraoComponent],
  templateUrl: './detalhes-pet.component.html',
  styleUrls: ['./detalhes-pet.component.css']
})
export class DetalhesPetComponent {
  private route = inject(ActivatedRoute);
  private http = inject(HttpClient);
  private router = inject(Router);

  pet: any = {};

  constructor() {
    const nome = this.route.snapshot.paramMap.get('nome');
    if (nome) {
      this.http.get(`${window.location.origin}/api/Cuidado/BuscaPetPorNome`, { params: new HttpParams().set('nome', nome) })
        .subscribe({
          next: (data: any) => {
            console.log('Dados recebidos:', data);
            this.pet = data;
          },
          error: (error) => {
            console.error('Erro ao buscar os dados:', error);
          }
        });
    }
  }

  salvar() {
    alert('A atualização de pets não está disponível no backend atual.');
  }
}
