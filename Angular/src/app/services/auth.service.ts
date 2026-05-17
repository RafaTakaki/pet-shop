import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { API_BASE_URL } from '../api.config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${API_BASE_URL}/Usuario/Login`;

  private http = inject(HttpClient);

  login(email: string, password: string): Observable<any> {
    const body = {
      Email: email,
      Senha: password
    };

    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.http.post<any>(this.apiUrl, body, { headers }).pipe(
      catchError(error => {
        console.error('Erro no login:', error);
        const errorMessage = error.error ? error.error : 'Erro desconhecido. Tente novamente.';
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
