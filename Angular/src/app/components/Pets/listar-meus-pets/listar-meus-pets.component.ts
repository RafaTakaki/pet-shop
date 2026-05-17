import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CadastroPet } from '../../../models/cadastro-pet';
import { ApiService } from '../../../services/api.service';
import { FilterByTipoPipe } from "../../../pipes/filter.pipe";
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'meus-pets',
  standalone: true,
  imports: [CommonModule, FormsModule, FilterByTipoPipe, ImagemPadraoComponent, RouterModule],
  templateUrl: './listar-meus-pets.component.html',
  styleUrl: './listar-meus-pets.component.css'
})
export class ListarMeusPetsComponent implements OnInit {
  pets: CadastroPet[] = [];
  agendamentos: any[] = []; // specify the type if known
  petSelecionado: string | null = null;
  tiposDePets: string[] = ['dog', 'cat'];
  petFiltrado: string = '';


  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.buscapets().subscribe((data) => {
      this.pets = data;
      console.log(this.pets);
    },
      error => {
        console.log('Erro ao buscar pets', error);
      }
    );
  }


  excluirPet(id: string): void {
    const confirmacao = window.confirm('Tem certeza que deseja excluir este pet?');

    if (!confirmacao) {
      return;
    }

    this.api.excluirPet(id).subscribe({
      next: () => {
        this.pets = this.pets.filter(pet => pet.id !== id);
        console.log('Pet excluído com sucesso');
      },
      error: error => {
        console.error('Erro ao excluir pet', error);
        alert('Não foi possível excluir o pet.');
      }
    });
  }

  excluirAgendamento(idAgendamento: string): void {
    const confirmacao = window.confirm('Tem certeza que deseja excluir este agendamento?');
    if (!confirmacao) {
      return;
    }

    this.api.excluirAgendamento(idAgendamento).subscribe({
      next: () => {
        alert('Exclusão de agendamento não está disponível no backend atual.');
      },
      error: error => {
        console.error('Erro ao excluir agendamento', error);
        alert('Este recurso não está disponível no backend atual.');
      }
    });
  }







  listarAgendamentosPorPet(nomePet: string) {
    this.api.buscaAgendamentosPorPet(nomePet).subscribe((data) => {
      this.agendamentos = data;
      console.log(this.agendamentos);
    },
      error => {
        console.log('Erro ao buscar agendamentos', error);
      }
    );
  }






  async mostrarAgendamentos(nomePet: string) {
    if (this.petSelecionado === nomePet) {
      this.petSelecionado = null;
      this.agendamentos = [];
    } else {
      this.agendamentos = [];
      await this.listarAgendamentosPorPet(nomePet)
      this.petSelecionado = nomePet;
    }
  }






}





