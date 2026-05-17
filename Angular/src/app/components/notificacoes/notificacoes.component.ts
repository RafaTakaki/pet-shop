import { Component, OnInit } from '@angular/core';
import { Notificacao } from '../../models/notificacao';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { ImagemPadraoComponent } from "../imagem-padrao/imagem-padrao.component";


@Component({
  selector: 'notificacoes',
  standalone: true,
  imports: [CommonModule, ImagemPadraoComponent],
  templateUrl: './notificacoes.component.html',
  styleUrl: './notificacoes.component.css'
})
export class NotificacoesComponent implements OnInit{
  notificacoes: Notificacao[] = [];
  
  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.buscarNotificacoes().subscribe(data => {
      this.notificacoes = data;
    });
  }

}
