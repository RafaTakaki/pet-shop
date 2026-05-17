import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { LoginComponent } from "./components/login-page/login/login.component";
import { RecuperarSenhaComponent } from "./components/login-page/recuperar-senha/recuperar-senha.component";
import { CriarContaComponent } from "./components/login-page/criar-conta/criar-conta.component";
import { ImagemPadraoComponent } from "./components/imagem-padrao/imagem-padrao.component";


@Component({
  selector: 'app-root',
  standalone: true, // Definindo o componente como standalone
  imports: [CommonModule, RouterOutlet, LoginComponent, RecuperarSenhaComponent, CriarContaComponent, ImagemPadraoComponent], // Importando o módulo necessário diretamente no componente
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'desafio-teste';

  usuario = {
    email: '',
    senha: ''
  };

  constructor(private authService: AuthService) { }

  public login() {
    this.authService.login(this.usuario.email, this.usuario.senha);

  }
}
