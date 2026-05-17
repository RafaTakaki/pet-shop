import { Injectable, inject } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { AuthService } from './services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private authService = inject(AuthService);

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = this.authService.getToken(); // Obtém o token armazenado

    if (token) {
      // Clona a requisição e adiciona o cabeçalho Authorization com o token JWT
      const clonedReq = req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
      return next.handle(clonedReq);
    }

    return next.handle(req); // Continua a requisição sem modificação caso não haja token
  }
}
