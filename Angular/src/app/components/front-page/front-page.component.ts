import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ImagemPadraoComponent } from "../imagem-padrao/imagem-padrao.component";
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'front-page',
  standalone: true,
  imports: [CommonModule, FormsModule, ImagemPadraoComponent],
  templateUrl: './front-page.component.html',
  styleUrl: './front-page.component.css'
})
export class FrontPageComponent implements OnInit {
  numeroDeNotificacoes: number = 0;

  constructor(private router: Router, private api: ApiService) { }

  ngOnInit() {
    this.api.numeroNotificacoes().subscribe(count=> {
      this.numeroDeNotificacoes = count
    })
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
