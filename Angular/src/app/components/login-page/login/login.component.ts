import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';  // Para diretivas como *ngIf, *ngFor
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { ImagemPadraoComponent } from "../../imagem-padrao/imagem-padrao.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ImagemPadraoComponent],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  mensagem: string = '';

  
  

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit() {
    if (!this.email || !this.password) {
      this.mensagem = 'Por favor, preencha todos os campos!';
      return;
    }

    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        if (response && response.token) {
          // Armazenando o token diretamente no componente
          localStorage.setItem('token', response.token);
          // console.info('Token:', response.token);
          this.router.navigate(['/front-page']);
          this.mensagem = 'Login realizado com sucesso!';
          // Aqui você pode redirecionar para outra página, se necessário
        } else {
          this.mensagem = 'Token não recebido!';
        }
      },
      error: (error) => {
        console.error('Erro ao fazer login:', error);
        this.mensagem = error.message || 'Erro ao realizar login. Tente novamente.';
      }
    });
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
