import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    // Verificar se o token está presente no localStorage
    const token = localStorage.getItem('token');
    if (token) {
      // Validar o token, caso necessário, ou simplesmente permitir o acesso
      return true;
    } else {
      // Se não houver token, redirecionar para a página de login
      this.router.navigate(['/login']);
      return false;
    }
  }
}
